using System;
using System.Linq;
using PetShop2021.Core.IServices;
using PetShop2021.Core.Models;

namespace PetShop2021.UI {
    internal class Menu {
        
        private readonly IPetService _service;
        private readonly IPetTypeService _petTypeService;
        public Menu(IPetService service,IPetTypeService petTypeService) {
            _service = service;
            _petTypeService = petTypeService;
        }
        public void Start() {
            TestData();
            Console.WriteLine(StringConstants.WelcomeGreeting2);
            StartLoop();
        }

        private void StartLoop() {
            int choice;
            while ((choice = GetMainMenuSelection()) != 0) {
                switch (choice) {
                    case 1:
                        ViewAllPets();
                        break;
                    case 2:
                        SearchPet();
                        break;
                    case 3:
                        CreatePet();
                        break;
                    case 4:
                        ViewPetsByPrice();
                        break;
                    case 5:
                        ViewCheapestPets();
                        break;
                    default:
                        PleaseTryAgain();
                        break;
                }
            }
        }

        private void ViewAllPets() {
            Print("Showing all our pets...");
            var pets = _service.GetAll();
            Print("ID | NAME | TYPE | PRICE | COLOR");
            foreach (var pet in pets) {
                PrintPet(pet);
            }
            SelectByIdPrompt();
        }
        
        private void ViewPetsByPrice() {
            Print("Showing all our pets sorted by price...");
            var pets = _service.GetAll().OrderByDescending(o=>o.Price).ToList();
            Print("ID | NAME | TYPE | PRICE | COLOR");
            foreach (var pet in pets) {
                PrintPet(pet);
            }
            SelectByIdPrompt();
        }
        
        private void ViewCheapestPets() {
            var pets = _service.GetAll().OrderBy(o => o.Price).ToList();
            if (pets.Count < 5) {
                if (!pets.Any()) {
                    Print("No pets available... Press any key to continue.");
                    Console.ReadLine();
                    return;
                }
                pets = pets.GetRange(0, pets.Count);
            }
            else {
                pets=pets.GetRange(0,5);
            }
            Print("Showing our 5 cheapest pets...");
            Print("ID | NAME | TYPE | PRICE | COLOR");
            foreach (var pet in pets) {
                PrintPet(pet);
            }
            SelectByIdPrompt();
        }

        private void SelectByIdPrompt() {
            Print("Type '-1' to return to main menu... Type corresponding pet id to select pet...");
            var selectionString = Console.ReadLine();
            if (!int.TryParse(selectionString, out var selection)) {
                InvalidIdPrompt(selectionString);
                return;
            }

            if (selection == -1) return;
            var pet=_service.FindById(selection);
            if (pet == null) {
                InvalidIdPrompt(selectionString);
                return;
            }

            SelectedPet(selection,pet);
        }

        private void SelectedPet(int id,Pet pet) {
            Print("Selected pet:");
            PrintPet(pet);
            Print(StringConstants.PetCRUD);
            int choice;
            while ((choice = GetPetCrudSelection()) != 0) {
                switch (choice) {
                    case 1: //Change name
                        ReadAndValidateInput("Input new name:", out var name);
                        pet.Name = name;
                        break;
                    case 2: //Change price
                        ReadAndValidateDoubleInput("Input new price:", out double price);
                        pet.Price=price;
                        break;
                    case 3: //Change color
                        ReadAndValidateInput("Input new color:", out var color);
                        pet.Color = color;
                        break;
                    case 4: //Change pet type
                        ReadAndValidateInput("Input new pet type:", out var petTypeName);
                        var petType=_petTypeService.FindByName(petTypeName) ?? _petTypeService.Create(new PetType{Name=petTypeName});
                        pet.Type=petType;
                        break;
                    case 5: //Delete
                        _service.Delete(id);
                        return;
                }
                _service.Update(id, pet);
                PrintPet(pet);
                Print(StringConstants.PetCRUD);
            }
        }

        private static int GetPetCrudSelection() {
            var selectionString = Console.ReadLine();
            return int.TryParse(selectionString, out var selection) ? selection : 0;
        }
        
