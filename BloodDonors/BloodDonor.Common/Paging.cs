using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonor.Common
{
    public class Paging
    {
        public int ItemsByPage { set; get; } = 20;
        public int PageNumber { set; get; } = 1;
    }
}
