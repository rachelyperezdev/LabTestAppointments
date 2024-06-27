using LabTestAppointments.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointments.Data.Contexts
{
    public class LabTestAppointmentsContext : DbContext
    {
        public LabTestAppointmentsContext(DbContextOptions<LabTestAppointmentsContext> options) : base(options)
        {
           
        }
        DbSet<LabTest> LabTests { get; set; }
        DbSet<MedicalImage> MedicalImages { get; set; }
        DbSet<Bioanalyst> Bioanalyst { get; set; }
        DbSet<InsuranceProvider> InsuranceProviders { get; set; }
        DbSet<HealthInsurance> HealthInsurances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Primary keys
            modelBuilder.Entity<LabTest>().HasKey(lt => lt.Id);
            modelBuilder.Entity<MedicalImage>().HasKey(mi => mi.Id);
            modelBuilder.Entity<Bioanalyst>().HasKey(mi => mi.Id);
            modelBuilder.Entity<InsuranceProvider>().HasKey(mi => mi.Id);
            modelBuilder.Entity<HealthInsurance>().HasKey(mi => mi.Id);
            #endregion

            #region Relationships
            modelBuilder.Entity<InsuranceProvider>()
                .HasMany(ip => ip.HealthInsurances)
                .WithOne(hi => hi.InsuranceProvider)
                .HasForeignKey(ip => ip.InsuranceProviderId);
            #endregion
        }
    }
}