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
    public class WorkstationController : ApiController
    {
        [Route("api/tills/{ip}")]
        [HttpGet]
        public Workstation Get(string ip)
        {
            //return listEmp.First(e => e.ID == id);  
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Data Source=" + ip + ";Database=TPCentralDB;Integrated Security=SSPI;";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select szWorkstationID, lWorkstationNmbr, szWorkstationGroupID, lOperatorID from workstation left join operator on lLoggedOnWorkstationNmbr = lWorkstationNmbr where bisthickpos<> 0";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Workstation till = null;
            while (reader.Read())
            {
                till = new Workstation();
                till.szIP = reader.GetValue(0).ToString();
                till.szWorkstationGroupID = reader.GetValue(1).ToString();
                till.szWorkstationID = reader.GetValue(2).ToString();
                till.szWorkstationNmbr = reader.GetValue(2).ToString();
            }
            myConnection.Close();
            return till;
        }
    }
}
