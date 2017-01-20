using mAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//Type of items:
//  1 - Active
//  2 - Inactive
//  3 - Deposit
//  4 - Weight
//  5 - PriceEntry
//  6 - Tobacco
//  7 - Art + linked deposit

namespace mAPI.Controllers
{
    public class ItemController : ApiController
    {
        [Route("api/item/{ip}/{ctype}")]
        [HttpGet]
        public List<Item> Get(string ip, int ctype)
        {
            var myConnectionString = @"Data Source=" + ip + ".mpos.madm.net" + ";Database=TPCentralDB;Integrated Security=true;";
            var CommandText = "";

            switch (ctype)
            {
                case 1:
                    CommandText = @"select top 30 a.szItemID,b.szitemlookupcode from POSIdentity as a inner join itemlookupcode as b on a.szPOSItemID=b.szPOSItemID where len(b.szitemlookupcode)=13 and a.blocked=0 order by NEWID()";
                    break;
                case 2:
                    CommandText = @"select top 30 a.szItemID,b.szitemlookupcode from POSIdentity as a inner join itemlookupcode as b on a.szPOSItemID=b.szPOSItemID where len(b.szitemlookupcode)=13 and a.blocked=-1 order by NEWID()";
                    break;
                case 3:
                    CommandText = @"select top 30 POSIdentity.szItemID, '-' from POSIdentity where POSIdentity.blocked=0 and POSIdentity.bIsDepositItem=-1 order by NEWID()";
                    break;
                case 4:
                    CommandText = @"select top 30 a.szItemID,b.szitemlookupcode from POSIdentity as a inner join itemlookupcode as b on a.szPOSItemID=b.szPOSItemID where len(b.szitemlookupcode)=13 and a.blocked=0 and a.bMeasurementEntryRequired=-1 order by NEWID()";
                    break;
                case 5:
                    CommandText = @"select top 30 a.szItemID,b.szitemlookupcode from POSIdentity as a inner join itemlookupcode as b on a.szPOSItemID=b.szPOSItemID where len(b.szitemlookupcode)=13 and a.blocked=0 and a.bPriceEntryRequiredFlag=-1 order by NEWID()";
                    break;
                case 6:
                    CommandText = @"select top 30 a.szItemID,b.szitemlookupcode from POSIdentity as a inner join itemlookupcode as b on a.szPOSItemID=b.szPOSItemID where len(b.szitemlookupcode)=13 and a.blocked=0 and szItemId in (select a.szItemId from Item as a inner join POSIdentity as b on a.szItemID=b.szItemId where lMerchandiseStructureID = 875 or b.bVatIncluded not like 0 or b.bIsTobaccoItem not like 0) order by NEWID()";
                    break;
                case 7:
                    CommandText = @"select top 30 a.szItemID,b.szitemlookupcode from POSIdentity as a inner join itemlookupcode as b on a.szPOSItemID=b.szPOSItemID where len(b.szitemlookupcode)=13 and a.blocked=0 and a.szArtLink is not null order by NEWID()";
                    break;

            }

            var listofitems = new List<Item>();

            using (SqlConnection con = new SqlConnection(myConnectionString))
            {
                using (SqlCommand command = new SqlCommand(CommandText, con))
                {
                    Item newitem = new Item();
                    con.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                newitem.ItemID = reader.GetValue(0).ToString();
                                newitem.itemLookUpCode = reader.GetValue(1).ToString();
                                listofitems.Add(newitem);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                    }
                }
            }
            return listofitems;
        }
    }
}
