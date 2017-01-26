using mAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace mAPI.Controllers
{
    public class ServerInfoController : ApiController
    {
        [Route("api/serverinfo/{serverhostname}/{tillhostname}")]
        [HttpGet]
        public List<ServerInfo> Get(string serverhostname, string tillhostname)
        {
            var listofserverinfo = new List<ServerInfo>();
            ServerInfo newServer = new ServerInfo();
            try
            {
                var MposInstallState = @"\\" + serverhostname + @".mpos.madm.net\c$\mgi\MposInstallState.xml";
                var loadMposInstallState = XDocument.Load(MposInstallState);
                string serverHF = "";
                string serverVersion = "";
                var childType = from t in loadMposInstallState.Descendants("CurrentVersion")
                                select new
                                {
                                    hotfix = t.Attribute("hotfix").Value,
                                    version = t.Attribute("version").Value
                                };
                foreach (var t in childType)
                {
                    serverHF += t.hotfix;
                    serverVersion += t.version;
                }
                newServer.msysversion = serverVersion;
                newServer.serverhf = serverHF;
            }
            catch { }


            try
            {
                var MposInstallState = @"\\" + tillhostname + @".mpos.madm.net\c$\mgi\MposInstallState.xml";
                var loadMposInstallState = XDocument.Load(MposInstallState);
                string tillHF = "";
                var childType = from t in loadMposInstallState.Descendants("CurrentVersion")
                                select new
                                {
                                    hotfix = t.Attribute("hotfix").Value
                                };
                foreach (var t in childType)
                {
                    tillHF += t.hotfix;
                }
                newServer.tillhf = tillHF;             
            }
            catch { }
            try
            {
                var myConnectionString = @"Data Source=" + serverhostname + ".mpos.madm.net" + ";Database=TPCentralDB;Integrated Security=true;";
                var CommandText = "select top 1 szVersion from EUSoftwareVersion where szPackageName = 'Metro_Common_TPDotnetSetupPos' order by szTimestamp desc";

                using (SqlConnection con = new SqlConnection(myConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(CommandText, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                newServer.wnversion = reader.GetValue(0).ToString();
                            }
                        }
                    }
                }
            }
            catch { }

            listofserverinfo.Add(newServer);

            return listofserverinfo;
        }

    }
}
