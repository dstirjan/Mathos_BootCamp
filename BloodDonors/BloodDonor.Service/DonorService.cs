using BloodDonor.Common;
using BloodDonor.Model;
using BloodDonor.Repository;
using BloodDonor.Repository.Common;
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
        private readonly IDonorRepository DIDonorRepository;
        public DonorService(IDonorRepository direpository)
        {
            this.DIDonorRepository = direpository;
        }

        public async Task<List<DonorModel>> GetDonorAsync(StringFiltering filter, Sorting sorting, Paging paging)
        {
            var donor = await DIDonorRepository.GetDonorAsync(filter, sorting, paging); ;
            return donor;
        }
        public async Task<List<DonorModel>> GetDonorByIdAsync(int id)
        {
            var donor = await DIDonorRepository.GetDonorByIdAsync(id);
            return donor;
        }
        public async Task IncludeDonorAsync(DonorModel donorModel)
        {
            await DIDonorRepository.IncludeDonorAsync(donorModel);
        }
        public async Task ChangeDonorByIdAsync(int id, DonorModel upgradedDonor)
        {
            await DIDonorRepository.ChangeDonorByIdAsync(id, upgradedDonor);
        }
        public async Task<bool> DeleteDonorAsync(int id)
        {
            var idCheck = await DIDonorRepository.DeleteDonorAsync(id);
            return idCheck;
        }
    }
}
