using System;
using System.Collections.Generic;
using PetShop2021.Core.Models;

namespace PetShop2021.Domain.IRepositories {
    public interface IPetTypeRepository {
        
        PetType Add(PetType petType);
        PetType Delete(long id);
        PetType Update(long id,PetType petType);
        List<PetType> FindAll();
        PetType FindById(long id);
        PetType FindByName(String name);
    }
}