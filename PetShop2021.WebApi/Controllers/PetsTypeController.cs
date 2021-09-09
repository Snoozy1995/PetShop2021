using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PetShop2021.Core.IServices;
using PetShop2021.Core.Models;

namespace PetShop2021.WebApi.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PetsTypeController : ControllerBase {
        private readonly IPetTypeService _videoService;

        public PetsTypeController(IPetTypeService videoService) {
            _videoService = videoService;
        }
    
        [HttpGet]
        public List<PetType> ReadAll() {
            return _videoService.GetAll();
        }
    
        [HttpGet("{id}")]
        public PetType ReadById(int id){
            return _videoService.FindById(id);
        }

        [HttpPost]  //Body there a json object that matches 
        public PetType Create(PetType pet) {
            if (pet == null)
            {
                return null;
            }
            return _videoService.Create(pet);
        }
    
        [HttpPut("{id}")]  //Body there a json object that matches 
        public PetType Update(int id, PetType pet) {
            return _videoService.Update(id,pet);
        }

        [HttpDelete("{id}")]
        public PetType Delete(long id) {
            return _videoService.Delete(id);
        }
    }
}