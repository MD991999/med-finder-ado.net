using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class Signin
    {
        //username
        [Required(ErrorMessage ="Field is required")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Username { get; set; }

        //password
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Invalid password")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        //  [Required(ErrorMessage = "Field is required")]
        [DisplayName ("Admin-pass")]
        [DataType (DataType.Password)]
        public string Identity_verification { get; set; }

        
       // public string Change_password { get; set; }
    }
}