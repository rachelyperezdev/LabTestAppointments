using LabTestAppointments.Data.Contexts;
using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Exceptions.InsuranceProvider;
using LabTestAppointments.Data.Repositories.Mocks;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointments.Test
{
    public class UnitTestInsuranceProviderRepository
    {
        [Fact]
        public async Task AddAsync_NullInsuranceProviderEntity_ThrowsAInsuranceProviderNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            InsuranceProvider insuranceProvider = null;

            string message = "El proveedor de seguros es requerido, no puede ser nulo.";

            // Assert //
            var nullInsuranceProviderExcep = await Assert.ThrowsAsync<InsuranceProviderNullException>(async () => await repository.AddAsync(insuranceProvider));
            Assert.Equal(message, nullInsuranceProviderExcep.Message);
        }

        [Fact]
        public async Task AddAsync_ExistingInsuranceProviderEntity_ThrowsAInsuranceProviderAlreadyExistsException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            InsuranceProvider insuranceProvider = new()
            {
                Id = 2,
                Name = "Humano",
                Phone = "809-582-4196",
                CreatedBy = "DefaultUser",
                CreatedOn = DateTime.Now
            };

            string message = "El proveedor de seguros ingresado ya existe.";

            // Assert // 
            var existingInsuranceProviderExcep = await Assert.ThrowsAsync<InsuranceProviderAlreadyExistsException>(async () => await repository.AddAsync(insuranceProvider));
            Assert.Equal(message, existingInsuranceProviderExcep.Message);
        }

        [Fact]
        public async Task AddAsync_InsuranceProviderEntityWithMissingNameRequiredProperty_ThrowsAInsuranceProviderMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            InsuranceProvider insuranceProvider = new()
            {
                Name = string.Empty,
                Phone = "809-963-4528"
            };

            string nameMessage = "Debe ingresar el nombre del proveedor de seguros.";

            // Assert //
            var missingRequiredPropsInsuranceProviderExcep = await Assert.ThrowsAsync<InsuranceProviderMissingPropertiesException>(async () => await repository.AddAsync(insuranceProvider));
            Assert.Equal(nameMessage, missingRequiredPropsInsuranceProviderExcep.Message);
        }

        [Fact]
        public async Task AddAsync_InsuranceProviderEntityWithMissingPhoneRequiredProperty_ThrowsAInsuranceProviderMissingPropertiesException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                     .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                     .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            InsuranceProvider insuranceProvider = new()
            {
                Name = "Atlántica Seguros",
                Phone = string.Empty
            };

            string descMessage = "Debe ingresar el número telefónico del proveedor de seguros.";

            // Assert //
            var missingRequiredPropsInsuranceProviderExcep = await Assert.ThrowsAsync<InsuranceProviderMissingPropertiesException>(async () => await repository.AddAsync(insuranceProvider));
            Assert.Equal(descMessage, missingRequiredPropsInsuranceProviderExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NullInsuranceProviderEntity_ThrowsAInsuranceProviderNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                    .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                    .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            InsuranceProvider insuranceProvider = null;
            string message = "El proveedor de seguros es requerido, no puede ser nulo.";

            // Assert //
            var nullInsuranceProviderExcep = await Assert.ThrowsAsync<InsuranceProviderNullException>(async () => await repository.DeleteAsync(insuranceProvider));
            Assert.Equal(message, nullInsuranceProviderExcep.Message);
        }

        [Fact]
        public async Task DeleteAsync_NotExistingInsuranceProviderEntity_ThrowsAInsuranceProviderNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;
            LabTestAppointmentsContext context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            InsuranceProvider insuranceProvider = new InsuranceProvider()
            {
                Id = 49
            };

            string message = "El proveedor de seguros a eliminar no fue encontrado.";

            // Assert //
            var notFoundInsuranceProviderExcep = await Assert.ThrowsAsync<InsuranceProviderNotFoundException>(async () => await repository.DeleteAsync(insuranceProvider));
            Assert.Equal(message, notFoundInsuranceProviderExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NullInsuranceProviderEntity_ThrowsAInsuranceProviderNullException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            InsuranceProvider insuranceProvider = null;
            var insuranceProviderId = 1;

            string message = "El proveedor de seguros es requerido, no puede ser nulo.";

            // Assert //
            var nullInsuranceProviderExcep = await Assert.ThrowsAsync<InsuranceProviderNullException>(async () => await repository.UpdateAsync(insuranceProvider, insuranceProviderId));
            Assert.Equal(message, nullInsuranceProviderExcep.Message);
        }

        [Fact]
        public async Task UpdateAsync_NotExistingInsuranceProviderEntity_ThrowsAInsuranceProviderNotFoundException()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            InsuranceProvider insuranceProvider = new InsuranceProvider()
            {
                Id = 63
            };

            string message = "El proveedor de seguros a actualizar no fue encontrado.";

            // Assert //
            var notFoundInsuranceProviderExcep = await Assert.ThrowsAsync<InsuranceProviderNotFoundException>(async () => await repository.UpdateAsync(insuranceProvider, insuranceProvider.Id));
            Assert.Equal(message, notFoundInsuranceProviderExcep.Message);
        }

        [Fact]
        public async Task GetAllAsync_WhenCalled_ReturnsAllInsuranceProvidersEntitiesOrderAlphabetically()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            var result = await repository.GetAllAsync();

            // Assert //
            Assert.NotNull(result);
            Assert.Equal(5, result.Count);

            var insuranceProviders = result.ToList();
            Assert.Equal("BMI", insuranceProviders[0].Name);
            Assert.Equal("Futuro", insuranceProviders[1].Name);
            Assert.Equal("Humano", insuranceProviders[2].Name);
            Assert.Equal("MAPFRE", insuranceProviders[3].Name);
            Assert.Equal("Yunen", insuranceProviders[4].Name);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCalled_ReturnsInsuranceProviderEntity()
        {
            // Arrange //
            var options = new DbContextOptionsBuilder<LabTestAppointmentsContext>()
                      .UseInMemoryDatabase(databaseName: "LabTestAppointmentDb")
                      .Options;

            using var context = new LabTestAppointmentsContext(options);
            MockInsuranceProviderRepository repository = new MockInsuranceProviderRepository(context);

            // Act //
            var insuranceProvider = await repository.GetByIdAsync(2);

            // Assert //
            Assert.IsType<InsuranceProvider>(insuranceProvider);
        }
    }
}