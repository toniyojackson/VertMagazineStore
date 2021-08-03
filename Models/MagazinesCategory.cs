using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertMagazineStore.Models
{
    public class MagazinesCategory
    {
        public bool success { get; set; }
        public string token { get; set; }
        public string message { get; set; }
        public MagazinesCategory()
        {
            data = new List<Magazine>();
        }

        public List<Magazine> data { get; set; }
    }
}
