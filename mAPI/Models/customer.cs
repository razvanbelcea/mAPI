using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mAPI.Models
{
    public class customer
    {
            public int szCustomerID { get; set; }
            public string Name { get; set; }
            public int ManagerId { get; set; }
    }
}