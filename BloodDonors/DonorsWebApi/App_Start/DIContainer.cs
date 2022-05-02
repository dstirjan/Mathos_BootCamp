using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BloodDonor.Repository;
using BloodDonor.Repository.Common;
using BloodDonor.Service;
using BloodDonor.Service.Common;
using AutoMapper;
using BloodDonorWebApi.Models;
using DonorsWebApi.Models;

namespace DonorsWebApi.App_Start
{
    public class DIContainer
    {
        public static void AutoFacConfig()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DonorService>().As<IDonorService>();
            builder.RegisterType<DoctorService>().As<IDoctorService>();
            builder.RegisterType<DonorRepository>().As<IDonorRepository>();
            builder.RegisterType<DoctorRepository>().As<IDoctorRepository>();
            builder.Register(context => new MapperConfiguration(config =>
            {
                config.AddProfile(new ControllerMappingProfile());
            }));
            builder.Register(context => context.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().SingleInstance();
            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}