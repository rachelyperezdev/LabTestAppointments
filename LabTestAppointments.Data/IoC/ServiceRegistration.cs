using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Interfaces.Repositories;
using LabTestAppointments.Data.Interfaces.Repositories.Generic;
using LabTestAppointments.Data.Repositories.Mocks;
using LabTestAppointments.Data.Repositories.Mocks.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LabTestAppointments.Data.IoC
{
    public static class ServiceRegistration
    {
        public static void AddDataLayer(this IServiceCollection services)
        {
            #region Context
            services.AddDbContext<LabTestAppointmentsContext>(options => options.UseInMemoryDatabase("LabTestAppointmentDb"));
            #endregion

            #region Services
            services.AddScoped(typeof(IGenericRepository<>), typeof(MockGenericRepository<>));
            services.AddScoped<ILabTestRepository, MockLabTestRepository>();
            services.AddScoped<IMedicalImageRepository, MockMedicalImageRepository>();
            services.AddScoped<IBioanalystRepository, MockBioanalystRepository>();
            services.AddScoped<IInsuranceProviderRepository, MockInsuranceProviderRepository>();
            #endregion

        }
    }
}