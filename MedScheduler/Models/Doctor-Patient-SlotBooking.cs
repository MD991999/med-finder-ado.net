using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class Doctor_Patient_SlotBooking
    {
        public List<DoctorSignUp> DoctorSignUp { get; set; }
        public List<PatientSignUp> PatientSignUp { get; set;}
        [Required(ErrorMessage ="Field is required")]
        public SlotBooking SlotBooking { get; set; }
    }
}