using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class SpecializationDept
    {
        [Key]
        //to depict primary key
        public int Id { get; set; }

        //firstname
        [Required(ErrorMessage = "Field is required")]
        public string Specialization { get; set; }
    }
}