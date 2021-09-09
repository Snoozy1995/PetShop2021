using System.Collections.Generic;
using System.Linq;
using PetShop2021.Core.Models;
using PetShop2021.Domain.IRepositories;
using PetShop2021.SQL.Converters;
using PetShop2021.SQL.Entities;

namespace PetShop2021.SQL.Repositories {
    public class PetRepository : IPetRepository {
        
        private static readonly List<PetEntity> PetsTable = new List<PetEntity>();
        private static int _id = 1;
        private readonly PetConverter _petConverter;

        public PetRepository() {
            _petConverter = new PetConverter();
        }
        
        public Pet Add(Pet pet) {
            var petEntity = _petConverter.Convert(pet);
            petEntity.Id = _id++;
            PetsTable.Add(petEntity);
            return _petConverter.Convert(petEntity);
        }

        public Pet Delete(long id) {
            var result = (from pet in PetsTable where pet.Id == id select pet).FirstOrDefault();
            PetsTable.Remove(result);
            return _petConverter.Convert(result);
        }

        public Pet Update(long id,Pet pet) {
            var petToUpdate = FindById(id);
            if (petToUpdate == null) return null;
            petToUpdate.Name = pet.Name;
            petToUpdate.Birthdate = pet.Birthdate;
            petToUpdate.Color = pet.Color;
            petToUpdate.Price = pet.Price;
            petToUpdate.Type = pet.Type;
            petToUpdate.SoldDate = pet.SoldDate;
            return petToUpdate;
        }
        
        public Pet FindById(long id) {
            return _petConverter.Convert((from pet in PetsTable where pet.Id == id select pet).FirstOrDefault());
        }

        public List<Pet> FindAll() {
            return PetsTable.Select(petTypeEntity => _petConverter.Convert(petTypeEntity)).ToList();
        }
    }
}