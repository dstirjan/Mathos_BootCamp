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
using BloodDonor.Service.Common;
using BloodDonor.Common;
using AutoMapper;

namespace BloodDonorWebApi.Controllers
{
    public class DoctorController : ApiController
    {
        private readonly IDoctorService DIDoctorService;
        private readonly IMapper _mapper;
        public DoctorController(IDoctorService diservice, IMapper mapper)
        {
            DIDoctorService = diservice;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("api/doctor")]
        public async Task<HttpResponseMessage> GetDoctorAsync([FromUri] StringFiltering filter, [FromUri] Sorting sorting, [FromUri] Paging paging)
        {
            List<DoctorModel> doctorModel;

            doctorModel = await DIDoctorService.GetDoctorAsync(filter, sorting, paging);

            if (doctorModel.Any())
            {

                return Request.CreateResponse(HttpStatusCode.OK, _mapper.Map<List<DoctorViewModel>>(doctorModel));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no doctor  at list");
            }

        }
    

        [HttpGet()]
        [Route("api/doctor/lastname/{lastName}")]
        public async Task<HttpResponseMessage> GetDoctorLNAsync([FromUri] string lastName)
        {
            List<DoctorModel> doctorModel;

            doctorModel = await DIDoctorService.GetDoctorLNAsync(lastName);

            if (doctorModel.Any())
            {

                return Request.CreateResponse(HttpStatusCode.OK, _mapper.Map<List<DoctorViewModel>>(doctorModel));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no doctor  at list");
            }

        }

        [HttpGet()]
        [Route("api/doctor/licence/{lid}")]
        public async Task<HttpResponseMessage> GetDoctorByLidAsync([FromUri] int lid)
        {
            List<DoctorModel> doctorModel;

            doctorModel = await DIDoctorService.GetDoctorByLidAsync(lid);

            if (doctorModel.Any())
            {

                return Request.CreateResponse(HttpStatusCode.OK, _mapper.Map<List<DoctorViewModel>>(doctorModel));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no doctor  at list");
            }

        }

        [HttpPost]
        [Route("api/doctor/add")]
        public async Task<HttpResponseMessage> InsertDoctorAsync([FromBody] DoctorViewModel doctor)
        {

            if (doctor.Licence == 0 || doctor == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"You can't add doctor without all information ");
            }
            else 
            {
                var newDoctor = new DoctorModel()
                {
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Licence = doctor.Licence,
                    Specialization = doctor.Specialization
                };
                await DIDoctorService.InsertDoctorAsync(newDoctor);

                return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully add new doctor ");
            }
        }


        [HttpPut]
        [Route("api/doctor/change/{lid}")]
        public async Task<HttpResponseMessage> ChangeDoctorAsync(DoctorViewModel doctorModel,
                                          int lid)
        {
             if (doctorModel == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"You change informations are incomplete ");
            }
            else
            {
                var upgradedDoctor = new DoctorModel()
                {
                    FirstName = doctorModel.FirstName,
                    LastName = doctorModel.LastName,
                    Specialization = doctorModel.Specialization,
                };
                await DIDoctorService.ChangeDoctorAsync(lid, upgradedDoctor);

                return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully update donor with Licence: {lid} ");
            }
        }



        [HttpDelete]
        [Route("api/doctor/delete/{lid}")]
        public async Task<HttpResponseMessage> DeleteDoctorAsync([FromUri] int lid)
        {
            var lidCheck = await DIDoctorService.DeleteDoctorAsync(lid);

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
