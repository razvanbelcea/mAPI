
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
    public class ServersController : ApiController
    {
        [Route("api/servers/{env}")]
        [HttpGet]
        public List<Servers> Get(string env)
        {
            var listofservers = new List<Servers>();
            //var myConnectionString = "Data Source=10.21.22.136;Database=mposx_usi;User ID=DevReadOnly;Password=Pa$$w0rd;";
            //var CommandText = "";
            //switch (env.ToUpper())
            //{
            //    case "QA":
            //        CommandText = "select a.MachineNameAlias,c.MyRegionCode,m.IPAddress,s.externalstoreid from dbo.MachineNameAliases a join Machines m ON a.MAC=m.MAC join Stores s ON m.MyStoreID=s.ID join Cities c on c.CityCode=s.MyCityCode where (a.MachineNameAlias like '%MPSQ%')";
            //        break;
            //    case "UAT":
            //        CommandText = "select a.MachineNameAlias,c.MyRegionCode,m.IPAddress,s.externalstoreid from dbo.MachineNameAliases a join Machines m ON a.MAC=m.MAC join Stores s ON m.MyStoreID=s.ID join Cities c on c.CityCode=s.MyCityCode where (a.MachineNameAlias like '%MPSU%')";
            //        break;
            //    case "DEV":
            //        CommandText = "select a.MachineNameAlias,c.MyRegionCode,m.IPAddress,s.externalstoreid from dbo.MachineNameAliases a join Machines m ON a.MAC=m.MAC join Stores s ON m.MyStoreID=s.ID join Cities c on c.CityCode=s.MyCityCode where (a.MachineNameAlias like '%MPSD%')";
            //        break;
            //}

            //for testing only
            var myConnectionString = @"Data Source=IAM\SQLEXPRESS;Database=Test;Integrated Security=true;";
            var CommandText = "select name, workstationnmbr, type, operator from workstation";

            using (SqlConnection con = new SqlConnection(myConnectionString))
            {
                using (SqlCommand command = new SqlCommand(CommandText, con))
                {
                    Servers server = new Servers();
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            server = new Servers();
                            server.IPAddress = reader.GetValue(2).ToString();
                            server.MachineNameAlias = reader.GetValue(0).ToString();
                            server.MyRegionCode = reader.GetValue(1).ToString();
                            server.StoreID = Convert.ToInt16(reader.GetValue(3).ToString());
                            server.Online = false;

                            //Ping ping = new Ping();
                            //try
                            //{
                            //    PingReply pingReply = ping.Send(reader.GetValue(2).ToString());
                            //    if (pingReply.Status == IPStatus.Success)
                            //    {
                            //        server.Online = true;
                            //    }
                            //}

                            //catch (Exception ex)
                            //{
                            //    Console.WriteLine(ex);
                            //}
                            listofservers.Add(server);

                        }
                    }
                }

            }
            return listofservers;
        }
    }
}
