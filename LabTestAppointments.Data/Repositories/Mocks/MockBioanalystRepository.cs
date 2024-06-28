using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.Bioanalyst;
using LabTestAppointments.Data.Interfaces.Repositories;
using LabTestAppointments.Data.Repositories.Mocks.Generic;

namespace LabTestAppointments.Data.Repositories.Mocks
{
    public class MockBioanalystRepository : MockGenericRepository<Bioanalyst>, IBioanalystRepository
    {
        private readonly LabTestAppointmentsContext _dbContext;
        public MockBioanalystRepository(LabTestAppointmentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            this.LoadData();
        }

        public override async Task AddAsync(Bioanalyst entity)
        {
            if (IsNull(entity))
            {
                throw new BioanalystNullException("El bioanalista es requerido, no puede ser nulo.");
            }

            if (await GetByIdAsync(entity.Id) is not null)
            {
                throw new BioanalystAlreadyExistsException("El bioanalista ingresado ya existe.");
            }

            Bioanalyst BioanalystToAdd = new()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                CreatedBy = "defaultUser",
                CreatedOn = DateTime.Now
            };

            await base.AddAsync(BioanalystToAdd);
        }

        public override async Task DeleteAsync(Bioanalyst entity)
        {
            if (IsNull(entity))
            {
                throw new BioanalystNullException("El bioanalista es requerido, no puede ser nulo.");
            }

            var BioanalystToRemove = await GetByIdAsync(entity.Id);

            if (BioanalystToRemove is null)
            {
                throw new BioanalystNotFoundException("El bioanalista a eliminar no fue encontrado.");
            }

            await base.DeleteAsync(BioanalystToRemove);
        }

        public override async Task<List<Bioanalyst>> GetAllAsync()
        {
            var Bioanalysts = await base.GetAllAsync();
            return Bioanalysts.OrderBy(b => b.FirstName).ToList();
        }

        public override async Task UpdateAsync(Bioanalyst entity, int id)
        {
            if (IsNull(entity))
            {
                throw new BioanalystNullException("El bioanalista es requerido, no puede ser nulp.");
            }

            var BioanalystToUpdate = await GetByIdAsync(entity.Id);

            if (BioanalystToUpdate is null)
            {
                throw new BioanalystNotFoundException("El bioanalista a actualizar no fue encontrado.");
            }

            await base.UpdateAsync(entity, id);
        }

        #region Private Methods
        private void LoadData()
        {

            if (!_dbContext.Set<Bioanalyst>().Any())
            {
                List<Bioanalyst> Bioanalysts = new()
            {
                new Bioanalyst()
                {
                    FirstName = "Julio",
                    LastName = "Matos",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new Bioanalyst()
                {
                    FirstName = "Tadeo",
                    LastName = "Rodríguez",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new Bioanalyst()
                {
                    FirstName = "Catherine",
                    LastName = "Melo",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new Bioanalyst()
                {
                    FirstName = "Paola",
                    LastName = "Garcia",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new Bioanalyst()
                {
                    FirstName = "Federico",
                    LastName = "Herrera",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                }

            };

                _dbContext.Set<Bioanalyst>().AddRange(Bioanalysts);
                _dbContext.SaveChanges();
            }

        }

        private bool IsNull(Bioanalyst Bioanalyst)
        {
            return Bioanalyst == null ? true : false;
        }
        #endregion
    }
}