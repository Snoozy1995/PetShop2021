using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PetShop2021.Core.IServices;
using PetShop2021.Core.Models;

namespace PetShop2021.WebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PetsController  : ControllerBase {
        private readonly IPetService _videoService;

        public PetsController(IPetService videoService) {
            _videoService = videoService;
        }
        
        [HttpGet]
        public List<Pet> ReadAll() {
            return _videoService.GetAll();
        }
        
        [HttpGet("{id}")]
        public Pet ReadById(int id){
            return _videoService.FindById(id);
        }

        [HttpPost]  //Body there a json object that matches 
        public Pet Create(Pet pet) {
            if (pet == null)
            {
                return null;
            }
            return _videoService.Create(pet);
        }
        
        [HttpPut("{id}")]  //Body there a json object that matches 
        public Pet Update(int id, Pet pet) {
            return _videoService.Update(id,pet);
        }

        [HttpDelete("{id}")]
        public Pet Delete(long id) {
            return _videoService.Delete(id);
        }
    }
}