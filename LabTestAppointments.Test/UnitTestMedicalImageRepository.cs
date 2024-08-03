using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.MedicalImage;
using LabTestAppointments.Data.Repositories.Mocks;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointments.Test
{
    public class UnitTestMedicalImageRepository
    {
        [Fact]
        public async Task AddAsync_NullMedicalImageEntity_ThrowsAMedicalImagesNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            MedicalImage medicalImage = null;

            string message = "La imagen médica es requerida, no puede ser nula.";

            // Assert //
            var nullMedicalImgExcep = await Assert.ThrowsAsync<MedicalImageNullException>(async () => await repository.AddAsync(medicalImage));
            Assert.Equal(message, nullMedicalImgExcep.Message);
        }

        [Fact]
        public async Task AddAsync_ExistingMedicalImageEntity_ThrowsAMedicalImageAlreadyExistsException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            MedicalImage medicalImage = new()
            {
                Id = 1,
                Name = "Radiografía de tórax",
                Description = "La radiografía de tórax utiliza rayos X para obtener imágenes del pecho, incluyendo los pulmones, el corazón, " +
                                  "las costillas y el diafragma. Es esencial para diagnosticar neumonía, insuficiencia cardíaca, cáncer de pulmón " +
                                  "y otras condiciones. No se requiere preparación especial.",
                CreatedBy = "DefaultUser",
                CreatedOn = DateTime.Now
            };

            string message = "La imagen médica ingresada ya existe.";

            // Assert // 
            var existingLabTestExcep = await Assert.ThrowsAsync<MedicalImageAlreadyExistsException>(async () => await repository.AddAsync(medicalImage));
            Assert.Equal(message, existingLabTestExcep.Message);
        }

        [Fact]
        public async Task AddAsync_MedicalImageEntityWithMissingNameRequiredProperty_ThrowsAMedicalImageMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            MedicalImage medicalImage = new()
            {
                Name = string.Empty,
                Description = "El ultrasonido Doppler utiliza ondas sonoras para evaluar el flujo sanguíneo en los vasos sanguíneos. Es " +
                  "esencial para detectar coágulos sanguíneos, bloqueos y el flujo sanguíneo reducido en arterias y venas. " +
                  "No se requiere ayuno, pero es importante informar al médico sobre cualquier medicamento que se esté tomando."
            };

            string nameMessage = "Debe ingresar el nombre de la imagen médica.";

            // Assert //
            var missingRequiredPropsMedicalImgExcep = await Assert.ThrowsAsync<MedicalImageMissingPropertiesException>(async () => await repository.AddAsync(medicalImage));
            Assert.Equal(nameMessage, missingRequiredPropsMedicalImgExcep.Message);
        }

        [Fact]
        public async Task AddAsync_MedicalImageEntityWithMissingDescriptionRequiredProperty_ThrowsAMedicalImageMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            MedicalImage medicalImage = new()
            {
                Name = "Ultrasonido Doppler",
                Description = string.Empty
            };

            string descMessage = "Debe ingresar la descripción de la imagen médica.";

            // Assert //
            var missingRequiredPropsMedicalImgExcep = await Assert.ThrowsAsync<MedicalImageMissingPropertiesException>(async () => await repository.AddAsync(medicalImage));
            Assert.Equal(descMessage, missingRequiredPropsMedicalImgExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NullMedicalImageEntity_ThrowsAMedicalImageNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                    .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                    .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            MedicalImage medicalImage = null;
            string message = "La imagen médica es requerida, no puede ser nula.";

            // Assert //
            var nullMedicalImgExcep = await Assert.ThrowsAsync<MedicalImageNullException>(async () => await repository.DeleteAsync(medicalImage));
            Assert.Equal(message, nullMedicalImgExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NotExistingMedicalImageEntity_ThrowsAMedicalImageNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            MedicalImage medicalImage = new MedicalImage()
            {
                Id = 16
            };

            string message = "La imagen médica a eliminar no fue encontrada.";

            // Assert //
            var notFoundMedicalImgExcep = await Assert.ThrowsAsync<MedicalImageNotFoundException>(async () => await repository.DeleteAsync(medicalImage));
            Assert.Equal(message, notFoundMedicalImgExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NullMedicalImageEntity_ThrowsAMedicalImageNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            MedicalImage medicalImage = null;
            var medicalImageId = 1;

            string message = "La imagen médica es requerida, no puede ser nula.";

            // Assert //
            var nullMedicalImgExcep = await Assert.ThrowsAsync<MedicalImageNullException>(async () => await repository.UpdateAsync(medicalImage, medicalImageId));
            Assert.Equal(message, nullMedicalImgExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NotExistingMedicalImageEntity_ThrowsAMedicalImageNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            MedicalImage medicalImage = new MedicalImage()
            {
                Id = 16
            };

            string message = "La imagen médica a actualizar no fue encontrada.";

            // Assert //
            var notFoundMedicalImgExcep = await Assert.ThrowsAsync<MedicalImageNotFoundException>(async () => await repository.UpdateAsync(medicalImage, medicalImage.Id));
            Assert.Equal(message, notFoundMedicalImgExcep.Message);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnsAllMedicalImagesEntitiesOrderAlphabetically()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            var result = await repository.GetAllAsync();

            // Assert //
            Assert.NotNull(result);
            Assert.Equal(10, result.Count);

            var medicalImages = result.ToList();
            Assert.Equal("Angiografía coronaria", medicalImages[0].Name);
            Assert.Equal("Densitometría ósea (DEXA)", medicalImages[1].Name);
            Assert.Equal("Ecocardiograma", medicalImages[2].Name);
            Assert.Equal("Ecografía abdominal", medicalImages[3].Name);
            Assert.Equal("Gammagrafía ósea", medicalImages[4].Name);
            Assert.Equal("Mamografía", medicalImages[5].Name);
            Assert.Equal("Radiografía de tórax", medicalImages[6].Name);
            Assert.Equal("Resonancia magnética (RM) cerebral", medicalImages[7].Name);
            Assert.Equal("Tomografía computarizada (TC) de abdomen", medicalImages[8].Name);
            Assert.Equal("Ultrasonido Doppler", medicalImages[9].Name);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCalled_ReturnsMedicalImageEntity()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockMedicalImageRepository repository = new MockMedicalImageRepository(context);

            // Act //
            var medicalImage = await repository.GetByIdAsync(2);

            // Assert //
            Assert.IsType<MedicalImage>(medicalImage);
        }
    }
}