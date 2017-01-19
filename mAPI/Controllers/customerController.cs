using mAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace mAPI.Controllers
{
    public class customerController : ApiController
    {
        [Route("api/getworkstation/{ip}")]
        [HttpGet]
        public customer Get(string ip)
        {
            //return listEmp.First(e => e.ID == id);  
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source="+ip+";Database=TPCentralDB;Integrated Security=SSPI;";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from tblEmployee where EmployeeId=" + ip + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            customer emp = null;
            while (reader.Read())
            {
                emp = new customer();
                emp.szCustomerID = Convert.ToInt32(reader.GetValue(0));
                emp.Name = reader.GetValue(1).ToString();
                emp.ManagerId = Convert.ToInt32(reader.GetValue(2));
            }
            myConnection.Close();
            return emp;
        }
    }
}
