using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using MedScheduler.DAL;
using MedScheduler.Models;

namespace MedScheduler.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {


        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// get the details of a   doctor using username and session tag
        /// </summary>
        /// <returns></returns>
        public ActionResult Details()
        {
            string username = Session["Username"]?.ToString(); // Use the null-conditional operator '?.' to safely access the session value
            try
            {
                if (username != null)
                {
                    Dal dal = new Dal();
                    var doctorSignUp = dal.viewparticulardoctor(username).FirstOrDefault();
                    return View(doctorSignUp);
                }
                return View();
            }
            catch(Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return View("Error");
            }
           
        }
        

        /// <summary>
        /// used to get  details of a doctor  to update  using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id != 0)
            {
                Dal dal = new Dal();
                var doctorlist = dal.particulardoctor(id).FirstOrDefault();
                Session["doctorListImage"] = doctorlist.Profilephoto;
                return View(doctorlist);
            }
            return View();

        }
     
       
        /// <summary>
        /// store the updated details of doctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorSignUp"></param>
        /// <param name="ImageFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, DoctorSignUp doctorSignUp, HttpPostedFileBase ImageFile)
        {
            bool isupdated = false;
            string photoid = doctorSignUp.Profilephoto;
            string image = Session["doctorListImage"].ToString();
            Dal dal = new Dal();
            try
            {
                {
                   
                    isupdated = dal.UpdateProducts(doctorSignUp, ImageFile, image);
                    if (isupdated)
                    {
                        Session["username"] = doctorSignUp.Username;

                        TempData["DoctorUpdationSuccessMessage"] = "Updated succesfully";
                        return RedirectToAction("ViewProfieDoctor");
                    }
                    else
                    {

                        TempData["DoctorUpdationErrorMessage"] = "Updation cancelled";
                        return View();
                    }
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
        /// to get the  details of a doctor based on id  to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id != 0)
            {
                Dal dal = new Dal();
                var doctorlist = dal.particulardoctor(id).FirstOrDefault(); ;
                return View(doctorlist);
            }
            return View();
        }

        /// <summary>
        /// used to delete the details of doctor using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")] 
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                Dal dal = new Dal();
                string result = dal.DeleteDoctorDetails(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                    return RedirectToAction("ViewProfieDoctor", "Doctor");

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
        /// to get all the  list of patients who took slot for appointment  to a doctor using the doctorid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Getappointmentdetails(int? id)
           {
               Dal dal = new Dal();
               try
               {
                   if (id.HasValue)
                   {
                       var appointmentdetails = dal.Getallappointmentdetails(id.Value);

                    // Sort the appointmentdetails list based on date and time
                    appointmentdetails.Sort((a1, a2) =>
                    {
                        DateTime dateTime1 = DateTime.ParseExact(a1.Date_of_appointment, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        DateTime dateTime2 = DateTime.ParseExact(a2.Date_of_appointment, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        return dateTime1.CompareTo(dateTime2);
                    });

                    ViewBag.AppointmentDetails = appointmentdetails;
                       return View(appointmentdetails);
                   }
                   else
                   {
                       TempData["ErrorMessage"] = "Id is null";
                       return View();
                   }
               }
               catch (Exception error)
               {
                ErrorLogger.LogError(error);
                   TempData["ErrorMessage"] = error;
                   return View("");
               }

           }  

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Getappointmentdetails()
        {
            return View();
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
       
        /// <summary>
        /// to view the profile details of the doctor using username
        /// </summary>
        /// <returns></returns> 
        public ActionResult ViewProfieDoctor()
        {
            string username = Session["Username"]?.ToString();
            try
            {
                if (username != null)
                {
                    Dal dal = new Dal();
                    var doctorSignUp = dal.viewparticulardoctor(username).FirstOrDefault();
                    return View(doctorSignUp);
                }
                return View();
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return View("Error");
            }

        }

          /// <summary>
          ///    to update password
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
              
                bool isupdated = false;
                data = dal.passwordchange(signin);
               
                if (data != null && data.Rows.Count > 0)
                {
                    if (data.Columns.Contains("Specialisation"))
                    {
                        isupdated = dal.Doctorpasswordchange(changepassword);
                        if (isupdated)
                        {
                            TempData["DPasswordUpdateSuccessMessage"] = "Successfully updated";
                            return RedirectToAction("Signin", "Home");
                        }
                        else
                        {
                            TempData["DPasswordUpdateErrorMessage"] = "Updation cancelled";
                            return RedirectToAction("ViewProfieDoctor", "Doctor");
                        }

                    }
                    else if (data.Columns.Contains("Identity_verification"))
                    {
                        isupdated = dal.Adminpasswordchange(changepassword);
                        if (isupdated)
                        {
                            TempData["APasswordUpdateSuccessMessage"] = "Successfully updated";
                            return RedirectToAction("Index", "Admin");
                        }
                        else
                        {
                            TempData["APasswordUpdateErrorMessage"] = "Updation cancelled";
                            return RedirectToAction("Index", "Admin");
                        }
                    }
                    else
                    {
                        isupdated = dal.Patientpasswordchange(changepassword);
                        if (isupdated)
                        {
                            TempData["PPasswordUpdateSuccessMessage"] = "Successfully updated";
                            return RedirectToAction("Viewprofile", "Patient");
                        }
                        else
                        {
                            TempData["PPasswordUpdateErrorMessage"] = "Invalid user credentials";
                            return RedirectToAction("Viewprofile", "Patient");
                        }
                    }
                }
                TempData["DPasswordUpdateErrorMessage"] = "Invalid credentials";
                return RedirectToAction("ViewProfieDoctor", "Doctor");
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                TempData["DPasswordUpdateErrorMessage"] = "Validation error";
              return  RedirectToAction("ViewProfieDoctor", "Doctor");
               
            }
        }
    }
}
