using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNU.JAVA.Core
{
    public class ChartDataElement
    {
        public DateTime Date;

        public string X => Date.Date.ToString("dd/MM/yyyy");
        public int Y { get; set; }
    }
}
