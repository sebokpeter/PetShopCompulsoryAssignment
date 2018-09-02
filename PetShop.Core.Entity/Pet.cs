using System;
using System.Text;

namespace PetShop.Core.Entity
{
    public enum PetType { Cat, Dog, Snake, Turtle, Goat, Rat}

    public class Pet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public PetType Type { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime SoldDate { get; set; }
        public string Color { get; set; }
        public Owner PreviousOwner { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Pet ID: {0}\n", ID));
            sb.Append(string.Format("\tPet Name: {0}\n", Name));
            sb.Append(string.Format("\tPet Type: {0}\n", Type));
            sb.Append(string.Format("\tBirth date: {0}\n", BirthDate.ToLongDateString()));
            sb.Append(string.Format("\tSold date: {0}\n", SoldDate.ToLongDateString()));
            sb.Append(string.Format("\tPet color: {0}\n", Color));
            sb.Append(string.Format("\tPrevious owner ID: {0}\n", PreviousOwner.ID));
            sb.Append(string.Format("\tPrice: {0}\n", Price));

            return sb.ToString();
        }
    }
}
