using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedScheduler.Models;
using MedScheduler.DAL;
using System.IO;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Net.Mail;
using System.Net;
using System.Web.ModelBinding;

namespace MedScheduler.Controllers
{

    public class HomeController : Controller
    {
        //index
        public ActionResult Index()
        {
            return View();
        }

        //about
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        //contact
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Patientsignup()
        {
            return View();
        }

        /// <summary>
        /// register patient details
        /// </summary>
        /// <param name="userdetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Patientsignup(PatientSignUp userdetails)
        {
            Dal dal = new Dal();
            bool isinserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    Console.WriteLine("Model state is valid");
                    isinserted = dal.patientsignupinsert(userdetails);
                    if (isinserted)
                    {
                        TempData["PatientSignupSuccessMessage"] = "Successfully registered";
                        return RedirectToAction("Signin","Home");
                    }
                    else
                    {
                        TempData["PatientSignupErrorMessage"] = "User Already exists";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["PatientSignupErrorMessage"] = "All fields are mandatory !!!";
                    TempData["UserDetails"] = userdetails;
                    return View();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                TempData["PatientSignupErrorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public ActionResult DoctorSignUp()
        {
            return View();
        }

        /// <summary>
        /// register doctor details
        /// </summary>
        /// <param name="userdetails"></param>
        /// <param name="ImageFile"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DoctorSignUp(DoctorSignUp userdetails, HttpPostedFileBase ImageFile)
        {
            Dal dal = new Dal();
           
            ModelState.Clear();  
            try
            {
                if (ModelState.IsValid)

                {
                    bool isinserted = false;
                    
                    isinserted = dal.doctorsignupinsert(userdetails, ImageFile);

                    if (isinserted)
                    {
                        TempData["SuccessMessage"] = "Successfully registered";
                        return RedirectToAction("Signin","Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "User Already exists/Invalid image format";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                   
                    TempData["ErrorMessage"] = "Validation error";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                TempData["ErrorMessage"] = ex;
                
               return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        /// <summary>
        /// check login credentials
        /// </summary>
        /// <param name="userdetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Signin(Signin userdetails)
        {
            Dal dal = new Dal();
            DataTable data = null;
            try
            {
                data = dal.signindetails(userdetails);
               
                if (data != null && data.Rows.Count > 0)
                {
                    if (data.Columns.Contains("Specialisation"))
                    {
                        TempData["SuccessMessage"] = "Successfully logged in..";
                        Session["username"] = userdetails.Username;
                        FormsAuthentication.SetAuthCookie(userdetails.Username, false);
                        Session["Role"] = "Doctor";
                        return RedirectToAction("Index", "Doctor");
                    }
                    else if (data.Columns.Contains("Identity_verification"))
                    {
                        TempData["SuccessMessage"] = "Successfully logged in..";
                        Session["username"] = userdetails.Username;
                        Session["Role"] = "Admin";
                        FormsAuthentication.SetAuthCookie(userdetails.Username, false);
                        return RedirectToAction("Index", "Admin");
                        //here first is the action name and second is the controller name
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Successfully logged in..";
                        Session["username"] = userdetails.Username;
                        Session["Role"] = "Patient";
                        FormsAuthentication.SetAuthCookie(userdetails.Username, false);
                        return RedirectToAction("Index", "Patient");
                    }
                }
              else
                {
                    TempData["ErrorMessage"] = "Invalid user credentials";
                    return RedirectToAction("Signin");
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                TempData["ErrorMessage"] = ex.ToString();
                return View("");
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
        
        [HttpGet]
        public ActionResult AddConcern()
        {
            return View();
        }

          /// <summary>
          /// to send messages to the admin
          /// </summary>
          /// <param name="contact"></param>
          /// <returns></returns>
        [HttpPost]
          public ActionResult AddConcern(Contact contact)
        {
            Dal dal = new Dal();
            try
            {
                ModelState.Clear();
                if (ModelState.IsValid)
                {
                    bool isinserted = false;
                    isinserted = dal.AddConcern(contact);
                    if (isinserted)
                    {
                        TempData["AddConcernSuccessMessage"] = "Message successfully send";
                        return RedirectToAction("Index");
                    }
                    return View();
                }
                else
                {
                    TempData["AddConcernErrorMessage"] = "Validation error";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                TempData["AddConcernErrorMessage"] = ex;
                return View();
            }
        }  
 }
}

