using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VistaEDI._2.BL;
using VistaEDI._2.Model;
using System.IO;

namespace VistaEDI._2.Web.Controllers
{

    public class ParserController : Controller
    {

        // GET: Parser
         public ActionResult Index()       
        {    
            return View();
        }

        [HttpPost]
       public ActionResult Upload(HttpPostedFileBase file, char status = '1')
        {
            bool isDeviation = false;
            if (status != '0')
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    BinaryReader b = new BinaryReader(file.InputStream);
                    byte[] binData = b.ReadBytes(file.ContentLength);
                    string result = System.Text.Encoding.UTF8.GetString(binData);
                   

                    ResultViewModel message = new VistaParser().ParseJson(result, status);

                    if (message.Success)
                    {
                        if (message.Message != "")
                        {
                            message.Message = "Deviations: " + message.Message;
                            ViewBag.Message = message.Message;
                            ViewBag.Status = "Choose Status and file to Accept/Reject with deviation ";
                            isDeviation = true;
                        }
                        else
                            ViewBag.Message = "File successfully uploaded";
                    }
                    else
                    {
                        ViewBag.Message = "File upload ERROR ";
                    }
                   
                }
                else
                {
                    ViewBag.Message = "File not selected";
                }
            }
            else
                ViewBag.Message = "Upload Rejected";
            if (isDeviation)
                return RedirectToAction("DeviationList");
            return View("Index");
        }

        #region Deviation

        public ActionResult DeviationList()
        {            
            return View();
        }
        
        public ActionResult DeviationListUpdate(int RecId, char status = '2')
        {
            ResultViewModel result = new ResultViewModel();
          //  bool isSuccess = false;
            
            try
            {
                //status = '3' : Accept on Deviation
                //status = '0' : Reject on Deviation
                result = new VistaParser().AcceptOrReject(status, RecId);
            }           
           catch(Exception ex)
            {
                result.Message = ex.Message;
            }
         
              return Json(new { isSuccess = result.Success, message = result.Message }, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}