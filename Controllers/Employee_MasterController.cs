using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emp_task.Models;
using System.Data;//added
using System.Data.SqlClient;
using System.Configuration;//added
namespace emp_task.Controllers//added
{
    public class Employee_MasterController : Controller
    {

        string Str = ConfigurationManager.AppSettings["DB_CONNECTION_STRING"]; 
            public ActionResult Index()
        {
            DataTable dt = new DataTable();
            //try
            //{
            //    using (SqlConnection con = new SqlConnection(Str))
            //    {
            //        con.Open();
            //        string q = "SELECT * FROM Employee_master";
            //        using (SqlDataAdapter da = new SqlDataAdapter(q, con))
            //        {
            //            da.Fill(dt);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception or display an error message
            //    ViewBag.Error = "Error: " + ex.Message;
            //}
            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("GetAllProfiles", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }

                if (dt.Rows.Count == 0)
                {
                    ViewBag.Message = "No records found.";
                }

                return View(dt);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }

            if (dt.Rows.Count == 0)
            {
                ViewBag.Message = "No records found.";
            }

            return View(dt);

        }
        // GET: Employee_Master/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee_Master/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee_Master/Create
        [HttpPost]
        public ActionResult Create(Employee_master employee_Master)
        {
            //    try
            //    {
            //        // TODO: Add insert logic here
            //        using (SqlConnection con = new SqlConnection(Str))
            //        {
            //            string formattedStartTime = employee_Master.Date_of_birth.ToString("yyyy-MM-dd HH:mm:ss");

            //            con.Open();
            //            string q = "insert into Employee_master values" +
            //                "('" + employee_Master.First_Name + "'," +
            //                "'" + employee_Master.Last_Name + "'," +
            //                 "'" + formattedStartTime + "'," +
            //                "'" + employee_Master.Phone_number + "'," +
            //                  "'" + employee_Master.Email_id + "')";



            //            SqlCommand cmd = new SqlCommand(q, con);
            //            cmd.ExecuteNonQuery();
            //        }
            //        return RedirectToAction("Index");
            //    }
            //    catch
            //    {
            //        return View();
            //    }


            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AddProfile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", employee_Master.First_Name);
                        cmd.Parameters.AddWithValue("@LastName", employee_Master.Last_Name);
                        cmd.Parameters.AddWithValue("@DateOfBirth", employee_Master.Date_of_birth);
                        cmd.Parameters.AddWithValue("@PhoneNumber", employee_Master.Phone_number);
                        cmd.Parameters.AddWithValue("@EmailId", employee_Master.Email_id);

                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    

    // GET: Employee_Master/Edit/5
    public ActionResult Edit(int id)
        {
            Employee_master employee_Master = new Employee_master();
            DataTable dataTable = new DataTable();
        //        try
        //        {
        //            // TODO: Add insert logic here
        //            using (SqlConnection con = new SqlConnection(Str))
        //            {
        //                con.Open();

        //                string q = "SELECT * FROM Employee_master where Profile_id=" + id;


        //                using (SqlDataAdapter da = new SqlDataAdapter(q, con))
        //                {
        //                    da.Fill(dataTable);
        //                }
        //                if (dataTable.Rows.Count == 1)
        //                {
        //                    employee_Master.Profile_id =(int) dataTable.Rows[0]["Profile_id"];

        //                    employee_Master.First_Name = dataTable.Rows[0]["First_Name"].ToString();
        //                    employee_Master.Last_Name = dataTable.Rows[0][2].ToString();
        //                    employee_Master.Date_of_birth = Convert.ToDateTime(dataTable.Rows[0][3]);
        //                    employee_Master.Phone_number = dataTable.Rows[0][4].ToString();
        //                    employee_Master.Email_id = dataTable.Rows[0][1].ToString();
        //                    return View(employee_Master);

        //                }
        //                // SqlCommand cmd = new SqlCommand(q, con);
        //                //  cmd.ExecuteNonQuery();
        //            }
        //            return RedirectToAction("Index");
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //}
            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("GetProfileByProfileID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProfileId", id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dataTable);
                        }
}

if (dataTable.Rows.Count == 1)
{
    employee_Master.Profile_id = (int)dataTable.Rows[0]["Profile_id"];

    employee_Master.First_Name = dataTable.Rows[0]["First_Name"].ToString();
    employee_Master.Last_Name = dataTable.Rows[0][2].ToString();
    employee_Master.Date_of_birth = Convert.ToDateTime(dataTable.Rows[0][3]);
    employee_Master.Phone_number = dataTable.Rows[0][4].ToString();
    employee_Master.Email_id = dataTable.Rows[0][1].ToString();
    return View(employee_Master);

}
                }
                    return RedirectToAction("Index");
            }
            catch
            {
    return View();
}
        }

        // POST: Employee_Master / Edit / 5
        [HttpPost]
        public ActionResult Edit(int id, Employee_master employee_Master)
        {
        //    try
        //    {
        //        // TODO: Add update logic here
        //        using (SqlConnection con = new SqlConnection(Str))
        //        {
        //            con.Open();
        //            string q = "UPDATE [dbo].[Employee_master] SET " +
        //                "[First_Name] =@First_Name,[Last_Name] = @Last_Name " +
        //                ",[Date_of_birth] =@Date_of_birth  ,[Phone_number] =@Phone_number " +
        //                ",[Email_id] =@Email_id  WHERE [Profile_id]=@id";


        //            SqlCommand cmd = new SqlCommand(q, con);
        //            cmd.Parameters.AddWithValue("@First_Name", employee_Master.First_Name);
        //            cmd.Parameters.AddWithValue("@Last_Name ", employee_Master.Last_Name);
        //            cmd.Parameters.AddWithValue("@Date_of_birth", employee_Master.Date_of_birth);
        //            cmd.Parameters.AddWithValue("@Phone_number", employee_Master.Phone_number);
        //            cmd.Parameters.AddWithValue("@Email_id", employee_Master.Email_id);
        //            cmd.Parameters.AddWithValue("@id", employee_Master.Profile_id);


        //            cmd.ExecuteNonQuery();
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("UpdateProfile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProfileId", employee_Master.Profile_id);
                        cmd.Parameters.AddWithValue("@FirstName", employee_Master.First_Name);
                        cmd.Parameters.AddWithValue("@LastName", employee_Master.Last_Name);
                        cmd.Parameters.AddWithValue("@DateOfBirth", employee_Master.Date_of_birth);
                        cmd.Parameters.AddWithValue("@PhoneNumber", employee_Master.Phone_number);
                        cmd.Parameters.AddWithValue("@EmailId", employee_Master.Email_id);

                        cmd.ExecuteNonQuery();
                    }
}
return RedirectToAction("Index");
            }
            catch
            {
    return View();
}
        }

         //GET: Employee_Master / Delete / 5
        public ActionResult Delete(int id)
{
            //            using (SqlConnection con = new SqlConnection(Str))
            //            { con.Open();
            //                string q = "Delete from Employee_master where Profile_id=@id";
            //                SqlCommand cmd = new SqlCommand(q, con);  
            //                cmd.Parameters.AddWithValue( "@id",id);
            //                cmd.ExecuteNonQuery();


            //            }
            //            return RedirectToAction("Index");
            //        }
            //    }

            //}

            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("DeleteProfile", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProfileId", id);

                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}