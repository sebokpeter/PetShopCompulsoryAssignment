using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService.Implementation
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;
        private readonly IOwnerRepository _ownerRepository;

        public PetService(IPetRepository repo, IOwnerRepository ownerRepository)
        {
            _repository = repo;
            _ownerRepository = ownerRepository;
        }

        public List<Pet> GetPets()
        {
            List<Pet> pets = _repository.ReadPets().ToList();

            return pets;
        }

        public IEnumerable<Pet> ReadPets()
        {
            return _repository.ReadPets();
        }

        public Pet AddPet(Pet pet)
        {
            return _repository.CreatePet(pet); 
        }

        public Pet RemovePet(int id)
        {
            return _repository.DeletePet(id);
        }

        public Pet GetPetById(int id)
        {
            Pet p = _repository.GetPetById(id);
            if (p == null)
            {
                throw new InvalidDataException("Could not find Pet based on ID.");
            }
            p.PreviousOwner = _ownerRepository.GetOwnerByID(p.PreviousOwner.ID);
            return p;
        }

        public Pet UpdatePet(Pet pet)
        {
            return _repository.UpdatePet(pet);
        }

        public List<Pet> GetPetsByType(PetType type)
        {
            return _repository.ReadPets().Where(p => p.Type == type).ToList();
        }

        public List<Pet> GetPetsByPrice(bool isAscending)
        {
            if (isAscending)
            {
                return _repository.ReadPets().OrderBy(p => p.Price).ToList();
            }
            else
            {
                return _repository.ReadPets().OrderByDescending(p => p.Price).ToList();
            }
        }

        public List<Pet> GetCheapest()
        {
            return _repository.ReadPets().OrderBy(p => p.Price).Take(5).ToList();
        }

        public List<Pet> GetOwnersPets(Owner owner)
        {
            return _ownerRepository.GetPetsByOwner(owner);
        }

        public List<Pet> GetFilteredPets(PetFilter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPerPage < 0)
            {
                throw new InvalidDataException("CurrentPage and ItemsPerPage must be higher or equal to 0");
            }

            if (((filter.CurrentPage - 1) * filter.ItemsPerPage) >= _repository.Count())
            {
                throw new InvalidDataException("Index out of bouns, CurrentPage is too high");
            }

            return _repository.ReadPets(filter).ToList();
        }
    }
}
