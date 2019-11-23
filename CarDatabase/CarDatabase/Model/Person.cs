using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CarDatabase.Model
{
    public class Person
    {
        public int PersonID { get; set; }

        [Required]
        [MaxLength(50)]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public String LastName { get; set; }

        //Foreign Keys

        public List<Ownership> Ownerships { get; set; }
    }
}
