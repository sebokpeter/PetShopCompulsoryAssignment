using System;
using System.Collections.Generic;
using PetShop.Core.ApplicationService;
using PetShop.Core.Entity;

namespace PetShop
{
    class Menu
    {
        private readonly IPetService _service;
        private readonly string[] commands = 
        {"1. - List all pets", "2. - Create pet", "3. - Delete pet", "4. - Update pet", "5. - Search pet by type", "6. - Sort pets by prize", "7. - List the five cheapest pets",
          "8. - List owners",  "9. - Create owner", "10. - Update owner", "11. - Delete owner", "12. - List pets of a previous owner","13. - Exit" };
        private bool run;


        public Menu(IPetService petService)
        {
            this._service = petService;
            this.run = true;
        }

        public void Run()
        {
            while (run)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                DrawCommands();
                int selection = ReadNumber();
                ProcessCommand(selection);
            }
        }

        private void ProcessCommand(int selection)
        {
            Console.Clear();
            switch (selection)
            {
                case 1:
                    ListAllPets();
                    break;
                case 2:
                    CreatePet();
                    break;
                case 3:
                    DeletePet();
                    break;
                case 4:
                    UpdatePet();
                    break;
                case 5:
                    SearchPetByType();
                    break;
                case 6:
                    ListPetsByPrice();
                    break;
                case 7:
                    Get5CheapestPets();
                    break;
                case 8:
                    ListOwners();
                    break;
                case 9:
                    CreateOwner();
                    break;
                case 10:
                    UpdateOwner();
                    break;
                case 11:
                    DeleteOwner();
                    break;
                case 12:
                    ListOwnersPets();
                    break;
                case 13:
                    run = false;
                    break;
                default:
                    Console.WriteLine("Invalid selection!");
                    break;
            }
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        #region Pet methods

        private void ListAllPets()
        {
            foreach (Pet pet in _service.GetPets())
            {
                Console.WriteLine(pet.ToString());
            }
        }

        private void CreatePet()
        {
            Pet newPet = ReadPet();


            if (_service.AddPet(newPet) != null)
            {
                Console.WriteLine("Successfully added pet!");
            }
            else
            {
                Console.WriteLine("Could not add pet.");
            }
        }

        private void DeletePet()
        {
            int id = ReadNumber("Please enter the ID of the pet you want to remove: ");

            if (_service.RemovePet(id) != null)
            {
                Console.WriteLine("Pet successfully removed.");
            }
            else
            {
                Console.WriteLine("Pet could not be removed.");
            }

        }

        private void UpdatePet()
        {
            int id = ReadNumber("Please enter the ID of the pet you want to update: ");

            Console.WriteLine("Enter the details: ");

            Pet updated = ReadPet();

            updated.ID = id;

            if (_service.UpdatePet(updated) != null)
            {
                Console.WriteLine("Pet successfully updated!");
            }
            else
            {
                Console.WriteLine("Pet could not be updated!");
            }
        }

        private void SearchPetByType()
        {
            Console.WriteLine("Select the type of the pet: ");

            PetType type = ReadPetType();

            List<Pet> petList = _service.GetPetsByType(type);

            if (petList.Count > 0)
            {
                Console.WriteLine("Here are the pets of the specified type: ");

                foreach (Pet pet in petList)
                {
                    Console.WriteLine(pet.ToString());
                }
            }
            else
            {
                Console.WriteLine("Seems like there are no pets of this type: ");
            }
        }

        private void ListPetsByPrice()
        {
            Console.WriteLine("Please select the ordering: ");
            Console.WriteLine("1. - Ascending");
            Console.WriteLine("2. - Descending");

            int selection;

            while (true)
            {
                selection = ReadNumber();
                if (selection == 1 || selection == 2)
                {
                    break;
                }
            }

            foreach (Pet pet in _service.GetPetsByPrice(selection == 1))
            {
                Console.WriteLine(pet.ToString());
            } 
        }
        
        private void Get5CheapestPets()
        {
            foreach (Pet pet in _service.GetCheapest())
            {
                Console.WriteLine(pet.ToString());
            }
        }

        #endregion

        #region Owner methods

        private void ListOwners()
        {
            foreach (Owner owner in _service.GetOwners())
            {
                Console.WriteLine(owner.ToString());
            }
        }
        
        private void CreateOwner()
        {
            Owner owner = ReadOwner();

            if (_service.AddOwner(owner) != null) 
            {
                Console.WriteLine("Owner successfully added!");
            }
            else
            {
                Console.WriteLine("Owner could not be added!");
            }
        }

        private void UpdateOwner()
        {
            int id = ReadNumber("Please enter the ID of the owner you want to update: ");

            Console.WriteLine("Please enter the details: ");

            Owner owner = ReadOwner();

            owner.ID = id;

            if (_service.UpdateOwner(owner) != null)
            {
                Console.WriteLine("Owner successfully updated!");
            }
            else
            {
                Console.WriteLine("Owner could not be updated!");
            }
        }

        private void DeleteOwner()
        {
            int id = ReadNumber("Please enter the ID of the owner you want to remove: ");

            if (_service.RemoveOwner(id) != null)
            {
                Console.WriteLine("Owner successfully removed.");
            }
            else
            {
                Console.WriteLine("Owner could not be removed.");
            }
        }

        private void ListOwnersPets()
        {
            int id = ReadNumber("Please enter the ID of the owner: ");

            Owner owner = _service.GetOwnerByID(id);

            Console.WriteLine("This owner used to own these pets: ");
            foreach (Pet pet in _service.GetOwnersPets(owner))
            {
                Console.WriteLine(pet.ToString());
            }
        }

        #endregion

        private void DrawCommands()
        {
            foreach (string command in commands)
            {
                Console.WriteLine(command);
            }
        }

        #region Helper functions

        private PetType ReadPetType()
        {
            bool correct = false;
            PetType type = PetType.Cat;


            while (!correct)
            {
                Console.WriteLine("Please select the type of the pet: ");

                int i = 1;
                foreach (var item in Enum.GetValues(typeof(PetType)))
                {
                    Console.WriteLine("{0}. - {1}", i, item);
                    i++;
                }

                int selection = ReadNumber("") - 1;

                if (Enum.IsDefined(typeof(PetType), selection))
                {
                    type = (PetType)selection;
                    correct = true;
                }
                else
                {
                    Console.WriteLine("Invalid selection!");
                }
            }

            return type;


        }

        private DateTime ReadDate(string message = "Please enter a date")
        {
            DateTime date;
            Console.WriteLine(message);

            while (!DateTime.TryParse(Console.ReadLine(), out date))
            {
                Console.WriteLine("Invalid format, please try again");
            }

            return date;
        }

        private int ReadNumber(string message = "Please enter a selection, and press enter!")
        {
            Console.WriteLine(message);

            int n;

            while (!int.TryParse(Console.ReadLine(), out n))
            {
                Console.WriteLine("Please enter a number!");
            }

            return n;
        }

        private double ReadDouble(string message = "Please enter the price")
        {
            Console.WriteLine(message);

            double d;

            while (!double.TryParse(Console.ReadLine(), out d))
            {
                Console.WriteLine("Please enter a number!");
            }

            return d;
        }

        private Pet GetPetByID(int id)
        {
            return _service.GetPetById(id);
        }

        private Owner SelectOwner()
        {
            Owner owner = null;

            Console.WriteLine("1. - Select owner from list");
            Console.WriteLine("2. - Enter new owner");

            int selection = ReadNumber();

            while (selection != 1 || selection != 2)
            {
                Console.WriteLine("Incorrect selection");
                selection = ReadNumber();
            }

            if (selection == 1)
            {
                Console.WriteLine("Please enter the ID if the previous owner: ");
                int id = ReadNumber();
                owner = _service.GetOwnerByID(id);

                while (owner == null)
                {
                    owner = _service.GetOwnerByID(ReadNumber("Please enter a valid ID."));
                }
            }
            else
            {
                owner = ReadOwner();
            }


            return owner;
        }

        private Owner ReadOwner()
        {
            Console.WriteLine("Please enter the details of the previous owner");
            Console.WriteLine("First name:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Last name:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Address: ");
            string address = Console.ReadLine();
            Console.WriteLine("Phone number (+dddddddd): ");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("EMail:");
            string email = Console.ReadLine();

            Owner owner = new Owner()
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                PhoneNumber = phoneNumber,
                Email = email
            };

            return owner;
        }

        private Pet ReadPet()
        {
            Console.WriteLine("Enter the name of the pet: ");
            string name = Console.ReadLine();
            PetType type = ReadPetType();
            Console.WriteLine("Enter the color of the pet: ");
            string color = Console.ReadLine();
            Console.WriteLine("Enter the name of previous owner of the pet: ");
            double price = ReadDouble();
            DateTime birth = ReadDate("Please enter the birth date of the pet (yyyy-mm-dd)! ");
            DateTime sold = ReadDate("Please enter the date when the pet was sold (yyyy-mm-dd)!");

            Pet newPet = new Pet
            {
                Name = name,
                Color = color,
                PreviousOwner = SelectOwner(),
                Price = price,
                BirthDate = birth,
                SoldDate = sold,
                Type = type
            };

            return newPet;
        }

        #endregion
    }
}
