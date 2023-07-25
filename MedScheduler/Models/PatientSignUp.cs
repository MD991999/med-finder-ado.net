using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace MedScheduler.Models
{
    public class PatientSignUp
    {
        [Key]
        //to depict primary key
        public int Id { get; set; }

        //firstname
        [Required(ErrorMessage ="Field is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage ="Only alphabets are possible")]
        [DisplayName("First name")]
        public string Firstname { get; set; }

        //lastname
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are possible")]
        [DisplayName("Last name")]
        public string Lastname { get; set; }

        //dateofbirth
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Date)]
        [DisplayName("Date of birth")]
        public string Dateofbirth { get; set; }

        //gender
        [Required(ErrorMessage = "Field is required")]
        public string Gender { get; set; }

        //phonenumber
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[0-9]{10}$",ErrorMessage ="Invalid phone number")]
        [DisplayName("Phone number")]
        public string Phonenumber { get; set; }

        //email
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        //address
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        
        //state
        [Required(ErrorMessage = "Field is required")]
        public string State { get; set; }

        //city
        [Required(ErrorMessage = "Field is required")]
        public string City { get; set; }

        //username
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid username")]
        [Compare("Email", ErrorMessage = "Email and password should match!!..")]
        public string Username { get; set; }

        //password
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be a minimum of eight characters, including at least one uppercase letter, one lowercase letter, one symbol, and one number.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password doesn't match!!..")]
        [DisplayName("Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}