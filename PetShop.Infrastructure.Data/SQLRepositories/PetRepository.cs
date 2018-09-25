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

        public int Count()
        {
            return _ctx.Pets.Count();
        }

        public Pet CreatePet(Pet pet)
        {
            _ctx.Attach(pet).State = EntityState.Added;
            _ctx.SaveChanges();

            return pet;
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

        public IEnumerable<Pet> ReadPets(PetFilter filter)
        {
            if (filter == null)
            {
                return _ctx.Pets.Include(p => p.PreviousOwner);
            }

            List<Pet> pets = _ctx.Pets
                .Skip((filter.CurrentPage - 1) * filter.ItemsPerPage)
                .Take(filter.ItemsPerPage).ToList();

            if (filter.OrderByDesc)
            {
                switch (filter.SearchField)
                {
                    case PetFilter.Field.Id:
                        pets = pets.OrderByDescending(p => p.ID).ToList();
                        break;
                    case PetFilter.Field.Name:
                        pets = pets.OrderByDescending(p => p.Name).ToList();
                        break;
                    case PetFilter.Field.Type:
                        pets = pets.OrderByDescending(p => p.Type).ToList();
                        break;
                    case PetFilter.Field.Color:
                        pets = pets.OrderByDescending(p => p.Color).ToList();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (filter.SearchField)
                {
                    case PetFilter.Field.Id:
                        pets = pets.OrderBy(p => p.ID).ToList();
                        break;
                    case PetFilter.Field.Name:
                        pets = pets.OrderBy(p => p.Name).ToList();
                        break;
                    case PetFilter.Field.Type:
                        pets = pets.OrderBy(p => p.Type).ToList();
                        break;
                    case PetFilter.Field.Color:
                        pets = pets.OrderBy(p => p.Color).ToList();
                        break;
                    default:
                        break;
                }
            }

            return pets;
        }

        public Pet UpdatePet(Pet pet)
        {
            _ctx.Attach(pet).State = EntityState.Modified;
            _ctx.SaveChanges();
            return pet;
        }
    }
}