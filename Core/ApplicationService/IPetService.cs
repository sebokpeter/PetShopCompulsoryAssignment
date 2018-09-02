using System;
using System.Collections.Generic;
using System.Text;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService
{
    public interface IPetService
    {
        List<Pet> GetPets();

        IEnumerable<Pet> ReadPets();

        Pet AddPet(Pet pet);

        Pet RemovePet(Pet pet);

        Pet GetPetById(int id);

        Pet UpdatePet(Pet pet);

        List<Pet> GetPetsByType(PetType type);

        List<Pet> GetPetsByPrice(bool isAscending);

        List<Pet> GetCheapest();

        List<Owner> GetOwners();

        Owner GetOwnerByID(int id);

        Owner AddOwner(Owner owner);

        Owner UpdateOwner(Owner owner);

        Owner RemoveOwner(Owner ownerToRemove);

        List<Pet> GetOwnersPets(Owner owner);
    } 
}
