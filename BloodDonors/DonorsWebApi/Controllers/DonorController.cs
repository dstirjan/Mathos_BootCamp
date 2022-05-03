using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BloodDonorWebApi.Models;
using System.Data.SqlClient;
using BloodDonor.Service;
using BloodDonor.Model;
using System.Threading.Tasks;
using BloodDonor.Service.Common;
using BloodDonor.Common;
using AutoMapper;

namespace BloodDonorWebApi.Controllers
{
    public class DonorController : ApiController
    {
        private readonly IDonorService DIDonorService;
        private readonly IMapper _mapper;
        public DonorController(IDonorService diservice,IMapper mapper)
        {
            DIDonorService = diservice;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/donor")]
        public async Task<HttpResponseMessage> GetDonorAsync([FromUri] StringFiltering filter, [FromUri] Sorting sorting, [FromUri] Paging paging)
        {
            List<DonorModel> donorModel;

            donorModel = await DIDonorService.GetDonorAsync(filter, sorting, paging);

            if (donorModel.Any())
            {

                return Request.CreateResponse(HttpStatusCode.OK, _mapper.Map<List<DoctorViewModel>>(donorModel));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no doctor  at list");
            }

        }

        [HttpGet()]
        [Route("api/donor/{id}")]
        public async Task<HttpResponseMessage> GetDonorByIdAsync([FromUri] int id)
        {
            List<DonorModel> donorModel;

             donorModel = await DIDonorService.GetDonorByIdAsync(id);

            if (donorModel.Any())
            {

                return Request.CreateResponse(HttpStatusCode.OK, _mapper.Map<List<DoctorViewModel>>(donorModel));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no doctor  at list");
            }
        }

        [HttpPost]
        [Route("api/donor/")]
        public async Task<HttpResponseMessage> IncludeDonorAsync([FromBody] DonorWriteModel addDonor)
        {
            if (addDonor == null || addDonor.FirstName == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"You can't add doctor without all information ");
            }
            else
            { 
                var newDonor = new DonorModel() {
                    FirstName = addDonor.FirstName,
                    LastName = addDonor.LastName,
                    DonationNumber = addDonor.DonationNumber,
                    Email = addDonor.Email,
                 };
                    newDonor.ReferentCode = Guid.NewGuid();
                    await DIDonorService.IncludeDonorAsync(newDonor);

            return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully add new donor ");
            }
        }

        [HttpPut]
        [Route("api/donor/{id}")]
        public async Task<HttpResponseMessage> ChangeDonorByIdAsync([FromUri] int id, [FromBody] DonorWriteModel upgradedDonor)
        {
            if (upgradedDonor == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"You change informations are incomplete ");
            }
            else
            {

                var donorModel = new DonorModel()
                {
                    FirstName = upgradedDonor.FirstName,
                    LastName = upgradedDonor.LastName,
                    DonationNumber = upgradedDonor.DonationNumber,
                    Email = upgradedDonor.Email,
                };
                await DIDonorService.ChangeDonorByIdAsync(id, donorModel);

                return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully update donor with id: {id} ");
            }
        }

        [HttpDelete]
        [Route("api/donor/{id}")]
        public async Task<HttpResponseMessage> DeleteDonorAsync([FromUri] int id)
        {

            var idCheck = await DIDonorService.DeleteDonorAsync(id);


            if (idCheck == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully remove donor " +
                    $"with id '{id}'");
            }
            else
                { 
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no donor with that id at list");
                }
            }
    }
}