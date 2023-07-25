using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Field is required")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage = "Only alphabets are possible")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid phone number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public string Message { get; set; }
    }

}