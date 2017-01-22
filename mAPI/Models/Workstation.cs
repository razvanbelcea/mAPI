using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mAPI.Models
{
    public class Workstation
    {
          
        public string szWorkstationID { get; set; }
        public string szWorkstationGroupID { get; set; }
        public int lOperatorID { get; set; }
        public int lWorkstationNmbr { get; set; }
        public string IP { get; set; }
        public string status { get; set; }
    }
}