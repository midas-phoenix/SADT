using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SA_TEST.Models
{
    public class TblPlateNumber
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key()]
        public int PlateNumberID { get; set; }

        [Required]
        public string platenumber { get; set; }

        public string LGACode { get; set; }

        public int platenumberint { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public string Owner { get; set; }
    }
}
