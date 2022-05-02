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
    public class DoctorService : IDoctorService
    {

        private readonly IDoctorRepository DIDoctorRepository;
        public DoctorService(IDoctorRepository direpository)
        {
           DIDoctorRepository = direpository;
        }

        public async Task<List<DoctorModel>> GetDoctorAsync(StringFiltering filter, Sorting sorting, Paging paging)
        {
            var doctorModel = await DIDoctorRepository.GetDoctorAsync(filter, sorting, paging);
            return doctorModel;
        }
        public async Task<List<DoctorModel>> GetDoctorLNAsync(String lastname)
        {
            var doctorModel = await DIDoctorRepository.GetDoctorLNAsync(lastname);
            return doctorModel;
        }
        public async Task<List<DoctorModel>> GetDoctorByLidAsync(int lid)
        {
            var doctorModel = await DIDoctorRepository.GetDoctorByLidAsync(lid);
            return doctorModel;
        }
        public async Task InsertDoctorAsync(DoctorModel doctorModel)
        {
            await DIDoctorRepository.InsertDoctorAsync(doctorModel);
        }
        public async Task ChangeDoctorAsync(int lid, DoctorModel doctorModel)
        {
            await DIDoctorRepository.ChangeDoctorAsync(lid, doctorModel);
        }
        public async Task<bool> DeleteDoctorAsync(int lid)
        {
            var lidCheck = await DIDoctorRepository.DeleteDoctorAsync(lid);
            return lidCheck;
        }

 
    }
}


