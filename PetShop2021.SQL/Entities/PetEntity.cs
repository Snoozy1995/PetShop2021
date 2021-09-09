using System;
using PetShop2021.Core.Models;

namespace PetShop2021.SQL.Entities {
    public class PetEntity {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? TypeId { get; set;  }
        public DateTime Birthdate { get; set; }
        public DateTime SoldDate { get; set; }
        public string Color { get; set; }
        public Double Price { get; set; }
    }
}