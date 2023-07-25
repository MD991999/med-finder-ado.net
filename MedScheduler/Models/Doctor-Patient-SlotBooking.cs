using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class Doctor_Patient_SlotBooking
    {
        public List<DoctorSignUp> DoctorSignUp { get; set; }
        public List<PatientSignUp> PatientSignUp { get; set;}
        public SlotBooking SlotBooking { get; set; }
    }
}