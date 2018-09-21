using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> ReadOwners();

        Owner GetOwnerByID(int id);

        List<Owner> GetOwners();

        Owner CreateOwner(Owner owner);

        Owner UpdateOwner(Owner owner);

        Owner DeleteOwner(int id);

        List<Pet> GetPetsByOwner(Owner owner);
    }
}
