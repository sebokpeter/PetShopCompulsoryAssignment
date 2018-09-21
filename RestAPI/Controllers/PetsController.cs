using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private IPetService _service;

        public PetsController(IPetService service)
        {
            _service = service;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<Pet>> Get(int currentPage = 1, int pageCount = 2)
        {
            try
            {
                return _service.GetPets().Skip((currentPage - 1) * pageCount).Take(pageCount).ToList();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Pet> Get(int id)

        {

            if (id < 1)
            {
                return BadRequest("Negative IDs are not allowed!");
            }

            try
            {
                Pet p = _service.GetPetById(id);

                if (p != null)
                {
                    return p;
                }
                else
                {
                    return NotFound("Could not find pet!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Pet> Post([FromBody] Pet pet)
        {
            pet.ID = 0;
            if (string.IsNullOrEmpty(pet.Name))
            {
                return BadRequest("The pets name canot be empty!");
            }

            try
            {
                if (_service.AddPet(pet) != null)
                {
                    string s = string.Format(string.Format("Pet created! ID: {0}", pet.ID));

                    return StatusCode(StatusCodes.Status201Created, s);
                }
                else
                {
                    return BadRequest("Could not add pet!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<Pet> Put([FromBody] Pet value, int id)
        {
            try
            {
                if (id < 1)
                {
                    return BadRequest("Cannot delete non-existing pet!");
                }

                if (value.ID != id)
                {
                    return BadRequest("Parameter mismatch: ID and Pet ID are not equal!");
                }

                if (_service.UpdatePet(value) != null)
                {
                    return Ok("Pet updated");
                }
                else
                {
                    return BadRequest("Cannot update pet!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<Pet> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Cannot update non-existing pet!");
            }

            if (_service.RemovePet(id) != null)
            {
                return Ok("Pet deleted");
            }
            else
            {
                return BadRequest("Could not delete pet!");
            }
        }
    }
}
