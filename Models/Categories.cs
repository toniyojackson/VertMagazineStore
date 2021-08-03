using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertMagazineStore.Models
{
    public class Categories
    {
        public bool success { get; set; }
        public string token { get; set; }
        public string message { get; set; }
        public Categories()
        {
            data = new List<string>();
        }

        public List<string> data { get; set; }
    }
}
