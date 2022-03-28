using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonorWebApi.Models
{
    public class DoctorViewModel
    {
        public int Licence { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
    }
}