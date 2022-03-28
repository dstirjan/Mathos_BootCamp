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
    public class DoctorService   : IDoctorService
    {
        public async Task<List<DoctorModel>> GetDoctorAsync()
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            var doctorModel = await doctorRepository.GetDoctorAsync();
            return doctorModel;
        }
        public async Task<List<DoctorModel>> GetDoctorLNAsync(String lastname)
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            var doctorModel = await doctorRepository.GetDoctorLNAsync(lastname);
            return doctorModel;
        }
        public async Task<List<DoctorModel>> GetDoctorByLidAsync(int lid)
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            var doctorModel = await doctorRepository.GetDoctorByLidAsync(lid);
            return doctorModel;
        }
        public async Task InsertDoctorAsync(DoctorModel doctorModel)
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            await doctorRepository.InsertDoctorAsync(doctorModel);
        }
        public async Task ChangeDoctorAsync(int lid, DoctorModel doctorModel)
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            await doctorRepository.ChangeDoctorAsync(lid, doctorModel);
        }
        public async Task<bool> DeleteDoctorAsync(int lid)
        {
            DoctorRepository doctorRepository = new DoctorRepository();
            var lidCheck = await doctorRepository.DeleteDoctorAsync(lid);
            return lidCheck;
        }
    }
}

