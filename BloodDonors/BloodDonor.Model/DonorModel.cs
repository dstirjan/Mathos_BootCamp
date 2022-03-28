using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonor.Model
{
    public class DonorModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DonationNumber { get; set; }
        public string Email { get; set; }
        public Guid ReferentCode { get; set; }
    }
}
