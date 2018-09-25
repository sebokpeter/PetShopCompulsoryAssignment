using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Core.Entity
{
    public class PetFilter
    {
        public enum Field
        {
            Id,
            Name,
            Type,
            Color
        }

        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public Field SearchField { get; set; }
        public bool OrderByDesc { get; set; }
    }
}
