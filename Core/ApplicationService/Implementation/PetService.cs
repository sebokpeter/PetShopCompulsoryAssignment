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

            //foreach (Pet p in pets)
            //{
            //    p.PreviousOwner = _ownerRepository.GetOwnerByID(p.PreviousOwner.ID);
            //}

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

        public Pet RemovePet(Pet pet)
        {
            return _repository.DeletePet(pet);
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

        public List<Owner> GetOwners()
        {
            return _ownerRepository.ReadOwners().ToList();
        }

        public Owner GetOwnerByID(int id)
        {
            return _ownerRepository.GetOwnerByID(id);
        }

        public Owner AddOwner(Owner owner)
        {
            //Check phone number validity
            if (!Regex.Match(owner.PhoneNumber, @"^(\+[0-9]{8})$").Success)
            {
                throw new ArgumentException("The phone number is not valid! It must be in the following format: +dddddddd (for example: +52123789");
            }

            //Verify email address
            //https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
            if (!Regex.Match(owner.Email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase).Success)
            {
                throw new ArgumentException("The email address is not valid!");
            }

            return _ownerRepository.CreateOwner(owner);
        }

        public Owner UpdateOwner(Owner owner)
        {
            return _ownerRepository.UpdateOwner(owner);
        }

        public Owner RemoveOwner(Owner ownerToRemove)
        {
            return _ownerRepository.DeleteOwner(ownerToRemove);
        }

        public List<Pet> GetOwnersPets(Owner owner)
        {
            return _ownerRepository.GetPetsByOwner(owner);
        }
    }
}
