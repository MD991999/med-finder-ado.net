using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class AdminModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are possible")]
        [DisplayName("Name")]
        public string Adminname { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are possible")]
        [DisplayName("Job role")]

        public string Jobrole { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address")]

        public string Username { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be a minimum of eight characters, including at least one uppercase letter, one lowercase letter, one symbol, and one number.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DisplayName("Identity verification")]
        [DataType(DataType.Password)]
        public string Identity_verification { get; set; }
    }
}
