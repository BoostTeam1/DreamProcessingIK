using System;
using System.Collections.Generic;
using System.Linq;
using Business.DependencyResolvers.Autofac;
using System.Text;
using System.Threading.Tasks;
using Business.Concrete;
using Business.Abstract;
using DataAccess.Concrete;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Identity;
using Entities.Concrete;
using Autofac;


namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModel:Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EventManager>().As<IEventService>();
            builder.RegisterType<EfEventDal>().As<IEventDal>();

            builder.RegisterType<UserCompanyManager>().As<IUserCompanyService>();
            builder.RegisterType<EfUserCompanyDal>().As<IUserCompanyDal>();

            builder.RegisterType<UserVacationManager>().As<IUserVacationService>();
            builder.RegisterType<EfUserVacationDal>().As<IUserVacationDal>();

            builder.RegisterType<VacationManager>().As<IVacationService>();
            builder.RegisterType<EfVacationDal>().As<IVacationDal>();

            builder.RegisterType<CompanyManager>().As<ICompanyService>();
            builder.RegisterType<EfCompanyDal>().As<ICompanyDal>();

            builder.RegisterType<UserForManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<EfCompanySectorDal>().As<ICompanySectorDal>();
            builder.RegisterType<CompanySectorManager>().As<ICompanySectorService>();

            builder.RegisterType<EfSectorDal>().As<ISectorDal>();
            builder.RegisterType<SectorManager>().As<ISectorService>();
            //builder.RegisterType<UserManager<AppUser>>().As<IDisposable>();



        }
    }
}
