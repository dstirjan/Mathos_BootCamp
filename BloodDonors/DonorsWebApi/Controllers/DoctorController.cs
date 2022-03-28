using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BloodDonorWebApi.Models;
using BloodDonor.Service;
using BloodDonor.Model;
using System.Threading.Tasks;

namespace BloodDonorWebApi.Controllers
{
    public class DoctorController : ApiController
    {
        [HttpGet]
        [Route("api/doctor")]
        public async Task<HttpResponseMessage> GetDoctorAsync()
        {
            DoctorService doctorService = new DoctorService();
            List<DoctorModel> mapModel = new List<DoctorModel>();
            List<DoctorViewModel> viewModel = new List<DoctorViewModel>();

            mapModel =await doctorService.GetDoctorAsync();

            foreach (var doctor in mapModel)
            {
                DoctorViewModel doctorViewModel = new DoctorViewModel();
                doctorViewModel.Licence = doctor.Licence;
                doctorViewModel.FirstName = doctor.FirstName;
                doctorViewModel.LastName = doctor.LastName;
                doctorViewModel.Specialization = doctor.Specialization;
                viewModel.Add(doctorViewModel);
            }
            if (mapModel.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, viewModel);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no donors at list");
            }

        }

        [HttpGet()]
        [Route("api/doctor/lastname/{lastName}")]
        public async Task<HttpResponseMessage> GetDoctorLNAsync([FromUri] string lastName)
        {

            DoctorService doctorService = new DoctorService();
            List<DoctorModel> mapModel = new List<DoctorModel>();
            List<DoctorViewModel> viewModel = new List<DoctorViewModel>();


            mapModel = await doctorService.GetDoctorLNAsync(lastName);
            foreach (var doctor in mapModel)
            {
                DoctorViewModel doctorView = new DoctorViewModel();
                doctorView.Licence = doctor.Licence;
                doctorView.FirstName = doctor.FirstName;
                doctorView.LastName = doctor.LastName;
                doctorView.Specialization = doctor.Specialization;

                viewModel.Add(doctorView);
            }

            if (mapModel.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, viewModel);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no any doctor with {lastName}");
            }
        }

        [HttpGet()]
        [Route("api/doctor/licence/{lid}")]
        public async Task<HttpResponseMessage> GetDoctorByLidAsync([FromUri] int lid)
        {
            DoctorService doctorService = new DoctorService();
            List<DoctorModel> mapModel = new List<DoctorModel>();
            List<DoctorViewModel> viewModel = new List<DoctorViewModel>();


            mapModel = await doctorService.GetDoctorByLidAsync(lid);
            foreach (var doctor in mapModel)
            {
                DoctorViewModel doctorView = new DoctorViewModel();
                doctorView.Licence = doctor.Licence;
                doctorView.FirstName = doctor.FirstName;
                doctorView.LastName = doctor.LastName;
                doctorView.Specialization = doctor.Specialization;

                viewModel.Add(doctorView);
            }

            if (mapModel.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, viewModel);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no any doctor with Licence: {lid}");
            }
        }

        [HttpPost]
        [Route("api/doctor/add")]
        public async Task<HttpResponseMessage> InsertDoctorAsync([FromBody] DoctorViewModel doctor)
        {
            DoctorService doctorService = new DoctorService();
            List<DoctorViewModel> bodyCheck = new List<DoctorViewModel>();

            var newDoctor = new DoctorModel()
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Licence = doctor.Licence,
                Specialization = doctor.Specialization,
            };
            await doctorService.InsertDoctorAsync(newDoctor);

            return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully add new doctor ");
        }

        [HttpPut]
        [Route("api/doctor/change/{lid}")]
        public async Task<HttpResponseMessage> ChangeDoctorAsync(DoctorViewModel doctorModel,
                                          int lid)
        {
            DoctorService doctorService = new DoctorService();
            var upgradedDoctor = new DoctorModel()
            {
                FirstName = doctorModel.FirstName,
                LastName = doctorModel.LastName,
                Specialization = doctorModel.Specialization,
            };
            await doctorService.ChangeDoctorAsync(lid, upgradedDoctor);
            return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully update donor with Licence: {lid} ");
        }



        [HttpDelete]
        [Route("api/doctor/delete/{lid}")]
        public async Task<HttpResponseMessage> DeleteDoctorAsync([FromUri] int lid)
        {
            DoctorService doctorService = new DoctorService();
            var lidCheck = await doctorService.DeleteDoctorAsync(lid);

            if (lidCheck == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully remove doctor with Licence:'{lid}'");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no doctor with that Licence");
            }
        }
    }
}
