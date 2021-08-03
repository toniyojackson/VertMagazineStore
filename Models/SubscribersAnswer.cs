using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertMagazineStore.Models
{
    public class SubscribersAnswer
    {
        public SubscribersAnswer()
        {
            subscribers = new List<string>();
        }
        public List<string> subscribers { get; set; }
    }
}
