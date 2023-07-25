using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class DoctorsPatientsModel
    {
        public List<DoctorSignUp> DoctorSignUp { get; set; }
        public List<PatientSignUp> patientSignUp { get; set; }
        public List<AdminModel> adminmodel { get; set; }

    }
}