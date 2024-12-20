using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DonorFlow
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
     /*   [WebMethod]
        public static string GetUserListChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT [User_Type], COUNT([Employee_ID]) AS User_List
                FROM Employee_TBL
                WHERE [User_Status] = 'Active'
                GROUP BY [User_Type]
                UNION ALL
                SELECT [User_Type], COUNT([User_ID])
                FROM Student_TBL
                WHERE [User_Status] = 'Active'
                GROUP BY [User_Type]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }*/

        [WebMethod]
        public static string GetCourseApprovalChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    SUM(CASE WHEN [Is Approved] = 1 THEN 1 ELSE 0 END) AS Approved,
                    SUM(CASE WHEN [Is Approved] = 0 THEN 1 ELSE 0 END) AS Not_Approved
                FROM 
                    course_reg_req_tbl";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }

      /*  [WebMethod]
        public static string GetLeaveApprovalChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT 
                    SUM(CASE WHEN [Is Approved] = 1 THEN 1 ELSE 0 END) AS Approved,
                    SUM(CASE WHEN [Is Approved] = 0 THEN 1 ELSE 0 END) AS Not_Approved
                FROM 
                    employee_attendance_tbl";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }

        [WebMethod]
        public static string GetFacultyWiseCourseListChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                        SELECT COUNT([Course Name]) AS CourseCount
                              ,[Faculty] AS Faculty
                          FROM course_amount_tbl
                          GROUP BY [Faculty]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }

        [WebMethod]
        public static string GetFacultyWiseBatchListChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                            SELECT COUNT([Batch No]) AS BatchNo,
                                   REPLACE([Faculty], 'Faculty of ', '') AS Faculty
                            FROM batch_tbl
                            GROUP BY REPLACE([Faculty], 'Faculty of ', '')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }

        [WebMethod]
        public static string GetGradeWiseChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                            SELECT COUNT([Grade]) AS Count_of_Grade
                                      ,[Grade] AS Grade
                                  FROM userdetail_in_batch_tbl
                                  GROUP BY [Grade]";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }

        [WebMethod]
        public static string GetPayemtChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
	                SELECT 
                    FORMAT([Date], 'MMMM') AS MonthName, 
                    SUM([Paid Amount]) AS TotalPaidAmount
                FROM 
                    student_payment_transactions
                GROUP BY 
                    FORMAT([Date], 'MMMM')
                ORDER BY 
                    FORMAT([Date], 'MMMM')";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }

        [WebMethod]
        public static string GetTotalStudentChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                        SELECT COUNT([User_ID]) AS TotalStudent
                          FROM Student_TBL";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }

        [WebMethod]
        public static string GetTotalEmployeeChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                            SELECT COUNT([Employee_ID]) AS TotalEmployee
                              FROM Employee_TBL";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }
        [WebMethod]
        public static string GetTotalRevenueChartData()
        {
            string connectionString = @"Data Source=DESKTOP-KUTNUTJ\SQLEXPRESS;Initial Catalog=macro_campus_db;Integrated Security=True;";
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                            SELECT 'LKR ' + CAST(SUM([Paid Amount]) AS VARCHAR) AS TotalRevenue
                            FROM student_payment_transactions";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dataTable);
                }
            }

            // Convert DataTable to JSON
            string jsonData = JsonConvert.SerializeObject(dataTable);
            return jsonData;
        }*/
    }
}