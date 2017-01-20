using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mAPI.Models
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public int StoreNo { get; set; }
        public int CH { get; set; }
        public int CV { get; set; }
        public int DistributionLineCode { get; set; }
        public int NumbericCountryCode { get; set; }

    }
}