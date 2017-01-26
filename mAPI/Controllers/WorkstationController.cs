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
            var listofwks = new List<Workstation>();
            var myConnectionString = "Data Source=" + ip + ".mpos.madm.net" + ";Database=TPCentralDB;Integrated Security=true;";
            var commandtext = "select szWorkstationID, lWorkstationNmbr, szWorkstationGroupID, lOperatorID from workstation left join operator on lLoggedOnWorkstationNmbr = lWorkstationNmbr where bisthickpos<> 0";

            //for testing only
            //var myConnectionString = @"Data Source=IAM\SQLEXPRESS;Database=Test;Integrated Security=true;";
            //var commandtext = "select name, workstationnmbr, type, operator from workstation";

            using (SqlConnection con = new SqlConnection(myConnectionString))
            {
                using (SqlCommand command = new SqlCommand(commandtext, con))
                {                    
                    Workstation till = new Workstation();
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
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
                            till.status = "offline";

                            Ping ping = new Ping();
                            var hostname = reader.GetValue(0).ToString() + ".mpos.madm.net";
                            try
                            {
                                PingReply pingReply = ping.Send(hostname);
                                if (pingReply.Status == IPStatus.Success)
                                {
                                    IPAddress[] IpInHostAddress = Dns.GetHostAddresses(reader.GetValue(0).ToString() + ".mpos.madm.net");
                                    till.IP = IpInHostAddress[0].ToString();
                                    till.status = "online";
                                }
                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                            listofwks.Add(till);
                        }
    
                    }
                }
            }
            return listofwks;
        }
    }
}
