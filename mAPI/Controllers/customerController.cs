using mAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//Type of customers:
//  1 - Active
//  2 - Employee
//  3 - Reducet VAT
//  4 - Restriction
//  5 - Exempted VAT
//  6 - Metro Credit
//  7 - Internal Purchase
//  8 - CONI

namespace mAPI.Controllers
{
    public class CustomerController : ApiController
    {
        [Route("api/customer/{ip}/{ctype}")]
        [HttpGet]
        public List<Customer> Get(string ip, int ctype)
        {
            var listofcustomer = new List<Customer>();
            var myConnectionString = @"Data Source=" + ip + ".mpos.madm.net" + ";Database=TPCentralDB;Integrated Security=true;";
            var CommandText = "";

            switch (ctype)
            {
                case 1:
                    CommandText = @"IF EXISTS (select * from MGICustomerCardholderCard)
			select top 30 szCustomerID,szExternalStoreID,szCardholderID,lCardVersionID,szDistributionLineCode,szNumericCountryCode,'0' from mgicustomercardholdercard as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szcustomerid in (select szcustomerid from mgicustomerkeycustomer where szRestrictionTypeCode is null and bEmployeeFlag=0 and sztaxind not in (5,6)) and a.szRestrictionTypeCode is null order by NEWID()
			ELSE
			select top 30 szCustomerID,szExternalStoreID,'1','1',szDistributionLineCode,szNumericCountryCode,'0' from MGICustomerKeyCustomer as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szRestrictionTypeCode is null and bEmployeeFlag=0 and sztaxind not in (5,6) order by NEWID()";
                    break;
                case 2:
                    CommandText = @"IF EXISTS (select * from MGICustomerCardholderCard)
			select top 30 szCustomerID,szExternalStoreID,szCardholderID,lCardVersionID,szDistributionLineCode,szNumericCountryCode,'0' from mgicustomercardholdercard as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szcustomerid in (select szcustomerid from mgicustomerkeycustomer where szRestrictionTypeCode is null and bEmployeeFlag=-1 and sztaxind not in (5,6)) and a.szRestrictionTypeCode is null order by NEWID()
			ELSE
			select top 30 szCustomerID,szExternalStoreID,'1','1',szDistributionLineCode,szNumericCountryCode,'0' from MGICustomerKeyCustomer as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szRestrictionTypeCode is null and bEmployeeFlag=-1 and sztaxind not in (5,6) order by NEWID()";
                    break;
                case 3:
                    CommandText = @"IF EXISTS (select * from MGICustomerCardholderCard)
			select top 30 szCustomerID,szExternalStoreID,szCardholderID,lCardVersionID,szDistributionLineCode,szNumericCountryCode,'0' from mgicustomercardholdercard as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szcustomerid in (select szcustomerid from mgicustomerkeycustomer where szRestrictionTypeCode is null and bEmployeeFlag=0 and sztaxind=5) and a.szRestrictionTypeCode is null order by NEWID()
			ELSE
			select top 30 szCustomerID,szExternalStoreID,'1','1',szDistributionLineCode,szNumericCountryCode,'0' from MGICustomerKeyCustomer as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szRestrictionTypeCode is null and bEmployeeFlag=0 and sztaxind=5 order by NEWID()";
                    break;
                case 4:
                    CommandText = @"IF EXISTS (select * from MGICustomerCardholderCard)
			select top 30 szCustomerID,szExternalStoreID,szCardholderID,lCardVersionID,szDistributionLineCode,szNumericCountryCode,'0' from mgicustomercardholdercard as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szcustomerid in (select szcustomerid from mgicustomerkeycustomer where szRestrictionTypeCode is not null and bEmployeeFlag=0 and sztaxind not in (5,6)) and a.szRestrictionTypeCode is not null order by NEWID()
			ELSE
			select top 30 szCustomerID,szExternalStoreID,'1','1',szDistributionLineCode,szNumericCountryCode,'0' from MGICustomerKeyCustomer as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szRestrictionTypeCode is not null and bEmployeeFlag=0 and sztaxind not in (5,6) order by NEWID()";
                    break;
                case 5:
                    CommandText = @"IF EXISTS (select * from MGICustomerCardholderCard)
			select top 30 szCustomerID,szExternalStoreID,szCardholderID,lCardVersionID,szDistributionLineCode,szNumericCountryCode,'0' from mgicustomercardholdercard as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szcustomerid in (select szcustomerid from mgicustomerkeycustomer where szRestrictionTypeCode is null and bEmployeeFlag=0 and szcustomerid in (select szcustomerid from MGiCustomerTaxExemption)) and a.szRestrictionTypeCode is null order by NEWID()
			ELSE
			select top 30 szCustomerID,szExternalStoreID,'1','1',szDistributionLineCode,szNumericCountryCode,'0' from MGICustomerKeyCustomer as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szRestrictionTypeCode is null and bEmployeeFlag=0 and szcustomerid in (select szcustomerid from MGiCustomerTaxExemption) order by NEWID()";
                    break;
                case 6:
                    CommandText = @"IF EXISTS (select * from MGICustomerCardholderCard)
			select top 30 szCustomerID,szExternalStoreID,szCardholderID,lCardVersionID,szDistributionLineCode,szNumericCountryCode,'0' from mgicustomercardholdercard as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szCustomerID in (select szCustomerID COLLATE SQL_Latin1_General_CP1_CI_AS from [MGIDB].[dbo].[MGICustomerLimits] where dMaxAmount > 1 and bIsMoneyLaundryLimit = 0 and szMediaMember = 600) and a.szRestrictionTypeCode is null order by NEWID()
			ELSE
			select top 30 szCustomerID,szExternalStoreID,'1','1',szDistributionLineCode,szNumericCountryCode,'0' from MGICustomerKeyCustomer as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szCustomerID in (select szCustomerID COLLATE SQL_Latin1_General_CP1_CI_AS from [MGIDB].[dbo].[MGICustomerLimits] where dMaxAmount > 1 and bIsMoneyLaundryLimit = 0 and szMediaMember = 600) order by NEWID()";
                    break;
                case 7:
                    CommandText = @"IF EXISTS (select * from MGICustomerCardholderCard)
			select top 30 szCustomerID,szExternalStoreID,szCardholderID,lCardVersionID,szDistributionLineCode,szNumericCountryCode,'0' from mgicustomercardholdercard as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szCustomerID in (select szCustomerID COLLATE SQL_Latin1_General_CP1_CI_AS from [MGIDB].[dbo].[MGICustomerLimits] where dMaxAmount > 1 and bIsMoneyLaundryLimit = 0 and szMediaMember = 602) and a.szRestrictionTypeCode is null order by NEWID()
			ELSE
			select top 30 szCustomerID,szExternalStoreID,'1','1',szDistributionLineCode,szNumericCountryCode,'0' from MGICustomerKeyCustomer as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szCustomerID in (select szCustomerID COLLATE SQL_Latin1_General_CP1_CI_AS from [MGIDB].[dbo].[MGICustomerLimits] where dMaxAmount > 1 and bIsMoneyLaundryLimit = 0 and szMediaMember = 602) order by NEWID()";
                    break;
                case 8:
                    CommandText = @"IF EXISTS (select * from MGICustomerCardholderCard)
			select top 30 szCustomerID,szExternalStoreID,szCardholderID,lCardVersionID,szDistributionLineCode,szNumericCountryCode,'0' from mgicustomercardholdercard as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szcustomerid in (select szcustomerid from mgicustomerkeycustomer where szRestrictionTypeCode is null and szInvoiceTypeCode='CONI' and sztaxind not in (5,6)) and a.szRestrictionTypeCode is null order by NEWID()
			ELSE
			select top 30 szCustomerID,szExternalStoreID,'1','1',szDistributionLineCode,szNumericCountryCode,'0' from MGICustomerKeyCustomer as a 
			inner join MGICountryCode as b on a.szCountryCode=b.szCountryCode 
			where szRestrictionTypeCode is null and szInvoiceTypeCode='CONI' and sztaxind not in (5,6) order by NEWID()";
                    break;
            }


            using (SqlConnection con = new SqlConnection(myConnectionString))
            {
                using (SqlCommand command = new SqlCommand(CommandText, con))
                {                   
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                Customer newcustomer = new Customer();
                                newcustomer.CustomerID = reader.GetValue(0).ToString();
                                newcustomer.StoreNo = Convert.ToInt16(reader.GetValue(1).ToString());
                                newcustomer.CH = Convert.ToInt16(reader.GetValue(2).ToString());
                                newcustomer.CV = Convert.ToInt16(reader.GetValue(3).ToString());
                                newcustomer.DistributionLineCode = Convert.ToInt16(reader.GetValue(4).ToString());
                                newcustomer.NumbericCountryCode = Convert.ToInt16(reader.GetValue(5).ToString());
                                listofcustomer.Add(newcustomer);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                    }
                }
            }
            return listofcustomer;
        }
    }
}
