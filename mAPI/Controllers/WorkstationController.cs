using mAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;

namespace mAPI.Controllers
{
    public class WorkstationController : ApiController
    {
        [Route("api/tills/{ip}")]
        [HttpGet]
        public List<Workstation> Get(string ip)
        {
            //return listEmp.First(e => e.ID == id);  
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = "Data Source=" + ip + ".mpos.madm.net" + ";Database=TPCentralDB;Integrated Security=true;";

            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select szWorkstationID, lWorkstationNmbr, szWorkstationGroupID, lOperatorID from workstation left join operator on lLoggedOnWorkstationNmbr = lWorkstationNmbr where bisthickpos<> 0";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Workstation till = null;
            var listofwks = new List<Workstation>();
            while (reader.Read())
            {
                till = new Workstation();
                till.szWorkstationGroupID = reader.GetValue(2).ToString();
                till.szWorkstationID = reader.GetValue(0).ToString();
                till.lOperatorID = reader.GetValue(3).Equals(DBNull.Value)
                                    ? 0
                                     : Convert.ToInt16(reader.GetValue(3)); ;
                till.lWorkstationNmbr = Convert.ToInt16(reader.GetValue(1));
                till.IP = "VirtualClient";
                till.online = false;

                Ping ping = new Ping();
                var hostname = reader.GetValue(0).ToString() + ".mpos.madm.net";
                try
                {
                    PingReply pingReply = ping.Send(hostname);
                    if (pingReply.Status == IPStatus.Success)
                    {
                        IPAddress[] IpInHostAddress = Dns.GetHostAddresses(reader.GetValue(0).ToString() + ".mpos.madm.net");
                        till.IP = IpInHostAddress[0].ToString();
                        till.online = true;
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                listofwks.Add(till);

            }
            myConnection.Close();
            return listofwks;
        }
    }
}
