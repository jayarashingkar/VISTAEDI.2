using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using VistaEDI._2.Model;
using VistaEDI._2.Data;


namespace VistaEDI._2.Web.Controllers
{
    public class GridController : Controller
    {
        // GET: Grid
        public ActionResult GetDeviationList(DataGridoption option)
        {
            //List<DeviationList> lstDeviation = null;
            List<DeviationsViewModel> lstDeviation = null;
            // DeviationList deviationList = new DeviationList();
            DeviationsViewModel deviationList = new DeviationsViewModel();
            int total = 0;
            try
            {
                lstDeviation = new ParserData().GetList(option);
                total = lstDeviation.Count();
                return Json(new { items = lstDeviation, total = total }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                lstDeviation = null;
                total = 0;
                throw ex;
            }

           
        }
    }
}