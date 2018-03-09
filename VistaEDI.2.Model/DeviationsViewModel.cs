using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VistaEDI._2.Model
{
    public class DeviationsViewModel
    {
        public int RecId { get; set; }
        public string HeatNumber { get; set; }
     //   public string DeviationList { get; set; }
        public char Status { get; set; }
        public string Deviations { get; set; }
        public int total { get; set; }
    }
}
