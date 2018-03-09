using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VistaEDI._2.Model
{
    public class DataGridoption
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public string sortDirection { get; set; }
        public string sortBy { get; set; }
        public string filterBy { get; set; }
        public string searchBy { get; set; }
    }
}
