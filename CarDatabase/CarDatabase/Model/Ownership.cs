using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDatabase.Model
{
    public class Ownership
    {
        public int OwnershipID { get; set; }

        [Required]
        [MaxLength(100)]
        public String VehicleIdentificationNumber { get; set; }

        //Foreign Keys

        [Required]
        public int PersonID { get; set; }

        public Person Person { get; set; }

        [Required]
        public int CarModelID { get; set; }

        public CarModel CarModel { get; set; }
    }
}