        private void InvalidIdPrompt(String id) {
            Print($"Unable to find pet with id {id}, please retry.");
            SelectByIdPrompt();
        }

        private void SearchPet() {
            Print(StringConstants.WhatToSearchFor);
            var choice = GetPetSearchMenuSelection();
            switch (choice) {
                case 1:
                    Print("Type the id to search for");
                    var idToSearchFor = Console.ReadLine();
                    if (!int.TryParse(idToSearchFor, out var selection)) return;
                    Print($"You searched for Id {selection}");
                    var idResult=_service.FindById(selection);
                    if (idResult == null)
                    {
                        Print("Couldn't find pet with corresponding id, press any key to return to main menu...");
                        Console.ReadKey();
                        return;
                    }
                    SelectedPet(selection,idResult);
                    break;
                case 2:
                    Print("Type name to search for");
                    var nameToSearchFor = Console.ReadLine();
                    Print($"You searched for Id {nameToSearchFor}");
                    var nameResults = _service.GetAll().Where(o => nameToSearchFor != null && o.Name.Contains(nameToSearchFor)).ToList();
                    Print($"You searched for pet name {nameToSearchFor}\nResults:");
                    if (nameResults.Count < 1)
                    {
                        Print("Couldn't find any pets with similar name, press any key to return to main menu...");
                        Console.ReadKey();
                        return;
                    }
                    foreach (var pet in nameResults) {
                        PrintPet(pet);
                    }

                    if (nameResults.Count > 0) {
                        SelectByIdPrompt();
                    }
                    break;
                case 3:
                    Print("Type pet type to search for");
                    var typeToSearchFor = Console.ReadLine();
                    var typeResults = _service.GetAll().Where(o => typeToSearchFor != null && o.Type.Name.Contains(typeToSearchFor)).ToList();
                    Print($"You searched for pet type {typeToSearchFor}\nResults:");
                    if (typeResults.Count < 1)
                    {
                        Print("Couldn't find any pets with similar pet type, press any key to return to main menu...");
                        Console.ReadKey();
                        return;
                    }
                    foreach (var pet in typeResults) {
                        PrintPet(pet);
                    }
                    if (typeResults.Count > 0) {
                        SelectByIdPrompt();
                    }
                    break;
                case 4:
                    var lowestPrice=ReadPrice("Type lowest price to search for");
                    while (lowestPrice < 0) {
                        lowestPrice = ReadPrice("Lowest price cannot be below 0, try again...");
                    }
                    var highestPrice=ReadPrice("Type highest price to search for");
                    while(highestPrice < lowestPrice)
                    {
                        highestPrice =
                            ReadPrice("Highest price has to be higher than lowest price value, try again...");
                    }
                    var priceResults = _service.GetAll().Where(o => o.Price>=lowestPrice&&o.Price<=highestPrice).ToList();
                    Print($"You searched for pets priced between {lowestPrice}-{highestPrice}\nResults:");
                    if (priceResults.Count < 1)
                    {
                        Print("Couldn't find any pets between selected prices, press any key to return to main menu...");
                        Console.ReadKey();
                        return;
                    }
                    foreach (var pet in priceResults) {
                        PrintPet(pet);
                    }
                    if (priceResults.Count > 0) {
                        SelectByIdPrompt();
                    }
                    break;
                case 5:
                    Print("Type pet color to search for");
                    var colorToSearchFor = Console.ReadLine();
                    var colorResults = _service.GetAll().Where(o => colorToSearchFor != null && o.Color.Contains(colorToSearchFor)).ToList();
                    Print($"You searched for pet type {colorToSearchFor}\nResults:");
                    if (colorResults.Count < 1)
                    {
                        Print("Couldn't find any pets with similar color, press any key to return to main menu...");
                        Console.ReadKey();
                        return;
                    }
                    foreach (var pet in colorResults) {
                        PrintPet(pet);
                    }
                    if (colorResults.Count > 0) {
                        SelectByIdPrompt();
                    }
                    break;
                case 0:
                case -1:
                    return;
                default:
                    Print(StringConstants.PleaseSelectCorrectSearchOptions);
                    break;
            }
        }

