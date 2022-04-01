using BloodDonor.Common;
using BloodDonor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonor.Repository.Common
{
    public interface IDonorRepository
    {
        Task<List<DonorModel>> GetDonorAsync(StringFiltering filter, Sorting sorting, Pageing pageing);
        Task<List<DonorModel>> GetDonorByIdAsync(int id);
        Task IncludeDonorAsync(DonorModel donorModel);
        Task ChangeDonorByIdAsync(int id, DonorModel upgradedDonor);
        Task<bool> DeleteDonorAsync(int id);

    }
}
