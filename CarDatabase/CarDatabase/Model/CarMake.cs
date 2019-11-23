using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDatabase.Model
{
    public class CarMake
    {
        public int CarMakeID { get; set; }

        [Required]
        [MaxLength(100)]
        public String Make { get; set; }

        //Foreign Keys

        public List<CarModel> CarModels { get; set; }
    }
}
