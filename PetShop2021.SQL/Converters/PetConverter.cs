using PetShop2021.Core.Models;
using PetShop2021.SQL.Entities;

namespace PetShop2021.SQL.Converters {
    public class PetConverter {
        public Pet Convert(PetEntity entity) {
            if (entity == null) return null;
            return new Pet {
                Id = entity.Id,
                Name = entity.Name,
                Birthdate = entity.Birthdate,
                SoldDate = entity.SoldDate,
                Color = entity.Color,
                Price = entity.Price,
                //Type = ...
            };
        }
        
        public PetEntity Convert(Pet pet) {
            if (pet == null) return null;
            return new PetEntity {
                Id = pet.Id,
                Name = pet.Name,
                Birthdate = pet.Birthdate,
                SoldDate = pet.SoldDate,
                Color = pet.Color,
                Price = pet.Price,
                TypeId = pet.Type != null ? pet.Type.Id : 0
            };
        }
    }
}