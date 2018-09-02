using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entity
{
    public class Owner
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("ID: {0}\n", ID));
            sb.Append(string.Format("\tFirst Name: {0}\n", FirstName));
            sb.Append(string.Format("\tLast Name: {0}\n", LastName));
            sb.Append(string.Format("\tAddress: {0}\n", Address));
            sb.Append(string.Format("\tPhone number: {0}\n", PhoneNumber));
            sb.Append(string.Format("\tEmail: {0}\n", Email));

            return sb.ToString();
        }


    }
}
