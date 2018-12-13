using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA_TEST.Models
{
    public class NewNumberPlates
    {
        public string LocalGovt { get; set; }

        public int Count { get; set; }

        public string Owner { get; set; }
    }

    public class PlateNumbers
    {
        public string LocalGovt { get; set; }

        //public int Count { get; set; }

        public string Owner { get; set; }

        public DateTime DateCreated { get; set; }

        public string platenumber { get; set; }
    }
}
