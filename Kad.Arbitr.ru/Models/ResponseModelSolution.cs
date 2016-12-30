using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kad.Arbitr.ru.Models
{
    class ResponseModelSolution : ResponseModel
    {
        public IList<string> Cases { get; set; }
        public bool GroupByCase { get; set; }
        public string Text { get; set; }
    }
}
