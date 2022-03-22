using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DonorsWebApi.Models
{
    public class DoctorViewModel
    {
        public int LicenceID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }
    }
}