using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.Bioanalyst;
using LabTestAppointments.Data.Repositories.Mocks;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointments.Test
{
    public class UnitTestBioanalystRepository
    {
        [Fact]
        public async Task AddAsync_NullBioanalystEntity_ThrowsABioanalystNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            Bioanalyst bioanalyst = null;

            string message = "El bioanalista es requerido, no puede ser nulo.";

            // Assert //
            var nullBioanalystExcep = await Assert.ThrowsAsync<BioanalystNullException>(async () => await repository.AddAsync(bioanalyst));
            Assert.Equal(message, nullBioanalystExcep.Message);
        }

        [Fact]
        public async Task AddAsync_ExistingBioanalystEntity_ThrowsABioanalystAlreadyExistsException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            Bioanalyst bioanalyst = new()
            {
                Id = 3,
                FirstName = "Paola",
                LastName = "Garcia",
                CreatedBy = "DefaultUser",
                CreatedOn = DateTime.Now
            };

            string message = "El bioanalista ingresado ya existe.";

            // Assert // 
            var existingBioanalystExcep = await Assert.ThrowsAsync<BioanalystAlreadyExistsException>(async () => await repository.AddAsync(bioanalyst));
            Assert.Equal(message, existingBioanalystExcep.Message);
        }

        [Fact]
        public async Task AddAsync_BioanalystEntityWithMissingFirstNameRequiredProperty_ThrowsABioanalystMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            Bioanalyst bioanalyst = new()
            {
                FirstName = string.Empty,
                LastName = "Abreu"
            };

            string firstNameMessage = "Debe ingresar el nombre del bioanalista.";

            // Assert //
            var missingRequiredPropsBioanalystExcep = await Assert.ThrowsAsync<BioanalystMissingPropertiesException>(async () => await repository.AddAsync(bioanalyst));
            Assert.Equal(firstNameMessage, missingRequiredPropsBioanalystExcep.Message);
        }

        [Fact]
        public async Task AddAsync_BioanalystEntityWithMissingLastNameRequiredProperty_ThrowsABioanalystMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            Bioanalyst bioanalyst = new()
            {
                FirstName = "Lucas",
                LastName = string.Empty
            };

            string lastNameMessage = "Debe ingresar el apellido del bioanalista.";

            // Assert //
            var missingRequiredPropsBioanalystExcep = await Assert.ThrowsAsync<BioanalystMissingPropertiesException>(async () => await repository.AddAsync(bioanalyst));
            Assert.Equal(lastNameMessage, missingRequiredPropsBioanalystExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NullBioanalystEntity_ThrowsABioanalystNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                    .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                    .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            Bioanalyst bioanalyst = null;
            string message = "El bioanalista es requerido, no puede ser nulo.";

            // Assert //
            var nullBioanalystExcep = await Assert.ThrowsAsync<BioanalystNullException>(async () => await repository.DeleteAsync(bioanalyst));
            Assert.Equal(message, nullBioanalystExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NotExistingBioanalystEntity_ThrowsABioanalystNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            Bioanalyst bioanalyst = new Bioanalyst()
            {
                Id = 12
            };

            string message = "El bioanalista a eliminar no fue encontrado.";

            // Assert //
            var notFoundBioanalystExcep = await Assert.ThrowsAsync<BioanalystNotFoundException>(async () => await repository.DeleteAsync(bioanalyst));
            Assert.Equal(message, notFoundBioanalystExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NullBioanalystEntity_ThrowsABioanalystNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            Bioanalyst bioanalyst = null;
            var bioanalystId = 1;

            string message = "El bioanalista es requerido, no puede ser nulo.";

            // Assert //
            var nullBioanalystExcep = await Assert.ThrowsAsync<BioanalystNullException>(async () => await repository.UpdateAsync(bioanalyst, bioanalystId));
            Assert.Equal(message, nullBioanalystExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NotExistingBioanalystEntity_ThrowsABioanalystNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            Bioanalyst bioanalyst = new Bioanalyst()
            {
                Id = 33
            };

            string message = "El bioanalista a actualizar no fue encontrado.";

            // Assert //
            var notFoundBioanalystExcep = await Assert.ThrowsAsync<BioanalystNotFoundException>(async () => await repository.UpdateAsync(bioanalyst, bioanalyst.Id));
            Assert.Equal(message, notFoundBioanalystExcep.Message);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnsAllBioanalystsEntitiesOrderAlphabetically()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            var result = await repository.GetAllAsync();

            // Assert //
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);

            var bioanalysts = result.ToList();
            Assert.Equal("Melo", bioanalysts[0].LastName);
            Assert.Equal("Catherine", bioanalysts[0].FirstName);
            Assert.Equal("Herrera", bioanalysts[1].LastName);
            Assert.Equal("Federico", bioanalysts[1].FirstName);
            Assert.Equal("Matos", bioanalysts[2].LastName);
            Assert.Equal("Julio", bioanalysts[2].FirstName);
            Assert.Equal("Garcia", bioanalysts[3].LastName);
            Assert.Equal("Paola", bioanalysts[3].FirstName);
            Assert.Equal("Rodríguez", bioanalysts[4].LastName);
            Assert.Equal("Tadeo", bioanalysts[4].FirstName);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCalled_ReturnsBioanalystEntity()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockBioanalystRepository repository = new MockBioanalystRepository(context);

            // Act //
            var bioanalyst = await repository.GetByIdAsync(4);

            // Assert //
            Assert.IsType<Bioanalyst>(bioanalyst);
        }
    }
}