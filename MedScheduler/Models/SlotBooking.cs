using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class SlotBooking
    {
        [Key]
        [DisplayName("Slot id")]
        public int AppointmentId { get;set; }
        [DisplayName("Patient id")]
        [Required(ErrorMessage = "Field is required")]

        public int PatientId { get; set; }

        // DoctorId
        [DisplayName("Doctor id")]
        [Required(ErrorMessage = "Field is required")]

        public int DoctorId { get; set; }

        //firstname
        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are possible")]
        [DisplayName("First name")]
        [Required(ErrorMessage = "Field is required")]

        public string Firstname { get; set; }


        //lastname
        //[RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only alphabets are possible")]
        [DisplayName("Last name")]
        [Required(ErrorMessage = "Field is required")]

        public string Lastname { get; set; }


        //dateofbirth
        [Required(ErrorMessage ="Field is required")]
       // [Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative number.")]
        public int Age { get; set; }


        //gender
        [Required(ErrorMessage = "Field is required")]

        public string Gender { get; set; }

        //    Email
        // [RegularExpression(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$", ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Field is required")]

        public string Email { get; set; }


        //phonenumber
        // [RegularExpression("^[0-9]{10}$", ErrorMessage = "Invalid phone number")]
        [Required(ErrorMessage = "Field is required")]

        [DisplayName("Phone number")]
        public string Phone { get; set; }

        //    Message
        [Required(ErrorMessage = "Field is required")]
        public string Message { get; set; }

        //   Doctorname
        [DisplayName("Doctor name")]
        [Required(ErrorMessage = "Field is required")]

        public string Doctorname { get; set; }

        //  Specialisation
        [DisplayName("Specialization")]
        [Required(ErrorMessage = "Field is required")]

        public string Specialisation { get; set; }

        //Date_of_appointment
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Date)]
        [DisplayName("Date of appointment")]

        public string Date_of_appointment { get; set; }

        //     Time_of_appointment
        [Required(ErrorMessage = "Field is required")]
        [DisplayName("Time of appointment")]

        public string Time_of_appointment { get; set; }

    }
}