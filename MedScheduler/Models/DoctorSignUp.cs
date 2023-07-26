using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.UI;

namespace MedScheduler.Models
{
    public class DoctorSignUp
    {
        [Key]
        //to depict primary key
        public int Id { get; set; }

        //firstname
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are possible")]
        [DisplayName("First name")]
        public string Firstname { get; set; }

        //lastname
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are possible")]
        [DisplayName("Last name")]

        public string Lastname { get; set; }

        //dateofbirth
        [Range(25, int.MaxValue, ErrorMessage = "Age must be a non-negative number.")]
        public int Age { get; set; }


        //gender
        [Required(ErrorMessage = "Field is required")]
        public string Gender { get; set; }

        //phonenumber
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid phone number")]
        [DisplayName("Phone number")]
        public string Phonenumber { get; set; }

        //email
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        //address
        [Required(ErrorMessage = "Field is required")]
        [DisplayName("Current hospital")]
        public string Workspace { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [DisplayName("Specialization")]

        public string Specialisation { get; set; }

        //Experience
        [Required(ErrorMessage = "Field is required")]
        public int Experience { get; set; }
        //username
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid username")]
        [System.ComponentModel.DataAnnotations.Compare("Email",ErrorMessage="Username and email should be the same")]
        public string Username { get; set; }

        //password
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must be a minimum of eight characters, including at least one uppercase letter, one lowercase letter, one symbol(@$!%*?&), and one number.")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]

        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password doesn't match!!..")]
        public string ConfirmPassword { get; set; }

        //profilephoto
   [Required(ErrorMessage = "Field is required")]
        [DisplayName("Profile photo")]
        public string Profilephoto { get; set; }
    

        [NotMapped]
         public HttpPostedFileBase ImageFile { get; set; }

         
        //When a file is uploaded through a form in an ASP.NET application, the uploaded file is sent as part of the HTTP request, and the server-side code can access and manipulate the file using the HttpPostedFileBase class.
        //Days

       [Required(ErrorMessage = "Days is required.")]
        public string Monday { get; set; }

       [Required(ErrorMessage = "Days is required.")]
        public string Tuesday { get; set; }
        [Required(ErrorMessage = "Days is required.")]
        public string Wednesday { get; set; }
        [Required(ErrorMessage = "Days is required.")]
        public string Thursday { get; set; }
       [Required(ErrorMessage = "Days is required.")]
        public string Friday { get; set; }
        [Required(ErrorMessage = "Days is required.")]
        public string Saturday { get; set; }
       [Required(ErrorMessage = "Days is required.")]
        public string Sunday { get; set; }

        internal static string DeleteDoctorDetails(int id)
        {
            throw new NotImplementedException();
        }
    }

}