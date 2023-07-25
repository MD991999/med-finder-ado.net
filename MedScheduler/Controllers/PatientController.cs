using MedScheduler.DAL;
using MedScheduler.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace MedScheduler.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        /// <summary>
        /// function to get the complete list of doctors
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Dal dal = new Dal();
            try
            {
                var doctorsList = dal.getalldoctor();
                if (doctorsList.Count == 0)
                {
                    TempData["SuccessMessage"] = "Sorry no one registered";
                }
                Session["DoctorsList"] = doctorsList; // Add doctorsList to the view session
                return View(doctorsList);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                TempData["ErrorMessage"] = ex;
                return View();
            }
        }

        // GET: Patient/Details/5
        /*   public ActionResult Details(int id)
          {

          string Username = Session["username"]?.ToString();
                  Console.WriteLine(Username);
                           if (Username != null)
                           {
                               Dal dal = new Dal();
                               var PatientDetails = dal.viewparticularpatientdetails(Username).FirstOrDefault(); ;
                    Session["patientdetails"] = PatientDetails;
                 ViewBag.Doctordetails = dal.particulardoctor(id);// Store patientDetails in the session
                      return View(PatientDetails);
                            }     
              return View(); 
          }    
                 */

        /*      public ActionResult Details()
         {
             string Username = Session["username"]?.ToString();
             Console.WriteLine(Username);
             if (Username != null)
             {
                 Dal dal = new Dal();
                 var patientDetails = dal.viewparticularpatientdetails(Username).FirstOrDefault();
                 ViewBag.PatientDetails = patientDetails;

                 // Retrieve doctorsList from session
                 var doctorsList = Session["DoctorsList"] as List<DoctorSignUp>;
                 ViewBag.DoctorsList = doctorsList;

                 return View(patientDetails);
             }
             return View();
         }      */

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// to access the details about a particular patient  using patientid for edit purpose
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                if (id != 0)
                {
                    Dal dal = new Dal();
                    var patientList = dal.particularpatient(id).FirstOrDefault(); ;
                    return View(patientList);
                }
                return View();
            }
            catch(Exception ex)
            {
                ErrorLogger.LogError(ex);
                TempData["ErrorMessage"]= ex;
                return View();
            }

        }

        /// <summary>
        ///  is used to bring updation in a particular patient details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patientSignUp"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, PatientSignUp patientSignUp)
        {
            try
            {
                Dal dal = new Dal();
                bool isupdated = false;
                {
                    isupdated = dal.UpdatePatientDetails(id, patientSignUp);
                    if (isupdated)
                    {
                        TempData["SuccessMessage"] = "Updated succesfully";
                        Session["username"] = patientSignUp.Username;
                        return RedirectToAction("ViewProfile", "Patient");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Updation cancelled";
                        return View();
                    }
                }

            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error.Message;
                return View();
            }
        }

        /// <summary>
        ///  to get a details of a patient inorder to delete using the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id != 0)
                {
                    Dal dal = new Dal();
                    var patientList = dal.particularpatient(id).FirstOrDefault(); ;
                    return View(patientList);
                }
                return View();
            }
            catch(Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return View();
            }
        }
       
        /// <summary>
        ///  for deleting a  patient details  using patientid
        /*In this example, the action method is named DeleteConfirmation, but by using [HttpPost, ActionName("Delete")],
         you specify that the action should be treated as the "Delete" action. So, when the form is submitted,
         it will invoke the DeleteConfirmation action method, but the URL and routing will still show the action as
         "Delete".*/
        /// </summary>
        /// <param name="id"></param>
        /// <returns>this function returns  a message to the view whether deletion is successfull/not</returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                Dal dal = new Dal();
                string result = dal.DeletePatientDetails(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Signin", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                    return RedirectToAction("Index", "Patient");
                }
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return View();
            }

        }

        [HttpGet]
        public ActionResult SlotBooking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SlotBooking(int id)
        {

            return View();
        }

        /// <summary>
        /// to get all the patients enrolled in the website 
        /// </summary>
        /// <returns>this function returns a List of patients to the view</returns>
        [HttpGet]
        public ActionResult PatientList()
        {
            Dal dal = new Dal();
            var patientList = dal.getallpatients();
            try
            {
                if (patientList.Count == 0)
                {
                    TempData["SuccessMessage"] = "Sorry noone registered";
                }
                Session["patientList"] = patientList;
                return View(patientList);
            }
           catch(Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"]=error;
                return View();
            }
        }

        [HttpGet]
        /// <summary>
        ///  used to get the details of a  patient,adoctor ,  using the concept of view models in the view side
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult BookingDoctorPatient(int id)
        {
            Dal dal = new Dal();
            string Username = Session["username"].ToString();
            Doctor_Patient_SlotBooking doctor_Patient_SlotBooking = new Doctor_Patient_SlotBooking();
            doctor_Patient_SlotBooking.DoctorSignUp = dal.particulardoctor(id);
            doctor_Patient_SlotBooking.PatientSignUp = dal.viewparticularpatientdetails(Username);
            return View(doctor_Patient_SlotBooking);

        }

        /// <summary>
        ///  to insert the slot booking details in database    ,where this view has multiple models in it.
        /// </summary>
        /// <param name="slotBooking"></param>
        /// <returns>this function returns a boolean value depends on whether they are inserted properly or not</returns>
        [HttpPost]
        public ActionResult BookingDoctorPatient(Doctor_Patient_SlotBooking model)
        {
            Dal dal = new Dal();
            ModelState.Clear();
            try
            {
                if (ModelState.IsValid)
                {
                    bool isinserted = false;
                    isinserted = dal.BookingOnlineInsert(model.SlotBooking);
                    if (isinserted)
                    {
                        TempData["SuccessMessage"] = "Booking Successfull";
                        return RedirectToAction("ViewProfile", "Patient");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Booking Cancelled";
                        return View("Index", "Patient");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Validation Error";
                    return View("Index", "Patient");
                }
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error.Message;
                //  return View("Index", "Patient");
                return View();
            }

        }

        /// <summary>
        ///  to get the complete list of appointment details of a  patient   based on patient id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult viewappointment(int? id)
        {
            Dal dal = new Dal();
            var appointmentDetailsView = dal.viewappointment(id.Value);
            try
            {
                if (appointmentDetailsView != null)
                {
                    return View(appointmentDetailsView);
                }
                else
                {
                    return View();
                }
            }
            catch(Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"]= error;
                return View();
            }
          
        }

        /// <summary>
        /// to get the complete details of an appointment  of a  patient based on appointment id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Editappointment1(int? id)
        {
            Dal dal = new Dal();
            try
            {
                if (id.HasValue && id != 0)
                {
                    var editappointmentDetailsView = dal.editappointmentid1(id.Value).FirstOrDefault();
                    if (editappointmentDetailsView != null)
                    {
                        return View(editappointmentDetailsView);
                    }
                }
                return View();
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);

                TempData["ErrorMessage"] = error;
                return View();
            }
        }

        /// <summary>
        /// s used to update the details of a appointment based on the appointment id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="slotBooking"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Editappointment1(int? id, SlotBookingEdit slotBookingedit)
        {
            Dal dal = new Dal();
            try
            {
                bool isinserted = false;
                if (slotBookingedit != null && id.HasValue && id != 0)
                {
                    isinserted = dal.updateappointmentid(id.Value,slotBookingedit);
                    if (isinserted)
                    {
                        TempData["SuccessMessage"] = "Updated successfully";
                        return RedirectToAction("viewappointment", "Patient", new { id = slotBookingedit.PatientId });
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Cannot update";
                        return RedirectToAction("viewappointment", "Patient", new { id = slotBookingedit.PatientId });
                    }
                }
                return View();
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return RedirectToAction("viewappointment", "Patient", new { id = slotBookingedit.PatientId });

            }

        }

        /// <summary>
        ///  to get the single appointment details of a person using appointment id   for delete purpose
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Deleteappointment(int? id)
        {
            Dal dal = new Dal();
            try
            {
                if (id.HasValue && id != 0)
                {
                    var deleteappointmentDetailsView = dal.editappointmentid(id.Value).FirstOrDefault();
                    if (deleteappointmentDetailsView != null)
                    {
                        return View(deleteappointmentDetailsView);
                    }
                }
                // Handle the case when id is null or 0, or when the appointment details are not found
                return View();
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return View();
            }
        }

        /// <summary>
        ///  used to delete single appointment details     /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Deleteappointment")]
        public ActionResult Delete(int? id)
        {
            Dal dal = new Dal();
            try
            {
                if (id.HasValue && id != 0)
                {
                    string result = dal.deleteappointmentdetails(id.Value);
                    if (result.Contains("deleted"))
                    {
                        TempData["SuccessMessage"] = "Deleted successfully";
                        return RedirectToAction("Index", "Patient");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid credentials";
                        return RedirectToAction("Index", "Patient");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid credentials";
                    return View();

                }
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return View();

            }
        }


        /// <summary>
        /// to view the details of a patient using username
        /// </summary>
        /// <returns></returns>
        public ActionResult ViewProfile()
        {
            string Username = Session["username"]?.ToString();
            try
            {
                if (Username != null)
                {
                    Dal dal = new Dal();
                    var patientDetails = dal.viewparticularpatientdetails(Username).FirstOrDefault();
                    return View(patientDetails);
                }
                return View();
            }
            catch(Exception error) {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"]= error;
                return View();
            }
        }

        /// <summary>
        /// to update the password of a patient
        /// </summary>
        /// <param name="changepassword"></param>
        /// <returns></returns>
        public ActionResult Changepassword(Changepassword changepassword)
        {
            Dal dal = new Dal();
            DataTable data = null;
            Signin signin = new Signin();
            signin.Username = changepassword.Username;
            signin.Password = changepassword.Password;
            signin.Identity_verification = null;
            ModelState.Clear();
            try
            {
               // if (!ModelState.IsValid)
               // {
                    // If the model state is not valid, return the view with the validation errors.
               //     TempData["ErrorMessage"] = "All fields are required";
                 //   return RedirectToAction("Viewprofile", "Patient");
              //  }
                bool isupdated = false;
                data = dal.passwordchange(signin);
                //  string specialisation = data.Rows[0]["Specialisation"].ToString();
                if (data != null && data.Rows.Count > 0)
                {
                    if (data.Columns.Contains("Specialisation"))
                    {
                        isupdated = dal.Doctorpasswordchange(changepassword);
                        if (isupdated)
                        {
                            TempData["SuccessMessage"] = "Successfully updated";
                            return RedirectToAction("ViewProfieDoctor", "Doctor");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Updation cancelled";
                            return View("");
                        }

                    }
                    else if (data.Columns.Contains("Identity_verification"))
                    {
                        isupdated = dal.Adminpasswordchange(changepassword);
                        if (isupdated)
                        {
                            TempData["SuccessMessage"] = "Successfully updated";
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Updation cancelled";
                            return View("");
                        }
                    }
                    else
                    {
                        isupdated = dal.Patientpasswordchange(changepassword);
                        if (isupdated)
                        {
                            TempData["SuccessMessage"] = "Successfully updated";
                            return RedirectToAction("Signin", "Home");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Invalid user credentials";
                            return View("Index");
                        }
                    }
                }
                TempData["ChangePasswordError"] = "Invalid user credentials";
                return View("Index");
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                TempData["ErrorMessage"] ="Validation error";
                return RedirectToAction("Index", "Patient");
                
            }
        }

        /// <summary>
        /// for sign out purpose
        /// </summary>
        /// <returns></returns>
        public ActionResult Signout()
{
    FormsAuthentication.SignOut();
    Session.Clear();
    return RedirectToAction("Index", "Home");
}
    }
}























