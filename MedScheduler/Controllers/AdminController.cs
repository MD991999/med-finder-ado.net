using MedScheduler.DAL;
using MedScheduler.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace MedScheduler.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        /// <summary>
        /// used to display all the three models view in a single view.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var viewModel = new DoctorsPatientsModel();
            Dal dal = new Dal();
            viewModel.DoctorSignUp = dal.getalldoctor();
            viewModel.patientSignUp = dal.getallpatients();
            viewModel.adminmodel = dal.getalladmin();
            return View(viewModel);
        }

        /// <summary>
        /// to get the  details of a doctor based on id  to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Deletedoctor(int id)
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
        /// to delete the details of a  doctor by admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Deletedoctor")]
        public ActionResult Delete(int id)
        {
            Dal dal = new Dal();
            try
            {
                if (id != 0)
                {
                    string result = dal.DeleteDoctorDetails(id);
                    if (result.Contains("deleted"))
                    {
                        TempData["SuccessMessage"] = result;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = result;
                        return View();
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Id is null";
                    return View();
                }
            }
           catch(Exception error)
            {
                ErrorLogger.LogError(error);

                TempData["ErrorMessage"] = error;
                return View("Error");
            }

        }

        [HttpGet]
        public ActionResult Addadmin()
        {
            return View();
        }

        /// <summary>
        ///  to  add admin details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Addadmin(AdminModel adminmodel)
        {
            Dal dal = new Dal();
        
            try
            {
                bool isinserted = false;
                if (ModelState.IsValid)

                {
                    Console.WriteLine("Model state is valid");
                    isinserted = dal.addadmindetails(adminmodel);
                    if (isinserted)
                    {
                        TempData["AddadminSuccessMessage"] = "Successfully registered";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["AddadminErrorMessage"] = "User Already exists";
                        return View();
                    }
                }
                else
                {
                    TempData["AddadminErrorMessage"] = "Validation error,All fields are required";
                    return View();
                }
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);

                TempData["ErrorMessage"] = error;
                // return RedirectToAction("Index");
                return View();
            }
        }

        /// <summary>
             /// fto get all the admins in the website
             /// </summary>
             /// <returns></returns>
        [HttpGet]
        public ActionResult AdminList()
        {
            Dal dal = new Dal();
            var adminList = dal.getalladmin();
            
            return View(adminList);

        }

        /// <summary>
        ///  to get an admin details by using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        [HttpGet]
        public ActionResult EditAdmin(int id)
        {
            if (id != 0)
            {
                Dal dal = new Dal();
                var adminList = dal.viewadmin(id).FirstOrDefault(); ;
                return View(adminList);
            }
            return View();

        }

        /// <summary>
        ///  to edit an  admin details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adminModel"></param>
        /// <returns></returns>
        [HttpPost]
       /* public ActionResult EditAdmin(int id, AdminModel adminModel)
        {
            try
            {
                Dal dal = new Dal();
                bool isupdated = false;
               
              
                {
                    isupdated = dal.UpdateAdminDetails(id, adminModel);
                    if (isupdated)
                    {
                        TempData["SuccessMessage"] = "Updated succesfully";
                       
                        return RedirectToAction("Index", "Admin");

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

                TempData["ErrorMessage"] = error;
                return View("Error");
            }
        }    */
      
        /// <summary>
        ///  to get a  admin details using id inorder to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AdminDelete(int id)
        {
            if (id != 0)
            {
                Dal dal = new Dal();
                var adminList = dal.viewadmin(id).FirstOrDefault(); ;
                return View(adminList);
            }
            return View();
        }

        /// <summary>
         /// to delete an admin details
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        [HttpPost, ActionName("AdminDelete")]
        public ActionResult DeleteConfirmation(int id)
        {
            try

            {
                Dal dal = new Dal();

              
                string result = dal.DeleteAdminDetails(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Index", "Admin");

                }
                else
                {
                    TempData["ErrorMessage"] = result;
                    return RedirectToAction("Index", "Admin");

                }
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);

                TempData["ErrorMessage"] = error;
                // return View();
                return View("Error");
            }

        }

        /// <summary>
        ///  to get a details of a patient inorder to delete using the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeletePatient(int id)
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
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return View("Error");
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
        [HttpPost, ActionName("DeletePatient")]
        public ActionResult DeletePatientConfirmation(int id)
        {
            try
            {
                Dal dal = new Dal();
                string result = dal.DeletePatientDetails(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                    return RedirectToAction("Index", "Admin");
                }
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return View("Error");
            }

        }


        /// <summary>
        /// to view the the profile of an admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Viewprofile(int id)
        {
            if (id != 0)
            {
                Dal dal = new Dal();
                var adminList = dal.viewadmin(id).FirstOrDefault(); ;
                return View(adminList);
            }
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
         /// to get the complete list of messages
         /// </summary>
         /// <returns></returns>
        [HttpGet]
        public ActionResult ViewMessageList()
        {
            Dal dal = new Dal();
            var ViewMessageList = dal.ViewMessageList();
            try
            {
                if (ViewMessageList.Count == 0)
                {
                    TempData["ViewMessagesSuccessMessage"] = "No messages";
                }
               
                return View(ViewMessageList);
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ViewMessageErrorMessage"] = error;
                return View();
            }

        }

        /// <summary>
        /// used to delete the details of doctor using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteMessageList(int id)
        {
            try
            {
                if (id != 0)
                {
                    Dal dal = new Dal();
                    var deletemessage = dal.deletemessage(id).FirstOrDefault(); ;
                    return View(deletemessage);
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
        ///  for deleting a  patient details  using patientid
        /*In this example, the action method is named DeleteConfirmation, but by using [HttpPost, ActionName("Delete")],
         you specify that the action should be treated as the "Delete" action. So, when the form is submitted,
         it will invoke the DeleteConfirmation action method, but the URL and routing will still show the action as
         "Delete".*/
        /// </summary>
        /// <param name="id"></param>
        /// <returns>this function returns  a message to the view whether deletion is successfull/not</returns>
        [HttpPost, ActionName("DeleteMessageList")]
        public ActionResult DeleteMessageConfirmation(int id)
        {
            try
            {
                Dal dal = new Dal();
                string result = dal.DeleteMessageDetails(id);
                if (result.Contains("deleted"))
                {
                    TempData["DeleteMessagelistSuccessMessage"] = result;
                    return RedirectToAction("ViewMessagelist", "Admin");
                }
                else
                {
                    TempData["DeleteMessagelistErrorMessage"] = result;
                    return RedirectToAction("Index", "Admin");
                }
            }
            catch (Exception error)
            {
                ErrorLogger.LogError(error);
                TempData["ErrorMessage"] = error;
                return View("Error");
            }

        }
     
        /// <summary>
        /// to update the password of admin
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
            signin.Identity_verification = changepassword.Verification_key;
            ModelState.Clear();
            try
            {
              /*  if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "All fields are required";
                    return RedirectToAction("Index", "Admin");
                }  */
                bool isupdated = false;
                data = dal.passwordchange(signin);
                if (data != null && data.Rows.Count > 0)
                {

                    if (data.Columns.Contains("Specialisation"))
                    {

                        isupdated = dal.Doctorpasswordchange(changepassword);
                        if (isupdated)
                        {
                            TempData["SuccessMessage"] = "Successfully updated";
                            return RedirectToAction("Index", "Home");
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
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["AdminChangePasswordErrorMessage"] = "Updation cancelled";
                            return RedirectToAction("Index", "Admin");
                        }

                    }

                    else
                    {
                        isupdated = dal.Patientpasswordchange(changepassword);
                        if (isupdated)
                        {
                            TempData["SuccessMessage"] = "Successfully updated";
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Invalid user credentials";
                            return View("");
                        }
                    }

                }
                TempData["AdminChangePasswordErrorMessage"] = "Invalid credentials";
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                TempData["ErrorMessage"] = "Validation error";
                return RedirectToAction("Index", "Admin");
               
            }

        }

    }

}
