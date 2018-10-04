using PetShop.Core.Entity;
using System;
using System.Collections.Generic;

namespace PetShop.Infrastructure.Data
{
    public class DBInitializer
    {
        public static void SeedDB(PetShopContext ctx)
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            Owner prewOwner1 = new Owner
            {
                FirstName = "Dave",
                LastName = "McColgan",
                Address = "93 Mendota Road",
                Email = "dave@dmail.com",
                PhoneNumber = "5246727702"
            };

            Owner prewOwner2 = new Owner
            {
                FirstName = "Munroe",
                LastName = "Wardlaw",
                Address = "6 Commercial Lane",
                Email = "mwardlaw7@hubpages.com",
                PhoneNumber = "5359527708"
            };

            ctx.Owners.Add(prewOwner1);
            ctx.Owners.Add(prewOwner2);

            Pet pet1 = new Pet
            {
                Name = "Diego",
                Type = PetType.Dog,
                BirthDate = DateTime.Now.AddMonths(-3),
                SoldDate = DateTime.Now,
                Colors = new List<Color> { },
                PreviousOwner = prewOwner1,
                Price = 199.9
            };

            Pet pet2 = new Pet
            {
                Name = "Tom",
                Type = PetType.Turtle,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Colors = new List<Color> { },
                PreviousOwner = prewOwner1,
                Price = 299.9
            };

            Pet pet3 = new Pet
            {
                Name = "Cat",
                Type = PetType.Cat,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Colors = new List<Color> { },
                PreviousOwner = prewOwner2,
                Price = 399.9
            };

            Pet pet4 = new Pet
            {
                Name = "Snek",
                Type = PetType.Snake,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Colors = new List<Color> { },
                PreviousOwner = prewOwner2,
                Price = 499.9
            };

            Pet pet5 = new Pet
            {
                Name = "Rat",
                Type = PetType.Rat,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Colors = new List<Color> { },
                PreviousOwner = prewOwner2,
                Price = 99.9
            };

            Pet pet6 = new Pet
            {
                Name = "Goat",
                Type = PetType.Goat,
                BirthDate = DateTime.Now.AddYears(-5),
                SoldDate = DateTime.Now,
                Colors = new List<Color> { },
                PreviousOwner = prewOwner2,
                Price = 149.9
            };

            ctx.Pets.Add(pet1);
            ctx.Pets.Add(pet2);
            ctx.Pets.Add(pet3);
            ctx.Pets.Add(pet4);
            ctx.Pets.Add(pet5);
            ctx.Pets.Add(pet6);


            ctx.SaveChanges();
        }
    }
}
