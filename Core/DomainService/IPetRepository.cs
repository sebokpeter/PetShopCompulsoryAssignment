﻿using PetShop.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.DomainService
{
    public interface IPetRepository
    {
        IEnumerable<Pet> ReadPets();

        Pet CreatePet(Pet pet);

        Pet DeletePet(Pet pet);

        Pet GetPetById(int id);

        Pet UpdatePet(Pet pet);
    }
}
