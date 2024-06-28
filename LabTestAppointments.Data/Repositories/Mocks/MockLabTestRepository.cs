using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.LabTest;
using LabTestAppointments.Data.Interfaces.Repositories;
using LabTestAppointments.Data.Repositories.Mocks.Generic;

namespace LabTestAppointments.Data.Repositories.Mocks
{
    public class MockLabTestRepository : MockGenericRepository<LabTest>, ILabTestRepository
    {
        private readonly LabTestAppointmentsContext _dbContext;
        public MockLabTestRepository(LabTestAppointmentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            this.LoadData();
        }

        public override async Task AddAsync(LabTest entity)
        {
            if (IsNull(entity))
            {
                throw new LabTestNullException("La prueba de laboratorio es requerida, no puede ser nula.");
            }

            if (await GetByIdAsync(entity.Id) is not null)
            {
                throw new LabTestAlreadyExistsException("La prueba de laboratorio ingresada ya existe.");
            }

            LabTest labTestToAdd = new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                CreatedBy = "defaultUser",
                CreatedOn = DateTime.Now
            };

            await base.AddAsync(labTestToAdd);
        }

        public override async Task DeleteAsync(LabTest entity)
        {
            if (IsNull(entity))
            {
                throw new LabTestNullException("La prueba de laboratorio es requerida, no puede ser nula.");
            }

            var labTestToRemove = await GetByIdAsync(entity.Id);

            if (labTestToRemove is null)
            {
                throw new LabTestNotFoundException("La prueba de laboratorio a eliminar no fue encontrada.");
            }

            await base.DeleteAsync(labTestToRemove);
        }

        public override async Task<List<LabTest>> GetAllAsync()
        {
            var labTests = await base.GetAllAsync();
            return labTests.OrderBy(lb => lb.Name).ToList();
        }

        public override async Task UpdateAsync(LabTest entity, int id)
        {
            if (IsNull(entity))
            {
                throw new LabTestNullException("La prueba de laboratorio es requerida, no puede ser nula.");
            }

            var labTestToUpdate = await GetByIdAsync(entity.Id);

            if (labTestToUpdate is null)
            {
                throw new LabTestNotFoundException("La prueba de laboratorio a actualizar no fue encontrada.");
            }

            await base.UpdateAsync(entity, id);
        }

        #region Private Methods
        private void LoadData()
        {

            if (!_dbContext.Set<LabTest>().Any())
            {
                List<LabTest> labTests = new()
            {
                new LabTest()
                {
                    Name = "Hemograma completo (CBC)",
                    Description = "El hemograma completo proporciona un análisis detallado " +
                                  "de los componentes celulares de la sangre, incluyendo glóbulos rojos, " +
                                  "glóbulos blancos y plaquetas. Es esencial para evaluar el estado general de salud, " +
                                  "diagnosticar anemia, infecciones y muchas otras condiciones médicas. No se requiere ayuno para esta prueba",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new LabTest()
                {
                    Name = "Glucosa en sangre",
                    Description = "Esta prueba mide los niveles de glucosa en la sangre, ayudando en el diagnóstico y " +
                                  "manejo de la diabetes mellitus y otras condiciones relacionadas con el metabolismo " +
                                  "de carbohidratos. Para obtener resultados precisos, se recomienda que el paciente " +
                                  "esté en ayunas al menos 8 horas antes de la prueba.\r\n\r\n",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new LabTest()
                {
                    Name = "Perfil lipídico",
                    Description = "El perfil lipídico mide los niveles de colesterol total, HDL, LDL y triglicéridos en la sangre. " +
                                  "Es crucial para evaluar el riesgo de enfermedades cardiovasculares y para monitorear el tratamiento " +
                                  "de hiperlipidemias. Se requiere que el paciente esté en ayunas entre 9 y 12 horas antes de la " +
                                  "extracción de sangre.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new LabTest()
                {
                    Name = "Pruebas de función hepática",
                    Description = "Estas pruebas evalúan la función del hígado midiendo enzimas hepáticas como ALT, AST, ALP y " +
                                  "niveles de bilirrubina. Son vitales para diagnosticar y monitorear enfermedades hepáticas y daño hepático. " +
                                  "No se requiere ayuno, aunque en algunos casos específicos, el médico puede recomendarlo.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new LabTest()
                {
                    Name = "Pruebas de función renal",
                    Description = "Miden los niveles de creatinina y nitrógeno ureico en sangre (BUN), así como otros marcadores, " +
                                  "para evaluar la función renal y detectar enfermedades renales y su progresión. Generalmente, " +
                                  "no se requiere ayuno para estas pruebas, pero se recomienda seguir las indicaciones específicas " +
                                  "del médico.\r\n\r\n",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new LabTest()
                {
                    Name = "Urinálisis",
                    Description = "El análisis de orina incluye exámenes físicos, químicos y microscópicos de la orina para " +
                                  "detectar infecciones, enfermedades renales, diabetes y otras condiciones médicas. No se " +
                                  "requiere ayuno. Se recomienda recolectar la primera orina de la mañana para obtener resultados " +
                                  "más precisos.\r\n\r\n",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new LabTest()
                {
                    Name = "Pruebas de función tiroidea",
                    Description = "Estas pruebas miden los niveles de hormonas tiroideas (TSH, T3 y T4) para evaluar la función de " +
                                  "la glándula tiroides. Son esenciales para el diagnóstico y manejo de trastornos como el " +
                                  "hipotiroidismo y el hipertiroidismo. No se requiere ayuno para estas pruebas.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new LabTest()
                {
                    Name = "Tiempo de protrombina (PT)",
                    Description = "Mide el tiempo que tarda la sangre en coagular. Es fundamental para evaluar trastornos de la " +
                                  "coagulación, así como para monitorear el tratamiento con anticoagulantes. No se requiere ayuno " +
                                  "para esta prueba.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new LabTest()
                {
                    Name = "Prueba de anticuerpos antinucleares (ANA)",
                    Description = "Detecta anticuerpos que atacan el núcleo de las células. Es crucial para el diagnóstico de " +
                                  "enfermedades autoinmunes, como el lupus eritematoso sistémico y otras condiciones relacionadas. " +
                                  "No se requiere ayuno para esta prueba.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new LabTest()
                {
                    Name = "Prueba de detección de VIH",
                    Description = "Esta prueba detecta anticuerpos contra el VIH en la sangre. Es esencial para el diagnóstico " +
                                  "temprano, tratamiento y monitoreo de la infección por VIH/SIDA. No se requiere ayuno para esta " +
                                  "prueba, aunque se recomienda evitar el consumo de alimentos y bebidas (excepto agua) al menos " +
                                  "una hora antes de la prueba para mejorar la precisión de los resultados.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                }

            };

                _dbContext.Set<LabTest>().AddRange(labTests);
                _dbContext.SaveChanges();
            }

        }

        private bool IsNull(LabTest labTest)
        {
            return labTest == null ? true : false;
        }
        #endregion
    }
}