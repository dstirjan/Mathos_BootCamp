using BloodDonor.Common;
using BloodDonor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonor.Repository.Common
{
    public interface IDoctorRepository
    {
         Task<List<DoctorModel>> GetDoctorAsync(StringFiltering filter, Sorting sorting, Paging paging);
         Task<List<DoctorModel>> GetDoctorLNAsync(string lastname);
         Task<List<DoctorModel>> GetDoctorByLidAsync(int lid);
         Task InsertDoctorAsync(DoctorModel doctorModel);
         Task ChangeDoctorAsync(int lid, DoctorModel doctorModel);
         Task<bool> DeleteDoctorAsync(int lid);

    }
}
