using PetShop.Core.DomainService;
using PetShop.Core.Entity;
using System.Collections.Generic;
using System.Linq;

namespace PetShop.Infrastructure.Data
{
    public class PetRepository : IPetRepository
    {
    
        public IEnumerable<Pet> ReadPets()
        {
            return FakeDB.Pets;
        }

        public Pet CreatePet(Pet pet)
        {
            pet.ID = FakeDB.PetID++;
            List<Pet> petList = FakeDB.Pets.ToList();
            petList.Add(pet);
            FakeDB.Pets = petList;
            return pet;
        }

        public Pet DeletePet(Pet pet)
        {
            List<Pet> petList = FakeDB.Pets.ToList();

            Pet petToDelete = petList.FirstOrDefault(p => p.ID == pet.ID);

            petList.Remove(petToDelete);
            FakeDB.Pets = petList;
            return petToDelete;
        }

        public Pet GetPetById(int id)
        {
            return FakeDB.Pets.FirstOrDefault(p => p.ID == id);
        }

        public Pet UpdatePet(Pet pet)
        {
            List<Pet> petList = FakeDB.Pets.ToList();

            Pet petToDUpdate = petList.FirstOrDefault(p => p.ID == pet.ID);

            if (petToDUpdate == null)
            {
                return null;
            }

            int i = petList.IndexOf(petToDUpdate);

            petList[i] = pet;
            petToDUpdate = pet;


            FakeDB.Pets = petList;
            return petToDUpdate;
        }
    }
}
