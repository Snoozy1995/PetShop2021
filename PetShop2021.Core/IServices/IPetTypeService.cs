using System;
using System.Collections.Generic;
using PetShop2021.Core.Models;

namespace PetShop2021.Core.IServices {
    public interface IPetTypeService {
        PetType Create(PetType petType);
        PetType Delete(long id);
        PetType Update(int id,PetType petType);
        List<PetType> GetAll();
        PetType FindById(int id);
        PetType FindByName(String name);
    }
}