using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emp_task.Models;
using System.Reflection;
using System.Drawing;
using System.Web.DynamicData;
using System.Configuration;

namespace emp_task.Controllers
{
    public class Employee_task_tableController : Controller
    {
        string Str = ConfigurationManager.AppSettings["DB_CONNECTION_STRING"];

        // GET: Employee_task_table
        public ActionResult Index(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    string q = "SELECT * FROM Employee_task_table where Profile_id=" + id;
                    using (SqlDataAdapter da = new SqlDataAdapter(q, con))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or display an error message
                ViewBag.Error = "Error: " + ex.Message;
            }

            if (dt.Rows.Count == 0)
            {
                ViewBag.Message = "No records found.";
            }
            // Pass the Profile ID to the ViewBag
            TempData["ProfileId"] = id;
            TempData.Keep("ProfileId");
            return View(dt);
        }


        // GET: Employee_task_table/Details/5

        // GET: Employee_task_table/Create
        public ActionResult Create()
        {
            int id = (int)TempData["ProfileId"];
            ViewBag.Profile_id = id;
            return View();

        }

        // POST: Employee_task_table/Create
        [HttpPost]
        public ActionResult Create(Employee_task_table employee_task_table)
        {
            //    try
            //    {
            //        // TODO: Add insert logic here
            //        using (SqlConnection con = new SqlConnection(Str))
            //        {
            //            con.Open();
            //            string formattedStartTime = employee_task_table.Start_time.ToString("yyyy-MM-dd HH:mm:ss");
            //            if (TempData["ProfileId"] != null)
            //            {
            //                employee_task_table.Profile_id = (int)TempData["ProfileId"];
            //            }

            //            string q = "insert into Employee_task_table values" +

            //                "(" + employee_task_table.Profile_id + "," +
            //                "'" + employee_task_table.Task_Name + "'," +
            //                "'" + employee_task_table.Task_Descr + "'," +
            //                 "'" + formattedStartTime + "'," +
            //                "" + employee_task_table.Status + "" + ")";



            //            SqlCommand cmd = new SqlCommand(q, con);
            //            cmd.ExecuteNonQuery();
            //        }
            //        return RedirectToAction("Index", new { id = employee_task_table.Profile_id });
            //    }

            //    // If we got this far, something failed; redisplay form
            //    catch
            //    {
            //        return View();
            //    }


            //    ViewBag.Profile_id = employee_task_table.Profile_id;
            //    return View(employee_task_table);
            //}
            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AddTask", con))
                    {
                        string formattedStartTime = employee_task_table.Start_time.ToString("yyyy-MM-dd HH:mm:ss");
                        if (TempData["ProfileId"] != null)
                        {
                            employee_task_table.Profile_id = (int)TempData["ProfileId"];
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ProfileId", employee_task_table.Profile_id);
                        cmd.Parameters.AddWithValue("@TaskName", employee_task_table.Task_Name);
                        cmd.Parameters.AddWithValue("@TaskDescription", employee_task_table.Task_Descr);
                        cmd.Parameters.AddWithValue("@StartTime", formattedStartTime);
                        cmd.Parameters.AddWithValue("@Status", employee_task_table.Status);

                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index", new { id = employee_task_table.Profile_id });
            }
            catch
            {
                return View();
            }
            ViewBag.Profile_id = employee_task_table.Profile_id;
                return View(employee_task_table);
            }

            // GET: Employee_task_table/Edit/5
            public ActionResult Edit(int id)
        {

            Employee_task_table employee_task_table = new Employee_task_table();
            DataTable dataTable = new DataTable();
            //    try
            //    {
            //        // TODO: Add insert logic here
            //        using (SqlConnection con = new SqlConnection(Str))
            //        {
            //            con.Open();

            //            string q = "SELECT * FROM Employee_task_table where id=" + id;


            //            using (SqlDataAdapter da = new SqlDataAdapter(q, con))
            //            {
            //                da.Fill(dataTable);
            //            }
            //            if (dataTable.Rows.Count == 1)
            //            {
            //                string formattedStartTime = employee_task_table.Start_time.ToString("dd/mm/yyyy HH:mm:ss");

            //                employee_task_table.Id = (int)dataTable.Rows[0][0];
            //                employee_task_table.Profile_id = (int)dataTable.Rows[0][1];


            //                employee_task_table.Task_Name = dataTable.Rows[0][2].ToString();
            //                employee_task_table.Task_Descr = dataTable.Rows[0][3].ToString();
            //                employee_task_table.Start_time = Convert.ToDateTime(dataTable.Rows[0][4]);
            //                employee_task_table.Status = (int)dataTable.Rows[0][5];

            //                return View(employee_task_table);

            //            }
            //            // SqlCommand cmd = new SqlCommand(q, con);
            //            //  cmd.ExecuteNonQuery();
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
                    using (SqlCommand cmd = new SqlCommand("GetTasksByProfileID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("Id", id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dataTable);
                        }
                        if (dataTable.Rows.Count == 1)
                        {
                            string formattedStartTime = employee_task_table.Start_time.ToString("dd/mm/yyyy HH:mm:ss");

                            employee_task_table.Id = (int)dataTable.Rows[0][0];
                            employee_task_table.Profile_id = (int)dataTable.Rows[0][1];


                            employee_task_table.Task_Name = dataTable.Rows[0][2].ToString();
                            employee_task_table.Task_Descr = dataTable.Rows[0][3].ToString();
                            employee_task_table.Start_time = Convert.ToDateTime(dataTable.Rows[0][4]);
                            employee_task_table.Status = (int)dataTable.Rows[0][5];

                            return View(employee_task_table);

                        }
                      
                               return RedirectToAction("Index");
                         
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error: " + ex.Message;
            }

            if (dataTable.Rows.Count == 0)
            {
                ViewBag.Message = "No records found.";
            }
            //TempData["ProfileId"] = id;
            //TempData.Keep("ProfileId");
            return View(dataTable);
        }


        // POST: Employee_task_table/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee_task_table employee_task_table)
        {
            //    try
            //    {
            //        // TODO: Add update logic here
            //        using (SqlConnection con = new SqlConnection(Str))
            //        {
            //            con.Open();
            //            string q = "UPDATE [dbo].[Employee_task_table] SET " +
            //                "[Task_Name] =@Task_Name,[Task_Descr]  = @Task_Descr " +
            //                ",[Start_time] =@Start_time  ,[Status] =@Status " +
            //                ",[Profile_id] =@Profile_id " +
            //                "  WHERE [Id]=@id";


            //            SqlCommand cmd = new SqlCommand(q, con);
            //            cmd.Parameters.AddWithValue("@id", employee_task_table.Id);
            //            cmd.Parameters.AddWithValue("@Profile_id", employee_task_table.Profile_id);


            //            cmd.Parameters.AddWithValue("@Task_Name", employee_task_table.Task_Name);
            //            cmd.Parameters.AddWithValue("@Task_Descr", employee_task_table.Task_Descr);
            //            cmd.Parameters.AddWithValue("@Start_time", employee_task_table.Start_time);
            //            cmd.Parameters.AddWithValue("@Status", employee_task_table.Status);




            //            cmd.ExecuteNonQuery();
            //        }
            //        return RedirectToAction("Index", new { id = employee_task_table.Profile_id });
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
                    using (SqlCommand cmd = new SqlCommand("UpdateTask", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TaskID", employee_task_table.Id);
                        cmd.Parameters.AddWithValue("@ProfileId", employee_task_table.Profile_id);
                        cmd.Parameters.AddWithValue("@TaskName", employee_task_table.Task_Name);
                        cmd.Parameters.AddWithValue("@TaskDescription", employee_task_table.Task_Descr);
                        cmd.Parameters.AddWithValue("@StartTime", employee_task_table.Start_time);
                        cmd.Parameters.AddWithValue("@Status", employee_task_table.Status);

                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction("Index", new { id = employee_task_table.Profile_id });
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee_task_table/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Employee_task_table/Delete/5

        public ActionResult Delete(int id)
        {
            
            Employee_task_table employee_task_table = new Employee_task_table();
            DataTable dataTable = new DataTable();
            //        try
            //        {
            //            // TODO: Add insert logic here
            //            using (SqlConnection con = new SqlConnection(Str))
            //            {
            //                con.Open();

            //                string q = "SELECT * FROM Employee_task_table where id=" + id;


            //                using (SqlDataAdapter da = new SqlDataAdapter(q, con))
            //                {
            //                    da.Fill(dataTable);
            //                }
            //                if (dataTable.Rows.Count == 1)
            //                {
            //                                      employee_task_table.Profile_id = (int)dataTable.Rows[0][1];
            //                }
            //                using (SqlConnection con2 = new SqlConnection(Str))
            //                {
            //                    con2.Open();
            //                    string q2 = "Delete from Employee_task_table where Id=@id";
            //                    SqlCommand cmd = new SqlCommand(q2, con);
            //                    cmd.Parameters.AddWithValue("@id", id);
            //                    cmd.ExecuteNonQuery();


            //                }
            //                return RedirectToAction("Index", new { id = employee_task_table.Profile_id });


            //            }
            //        }
            //        catch
            //        {
            //            return View();
            //        }
            //    }
            //}

            try
            {
                using (SqlConnection con = new SqlConnection(Str))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("GetTasksByProfileID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", id);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            da.Fill(dataTable);
                        }                 }
                    if (dataTable.Rows.Count == 1)
                    {
                        employee_task_table.Profile_id = (int)dataTable.Rows[0][1];
                    }

                   
                    //    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    //    {
                    //        da.Fill(dataTable);
                    //    }
                    //}

                    //if (dataTable.Rows.Count == 1)
                    //{
                    //    employee_task_table.Profile_id = (int)dataTable.Rows[0]["Profile_id"];
                    //}

                    using (SqlCommand cmd = new SqlCommand("DeleteTask", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TaskID", id);

                        cmd.ExecuteNonQuery();
                    }

                    return RedirectToAction("Index", new { id = employee_task_table.Profile_id });
                }
            }
            catch
            {
                return View();
            }
        }
    }

}
