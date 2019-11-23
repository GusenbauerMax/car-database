using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDatabase.Model
{
    public class CarModel
    {
        public int CarModelID { get; set; }

        [Required]
        [MaxLength(100)]
        public String Model { get; set; }

        //Foreign Keys

        public List<Ownership> Ownerships { get; set; }

        [Required]
        public int CarMakeID { get; set; }

        public CarMake CarMake { get; set; }
    }
}
