using MedScheduler.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Web.Mvc;
using System.Net;
using System.Reflection;
using System.Web.Helpers;
using System.Text;

namespace MedScheduler.DAL
{
    public class Dal
    {
        public SqlConnection conn;

        /// <summary>
        ///  used to store connection string
        /// </summary>
        public void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            conn = new SqlConnection(constr);
        }

        /// <summary>
        ///   used to register the details of the patients in database
        /// </summary>
        /// <param name="patientdetails"></param>
        /// <returns></returns>
        public bool patientsignupinsert(PatientSignUp patientdetails)
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SPI_TblPatientregistration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Firstname", patientdetails.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", patientdetails.Lastname);
                cmd.Parameters.AddWithValue("@Dateofbirth", patientdetails.Dateofbirth);
                cmd.Parameters.AddWithValue("@Gender", patientdetails.Gender);
                cmd.Parameters.AddWithValue("@Phonenumber", patientdetails.Phonenumber);
                cmd.Parameters.AddWithValue("@Email", patientdetails.Email);
                cmd.Parameters.AddWithValue("@Address", patientdetails.Address);
                cmd.Parameters.AddWithValue("@State", patientdetails.State);
                cmd.Parameters.AddWithValue("@City", patientdetails.City);
                cmd.Parameters.AddWithValue("@Username", patientdetails.Username);
                cmd.Parameters.AddWithValue("@Password", Encrypt(patientdetails.Password));
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///    to register the details of the doctors in database
        /// </summary>
        /// <param name="doctordetails"></param>
        /// <param name="ImageFile"></param>
        /// <returns></returns>
        public bool doctorsignupinsert(DoctorSignUp doctordetails, HttpPostedFileBase ImageFile)
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SPI_Tbl_Doctorregistration", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Firstname", doctordetails.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", doctordetails.Lastname);
                cmd.Parameters.AddWithValue("@Age", doctordetails.Age);
                cmd.Parameters.AddWithValue("@Gender", doctordetails.Gender);
                cmd.Parameters.AddWithValue("@Phonenumber", doctordetails.Phonenumber);
                cmd.Parameters.AddWithValue("@Email", doctordetails.Email);
                cmd.Parameters.AddWithValue("@Workspace", doctordetails.Workspace);
                cmd.Parameters.AddWithValue("@Specialisation", doctordetails.Specialisation);
                cmd.Parameters.AddWithValue("@Experiance", doctordetails.Experience);
                cmd.Parameters.AddWithValue("@Username", doctordetails.Username);
                cmd.Parameters.AddWithValue("@Password", Encrypt(doctordetails.Password));
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                    string fileExtension = Path.GetExtension(ImageFile.FileName);
                    if (allowedExtensions.Contains(fileExtension.ToLower()))
                    {
                        string filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string uniqueFilename = filename + DateTime.Now.ToString("yymmssfff") + fileExtension;
                        string physicalPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), uniqueFilename);
                        doctordetails.Profilephoto = "~/Images/" + uniqueFilename;
                        ImageFile.SaveAs(physicalPath);
                        cmd.Parameters.AddWithValue("@Profilephoto", doctordetails.Profilephoto);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Profilephoto", DBNull.Value);
                    //By passing DBNull.Value as the parameter value, you inform the database that the corresponding field should be set to a null value, allowing you to handle the absence of an image file in your database queries or operations.
                }
                cmd.Parameters.AddWithValue("@Monday", doctordetails.Monday);
                cmd.Parameters.AddWithValue("@Tuesday", doctordetails.Tuesday);
                cmd.Parameters.AddWithValue("@Wednesday", doctordetails.Wednesday);
                cmd.Parameters.AddWithValue("@Thursday", doctordetails.Thursday);
                cmd.Parameters.AddWithValue("@Friday", doctordetails.Friday);
                cmd.Parameters.AddWithValue("@Saturday", doctordetails.Saturday);
                cmd.Parameters.AddWithValue("@Sunday", doctordetails.Sunday);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  used to check the user credentials
        /// </summary>
        /// <param name="userdetails"></param>
        /// <returns></returns>
        public DataTable signindetails(Signin userdetails)
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SPS_logincredentials", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", userdetails.Username);
                cmd.Parameters.AddWithValue("@Password", Encrypt(userdetails.Password));
                cmd.Parameters.AddWithValue("@Identity_verification", userdetails.Identity_verification);
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(sdr); 
                return dataTable;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// logic for password change
        /// </summary>
        /// <param name="userdetails"></param>
        /// <returns></returns>
        public DataTable passwordchange(Signin userdetails)
        {
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SPS_logincredentials", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", userdetails.Username);
                cmd.Parameters.AddWithValue("@Password", Encrypt(userdetails.Password));
                cmd.Parameters.AddWithValue("@Identity_verification", userdetails.Identity_verification);
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(sdr); 
                return dataTable;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  to list all doctors in the database
        /// </summary>
        /// <returns></returns>
        public List<DoctorSignUp> getalldoctor()
        {
            List<DoctorSignUp> doctorcollection = new List<DoctorSignUp>();
            try {
                connection();
                SqlCommand cmd = new SqlCommand("SPS_Doctordetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                conn.Open();

                sdr.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    doctorcollection.Add(new DoctorSignUp //DoctorSignUp is the model
                    {
                        Id = Convert.ToInt32(dr["Id"]),  //the data that are  retreived from the database are placed in the key that we defined in the model
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Gender = dr["Gender"].ToString(),
                        Phonenumber = dr["Phonenumber"].ToString(),
                        Email = dr["Email"].ToString(),
                        Workspace = dr["Workspace"].ToString(),
                        Specialisation = dr["Specialisation"].ToString(),
                        Experience = Convert.ToInt32(dr["Experiance"]),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        Profilephoto = dr["Profilephoto"].ToString(),
                        Monday = dr["Monday"].ToString(),
                        Tuesday = dr["Tuesday"].ToString(),
                        Wednesday = dr["Wednesday"].ToString(),
                        Thursday = dr["Thursday"].ToString(),
                        Friday = dr["Friday"].ToString(),
                        Saturday = dr["Saturday"].ToString(),
                        Sunday = dr["Sunday"].ToString()
                    });

                }
                return doctorcollection;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  to view details of a particular doctor using username
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public List<DoctorSignUp> viewparticulardoctor(string Username)
        {
            List<DoctorSignUp> DoctorList = new List<DoctorSignUp>();
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SPG_ParticularDoctordetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", Username);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                //now the data is available in the table,what we do next is we will access this data row by row and introduce them as a list of itms in the productList
                foreach (DataRow dr in dt.Rows)
                {
                    DoctorList.Add(new DoctorSignUp
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Gender = dr["Gender"].ToString(),
                        Phonenumber = dr["Phonenumber"].ToString(),
                        Email = dr["Email"].ToString(),
                        Workspace = dr["Workspace"].ToString(),
                        Specialisation = dr["Specialisation"].ToString(),
                        Experience = Convert.ToInt32(dr["Experiance"]),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        ConfirmPassword = dr["ConfirmPassword"].ToString() ,
                        Profilephoto = dr["Profilephoto"].ToString(),
                        Monday = dr["Monday"].ToString(),
                        Tuesday = dr["Tuesday"].ToString(),
                        Wednesday = dr["Wednesday"].ToString(),
                        Thursday = dr["Thursday"].ToString(),
                        Friday = dr["Friday"].ToString(),
                        Saturday = dr["Saturday"].ToString(),
                        Sunday = dr["Sunday"].ToString()
                    }); ;
                }
                return DoctorList;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  to get the details of single doctor passing id as parameter
        /// </summary>
        /// <param name="id"></param>
        /// <returns>this function returns list of doctors with that specific id</returns>
        public List<DoctorSignUp> particulardoctor(int id)
        {
            List<DoctorSignUp> DoctorList = new List<DoctorSignUp>();
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SPS_ViewDoctordetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                conn.Open();
                adapter.Fill(dt);
                //now the data is available in the table,what we do next is we will access this data row by row and introduce them as a list of itms in the productList

                foreach (DataRow dr in dt.Rows)
                {
                    DoctorList.Add(new DoctorSignUp
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Gender = dr["Gender"].ToString(),
                        Phonenumber = dr["Phonenumber"].ToString(),
                        Email = dr["Email"].ToString(),
                        Workspace = dr["Workspace"].ToString(),
                        Specialisation = dr["Specialisation"].ToString(),
                        Experience = Convert.ToInt32(dr["Experiance"]),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        Profilephoto = dr["Profilephoto"].ToString(),
                        Monday = dr["Monday"].ToString(),
                        Tuesday = dr["Tuesday"].ToString(),
                        Wednesday = dr["Wednesday"].ToString(),
                        Thursday = dr["Thursday"].ToString(),
                        Friday = dr["Friday"].ToString(),
                        Saturday = dr["Saturday"].ToString(),
                        Sunday = dr["Sunday"].ToString()
                    });

                }
                return DoctorList;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  to update the details of a doctor 
        /// </summary>
        /// <param name="doctorSignUp"></param>
        /// <param name="ImageFile"></param>
        /// <returns></returns>
        public bool UpdateProducts(DoctorSignUp doctorSignUp, HttpPostedFileBase ImageFile,String photo)
        {
            int i = 0;
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SPU_UpdateAlldetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", doctorSignUp.Id);
                cmd.Parameters.AddWithValue("@Firstname", doctorSignUp.Firstname);
                cmd.Parameters.AddWithValue("@Lastname", doctorSignUp.Lastname);
                cmd.Parameters.AddWithValue("@Age", doctorSignUp.Age);
                cmd.Parameters.AddWithValue("@Gender", doctorSignUp.Gender);
                cmd.Parameters.AddWithValue("@Phonenumber", doctorSignUp.Phonenumber);
                cmd.Parameters.AddWithValue("@Email", doctorSignUp.Email);
                cmd.Parameters.AddWithValue("@Workspace", doctorSignUp.Workspace);
                cmd.Parameters.AddWithValue("@Specialisation", doctorSignUp.Specialisation);
                cmd.Parameters.AddWithValue("@Experiance", doctorSignUp.Experience);
                cmd.Parameters.AddWithValue("@Username", doctorSignUp.Username);
               if(ImageFile != null && ImageFile.ContentLength > 0)
                {
                    string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };
                    string fileExtension = Path.GetExtension(ImageFile.FileName);

                    if (allowedExtensions.Contains(fileExtension.ToLower()))
                    {
                        string filename = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                        string uniqueFilename = filename + DateTime.Now.ToString("yymmssfff") + fileExtension;
                        string physicalPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images/"), uniqueFilename);
                        doctorSignUp.Profilephoto = "~/Images/" + uniqueFilename;
                        ImageFile.SaveAs(physicalPath);
                        cmd.Parameters.AddWithValue("@Profilephoto", doctorSignUp.Profilephoto);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Profilephoto", photo);
                }  
                conn.Open();
                i = cmd.ExecuteNonQuery(); //ExecuteNonQuery will have either 1 or 0 as its value.If we insert the data successfully the value that we obtain will be 1 other wise it will be 0.
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  used to delete the details of a single doctor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteDoctorDetails(int id)
        {
            try
            {
                connection();
                string result = "";
                SqlCommand command = new SqlCommand("SPD_Tbl_Doctorregistration", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.Add("@outputmessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                conn.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@outputmessage"].Value.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }
        }
       

        /// <summary>
        ///      to get the list of patients stored in the database
        /// </summary>
        /// <returns>this function returns a list of patients </returns>
        public List<PatientSignUp> getallpatients()
        {
            try
            {
                connection();
                List<PatientSignUp> patientcollection = new List<PatientSignUp>();
                SqlCommand command = new SqlCommand("SPS_TblUserregistration", conn);
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();

                conn.Open();
                adapter.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    patientcollection.Add(new PatientSignUp
                    {
                        Id= Convert.ToInt32(dr["Id"]),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Dateofbirth = dr["Dateofbirth"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Phonenumber = dr["Phonenumber"].ToString(),
                        Email = dr["Email"].ToString(),
                        Address = dr["Address"].ToString(),
                        State = dr["State"].ToString(),
                        City = dr["City"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString()

                    });



                }
                return patientcollection;
            }
            finally
            {
                conn.Close();
            }
        }
      
        /// <summary>
                   ///   to encrypt password
                   /// </summary>
                   /// <param name="clearText"></param>
                   /// <returns></returns>
                   /// <exception cref="Exception"></exception>
        private string Encrypt(string clearText)
        {
            try
            {
                if(clearText!= null)
                {
                    byte[] bytes = new byte[clearText.Length];
                    bytes = System.Text.Encoding.UTF8.GetBytes(clearText);
                    string encryptedData = Convert.ToBase64String(bytes);
                    return encryptedData;
                }
                else
                {
                    return null;
                }
            }
         
            catch (Exception errror)
            {
                throw new Exception("Error in encode" + errror.Message);
            }

        }

      
        /// <summary>
        /// update patient details
        /// </summary>
        /// <param name="patientSignUp"></param>
        /// <returns>this function is for updating the patient details and it returns true or false</returns>
        public bool UpdatePatientDetails(int Id, PatientSignUp patientSignUp)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPU_TblUserregistration", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@Firstname", patientSignUp.Firstname);
                command.Parameters.AddWithValue("@Lastname", patientSignUp.Lastname);
                command.Parameters.AddWithValue("@Dateofbirth", patientSignUp.Dateofbirth);
                command.Parameters.AddWithValue("@Gender", patientSignUp.Gender);
                command.Parameters.AddWithValue("@Phonenumber", patientSignUp.Phonenumber);
                command.Parameters.AddWithValue("@Email", patientSignUp.Email);
                command.Parameters.AddWithValue("@Address", patientSignUp.Address);
                command.Parameters.AddWithValue("@State", patientSignUp.State);
                command.Parameters.AddWithValue("@City", patientSignUp.City);
                command.Parameters.AddWithValue("@Username", patientSignUp.Username);
                conn.Open();
                int i = command.ExecuteNonQuery(); //ExecuteNonQuery will have either 1 or 0 as its value.If we insert the data successfully the value that we obtain will be 1 other wise it will be 0.
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// to delete patient details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>this returns a message stating whether the deletion is successfull/not</returns>
        public string DeletePatientDetails(int id)
        {
            try
            {
                connection();
                string result = "";

                SqlCommand command = new SqlCommand("SPD_TblUserregistration", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.Add("@outputmessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                //the above method means adding a new parameter,its called return  message and it is of the type varchar with 50 length and by parameterdirection.Output wat it means is that  it will retrieve a value from the stored procedure.
                conn.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@outputmessage"].Value.ToString();
                //by the above method we wil retrieve the value of returnmessage from it and store them in a variable called result.
                return result;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// to get the detail of a patient using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<PatientSignUp> particularpatient(int id)
        {
            List<PatientSignUp> PatientList = new List<PatientSignUp>();
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPS_ViewPatientdetails", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    PatientList.Add(new PatientSignUp
                    {
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Dateofbirth = dr["Dateofbirth"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Phonenumber = dr["Phonenumber"].ToString(),
                        Email = dr["Email"].ToString(),
                        Address = dr["Address"].ToString(),
                        State = dr["State"].ToString(),
                        City = dr["City"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString()
                    });
                }
                return PatientList;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  used to get the details of a single patient using username
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public List<PatientSignUp> viewparticularpatientdetails(string Username)
        {
            List<PatientSignUp> patientSignUp = new List<PatientSignUp>();
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPS_SinglePatientDetails", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", Username);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                conn.Open();
                adapter.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    patientSignUp.Add(new PatientSignUp
                    {
                        Id = Convert.ToInt32(dr["id"]),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Dateofbirth = dr["Dateofbirth"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Phonenumber = dr["Phonenumber"].ToString(),
                        Email = dr["Email"].ToString(),
                        Address = dr["Address"].ToString(),
                        State = dr["State"].ToString(),
                        City = dr["City"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = Encrypt(dr["Password"].ToString())
                    });
                }
                return patientSignUp;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// for online booking purpose'
        /// /// </summary>
        /// <param name="SlotBooking"></param>
        /// <returns>this function returns a true /false depending upon whether the value get inserted or not</returns>
        public bool BookingOnlineInsert(SlotBooking slotBooking)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPI_Table_SlotBooking", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PatientId", slotBooking.PatientId);
                command.Parameters.AddWithValue("@Firstname", slotBooking.Firstname);
                command.Parameters.AddWithValue("@Lastname", slotBooking.Lastname);
                command.Parameters.AddWithValue("@Age", slotBooking.Age);
                command.Parameters.AddWithValue("@Gender", slotBooking.Gender);
                command.Parameters.AddWithValue("@Email", slotBooking.Email);
                command.Parameters.AddWithValue("@Phone", slotBooking.Phone);
                command.Parameters.AddWithValue("@Message", slotBooking.Message);
                command.Parameters.AddWithValue("@DoctorId", slotBooking.DoctorId);
                command.Parameters.AddWithValue("@Doctorname", slotBooking.Doctorname);
                command.Parameters.AddWithValue("@Specialisation", slotBooking.Specialisation);
                command.Parameters.AddWithValue("@Date_of_appointment", slotBooking.Date_of_appointment);
                command.Parameters.AddWithValue("@Time_of_appointment", slotBooking.Time_of_appointment);
                int i = 0;
                conn.Open();
                i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
                finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// to add the admin details in the database
        /// </summary>
        /// <param name="adminmodel"></param>
        /// <returns></returns>
        public bool addadmindetails(AdminModel adminmodel)
        {
            
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SPI_Admindetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Adminname", adminmodel.Adminname);
                cmd.Parameters.AddWithValue("@Jobrole", adminmodel.Jobrole);
                cmd.Parameters.AddWithValue("@Username", adminmodel.Username);
                cmd.Parameters.AddWithValue("@Password", Encrypt(adminmodel.Password));
                cmd.Parameters.AddWithValue("@Identity_verification", adminmodel.Identity_verification);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  to get the details of all the admins in the database
        /// </summary>
        /// <returns></returns>
        public List<AdminModel> getalladmin()
        {
            List<AdminModel> admincollection = new List<AdminModel>();
            try
            {
                connection();
                SqlCommand cmd = new SqlCommand("SPS_Tbl_Admindetails", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sdr = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                conn.Open();
                sdr.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    admincollection.Add(new AdminModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),  //the data that are  retreived from the database are placed in the key that we defined in the model
                        Adminname = dr["Admin name"].ToString(),
                        Jobrole = dr["Job role"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        Identity_verification = dr["Identity_verification"].ToString(),
                    });
                }
                return admincollection;
            }
            finally
            {
                conn.Close();
            }
          
          
        }


        /// <summary>
        /// to retrive the information of a single admin based on their id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<AdminModel> viewadmin(int id)
        {
            List<AdminModel> adminList = new List<AdminModel>();
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPS_Tbl_ParticularAdminDetails", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();

                conn.Open();
                adapter.Fill(dt);
                //now the data is available in the table,what we do next is we will access this data row by row and introduce them as a list of itms in the productList

                foreach (DataRow dr in dt.Rows)
                {
                    adminList.Add(new AdminModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),  //the data that are  retreived from the database are placed in the key that we defined in the model
                        Adminname = dr["Admin name"].ToString(),
                        Jobrole = dr["Job role"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        Identity_verification = dr["Identity_verification"].ToString()

                    });

                }
                return adminList;

            }

            finally
            {
                conn.Close();
            }
           

          
        }

        /// <summary>
        ///  to delete the admin details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteAdminDetails(int id)
        {
            try
            {
                connection();
                string result = "";

                SqlCommand command = new SqlCommand("SPD_Tbl_AdminDetails", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.Add("@outputmessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                //the above method means adding a new parameter,its called return  message and it is of the type varchar with 50 length and by parameterdirection.Output wat it means is that  it will retrieve a value from the stored procedure.
                conn.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@outputmessage"].Value.ToString();
                //by the above method we wil retrieve the value of returnmessage from it and store them in a variable called result.
                return result;
            }
            finally
            {
                conn.Close();
            }
          
        }


        /// <summary>
        /// to get the details of a  appointmants based on the patient id (there will list of appointments)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>this will return a list of appointments that the patient had booked</returns>
        public List<SlotBooking> viewappointment(int id)
        {
            List<SlotBooking> slotBooking = new List<SlotBooking>();
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPS_Viewappointmentwithid", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Patientid", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                conn.Open();
                adapter.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    slotBooking.Add(new SlotBooking
                    {
                        AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                        PatientId = Convert.ToInt32(dr["PatientId"]),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Gender = dr["Gender"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Message = dr["Message"].ToString(),
                        Doctorname = dr["Doctorname"].ToString(),
                        Specialisation = dr["Specialisation"].ToString(),
                        Date_of_appointment = dr["Date_of_appointment"].ToString(),
                        Time_of_appointment = dr["Time_of_appointment"].ToString(),
                        DoctorId = Convert.ToInt32(dr["DoctorId"])
                    });
                }
                return slotBooking;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  to display the appointment details of a person based on their appointment id,not based on the user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>return a single aappointment details of a patient based on appointment id</returns>
        public List<SlotBooking> editappointmentid(int id)
        {
            List<SlotBooking> slotBooking = new List<SlotBooking>();
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPS_Editappointmentid", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AppointmentId", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                conn.Open();
                adapter.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    slotBooking.Add(new SlotBooking

                    {
                        AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                        PatientId = Convert.ToInt32(dr["PatientId"]),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Gender = dr["Gender"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Message = dr["Message"].ToString(),
                        Doctorname = dr["Doctorname"].ToString(),
                        Specialisation = dr["Specialisation"].ToString(),
                        Date_of_appointment = dr["Date_of_appointment"].ToString(),
                        Time_of_appointment = dr["Time_of_appointment"].ToString(),
                        DoctorId = Convert.ToInt32(dr["DoctorId"])

                    });
                }
                return slotBooking;
            }
            finally
            {
                conn.Close();
            }
        }


           /// <summary>
           /// to get the details of a particular id on the basis of appointment id
           /// </summary>
           /// <param name="id"></param>
           /// <returns></returns>

        public List<SlotBookingEdit> editappointmentid1(int id)
        {
            List<SlotBookingEdit> slotBooking = new List<SlotBookingEdit>();
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPS_Editappointmentid", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AppointmentId", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                conn.Open();
                adapter.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    slotBooking.Add(new SlotBookingEdit
                    {
                        AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                        PatientId = Convert.ToInt32(dr["PatientId"]),
                        Age = Convert.ToInt32(dr["Age"]),
                        Message = dr["Message"].ToString(),
                        Date_of_appointment = dr["Date_of_appointment"].ToString(),
                        Time_of_appointment = dr["Time_of_appointment"].ToString()
                    });
                }
                return slotBooking;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  provides logic to how to update appointment details based on appointment id of a single user in the database
        /// </summary>
        /// <param name="appointmentid"></param>
        /// <param name="slotBooking"></param>
        /// <returns></returns>
        public bool updateappointmentid(int appointmentid, SlotBookingEdit slotBookingEdit)
        {

            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPS_Updatechangesappointmentid", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AppointmentId", appointmentid);
                command.Parameters.AddWithValue("@Age", slotBookingEdit.Age);
                command.Parameters.AddWithValue("@Message", slotBookingEdit.Message);
                command.Parameters.AddWithValue("@Date_of_appointment", slotBookingEdit.Date_of_appointment);
                command.Parameters.AddWithValue("@Time_of_appointment", slotBookingEdit.Time_of_appointment);
                int i = 0;
                conn.Open();
                i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        ///  is used to delete appointment details fron database using appointmentid
        /// </summary>
        /// <param name="appointmentid"></param>
        /// <returns></returns>
        public string deleteappointmentdetails(int appointmentid)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPD_deleteappointmentdetails", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AppointmentId", appointmentid);
                command.Parameters.Add("@outputmessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                conn.Open();
                command.ExecuteNonQuery();
                string result = command.Parameters["@outputmessage"].Value.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }
        }

          /// <summary>
          /// to get the list of appointment details based on doctorid
          /// </summary>
          /// <param name="id"></param>
          /// <returns></returns>
        public List<SlotBooking> Getallappointmentdetails(int? id)
        {
            try
            {
                connection();
                List<SlotBooking> slotBooking = new List<SlotBooking>();

                SqlCommand command = new SqlCommand("SPS_Viewappointmentwithdoctorid", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@DoctorId", id.Value);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                conn.Open();
                adapter.Fill(dataTable);

                foreach (DataRow dr in dataTable.Rows)
                {
                    slotBooking.Add(new SlotBooking

                    {
                        AppointmentId = Convert.ToInt32(dr["AppointmentId"]),
                        PatientId = Convert.ToInt32(dr["PatientId"]),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Age = Convert.ToInt32(dr["Age"]),
                        Gender = dr["Gender"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        Message = dr["Message"].ToString(),
                        Doctorname = dr["Doctorname"].ToString(),
                        Specialisation = dr["Specialisation"].ToString(),
                        Date_of_appointment = dr["Date_of_appointment"].ToString(),
                        Time_of_appointment = dr["Time_of_appointment"].ToString(),
                        DoctorId = Convert.ToInt32(dr["DoctorId"])

                    });
                }
                return slotBooking;
            }
            finally
            {
                conn.Close();
            }
        }

          /// <summary>
          /// updating doctor password
          /// </summary>
          /// <param name="changepassword"></param>
          /// <returns></returns>
        public bool Doctorpasswordchange(Changepassword changepassword)
        {
            try
            {
                connection();
                int i = 0;
                SqlCommand command = new SqlCommand("SPU_Changepassworddoctor", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", changepassword.Username);
                command.Parameters.AddWithValue("@Password", Encrypt(changepassword.Password));
                command.Parameters.AddWithValue("@New_Password", Encrypt(changepassword.New_password));
                conn.Open();
                i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
        
          
        }

        /// <summary>
        /// updating patient password
        /// </summary>
        /// <param name="changepassword"></param>
        /// <returns></returns>
        public bool Patientpasswordchange(Changepassword changepassword)
        {
            try
            {
                connection();
                int i = 0;
                SqlCommand command = new SqlCommand("SPU_Changepasswordpatient", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", changepassword.Username);
                command.Parameters.AddWithValue("@Password", Encrypt(changepassword.Password));
                command.Parameters.AddWithValue("@New_Password", Encrypt(changepassword.New_password));
                conn.Open();
                i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
            
        }


        /// <summary>
        /// updating admin password
        /// </summary>
        /// <param name="changepassword"></param>
        /// <returns></returns>
        public bool Adminpasswordchange(Changepassword changepassword)
        {
            try
            {
                connection();
                int i = 0;
                SqlCommand command = new SqlCommand("SPU_Changepasswordadmin", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", changepassword.Username);
                command.Parameters.AddWithValue("@Password", Encrypt(changepassword.Password));
                command.Parameters.AddWithValue("@New_Password", Encrypt(changepassword.New_password));
                conn.Open();
                i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

            /// <summary>
            /// logic that defines how to send message to  admin session
            /// </summary>
            /// <param name="contact"></param>
            /// <returns></returns>
        public bool AddConcern(Contact contact)
        {
            try
            {
                connection();
                int i = 0;
                SqlCommand command = new SqlCommand("SPI_Table_Addconcern", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", contact.Name);
                command.Parameters.AddWithValue("@Email", contact.Email);
                command.Parameters.AddWithValue("@Mobile", contact.Mobile);
                command.Parameters.AddWithValue("@Message", contact.Message);
                conn.Open();
                 i = command.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// to get the list of messages send to the admin
        /// </summary>
        /// <returns></returns>
             public List<Contact> ViewMessageList()
        {
            List<Contact> contact = new List<Contact>();
            connection();
            SqlCommand command = new SqlCommand("SPS_Table_Addconcern", conn);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            conn.Open();
            dataAdapter.Fill(dataTable);
            conn.Close();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                contact.Add(new Contact
                {
                    Id = Convert.ToInt32(dataRow["Id"]),  //the data that are  retreived from the database are placed in the key that we defined in the model
                    Name = dataRow["Name"].ToString(),
                    Email = dataRow["Email"].ToString(),
                    Mobile = dataRow["Mobile"].ToString(),
                    Message = dataRow["Message"].ToString(),
                });

            }
            return contact;
        }


       /// <summary>
       /// get  messages that is send  to the admin based on id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public List<Contact> deletemessage(int id)
        {
            List<Contact> Contact = new List<Contact>();
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPS_messagedetails", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                conn.Open();
                adapter.Fill(dataTable);
                foreach (DataRow dr in dataTable.Rows)
                {
                    Contact.Add(new Contact

                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Email = dr["Email"].ToString(),
                        Mobile = dr["Mobile"].ToString(),
                        Message = dr["Message"].ToString(),

                        


                    });
                }
                return Contact;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// delete the message send to admin based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
    public string DeleteMessageDetails(int id)
        {
            try
            {
                connection();
                SqlCommand command = new SqlCommand("SPD_Table_Addconcern", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.Add("@outputmessage", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                conn.Open();
                command.ExecuteNonQuery();
                string result = command.Parameters["@outputmessage"].Value.ToString();
                return result;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}



                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               