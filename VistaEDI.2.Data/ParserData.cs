using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VistaEDI._2.Model;

namespace VistaEDI._2.Data
{
    public class ParserData : DatabaseOperations
    {
        public ResultViewModel SaveItem(ChemistryInfo data, char status)
        {
            try
            {
                // StoredProcedureName = "ChemistryInfoInsert_Proc";
                StoredProcedureName = "VISTAEDIChemistryInfoInsert_Check";

                this.ConnectionString = ConfigurationManager.AppSettings["DBConnection"].ToString();
                SQLParameters = new Dictionary<string, object>();

                SQLParameters["Status"] = status;

                SQLParameters["ShipmentNumber"] = data.ShipmentNumber;
                SQLParameters["ShipmentDate"] = data.ShipmentDate;
                SQLParameters["HeatNumber"] = data.HeatNumber;
                SQLParameters["Alloy"] = data.Alloy;
                SQLParameters["Diameter"] = data.Diameter;
                SQLParameters["Al"] = data.Al;
                SQLParameters["Si"] = data.Si;
                SQLParameters["Fe"] = data.Fe;
                SQLParameters["Cu"] = data.Cu;
                SQLParameters["Mn"] = data.Mn;
                SQLParameters["Mg"] = data.Mg;
                SQLParameters["Cr"] = data.Cr;
                SQLParameters["Ni"] = data.Ni;
                SQLParameters["Zn"] = data.Zn;
                SQLParameters["Ti"] = data.Ti;
                SQLParameters["V"] = data.V;
                SQLParameters["Pb"] = data.Pb;
                SQLParameters["Sn"] = data.Sn;
                SQLParameters["B"] = data.B;
                SQLParameters["Be"] = data.Be;
                SQLParameters["Na"] = data.Na;
                SQLParameters["Ca"] = data.Ca;
                SQLParameters["Bi"] = data.Bi;
                SQLParameters["Zr"] = data.Zr;
                SQLParameters["Li"] = data.Li;
                SQLParameters["Ag"] = data.Ag;
                SQLParameters["Sc"] = data.Sc;
                SQLParameters["Sr"] = data.Sr;
                SQLParameters["TiZr"] = data.TiZr;

                DataTable failList = Execute();

                ResultViewModel res = new ResultViewModel();
                res.Message = "";
                res.Message1 = "";
                res.Success = true;

                int j;
                foreach (DataRow row in failList.Rows)
                {
                    j = 0;
                    //return message
                    res.Message = row[j].ToString();j++;
                    //heat number
                    res.Message1 = row[j].ToString();
                    res.Success = true;
                }
                return res;
            }
            catch (Exception ex)
            {
                ResultViewModel res = new ResultViewModel();
                res.Message1 = "FAIL";
                res.Success = false;
                return res;
            }
        }

        public ResultViewModel AcceptOrReject(char Status, int RecId)
        {
            ResultViewModel res = new ResultViewModel();
            res.Message = "";
            res.Message1 = "";
            res.Success = false;

            try
            {
                //Store Procedure 
                //IF Status '3' : Inserts Data with sent RecId from Deviation table to the ChemInfo and ChemDetail - inspite of deviation with Status = 3
                //IF Status '3' : Inserts Data with sent RecId from Deviation table to the ChemInfo and ChemDetail - inspite of deviation with Status = 3
                // Deletes the record with sent RecId from the Deviation table.

                StoredProcedureName = "VistaEDIAcceptOrRejectDeviation";
                this.ConnectionString = ConfigurationManager.AppSettings["DBConnection"].ToString();

                SQLParameters = new Dictionary<string, object>();

                SQLParameters["Status"] = Status;
                SQLParameters["DeviationRecId"] = RecId;

                DataTable result = Execute();

                res.Message = result.Rows[0][0].ToString(); 
                if (res.Message != "")
                    res.Success = true;
                               
                return res;
            }
            catch (Exception ex)
            {                
                res.Message = "FAIL";
                res.Message1 = ex.ToString();
             //   res.Success = false;
                return res;
            }
        }

