using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertMagazineStore.Models
{
    public class AnswerResponseTime
    {
        public AnswerResponseTime()
        {
            shouldBe = new List<string>();
        }

        public string totalTime { get; set; }
        public bool answerCorrect { get; set; }
        public List<string> shouldBe { get; set; }
    }
}
