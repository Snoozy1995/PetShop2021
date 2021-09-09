using System.Collections.Generic;
using PetShop2021.Core.Models;

namespace PetShop2021.Core.IServices {
    public interface IPetService {
        Pet Create(Pet pet);
        Pet Delete(long id);
        Pet Update(int id,Pet pet);
        List<Pet> GetAll();
        Pet FindById(int id);
    }
}