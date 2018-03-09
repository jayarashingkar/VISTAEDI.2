using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaEDI._2.Data;
using VistaEDI._2.Model;
using Newtonsoft.Json;

namespace VistaEDI._2.BL
{

    public class VistaParser
    {
        public ResultViewModel ParseJson(string data, char status)
        {
            ResultViewModel resSend = new ResultViewModel();
            resSend.Message = "";
            resSend.Success = true;
            try
            {
                ResultViewModel resReceive;              

                var result = JsonConvert.DeserializeObject<List<ChemistryInfo>>(data);

                foreach (var item in result)
                {
                    resReceive = new ParserData().SaveItem(item, status);
                    if (resReceive.Success)
                    {
                        if (resReceive.Message != "")  
                            resSend.Message += "<br />" + "Heat No." + resReceive.Message1 + ":" + resReceive.Message + "; ";
                        resSend.Success = true;                        
                    }
                    else
                    {                      
                        resSend.Success = false;                       
                    }
                   
                }
            }
            catch (Exception ex)
            {
                resSend.Message = "";
                resSend.Success = false;
                resSend.Message1 = ex.ToString();
            }
            return resSend;
        }

        public ResultViewModel AcceptOrReject( char status, int RecId)
        {
            ResultViewModel resSend = new ResultViewModel();
            resSend.Message = "";
            resSend.Success = true;
            try
            {
                resSend =  new ParserData().AcceptOrReject(status, RecId);
               
            }
            catch(Exception ex)
            {
                resSend.Message1 = ex.ToString();
                resSend.Message = "Record is not Inserted";
            }
            return resSend;  
        
        }

    }
}
