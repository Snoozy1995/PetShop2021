using PetShop2021.Core.Models;
using PetShop2021.SQL.Entities;

namespace PetShop2021.SQL.Converters {
    public class PetTypeConverter {
        public PetType Convert(PetTypeEntity entity) {
            if (entity == null) return null;
            return new PetType {
                Id = entity.Id,
                Name = entity.Name
            };
        }
        
        public PetTypeEntity Convert(PetType pet) {
            if (pet == null) return null;
            return new PetTypeEntity {
                Id = pet.Id,
                Name = pet.Name
            };
        }
    }
}