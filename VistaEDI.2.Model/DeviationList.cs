using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VistaEDI._2.Model
{
    public class DeviationList
    {
        public int RecId { get; set; }
        public string HeatNumber { get; set; }
        public string UACCode { get; set; }
        public char Type { get; set; }
        public int DetailID { get; set; }       
        public string Al { get; set; }
        public string Si { get; set; }
        public string Fe { get; set; }
        public string Cu { get; set; }
        public string Mn { get; set; }
        public string Mg { get; set; }
        public string Cr { get; set; }
        public string Ni { get; set; }
        public string Zn { get; set; }
        public string Ti { get; set; }
        public string V { get; set; }
        public string Pb { get; set; }
        public string Sn { get; set; }
        public string B { get; set; }
        public string Be { get; set; }
        public string Na { get; set; }
        public string Ca { get; set; }
        public string Bi { get; set; }
        public string Zr { get; set; }
        public string Li { get; set; }
        public string Ag { get; set; }

        //not in test json file but are present in the database 
        public string Sc { get; set; }
        public string Sr { get; set; }
        public string TiZr { get; set; }

        public string Supplier { get; set; }
        public DateTime EntryDate { get; set; }
        public string Alloy { get; set; }
        public string Plant { get; set; }
        public string Diameter { get; set; }
        public char Status { get; set; }
        public int total { get; set; }
        public string TransBy { get; set; }
        public int LookupID { get; set; }
   }
}
