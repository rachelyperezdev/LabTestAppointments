using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.HealthInsurance;
using LabTestAppointments.Data.Interfaces.Repositories;
using LabTestAppointments.Data.Repositories.Mocks.Generic;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointments.Data.Repositories.Mocks
{
    public class MockHealthInsuranceRepository : MockGenericRepository<HealthInsurance>, IHealthInsuranceRepository
    {
        private readonly LabTestAppointmentsContext _dbContext;
        public MockHealthInsuranceRepository(LabTestAppointmentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            this.LoadData();
        }

        public override async Task AddAsync(HealthInsurance entity)
        {
            if (IsNull(entity))
            {
                throw new HealthInsuranceNullException("El seguro médico es requerido, no puede ser nulo.");
            }

            if (await GetByIdAsync(entity.Id) is not null)
            {
                throw new HealthInsuranceAlreadyExistsException("El seguro médico ingresado ya existe.");
            }

            HealthInsurance healthInsuranceProviderToAdd = new()
            {
                Id = entity.Id,
                Plan = entity.Plan,
                InsuranceProviderId = entity.InsuranceProviderId,
                CreatedBy = "defaultUser",
                CreatedOn = DateTime.Now
            };

            await base.AddAsync(healthInsuranceProviderToAdd);
        }

        public override async Task DeleteAsync(HealthInsurance entity)
        {
            if (IsNull(entity))
            {
                throw new HealthInsuranceNullException("El seguro médico es requerido, no puede ser nulo.");
            }

            var healthInsuranceToRemove = await GetByIdAsync(entity.Id);

            if (healthInsuranceToRemove is null)
            {
                throw new HealthInsuranceNotFoundException("El seguro médico a eliminar no fue encontrado.");
            }

            await base.DeleteAsync(healthInsuranceToRemove);
        }

        public override async Task<List<HealthInsurance>> GetAllAsync()
        {
            var healthInsurances = await base.GetAllAsync();
            return healthInsurances.OrderBy(b => b.Plan).ToList();
        }

        public async Task<List<HealthInsurance>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = _dbContext.Set<HealthInsurance>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            var entities = await query.ToListAsync();
            entities = entities.OrderBy(b => b.Plan).ToList();

            return entities;
        }

        public async Task<HealthInsurance> GetByIdWithIncludeAsync(int id, List<string> properties)
        {
            var query = _dbContext.Set<HealthInsurance>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public override async Task UpdateAsync(HealthInsurance entity, int id)
        {
            if (IsNull(entity))
            {
                throw new HealthInsuranceNullException("El seguro médico es requerido, no puede ser nulo.");
            }

            var healthInsuranceToUpdate = await GetByIdAsync(entity.Id);

            if (healthInsuranceToUpdate is null)
            {
                throw new HealthInsuranceNotFoundException("El seguro médico a actualizar no fue encontrado.");
            }

            await base.UpdateAsync(entity, id);
        }

        #region Private Methods
        private void LoadData()
        {

            if (!_dbContext.Set<HealthInsurance>().Any())
            {
                List<HealthInsurance> healthInsurances = new()
            {
                new HealthInsurance()
                {
                    Plan = "Regular",
                    InsuranceProviderId = 1,
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new HealthInsurance()
                {
                    Plan = "Premium",
                    InsuranceProviderId = 2,
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new HealthInsurance()
                {
                    Plan = "Gold",
                    InsuranceProviderId = 3,
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new HealthInsurance()
                {
                    Plan = "Salud Superior",
                    InsuranceProviderId = 4,
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new HealthInsurance()
                {
                    Plan = "Infinity",
                    InsuranceProviderId = 5,
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                }

            };

                _dbContext.Set<HealthInsurance>().AddRange(healthInsurances);
                _dbContext.SaveChanges();
            }

        }

        private bool IsNull(HealthInsurance healthInsurance)
        {
            return healthInsurance == null ? true : false;
        }
        #endregion
    }
}
