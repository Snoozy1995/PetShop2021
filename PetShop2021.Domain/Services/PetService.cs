using System.Collections.Generic;
using PetShop2021.Core.IServices;
using PetShop2021.Core.Models;
using PetShop2021.Domain.IRepositories;

namespace PetShop2021.Domain.Services {
    public class PetService : IPetService {
        private IPetRepository _repo;
        public PetService(IPetRepository repo) {
            _repo = repo;
        }
        
        public Pet Create(Pet pet) {
            return _repo.Add(pet);
        }

        public Pet Delete(long id) {
            return _repo.Delete(id);
        }

        public Pet Update(int id,Pet pet) {
            return _repo.Update(id, pet);
        }

        public List<Pet> GetAll() {
            return _repo.FindAll();
        }

        public Pet FindById(int id) {
            return _repo.FindById(id);
        }
    }
}