        // public DataSearch<DeviationList> GetList(DataGridoption option)
        // use DataSearch is multiple lists are needed
        public List<DeviationsViewModel> GetList(DataGridoption option)
        {
            // List<DeviationList> lstDeviation = new List<DeviationList>();
            List<DeviationsViewModel> lstDeviation = new List<DeviationsViewModel>();
            // lstDeviation = null;
            try
            {
                StoredProcedureName = "VistaEDIGetDeviationList";

                this.ConnectionString = ConfigurationManager.AppSettings["DBConnection"].ToString();

                SQLParameters = new Dictionary<string, object>();            

                SQLParameters["CurrentPage"] = option.pageIndex;
                SQLParameters["NoOfRecords"] = option.pageSize;

                AddSearchFilter(option, SQLParameters);                               
               
                DataTable result = Execute();

                // DeviationList deviation = null;
                DeviationsViewModel deviation = null;
                int j;
                foreach (DataRow row in result.Rows)
                {
                    deviation = new DeviationsViewModel();
                    j = 0;
                    deviation.total = Convert.ToInt32(row[j]); j++;
                    deviation.RecId = Convert.ToInt32(row[j]); j++;
                    deviation.HeatNumber = row[j].ToString(); j++;
                    deviation.Deviations = row[j].ToString(); j++;
                    deviation.Status = Convert.ToChar(row[j]);
                    //  deviation.UACCode = row[j].ToString(); j++;
                    //  deviation.Type = Convert.ToChar(row[j]); j++; ;
                    //  if (row[j] != null)                    
                    //      deviation.DetailID = Convert.ToInt32(row[j]); j++;                                     
                    ////  deviation.Al = row[j].ToString(); j++;
                    //  deviation.Si = row[j].ToString(); j++;
                    //  deviation.Fe = row[j].ToString(); j++;
                    //  deviation.Cu = row[j].ToString(); j++;
                    //  deviation.Mn = row[j].ToString(); j++;
                    //  deviation.Mg = row[j].ToString(); j++;
                    //  deviation.Cr = row[j].ToString(); j++;                    
                    //  deviation.Zn = row[j].ToString(); j++;
                    //  deviation.Ti = row[j].ToString(); j++;
                    //  deviation.Zr = row[j].ToString(); j++;
                    //  deviation.Li = row[j].ToString(); j++;
                    //  deviation.B = row[j].ToString(); j++;
                    //  deviation.Ca = row[j].ToString(); j++;
                    //  deviation.Pb = row[j].ToString(); j++;
                    //  deviation.Ag = row[j].ToString(); j++;
                    //  deviation.Na = row[j].ToString(); j++;
                    //  deviation.Bi = row[j].ToString(); j++;
                    //  deviation.Sc = row[j].ToString(); j++;
                    //  deviation.Sr = row[j].ToString(); j++;
                    //  deviation.Be = row[j].ToString(); j++;
                    //  deviation.Ni = row[j].ToString(); j++;
                    //  deviation.V = row[j].ToString(); j++;
                    //  deviation.TiZr = row[j].ToString(); j++;                                        
                    //  deviation.Supplier = row[j].ToString(); j++;
                    //  deviation.EntryDate = Convert.ToDateTime(row[j]); j++;
                    //  deviation.Alloy = row[j].ToString(); j++;
                    //  deviation.Plant = row[j].ToString(); j++;
                    //  deviation.Diameter = row[j].ToString(); j++;

                    //  deviation.TransBy = row[j].ToString(); j++;                   
                    //   deviation.LookupID = Convert.ToInt32(row[j]); 

                    lstDeviation.Add(deviation);
                }           
            }
            catch (Exception ex)
            {
               // lstDeviation = null;
                //DeviationList deviation = new DeviationList();
                //deviation.Status = 'F';
                //lstDeviation.Add(deviation);
            }
            return lstDeviation;
        }

        private static void AddSearchFilter(DataGridoption option, Dictionary<string, object> SQLParameters)
        {
            if (option != null && !string.IsNullOrEmpty(option.searchBy))
            {
                string[] searchSplit = option.searchBy.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (searchSplit != null && searchSplit.Length > 0)
                {
                    foreach (var item in searchSplit)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            var itemSplit = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (itemSplit != null && itemSplit.Length == 2)
                            {
                                if (!string.IsNullOrEmpty(itemSplit[1]) && itemSplit[1] != "-1")
                                {
                                    if (itemSplit[0] == "searchHeatNo")
                                        SQLParameters["HeatNo"] = itemSplit[1];
                                    if (itemSplit[0] == "Status")
                                        SQLParameters["Status"] = itemSplit[1];
                                }
                                  //  lstSqlParameter.Add(new SqlParameter("@" + itemSplit[0], itemSplit[1]));
                            }
                        }
                    }
                }
            }
        }
    }
}
