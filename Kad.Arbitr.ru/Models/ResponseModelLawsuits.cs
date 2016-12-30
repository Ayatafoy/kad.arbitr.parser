using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kad.Arbitr.ru.Models
{
    class ResponseModelLawsuits : ResponseModel
    {
        public IList<string> Courts { get; set; }
        public IList<string> CaseNumbers { get; set; }
        public bool WithVKSInstances { get; set; }
    }
}
