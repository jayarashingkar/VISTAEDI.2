using System.Collections.Generic;

namespace VistaEDI._2.Model
{
    public class DataSearch<T> where T : class
    {
        public List<T> items { get; set; }
        public int total { get; set; }
    }
}