        private int ReadPrice(String text) {
            Print(text);
            var priceString = Console.ReadLine();
            int price;
            while (!int.TryParse(priceString, out price))
            {
                Print("Invalid price, try again...");
                priceString = Console.ReadLine();
            }

            return price;
        }

        private int GetPetSearchMenuSelection() {
            var selectionString = Console.ReadLine();
            if (int.TryParse(selectionString, out var selection)) {
                return selection;
            }
            return -1;
        }

        private void CreatePet() {
            PrintNewLine();
            ReadAndValidateInput("Type pet name:", out var petName);
            ReadAndValidateInput("Assign pet type (Ex... Dog, Cat):", out var petTypeName);
            ReadAndValidateInput("Assign pet color:", out var petColor);
            ReadAndValidateDoubleInput("Assign pet price:", out var petPrice);
            
            Print(petTypeName);
            var petType=_petTypeService.FindByName(petTypeName) ?? _petTypeService.Create(new PetType{Name=petTypeName});

            var pet = new Pet {
                Name = petName,
                Birthdate = DateTime.Now,
                Color=petColor,
                Price=petPrice,
                Type=petType
            };
            pet = _service.Create(pet);
            Clear();
            Print($"Pet with following properties created...\nId: {pet.Id}\nName: {pet.Name}\nBirthdate: {pet.Birthdate}\nColor: {pet.Color}\nPrice: {pet.Price}\nType.id: {pet.Type.Id}\nType.name: {pet.Type.Name}");
            Print("Press any key to continue...");
            Console.ReadKey();
        }

        private void ReadAndValidateInput(String prompt,out String validate) {
            Print(prompt);
            validate=Console.ReadLine();
            while (!ValidateString(validate)) {
                validate = Console.ReadLine();
            }
        }
        
        private void ReadAndValidateDoubleInput(String prompt,out double validate) {
            Print(prompt);
            var line = Console.ReadLine();
            while (!double.TryParse(line, out validate)) {
                line = Console.ReadLine();
            }
        }

        private bool ValidateString(String name) {
            if (name.Any(char.IsDigit)) return false;
            return name.Length is >= 3 and <= 32;
        }

        private static int GetMainMenuSelection() {
            Print(StringConstants.InitialMenu);
            Print(StringConstants.ExitMainMenuText);
            PrintNewLine();
            var selectionString = Console.ReadLine();
            if (int.TryParse(selectionString, out var selection)) {
                return selection;
            }
            return -1;
        }
        private static void PrintPet(Pet pet) {
            Print($"{pet.Id} | {pet.Name} | {pet.Type.Name} | {pet.Price} | {pet.Color}");
        }
        private static void PleaseTryAgain(){
            Print(StringConstants.PleaseSelectCorrectItem);
        }
        private static void PrintNewLine() {
           Console.WriteLine("");
        }
        private static void Print(string value) {
            Console.WriteLine(value);
        }
        private static void Clear() {
            Console.Clear();
        }

        private void TestData()
        {
            //Adding some start data for quick testing...
            var dog=_petTypeService.Create(new PetType() {Name = "Dog"});
            var cat=_petTypeService.Create(new PetType() {Name = "Cat"});
            var alligator=_petTypeService.Create(new PetType() {Name = "Alligator"});
            _service.Create(new Pet()
            {
                Name="Jesus",
                Birthdate = DateTime.Now,
                Color="Brown",
                Price=250,
                Type=dog
            });
            _service.Create(new Pet()
            {
                Name="Doggy",
                Birthdate = DateTime.Now,
                Color="Black",
                Price=200,
                Type=dog
            });
            _service.Create(new Pet()
            {
                Name="Catty",
                Birthdate = DateTime.Now,
                Color="Brown",
                Price=125,
                Type=cat
            });
            _service.Create(new Pet()
            {
                Name="Sharpie",
                Birthdate = DateTime.Now,
                Color="Brown",
                Price=650,
                Type=alligator
            });
        }
    }
}