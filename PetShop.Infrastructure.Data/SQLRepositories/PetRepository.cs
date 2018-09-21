using Microsoft.EntityFrameworkCore;
using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Infrastructure.Data.SQLRepositories
{
    public class PetRepository : IPetRepository
    {
        private readonly PetShopContext _ctx;

        public PetRepository(PetShopContext ctx)
        {
            _ctx = ctx;
        }

        public Pet CreatePet(Pet pet)
        {
            if (pet.PreviousOwner != null)
            {
                pet.PreviousOwner = _ctx.Owners.FirstOrDefault(o => o.ID == pet.PreviousOwner.ID);
            }

            Pet saved = _ctx.Pets.Add(pet).Entity;
            _ctx.SaveChanges();
            return saved;
        }

        public Pet DeletePet(int id)
        {
            IEnumerable<Owner> ownersToRemove = _ctx.Owners.Where(c => c.ID == id);
            _ctx.RemoveRange(ownersToRemove);
            Pet p = GetPetById(id);
            _ctx.Pets.Remove(p);
            _ctx.SaveChanges();
            return p;
        }

        public Pet GetPetById(int id)
        {
            return _ctx.Pets.Include(p => p.PreviousOwner ).FirstOrDefault(c => c.ID == id);
        }

        public IEnumerable<Pet> ReadPets()
        {
            return _ctx.Pets.Include(p => p.PreviousOwner);
        }

        public Pet UpdatePet(Pet pet)
        {
            if (pet.PreviousOwner != null)
            {
                pet.PreviousOwner = _ctx.Owners.FirstOrDefault(o => o.ID == pet.PreviousOwner.ID);
            }

            _ctx.Pets.Update(pet);
            _ctx.SaveChanges();
            return pet;
        }
    }
}