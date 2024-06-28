using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.MedicalImage;
using LabTestAppointments.Data.Interfaces.Repositories;
using LabTestAppointments.Data.Repositories.Mocks.Generic;

namespace LabTestAppointments.Data.Repositories.Mocks
{
    public class MockMedicalImageRepository : MockGenericRepository<MedicalImage>, IMedicalImageRepository
    {
        private readonly LabTestAppointmentsContext _dbContext;
        public MockMedicalImageRepository(LabTestAppointmentsContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            this.LoadData();
        }

        public override async Task AddAsync(MedicalImage medicalImage)
        {
            if (IsNull(medicalImage))
            {
                throw new MedicalImageNullException("La imagen médica es requerida, no puede ser nula.");
            }

            if (await GetByIdAsync(medicalImage.Id) is not null)
            {
                throw new MedicalImageAlreadyExistsException("La imagen médica ingresada ya existe.");
            }

            MedicalImage medicalImageToAdd = new()
            {
                Id = medicalImage.Id,
                Name = medicalImage.Name,
                Description = medicalImage.Description,
                CreatedBy = "defaultUser",
                CreatedOn = DateTime.Now
            };

            await base.AddAsync(medicalImageToAdd);
        }

        public override async Task DeleteAsync(MedicalImage medicalImage)
        {
            if (IsNull(medicalImage))
            {
                throw new MedicalImageNullException("La imagen médica es requerida, no puede ser nula.");
            }

            var labTestToRemove = await GetByIdAsync(medicalImage.Id);

            if (labTestToRemove is null)
            {
                throw new MedicalImageNotFoundException("La imagen médica a eliminar no fue encontrada.");
            }

            await base.DeleteAsync(medicalImage);
        }

        public override async Task<List<MedicalImage>> GetAllAsync()
        {
            var medicalImages = await base.GetAllAsync();
            return medicalImages.OrderBy(mi => mi.Name).ToList();
        }

        public override async Task UpdateAsync(MedicalImage medicalImage, int id)
        {
            if (IsNull(medicalImage))
            {
                throw new MedicalImageNullException("La imagen médica es requerida, no puede ser nula.");
            }

            var medicalImageToUpdate = await GetByIdAsync(medicalImage.Id);

            if (medicalImageToUpdate is null)
            {
                throw new MedicalImageNotFoundException("La iamgen médica a actualizar no fue encontrada.");
            }

            await base.UpdateAsync(medicalImage, id);
        }

        #region Private Methods
        private void LoadData()
        {
            if (!_dbContext.Set<MedicalImage>().Any())
            {
                List<MedicalImage> medicalImages = new()
            {
                new MedicalImage()
                {
                    Name = "Radiografía de tórax",
                    Description = "La radiografía de tórax utiliza rayos X para obtener imágenes del pecho, incluyendo los pulmones, el corazón, " +
                                  "las costillas y el diafragma. Es esencial para diagnosticar neumonía, insuficiencia cardíaca, cáncer de pulmón " +
                                  "y otras condiciones. No se requiere preparación especial.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new MedicalImage()
                {
                    Name = "Tomografía computarizada (TC) de abdomen",
                    Description = "La TC de abdomen utiliza rayos X y tecnología computarizada para crear imágenes detalladas del abdomen. " +
                                  "Es útil para evaluar lesiones, infecciones, tumores y otras patologías abdominales. Puede requerir ayuno " +
                                  "y la ingestión de un contraste oral antes del examen.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new MedicalImage()
                {
                    Name = "Resonancia magnética (RM) cerebral",
                    Description = "La RM cerebral utiliza un campo magnético y ondas de radio para crear imágenes detalladas del cerebro y " +
                                  "el tronco encefálico. Es crucial para diagnosticar tumores, accidentes cerebrovasculares, aneurismas y " +
                                  "otras afecciones neurológicas. No se requiere ayuno, pero es importante informar si se tienen implantes " +
                                  "metálicos o dispositivos médicos.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new MedicalImage()
                {
                    Name = "Ecografía abdominal",
                    Description = "La ecografía abdominal utiliza ondas sonoras para producir imágenes de los órganos y estructuras del abdomen. " +
                                  "Es esencial para evaluar el hígado, la vesícula biliar, el páncreas, los riñones y otros órganos abdominales. " +
                                  "Generalmente, se requiere ayuno de 6 a 8 horas antes del examen.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new MedicalImage()
                {
                    Name = "Mamografía",
                    Description = "La mamografía utiliza rayos X de baja dosis para examinar las mamas. Es fundamental para la detección temprana " +
                                  "de cáncer de mama y otras anomalías mamarias. No se requiere ayuno, pero se recomienda no usar desodorantes " +
                                  "ni lociones en la axila o los senos el día del examen.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new MedicalImage()
                {
                    Name = "Densitometría ósea (DEXA)",
                    Description = "La densitometría ósea mide la densidad mineral ósea usando rayos X. Es crucial para diagnosticar osteoporosis " +
                                  "y evaluar el riesgo de fracturas. No se requiere ayuno, pero se debe evitar el uso de suplementos de calcio " +
                                  "al menos 24 horas antes del examen.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new MedicalImage()
                {
                    Name = "Gammagrafía ósea",
                    Description = "La gammagrafía ósea utiliza una pequeña cantidad de material radiactivo para examinar los huesos. Es útil " +
                                  "para detectar fracturas, infecciones óseas, artritis y cánceres que se han diseminado a los huesos. " +
                                  "No se requiere ayuno, pero es importante beber mucha agua antes y después del examen para ayudar a " +
                                  "eliminar el material radiactivo del cuerpo.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new MedicalImage()
                {
                    Name = "Ecocardiograma",
                    Description = "El ecocardiograma utiliza ondas sonoras para producir imágenes del corazón. Es fundamental para evaluar " +
                                  "la estructura y función del corazón, detectando condiciones como enfermedades de las válvulas, insuficiencia " +
                                  "cardíaca y cardiopatías congénitas. No se requiere ayuno para este examen.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new MedicalImage()
                {
                    Name = "Angiografía coronaria",
                    Description = "La angiografía coronaria utiliza un tinte especial y rayos X para ver cómo fluye la sangre a través de las " +
                                  "arterias coronarias. Es crucial para diagnosticar enfermedades cardíacas, como arterias bloqueadas o estrechas. " +
                                  "Se requiere ayuno de 6 a 8 horas antes del procedimiento y el paciente debe seguir las indicaciones del médico " +
                                  "respecto a la medicación.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                },
                new MedicalImage()
                {
                    Name = "Ultrasonido Doppler",
                    Description = "El ultrasonido Doppler utiliza ondas sonoras para evaluar el flujo sanguíneo en los vasos sanguíneos. Es " +
                                  "esencial para detectar coágulos sanguíneos, bloqueos y el flujo sanguíneo reducido en arterias y venas. " +
                                  "No se requiere ayuno, pero es importante informar al médico sobre cualquier medicamento que se esté tomando.",
                    CreatedBy = "DefaultUser",
                    CreatedOn = DateTime.Now
                }

            };

                _dbContext.Set<MedicalImage>().AddRange(medicalImages);
                _dbContext.SaveChanges();
            }
        }

        private bool IsNull(MedicalImage medicalImage)
        {
            return medicalImage == null ? true : false;
        }
        #endregion
    }
}