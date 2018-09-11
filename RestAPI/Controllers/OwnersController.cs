using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;

namespace RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IPetService _service;

        public OwnersController(IPetService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Owner>> Get()
        {
            return _service.GetOwners();
        }


        [HttpGet("{id}")]
        public ActionResult<Owner> Get(int id)
        {
            if (id < 0)
            {
                return BadRequest("ID cannot be negative!");
            }

            Owner owner = _service.GetOwnerByID(id);

            if (owner != null)
            {
                return owner;
            }
            else
            {
                return NotFound($"Owner with the ID of {id} was not found!");
            }
        }

        [HttpPost]
        public ActionResult<Owner> Post([FromBody] Owner owner)
        {
            owner.ID = 0;
            if (string.IsNullOrEmpty(owner.FirstName))
            {
                return BadRequest("Owner must have a first name!");
            }

            if (string.IsNullOrEmpty(owner.LastName))
            {
                return BadRequest("Owner must have a last name!");
            }
            
            if (string.IsNullOrEmpty(owner.PhoneNumber))
            {
                return BadRequest("Owner must have a phone number!");
            }
            
            if (string.IsNullOrEmpty(owner.Email))
            {
                return BadRequest("Owner must have aa email address!");
            }
            try
            {
                if (_service.AddOwner(owner) != null)
                {
                    string s = string.Format(string.Format("Owner created! ID: {0}", owner.ID));

                    return StatusCode(StatusCodes.Status201Created, s);
                }
                else
                {
                    return BadRequest("Could not add owner!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("{id}")]
        public ActionResult<Owner> Put([FromBody] Owner owner)
        {
            if (owner.ID < 0)
            {
                return BadRequest("ID cannot be negative!");
            }

            if (_service.UpdateOwner(owner) != null)
            {
                return Ok("Owner updated!");
            }
            else
            {
                return BadRequest("Owner could not be updated!");
            }
        }

        [HttpDelete]
        public ActionResult<Owner> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("ID cannot be negative!");
            }

            if (_service.RemoveOwner(_service.GetOwnerByID(id)) != null)
            {
                return Ok("Owner deleted!");
            }
            else
            {
                return BadRequest("Could not delete owner!");
            }
        }
    }
}