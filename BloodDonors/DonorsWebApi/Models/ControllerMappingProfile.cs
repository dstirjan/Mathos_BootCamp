using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using BloodDonor.Model;
using BloodDonorWebApi.Models;

namespace DonorsWebApi.Models
{
    public class ControllerMappingProfile : Profile
    {
        public ControllerMappingProfile()
        {
            CreateMap<DoctorViewModel, DoctorModel>().ReverseMap();
            CreateMap<DonorViewModel, DonorModel>().ReverseMap();
 
        }
    }  
}