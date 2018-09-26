using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.ApplicationService
{
    public interface IOwnerService
    {
        List<Owner> GetOwners();

        List<Owner> GetFilteredOwners(OwnerFilter filter);

        Owner GetOwnerByID(int id);

        Owner AddOwner(Owner owner);

        Owner UpdateOwner(Owner owner);

        Owner RemoveOwner(int id);
    }
}
