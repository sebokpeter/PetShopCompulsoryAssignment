using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Infrastructure.Data.SQLRepositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly PetShopContext _ctx;

        public OwnerRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public Owner CreateOwner(Owner owner)
        {
            _ctx.Owners.Add(owner);
            _ctx.SaveChanges();
            return owner;
        }

        public Owner DeleteOwner(int id)
        {
            Owner ownerToRemove = GetOwnerByID(id);
            _ctx.Owners.Remove(ownerToRemove);
            return ownerToRemove;
        }

        public Owner GetOwnerByID(int id)
        {
            return _ctx.Owners.FirstOrDefault(o => o.ID == id);
        }

        public List<Owner> GetOwners()
        {
            return _ctx.Owners.ToList();
        }

        public List<Pet> GetPetsByOwner(Owner owner)
        {
            return _ctx.Pets.Where(p => p.PreviousOwner.ID == owner.ID).ToList();
        }

        public IEnumerable<Owner> ReadOwners()
        {
            return _ctx.Owners;
        }

        public Owner UpdateOwner(Owner owner)
        {
            _ctx.Owners.Update(owner);
            _ctx.SaveChanges();
            return owner;
        }
    }
}
