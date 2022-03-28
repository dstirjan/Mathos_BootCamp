using BloodDonor.Model;
using BloodDonor.Repository;
using BloodDonor.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonor.Service
{
    public class DonorService : IDonorService
    {
        public async Task<List<DonorModel>> GetDonorAsync()
        {
            DonorRepository donorRepository = new DonorRepository();
            var donor = await donorRepository.GetDonorAsync();
            return donor;
        }
        public async Task<List<DonorModel>> GetDonorByIdAsync(int id)
        {
            DonorRepository donorRepository = new DonorRepository();
            var donor = await donorRepository.GetDonorByIdAsync(id);
            return donor;
        }
        public async Task IncludeDonorAsync(DonorModel donorModel)
        {
            DonorRepository donorRepository = new DonorRepository();
            await donorRepository.IncludeDonorAsync(donorModel);
        }
        public async Task ChangeDonorByIdAsync(int id, DonorModel upgradedDonor)
        {
            DonorRepository donorRepository = new DonorRepository();
            await donorRepository.ChangeDonorByIdAsync(id, upgradedDonor);
        }
        public async Task<bool> DeleteDonorAsync(int id)
        {
            DonorRepository donorRepository = new DonorRepository();
            var idCheck = await donorRepository.DeleteDonorAsync(id);
            return idCheck;
        }
    }
}
