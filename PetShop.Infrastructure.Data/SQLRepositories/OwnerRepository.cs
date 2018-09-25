using Microsoft.EntityFrameworkCore;
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

        public int Count()
        {
            return _ctx.Owners.Count();
        }

        public Owner CreateOwner(Owner owner)
        {
            _ctx.Attach(owner).State = EntityState.Added;
            _ctx.SaveChanges();
            return owner;
        }

        public Owner DeleteOwner(int id)
        {
            Owner ownerToRemove = GetOwnerByID(id);
            _ctx.Owners.Remove(ownerToRemove);
            _ctx.SaveChanges();
            return ownerToRemove;
        }

        public Owner GetOwnerByID(int id)
        {
            Owner owner = _ctx.Owners.FirstOrDefault(o => o.ID == id);
            if (owner == null)
            {
                return null;
            }
            owner.Pets = GetPetsByOwner(owner);
            return owner;
        }

        public List<Owner> GetOwners()
        {
            List<Owner> owners = _ctx.Owners.ToList();
            foreach (Owner owner in owners)
            {
                owner.Pets = GetPetsByOwner(owner);
            }
            return owners;
        }

        public List<Pet> GetPetsByOwner(Owner owner)
        {
            if (owner == null)
            {
                return null;
            }

            return _ctx.Pets.Where(p => p.PreviousOwner.ID == owner.ID).ToList();
        }

        public IEnumerable<Owner> ReadOwners(OwnerFilter filter)
        {
            if (filter == null)
            {
                return _ctx.Owners;
            }

            return _ctx.Owners.Skip((filter.CurrentPage - 1) * filter.ItemsPerPage).Take(filter.ItemsPerPage);
        }

        public Owner UpdateOwner(Owner owner)
        {
            _ctx.Attach(owner).State = EntityState.Modified;
            _ctx.Entry(owner).Collection(o => o.Pets).IsModified = true;
            _ctx.SaveChanges();
            return owner;
        }
    }
}
