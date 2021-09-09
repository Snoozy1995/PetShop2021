using System.Collections.Generic;
using System.Linq;
using PetShop2021.Core.Models;
using PetShop2021.Domain.IRepositories;

namespace PetShop2021.Infrastructure.DataAccess.Repositories
{
    public class PetRepositoryInMemory : IPetRepository
    {
        private static List<Pet> _petsTable = new List<Pet>();
        private static int _id = 1;
        
        public Pet Add(Pet pet) {
            pet.Id = _id++;
            _petsTable.Add(pet);
            return pet;
        }

        public Pet Delete(long id)
        {
            var result = _petsTable.FirstOrDefault(o => o.Id == id);
            if (result != null)
            {
                _petsTable.Remove(result);
            }
            return result;
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

        public List<Pet> FindAll() {
            return _petsTable;
        }

        public Pet FindById(long id)
        {
            return (from pet in _petsTable where pet.Id == id select pet).FirstOrDefault();
        }
    }
}