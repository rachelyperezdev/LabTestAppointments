using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Repositories.Mocks;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.HealthInsurance;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointments.Test
{
    public class UnitTestHealthInsuranceRepository
    {
        [Fact]
        public async Task AddAsync_NullHealthInsuranceEntity_ThrowsAHealthInsuranceNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            HealthInsurance healthInsurance = null;

            string message = "El seguro médico es requerido, no puede ser nulo.";

            // Assert //
            var nullHealthInsuranceExcep = await Assert.ThrowsAsync<HealthInsuranceNullException>(async () => await repository.AddAsync(healthInsurance));
            Assert.Equal(message, nullHealthInsuranceExcep.Message);
        }

        [Fact]
        public async Task AddAsync_ExistingHealthInsuranceEntity_ThrowsAHealthInsuranceAlreadyExistsException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            HealthInsurance healthInsurance = new()
            {
                Id = 4,
                Plan = "Infinity",
                InsuranceProviderId = 5,
                CreatedBy = "DefaultUser",
                CreatedOn = DateTime.Now
            };

            string message = "El seguro médico ingresado ya existe.";

            // Assert // 
            var existingHealthInsuranceExcep = await Assert.ThrowsAsync<HealthInsuranceAlreadyExistsException>(async () => await repository.AddAsync(healthInsurance));
            Assert.Equal(message, existingHealthInsuranceExcep.Message);
        }

        [Fact]
        public async Task AddAsync_HealthInsuranceEntityWithMissingPlanRequiredProperty_ThrowsAHealthInsuranceMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            HealthInsurance healthInsurance = new()
            {
                Plan = string.Empty,
                InsuranceProviderId = 1
            };

            string planMessage = "Debe ingresar el plan del seguro médico.";

            // Assert //
            var missingRequiredPropHealthInsuranceExcep = await Assert.ThrowsAsync<HealthInsuranceMissingPropertiesException>(async () => await repository.AddAsync(healthInsurance));
            Assert.Equal(planMessage, missingRequiredPropHealthInsuranceExcep.Message);
        }

        [Fact]
        public async Task AddAsync_HealthInsuranceEntityWithMissingInsuranceProviderIdRequiredProperty_ThrowsAHealthInsuranceMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            HealthInsurance healthInsurance = new()
            {
                Plan = "Silver",
                InsuranceProviderId = -1
            };

            string descMessage = "Debe ingresar el id del proveedor del seguro médico y debe ser 0 o mayor.";

            // Assert //
            var missingRequiredPropHealthInsuranceExcep = await Assert.ThrowsAsync<HealthInsuranceMissingPropertiesException>(async () => await repository.AddAsync(healthInsurance));
            Assert.Equal(descMessage, missingRequiredPropHealthInsuranceExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NullHealthInsuranceEntity_ThrowsAHealthInsuranceNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                    .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                    .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            HealthInsurance healthInsurance = null;
            string message = "El seguro médico es requerido, no puede ser nulo.";

            // Assert //
            var nullHealthInsuranceExcep = await Assert.ThrowsAsync<HealthInsuranceNullException>(async () => await repository.DeleteAsync(healthInsurance));
            Assert.Equal(message, nullHealthInsuranceExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NotExistingHealthInsuranceEntity_ThrowsAHealthInsuranceNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            HealthInsurance healthInsurance = new HealthInsurance()
            {
                Id = 23
            };

            string message = "El seguro médico a eliminar no fue encontrado.";

            // Assert //
            var notFoundHealthInsuranceExcep = await Assert.ThrowsAsync<HealthInsuranceNotFoundException>(async () => await repository.DeleteAsync(healthInsurance));
            Assert.Equal(message, notFoundHealthInsuranceExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NullHealthInsuranceEntity_ThrowsAHealthInsuranceNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            HealthInsurance healthInsurance = null;
            var healthInsuranceId = 1;

            string message = "El seguro médico es requerido, no puede ser nulo.";

            // Assert //
            var nullHealthInsuranceExcep = await Assert.ThrowsAsync<HealthInsuranceNullException>(async () => await repository.UpdateAsync(healthInsurance, healthInsuranceId));
            Assert.Equal(message, nullHealthInsuranceExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NotExistingHealthInsuranceEntity_ThrowsAHealthInsuranceNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            HealthInsurance healthInsurance = new HealthInsurance()
            {
                Id = 46
            };

            string message = "El seguro médico a actualizar no fue encontrado.";

            // Assert //
            var notFoundHealthInsuranceExcep = await Assert.ThrowsAsync<HealthInsuranceNotFoundException>(async () => await repository.UpdateAsync(healthInsurance, healthInsurance.Id));
            Assert.Equal(message, notFoundHealthInsuranceExcep.Message);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnsAllHealthInsurancesEntitiesOrderAlphabetically()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            var result = await repository.GetAllAsync();

            // Assert //
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);

            var healthInsurances = result.ToList();

            Assert.Equal("Gold", healthInsurances[0].Plan);
            Assert.Equal(3, healthInsurances[0].InsuranceProviderId);

            Assert.Equal("Infinity", healthInsurances[1].Plan);
            Assert.Equal(5, healthInsurances[1].InsuranceProviderId);

            Assert.Equal("Premium", healthInsurances[2].Plan);
            Assert.Equal(2, healthInsurances[2].InsuranceProviderId);

            Assert.Equal("Regular", healthInsurances[3].Plan);
            Assert.Equal(1, healthInsurances[3].InsuranceProviderId);

            Assert.Equal("Salud Superior", healthInsurances[4].Plan);
            Assert.Equal(4, healthInsurances[4].InsuranceProviderId);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCalled_ReturnsHealthInsuranceEntity()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Act //
            var healthInsurance = await repository.GetByIdAsync(3);

            // Assert //
            Assert.IsType<HealthInsurance>(healthInsurance);
        }

        [Fact]
        public async Task GetAllWithIncludeAsync_WhenCalled_ReturnsAllHealthInsurancesEntitiesIncludingInsuranceProvidersOrderAlphabetically()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);
            MockInsuranceProviderRepository ipRepository = new MockInsuranceProviderRepository(context);

            // Act //
            var result = await repository.GetAllWithIncludeAsync(new List<string>() { "InsuranceProvider" });

            // Assert //
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);

            var healthInsurances = result.ToList();

            Assert.Equal("Gold", healthInsurances[0].Plan);
            Assert.Equal("Infinity", healthInsurances[1].Plan);
            Assert.Equal("Premium", healthInsurances[2].Plan);
            Assert.Equal("Regular", healthInsurances[3].Plan);
            Assert.Equal("Salud Superior", healthInsurances[4].Plan);

            Assert.All(result, ip => Assert.NotNull(ip.InsuranceProvider));
        }

        [Fact]
        public async Task GetByIdWithIncludeAsync_WhenCalled_ReturnsAHealthInsuranceEntityIncludingItsInsuranceProvider()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                 .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                 .Options;
            using var context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository ipRepository = new MockInsuranceProviderRepository(context);
            MockHealthInsuranceRepository repository = new MockHealthInsuranceRepository(context);

            // Test Data //
            var insuranceProvider = new InsuranceProvider
            {
                Id = 1,
                Name = "Humano",
                Phone = "809-582-4196",
                CreatedBy = "DefaultUser",
                CreatedOn = DateTime.Now
            };

            var healthInsurance = new HealthInsurance
            {
                Id = 1,
                Plan = "Regular",
                InsuranceProviderId = 1,
                CreatedBy = "DefaultUser",
                CreatedOn = DateTime.Now
            };

            // Act //
            var result = await repository.GetByIdWithIncludeAsync(1, new List<string>() { "InsuranceProvider" });

            // Assert //
            Assert.NotNull(result);
            Assert.IsType<HealthInsurance>(result);
            Assert.Equal(healthInsurance.Id, result.Id);
            Assert.Equal(healthInsurance.Plan, result.Plan);
            Assert.NotNull(result.InsuranceProvider);
            Assert.Equal(insuranceProvider.Id, result.InsuranceProvider.Id);
            Assert.Equal(insuranceProvider.Name, result.InsuranceProvider.Name);
        }
    }
}
