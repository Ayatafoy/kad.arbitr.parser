using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kad.Arbitr.ru.Models
{
    abstract class ResponseModel
    {
        public int Count { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public IList<string> Judges { get; set; }
        public int Page { get; set; }
        public IList<string> Sides { get; set; }
    }
}
