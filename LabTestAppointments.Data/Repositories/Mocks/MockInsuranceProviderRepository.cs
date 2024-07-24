using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.InsuranceProvider;
using LabTestAppointments.Data.Interfaces.Repositories;
using LabTestAppointments.Data.Repositories.Mocks.Generic;

namespace LabTestAppointments.Data.Repositories.Mocks
{
    public class MockInsuranceProviderRepository : MockGenericRepository<InsuranceProvider>, IInsuranceProviderRepository
    {
        private readonly LabTestAppointmentsContext _dbContext;
        public MockInsuranceProviderRepository(LabTestAppointmentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            this.LoadData();
        }

        public override async Task AddAsync(InsuranceProvider entity)
        {
            if (IsNull(entity))
            {
                throw new InsuranceProviderNullException("El proveedor de seguros es requerido, no puede ser nulo.");
            }

            if (await GetByIdAsync(entity.Id) is not null)
            {
                throw new InsuranceProviderAlreadyExistsException("El proveedor de seguros ingresado ya existe.");
            }

            ValidateInsuranceProvider(entity);

            InsuranceProvider insuranceProviderToAdd = new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Phone = entity.Phone,
                CreatedBy = "defaultUser",
                CreatedOn = DateTime.Now
            };

            await base.AddAsync(insuranceProviderToAdd);
        }

        public override async Task DeleteAsync(InsuranceProvider entity)
        {
            if (IsNull(entity))
            {
                throw new InsuranceProviderNullException("El proveedor de seguros es requerido, no puede ser nulo.");
            }

            var insuranceProviderToRemove = await GetByIdAsync(entity.Id);

            if (insuranceProviderToRemove is null)
            {
                throw new InsuranceProviderNotFoundException("El proveedor de seguros a eliminar no fue encontrado.");
            }

            await base.DeleteAsync(insuranceProviderToRemove);
        }

        public override async Task<List<InsuranceProvider>> GetAllAsync()
        {
            var insuranceProviders = await base.GetAllAsync();
            return insuranceProviders.OrderBy(b => b.Name).ToList();
        }

        public override async Task UpdateAsync(InsuranceProvider entity, int id)
        {
            if (IsNull(entity))
            {
                throw new InsuranceProviderNullException("El proveedor de seguros es requerido, no puede ser nulo.");
            }

            var insuranceProviderToUpdate = await GetByIdAsync(entity.Id);

            if (insuranceProviderToUpdate is null)
            {
                throw new InsuranceProviderNotFoundException("El proveedor de seguros a actualizar no fue encontrado.");
            }

            ValidateInsuranceProvider(entity);

            await base.UpdateAsync(entity, id);
        }

        #region Private Methods
        private void LoadData()
        {

            if (!_dbContext.Set<InsuranceProvider>().Any())
            {
                List<InsuranceProvider> insuranceProviders = new()
            {
                new InsuranceProvider()
                {
                    Name = "Humano",
                    Phone = "809-582-4196",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new InsuranceProvider()
                {
                    Name = "BMI",
                    Phone = "809-451-6352",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new InsuranceProvider()
                {
                    Name = "MAPFRE",
                    Phone = "829-785-4523",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new InsuranceProvider()
                {
                    Name = "Futuro",
                    Phone = "809-332-9914",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new InsuranceProvider()
                {
                    Name = "Yunen",
                    Phone = "849-769-2429",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                }

            };

                _dbContext.Set<InsuranceProvider>().AddRange(insuranceProviders);
                _dbContext.SaveChanges();
            }

        }

        private bool IsNull(InsuranceProvider insuranceProvider)
        {
            return insuranceProvider == null ? true : false;
        }
        private void ValidateInsuranceProvider(InsuranceProvider insuranceProvider)
        {
            if (string.IsNullOrEmpty(insuranceProvider.Name))
            {
                throw new InsuranceProviderMissingPropertiesException("Debe ingresar el nombre del proveedor de seguros.");
            }

            if (string.IsNullOrEmpty(insuranceProvider.Phone))
            {
                throw new InsuranceProviderMissingPropertiesException("Debe ingresar el número telefónico del proveedor de seguros.");
            }
        }
        #endregion
    }
}