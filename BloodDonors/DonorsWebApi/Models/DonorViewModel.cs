using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonorWebApi.Models
{
    public class DonorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DonationNumber { get; set; }
    }
}