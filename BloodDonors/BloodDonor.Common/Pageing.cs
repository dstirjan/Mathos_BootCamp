using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonor.Common
{
    public class Pageing
    {
        public int ItemsByPage { set; get; } = 4;
        public int PageNumber { set; get; } = 1;
    }
}
