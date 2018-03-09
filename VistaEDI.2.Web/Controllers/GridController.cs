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
            }
            catch (Exception ex)
            {
                lstDeviation = null;
                total = 0;
            }
            //DataSearch<DeviationList> ds = new DataSearch<DeviationList>
            //{
            //    items = lstDeviation,
            //    total = (lstDeviation != null && lstDeviation.Count > 0) ? lstDeviation[0].total : 0
            //};
            //return ds;
            // return Serializer.ReturnContent(ds, this.Configuration.Services.GetContentNegotiator(), this.Configuration.Formatters, this.Request);

              return Json(new { items = lstDeviation, total = total }, JsonRequestBehavior.AllowGet);
        }
    }
}