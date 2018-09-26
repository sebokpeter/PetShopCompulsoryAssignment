using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PetShop.Core.ApplicationService.Implementation
{
    public class OwnerService : IOwnerService
    {
        private readonly IPetRepository _repository;
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IPetRepository repo, IOwnerRepository ownerRepository)
        {
            _repository = repo;
            _ownerRepository = ownerRepository;
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

        public Owner RemoveOwner(int id)
        {
            return _ownerRepository.DeleteOwner(id);
        }

        public List<Owner> GetFilteredOwners(OwnerFilter filter)
        {
            if (filter.CurrentPage < 0 || filter.ItemsPerPage < 0)
            {
                throw new InvalidDataException("CurrentPage and ItemsPerPage must be higher or equal to 0");
            }

            if (((filter.CurrentPage - 1) * filter.ItemsPerPage) >= _ownerRepository.Count())
            {
                throw new InvalidDataException("Index out of bouns, CurrentPage is too high");
            }

            return _ownerRepository.ReadOwners(filter).ToList();
        }
    }
}
