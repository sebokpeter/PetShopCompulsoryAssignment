using System.Collections.Generic;
using System.Linq;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;

namespace PetShop.Infrastructure.Data
{
    public class OwnerRepository : IOwnerRepository
    {
        public IEnumerable<Owner> ReadOwners()
        {
            return FakeDB.Owners;
        }

        public Owner GetOwnerByID(int id)
        {
            return FakeDB.Owners.FirstOrDefault(o => o.ID == id);
        }

        public List<Owner> GetOwners()
        {
            return FakeDB.Owners.ToList();
        }

        public Owner CreateOwner(Owner owner)
        {
            owner.ID = FakeDB.OwnerID++;
            List<Owner> owners = FakeDB.Owners.ToList();
            owners.Add(owner);
            FakeDB.Owners = owners;
            return owner;
        }

        public Owner UpdateOwner(Owner owner)
        {
            List<Owner> ownerList = FakeDB.Owners.ToList();

            Owner ownerToUpdate = ownerList.FirstOrDefault(o => o.ID == owner.ID);

            if (ownerToUpdate == null)
            {
                return null;
            }

            int i = ownerList.IndexOf(ownerToUpdate);

            ownerList[i] = owner;
            ownerToUpdate = owner;

            FakeDB.Owners = ownerList;
            return ownerToUpdate;
        }

        public Owner DeleteOwner(int id)
        {
            Owner owner = GetOwnerByID(id);
            List<Owner> ownerList = FakeDB.Owners.ToList();

            Owner ownerToRemove = ownerList.FirstOrDefault(o => o.ID == owner.ID);

            ownerList.Remove(ownerToRemove);
            FakeDB.Owners = ownerList;
            return ownerToRemove;
        }

        public List<Pet> GetPetsByOwner(Owner owner)
        {
            List<Pet> pets = FakeDB.Pets.Where(p => p.PreviousOwner.ID == owner.ID).ToList();
            return pets;
        }
    }
}
