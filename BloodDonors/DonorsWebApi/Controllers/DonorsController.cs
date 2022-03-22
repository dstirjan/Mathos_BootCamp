using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DonorsWebApi.Models;

namespace DonorsWebApi.Controllers
{
    public class DonorsController : ApiController
    {
        static List<DonorViewModel> donors = new List<DonorViewModel>()
        {
            new DonorViewModel
            {
            Id = 1,
            FirstName = "Dejan",
            LastName = "Stirjan",
            DonNumber = "6",
            ReferentCode = Guid.NewGuid(),
            },
            new DonorViewModel
            {
            Id = 2,
            FirstName = "Ivan",
            LastName = "Kovac",
            DonNumber = "5",
            ReferentCode = Guid.NewGuid(),
            },
            new DonorViewModel
            {
            Id = 3,
            FirstName = "Marko",
            LastName = "Hrastinski",
            DonNumber = "12",
            ReferentCode = Guid.NewGuid(),
            }
        };

        [HttpGet]
        public HttpResponseMessage Point()
        {
            if (donors == null || donors.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no donors at list");
            }
            return Request.CreateResponse(HttpStatusCode.OK, donors);
        }

        [HttpGet()]
        public HttpResponseMessage Point(int id)
        {
            var specificDonor = donors.Find(c => c.Id == id);
            if (donors == null || donors.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no donors at list");
            }
            else if (specificDonor == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no donor with  id '{id}'");
            }
            return Request.CreateResponse(HttpStatusCode.OK, specificDonor);
        }
        
        [HttpPost]
        public HttpResponseMessage Add(DonorViewModel donor)
        {
            if (donors.Count > 0)
            {
                donor.Id = donors.Max(s => s.Id) + 1;
            }
            else
            {
                donor.Id = 1;
            }
            int id = donor.Id;
            donors.Add(donor);
            donor.ReferentCode = Guid.NewGuid();
            HttpResponseMessage responseMessageOk = Request.CreateResponse(HttpStatusCode.Created, donor);
            return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully add donor with id '{id}'");
        }

        [HttpPut]
        public HttpResponseMessage Change(DonorViewModel donor)
        {
            var existingDonor = donors.Find(s => s.Id == donor.Id);

            if (existingDonor == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no donor with  id '{donor.Id}'");
            }

            existingDonor.Id = donor.Id;
            existingDonor.FirstName = donor.FirstName;
            existingDonor.LastName = donor.LastName;
            existingDonor.DonNumber = donor.DonNumber;

            HttpResponseMessage responseMessageOk = Request.CreateResponse(HttpStatusCode.OK, donor);
            return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully change donor with id '{donor.Id}'");
        }

        [HttpDelete]
        public HttpResponseMessage Remove(int id)
        {
            var specificDonor = donors.Find(c => c.Id == id);
            donors.Remove(specificDonor);
            if (specificDonor == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no donor with  id '{id}' at list");
            }
            else
            {
                HttpResponseMessage responseMessageOk = Request.CreateResponse(HttpStatusCode.OK, specificDonor);
                return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully remove donor with id '{specificDonor.Id}'");
            }
        }
    }
}
