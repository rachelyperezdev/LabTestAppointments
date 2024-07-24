using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.LabTest;
using LabTestAppointments.Data.Repositories.Mocks;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointment.Test
{
    public class UnitTestLabTestRepository
    {
        [Fact]
        public async Task AddAsync_NullLabTestEntity_ThrowsALabTestNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockLabTestRepository repository = new MockLabTestRepository(context);

            // Act //
            LabTest labTest = null;

            string message = "La prueba de laboratorio es requerida, no puede ser nula.";

            // Assert //
            var nullLabTestExcep = await Assert.ThrowsAsync<LabTestNullException>(async () => await repository.AddAsync(labTest));
            Assert.Equal(message, nullLabTestExcep.Message);
        }

        [Fact]
        public async Task AddAsync_ExistingLabTestEntity_ThrowsALabTestAlreadyExistsException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockLabTestRepository repository = new MockLabTestRepository(context);

            // Act //
            LabTest labTest = new()
            {
                Id = 2,
                Name = "Perfil lipídico",
                Description = "El perfil lipídico mide los niveles de colesterol total, HDL, LDL y triglicéridos en la sangre. " +
                                  "Es crucial para evaluar el riesgo de enfermedades cardiovasculares y para monitorear el tratamiento " +
                                  "de hiperlipidemias. Se requiere que el paciente esté en ayunas entre 9 y 12 horas antes de la " +
                                  "extracción de sangre.",
                CreatedBy = "DefaultUser",
                CreatedOn = DateTime.Now
            };

            string message = "La prueba de laboratorio ingresada ya existe.";

            // Assert // 
            var existingLabTestExcep = await Assert.ThrowsAsync<LabTestAlreadyExistsException>(async () => await repository.AddAsync(labTest));
            Assert.Equal(message, existingLabTestExcep.Message);
        }

        [Fact]
        public async Task AddAsync_LabTestEntityWithMissingNameRequiredProperty_ThrowsALabTestMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockLabTestRepository repository = new MockLabTestRepository(context);

            // Act //
            LabTest labTest = new()
            {
                Name = string.Empty,
                Description = "Prueba que evalúa los niveles de adiponectina en la sangre."
            };

            string nameMessage = "Debe ingresar el nombre de la prueba de laboratorio.";

            // Assert //
            var missingRequiredPropsLabTestExcep = await Assert.ThrowsAsync<LabTestMissingPropertiesException>(async () => await repository.AddAsync(labTest));
            Assert.Equal(nameMessage, missingRequiredPropsLabTestExcep.Message);
        }

        [Fact]
        public async Task AddAsync_LabTestEntityWithMissingDescriptionRequiredProperty_ThrowsALabTestMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockLabTestRepository repository = new MockLabTestRepository(context);

            // Act //
            LabTest labTest = new()
            {
                Name = "Prueba de adiponectina",
                Description = string.Empty
            };

            string descMessage = "Debe ingresar la descripción de la prueba de laboratorio.";

            // Assert //
            var missingRequiredPropsLabTestExcep = await Assert.ThrowsAsync<LabTestMissingPropertiesException>(async () => await repository.AddAsync(labTest));
            Assert.Equal(descMessage, missingRequiredPropsLabTestExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NullLabTestEntity_ThrowsALabTestNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                    .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                    .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockLabTestRepository repository = new MockLabTestRepository(context);

            // Act //
            LabTest labTest = null;
            string message = "La prueba de laboratorio es requerida, no puede ser nula.";

            // Assert //
            var nullLabTestExcep = await Assert.ThrowsAsync<LabTestNullException>(async () => await repository.DeleteAsync(labTest));
            Assert.Equal(message, nullLabTestExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NotExistingLabTestEntity_ThrowsALabTestNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockLabTestRepository repository = new MockLabTestRepository(context);

            // Act //
            LabTest labTest = new LabTest()
            {
                Id = 20
            };

            string message = "La prueba de laboratorio a eliminar no fue encontrada.";

            // Assert //
            var notFoundLabTestExcep = await Assert.ThrowsAsync<LabTestNotFoundException>(async () => await repository.DeleteAsync(labTest));
            Assert.Equal(message, notFoundLabTestExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NullLabTestEntity_ThrowsALabTestNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            var repository = new MockLabTestRepository(context);

            // Act //
            LabTest labTest = null;
            var labTestId = 1;

            string message = "La prueba de laboratorio es requerida, no puede ser nula.";

            // Assert //
            var nullLabTestExcep = await Assert.ThrowsAsync<LabTestNullException>(async () => await repository.UpdateAsync(labTest, labTestId));
            Assert.Equal(message, nullLabTestExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NotExistingLabTestEntity_ThrowsALabTestNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            var repository = new MockLabTestRepository(context);

            // Act //
            LabTest labTest = new LabTest()
            {
                Id = 20
            };

            string message = "La prueba de laboratorio a actualizar no fue encontrada.";

            // Assert //
            var notFoundLabTestExcep = await Assert.ThrowsAsync<LabTestNotFoundException>(async () => await repository.UpdateAsync(labTest, labTest.Id));
            Assert.Equal(message, notFoundLabTestExcep.Message);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnsAllLabTestsEntitiesOrderAlphabetically()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            var repository = new MockLabTestRepository(context);

            // Act //
            var result = await repository.GetAllAsync();

            // Assert //
            Assert.NotNull(result);
            Assert.Equal(10, result.Count);

            var labTests = result.ToList();
            Assert.Equal("Glucosa en sangre", labTests[0].Name);
            Assert.Equal("Hemograma completo (CBC)", labTests[1].Name);
            Assert.Equal("Perfil lipídico", labTests[2].Name);
            Assert.Equal("Prueba de anticuerpos antinucleares (ANA)", labTests[3].Name);
            Assert.Equal("Prueba de detección de VIH", labTests[4].Name);
            Assert.Equal("Pruebas de función hepática", labTests[5].Name);
            Assert.Equal("Pruebas de función renal", labTests[6].Name);
            Assert.Equal("Pruebas de función tiroidea", labTests[7].Name);
            Assert.Equal("Tiempo de protrombina (PT)", labTests[8].Name);
            Assert.Equal("Urinálisis", labTests[9].Name);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCalled_ReturnsLabTestEntity()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            var repository = new MockLabTestRepository(context);

            // Act //
            var labTest = await repository.GetByIdAsync(2);

            // Assert //
            Assert.IsType<LabTest>(labTest);
        }
    }
}
