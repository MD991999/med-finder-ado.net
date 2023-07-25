using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MedScheduler.Models
{
    public class SlotBookingEdit
    {


        [Key]
        [DisplayName("Slot id")]

        public int AppointmentId { get; set; }
        [DisplayName("Patient id")]

        public int PatientId { get; set; }
        //dateofbirth
        [Required(ErrorMessage = "Field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative number.")]
        public int Age { get; set; }

        //    Message
        [Required(ErrorMessage = "Field is required")]
        public string Message { get; set; }

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