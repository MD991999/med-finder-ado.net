using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class PatientIndex
    {
        public DoctorSignUp DoctorSignUp { get; set; }
        public PatientSignUp PatientSignUp { get; set; }

    }
}