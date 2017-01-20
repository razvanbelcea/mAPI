using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mAPI.Models
{
    public class Servers
    {
        public string MachineNameAlias { get; set; }
        public string MyRegionCode { get; set; }
        public string IPAddress { get; set; }
        public bool Online { get; set; }
        public int StoreID { get; set; }
        public string VersionID { get; set; }
        public string hfNo { get; set; }
    }
}