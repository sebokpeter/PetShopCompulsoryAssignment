using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IPetRepository
    {
        IEnumerable<Pet> ReadPets(PetFilter filter = null);

        Pet CreatePet(Pet pet);

        Pet DeletePet(int id);

        Pet GetPetById(int id);

        Pet UpdatePet(Pet pet);
        int Count();
    }
}
