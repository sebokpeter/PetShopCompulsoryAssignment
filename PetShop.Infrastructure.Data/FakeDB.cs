using PetShop.Core.Entity;
using System;
using System.Collections.Generic;

namespace PetShop.Infrastructure.Data
{
    public static class FakeDB
    {
        public static int PetID { get; set; }
        public static int OwnerID { get; set; }
        public static IEnumerable<Pet> Pets { get; set; }
        public static IEnumerable<Owner> Owners { get; set; }

        public static void InitData()
        {
            PetID = 1;
            OwnerID = 1;

            Owner prewOwner1 = new Owner
            {
                ID = OwnerID++,
                FirstName = "Dave",
                LastName = "McColgan",
                Address  = "93 Mendota Road",
                Email = "dave@dmail.com",
                PhoneNumber = "5246727702"
            };

            Owner prewOwner2 = new Owner
            {
                ID = OwnerID++,
                FirstName = "Munroe",
                LastName = "Wardlaw",
                Address = "6 Commercial Lane",
                Email = "mwardlaw7@hubpages.com",
                PhoneNumber = "5359527708"
            };

            Owners = new List<Owner> { prewOwner1, prewOwner2};

            Pet pet1 = new Pet
            {
                ID = PetID++,
                Name = "Diego",
                Type = PetType.Dog,
                BirthDate = DateTime.Now.AddMonths(-3),
                SoldDate = DateTime.Now,
                Color = "Brown",
                PreviousOwner = prewOwner1,
                Price = 199.9
            };

            Pet pet2 = new Pet
            {
                ID = PetID++,
                Name = "Tom",
                Type = PetType.Turtle,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Color = "Yellow",
                PreviousOwner = prewOwner1,
                Price = 299.9
            };

            Pet pet3 = new Pet
            {
                ID = PetID++,
                Name = "Cat",
                Type = PetType.Cat,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Color = "Blue",
                PreviousOwner = prewOwner1,
                Price = 399.9
            };

            Pet pet4 = new Pet
            {
                ID = PetID++,
                Name = "Snek",
                Type = PetType.Snake,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Color = "Red",
                PreviousOwner = prewOwner2,
                Price = 499.9
            };

            Pet pet5 = new Pet
            {
                ID = PetID++,
                Name = "Rat",
                Type = PetType.Rat,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Color = "Grey",
                PreviousOwner = prewOwner2,
                Price = 99.9
            };

            Pet pet6 = new Pet
            {
                ID = PetID++,
                Name = "Goat",
                Type = PetType.Goat,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Color = "Grey",
                PreviousOwner = prewOwner2,
                Price = 149.9
            };

            Pets = new List<Pet> { pet1, pet2, pet3, pet4, pet5, pet6 };

        }
    }
}
