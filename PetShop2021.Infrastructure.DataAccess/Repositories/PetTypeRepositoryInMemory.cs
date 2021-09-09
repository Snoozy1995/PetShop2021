using System;
using System.Collections.Generic;
using System.Linq;
using PetShop2021.Core.Models;
using PetShop2021.Domain.IRepositories;

namespace PetShop2021.Infrastructure.DataAccess.Repositories
{
    public class PetTypeRepositoryInMemory : IPetTypeRepository
    {
        private static readonly List<PetType> PetsTypeTable = new List<PetType>();
        private static int _id = 1;
        public PetType Add(PetType petType)
        {
            petType.Id = _id++;
            PetsTypeTable.Add(petType);
            return petType;
        }

        public PetType Delete(long id)
        {
            var result = (from pet in PetsTypeTable where pet.Id == id select pet).FirstOrDefault();
            PetsTypeTable.Remove(result);
            return result;
        }

        public PetType Update(long id,PetType petType)
        {
            var petTypeToUpdate = FindById(id);
            if (petTypeToUpdate == null) return null;
            petTypeToUpdate.Name = petType.Name;
            return petTypeToUpdate;
        }

        public List<PetType> FindAll()
        {
            return PetsTypeTable;
        }

        public PetType FindById(long id)
        {
            return (from pet in PetsTypeTable where pet.Id == id select pet).FirstOrDefault();
        }

        public PetType FindByName(string name)
        {
            return (from petType in PetsTypeTable where string.Equals(petType.Name, name, StringComparison.CurrentCultureIgnoreCase) select petType).FirstOrDefault();
        }
    }
}