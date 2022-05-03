using BloodDonor.Common;
using BloodDonor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonor.Service.Common
{
    public interface IDonorService
    {
       Task<List<DonorModel>> GetDonorAsync(StringFiltering filter, Sorting sorting, Paging paging);
       Task<List<DonorModel>> GetDonorByIdAsync(int id);
        Task IncludeDonorAsync(DonorModel donorModel);
        Task ChangeDonorByIdAsync(int id, DonorModel upgradedDonor);
        Task<bool> DeleteDonorAsync(int id);
    }
}
