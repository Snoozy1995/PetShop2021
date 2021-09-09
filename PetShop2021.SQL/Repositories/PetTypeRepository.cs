using System;
using System.Collections.Generic;
using System.Linq;
using PetShop2021.Core.Models;
using PetShop2021.Domain.IRepositories;
using PetShop2021.SQL.Converters;
using PetShop2021.SQL.Entities;

namespace PetShop2021.SQL.Repositories {
    public class PetTypeRepository : IPetTypeRepository {
        private static readonly List<PetTypeEntity> PetsTypeTable = new List<PetTypeEntity>();
        private static int _id = 1;
        private readonly PetTypeConverter _petTypeConverter;
        
        public PetTypeRepository() {
            _petTypeConverter = new PetTypeConverter();
        }
        
        public PetType Add(PetType pet) {
            var petEntity = _petTypeConverter.Convert(pet);
            petEntity.Id = _id++;
            PetsTypeTable.Add(petEntity);
            return _petTypeConverter.Convert(petEntity);
        }

        public PetType Delete(long id) {
            var result = (from pet in PetsTypeTable where pet.Id == id select pet).FirstOrDefault();
            PetsTypeTable.Remove(result);
            return _petTypeConverter.Convert(result);
        }

        public PetType Update(long id,PetType petType) {
            var petTypeToUpdate = FindById(id);
            if (petTypeToUpdate == null) return null;
            petTypeToUpdate.Name = petType.Name;
            return petTypeToUpdate;
        }
        
        public PetType FindById(long id) {
            return _petTypeConverter.Convert((from petType in PetsTypeTable where petType.Id == id select petType).FirstOrDefault());
        }

        public PetType FindByName(string name) {
            return _petTypeConverter.Convert((from petType in PetsTypeTable where string.Equals(petType.Name, name, StringComparison.CurrentCultureIgnoreCase) select petType).FirstOrDefault());
        }

        public List<PetType> FindAll() {
            return PetsTypeTable.Select(petTypeEntity => _petTypeConverter.Convert(petTypeEntity)).ToList();
        }
    }
}