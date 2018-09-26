using System;
using System.Collections.Generic;
using System.Text;
using PetShop.Core.Entity;

namespace PetShop.Core.ApplicationService
{

    // TODO: split up pet service and owner service
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

        List<Pet> GetOwnersPets(Owner owner);
    } 
}
