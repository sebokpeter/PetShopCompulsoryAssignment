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

        Pet RemovePet(int id);

        Pet GetPetById(int id);

        Pet UpdatePet(Pet pet);

        List<Pet> GetFilteredPets(PetFilter filter);
        
        List<Pet> GetPetsByType(PetType type);

        List<Pet> GetPetsByPrice(bool isAscending);

        List<Pet> GetCheapest();

        List<Owner> GetOwners();

        List<Owner> GetFilteredOwners(OwnerFilter filter);

        Owner GetOwnerByID(int id);

        Owner AddOwner(Owner owner);

        Owner UpdateOwner(Owner owner);

        Owner RemoveOwner(int id);

        List<Pet> GetOwnersPets(Owner owner);
    } 
}
