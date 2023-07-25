using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class Changepassword
    {
        //username
       [Required(ErrorMessage = "Field is required")]
       [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Username { get; set; }

        //password
     [Required(ErrorMessage = "Field is required")]
     [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be a minimum of eight characters, including at least one uppercase letter, one lowercase letter, one symbol, and one number.")]
        public string Password { get; set; }


        //Verification_key
        [Required(ErrorMessage = "Field is required")]
        public string Verification_key { get; set; }

        //new password
        [Required(ErrorMessage = "Field is required")]
     [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be a minimum of eight characters, including at least one uppercase letter, one lowercase letter, one symbol, and one number.")]
        [DisplayName("New password")]
        public string New_password { get; set; }
    }
}