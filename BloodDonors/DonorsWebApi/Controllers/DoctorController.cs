using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BloodDonorWebApi.Models;

namespace BloodDonorWebApi.Controllers
{
    public class DoctorController : ApiController
    {
        static List<DoctorViewModel> doctors = new List<DoctorViewModel>()
        {
            new DoctorViewModel
            {
            LicenceID = 111111,
            FirstName = "Ivan",
            LastName = "Matic",
            Specialization = "Cardiology",
            },
            new DoctorViewModel
            {
            LicenceID = 436625,
            FirstName = "Goran",
            LastName = "Otic",
            Specialization = "Otorinolaringology",
            },
            new DoctorViewModel
            {
            LicenceID = 3458879,
            FirstName = "Ana",
            LastName = "Matic",
            Specialization = "Transfusiology",
            },
        };

        [HttpGet]
        [Route("api/doctors")]
        public HttpResponseMessage Point()
        {
            if (doctors == null || doctors.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no doctor at list");
            }
            return Request.CreateResponse(HttpStatusCode.OK,doctors);
        }

        [HttpGet()]
        [Route("api/doctors/lastname/{lastName}")]
        public HttpResponseMessage Point(string lastName)
        {

            var doctorsByLastName = doctors.FindAll(c => c.LastName == lastName);
            if (doctors == null || !doctors.Any())
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no doctors at list");
            }
            else if (doctorsByLastName.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no doctors with '{lastName}' last name");
            }
            return Request.CreateResponse(HttpStatusCode.OK, doctorsByLastName);
        }

        [HttpGet()]
        [Route("api/doctors/licence/{lid}")]
        public HttpResponseMessage Point(int lid)
        {
            var doctorLID = doctors.Find(c => c.LicenceID == lid);
            if (doctors == null || doctors.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "There is no doctors at list");
            }
            else if (doctorLID == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no doctor with Licence:'{lid}'");
            }
            return Request.CreateResponse(HttpStatusCode.OK, doctorLID);
        }

        [HttpPost]
        [Route("api/doctors/add")]
        public HttpResponseMessage Add(DoctorViewModel doctor)
        {
            doctors.Add(doctor);
            HttpResponseMessage responseMessageOk = Request.CreateResponse(HttpStatusCode.Created, doctor);
            return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully add doctor of " +
                $"{doctor.Specialization} , doc.{doctor.FirstName} {doctor.LastName}, with Licence: { doctor.LicenceID} ");
        }

        [HttpPut]
        [Route("api/doctors/change/{lid}")]
        public HttpResponseMessage Change(DoctorViewModel doctor, [FromUri] int lid)
        {
            var existingLID = doctors.Find(s => s.LicenceID == lid);
            if (existingLID == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no doctor with LicenceID:'{lid}' on the list");
            }

            existingLID.FirstName = doctor.FirstName;
            existingLID.LastName = doctor.LastName;
            existingLID.Specialization = doctor.Specialization;

            HttpResponseMessage responseMessageOk = Request.CreateResponse(HttpStatusCode.OK, doctor);
            return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully change doctor with LicenceID:'{lid}' on the list");
        }

        [HttpDelete]
        [Route("api/doctors/delete/{lid}")]
        public HttpResponseMessage Remove(int lid)
        {
            var doctorLid = doctors.Find(s => s.LicenceID == lid);
            doctors.Remove(doctorLid);
            if (doctorLid == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, $"There is no doctor with LicenceID:'{lid}'");
            }
            else
            {
                HttpResponseMessage responseMessageOk = Request.CreateResponse(HttpStatusCode.OK, doctorLid);
                return Request.CreateResponse(HttpStatusCode.OK, $"You succsessfully remove doctor with LicenceID:'{doctorLid.LicenceID}' from the list");
            }
        }
    }
}

