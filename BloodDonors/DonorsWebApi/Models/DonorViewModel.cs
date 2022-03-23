using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BloodDonorWebApi.Models
{
    public class DonorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DonNumber { get; set; }
        public string Email { get; set; }
        public Guid ReferentCode { get; set; }
    }
}