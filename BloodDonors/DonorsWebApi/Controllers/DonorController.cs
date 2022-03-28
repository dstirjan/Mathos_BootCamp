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

namespace BloodDonorWebApi.Controllers
{
    public class DonorController : ApiController
    {
        [HttpGet]
        [Route("api/donor")]
        public async Task <HttpResponseMessage> GetDonorAsync()
        {
            DonorService donorService = new DonorService();
            List<DonorModel> mapedDonors = new List<DonorModel>();
            List<DonorViewModel> showDonor = new List<DonorViewModel>();

            mapedDonors = await donorService.GetDonorAsync();
            foreach (var donor in mapedDonors)
            {
                DonorViewModel donorViewModel = new DonorViewModel();
                donorViewModel.FirstName = donor.FirstName;
                donorViewModel.LastName = donor.LastName;
                donorViewModel.DonNumber = donor.DonationNumber;
                showDonor.Add(donorViewModel);
            }
            if (mapedDonors.Any())
            {

                return Request.CreateResponse(HttpStatusCode.OK, showDonor);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no donors at list");
            }

        }

        [HttpGet()]
        [Route("api/donor/{id}")]
        public async Task<HttpResponseMessage> GetDonorByIdAsync([FromUri] int id)
        {
            DonorService donorService = new DonorService();
            List<DonorModel> mapedDonors = new List<DonorModel>();
            List<DonorViewModel> showDonor = new List<DonorViewModel>();

            mapedDonors = await donorService.GetDonorByIdAsync(id);
            foreach (var donor in mapedDonors)
            {
                DonorViewModel donorViewModel = new DonorViewModel();
                donorViewModel.FirstName = donor.FirstName;
                donorViewModel.LastName = donor.LastName;
                donorViewModel.DonNumber = donor.DonationNumber;
                showDonor.Add(donorViewModel);
            }

            if (mapedDonors.Any())
            {
                return Request.CreateResponse(HttpStatusCode.OK, showDonor);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no donor with Id = {id}");
            }
        }

        [HttpPost]
        [Route("api/donor/")]
        public async Task<HttpResponseMessage> IncludeDonorAsync([FromBody] DonorWriteModel addDonor)
        {
            DonorService donorService = new DonorService();

            var newDonor = new DonorModel() {
                FirstName = addDonor.FirstName,
                LastName = addDonor.LastName,
                DonationNumber = addDonor.DonNumber,
                Email = addDonor.Email,
            };
           newDonor.ReferentCode = Guid.NewGuid();
           await donorService.IncludeDonorAsync(newDonor);

            return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully add new donor ");
        }

        [HttpPut]
        [Route("api/donor/{id}")]
        public async Task<HttpResponseMessage> ChangeDonorByIdAsync([FromUri] int id, [FromBody] DonorWriteModel upgradedDonor)
        {
            DonorService donorService = new DonorService();

            var donorModel = new DonorModel()
            {
                FirstName = upgradedDonor.FirstName,
                LastName = upgradedDonor.LastName,
                DonationNumber = upgradedDonor.DonNumber,
                Email = upgradedDonor.Email,
            };
           await donorService.ChangeDonorByIdAsync(id, donorModel);

           return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully update donor with id: {id} ");
        }

        [HttpDelete]
        [Route("api/donor/{id}")]
        public async Task<HttpResponseMessage> DeleteDonorAsync([FromUri] int id)
        {
            DonorService donorService = new DonorService();
            var idCheck = await donorService.DeleteDonorAsync(id);


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