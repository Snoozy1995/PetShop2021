using System.Collections.Generic;
using PetShop2021.Core.Models;

namespace PetShop2021.Domain.IRepositories {
    public interface IPetRepository {
        Pet Add(Pet pet);
        Pet Delete(long id);
        Pet Update(long id,Pet pet);
        List<Pet> FindAll();
        Pet FindById(long id);
    }
}