using System;
using System.Collections.Generic;
using PetShop2021.Core.IServices;
using PetShop2021.Core.Models;
using PetShop2021.Domain.IRepositories;

namespace PetShop2021.Domain.Services {
    public class PetTypeService : IPetTypeService {
        private IPetTypeRepository _repo;
        public PetTypeService(IPetTypeRepository repo) {
            _repo = repo;
        }
        
        public PetType Create(PetType pet) {
            return _repo.Add(pet);
        }

        public PetType Delete(long id) {
            return _repo.Delete(id);
        }

        public PetType Update(int id,PetType pet) {
            return _repo.Update(id, pet);
        }

        public List<PetType> GetAll() {
            return _repo.FindAll();
        }

        public PetType FindById(int id) {
            return _repo.FindById(id);
        }

        public PetType FindByName(String name) {
            return _repo.FindByName(name);
        }
    }
}