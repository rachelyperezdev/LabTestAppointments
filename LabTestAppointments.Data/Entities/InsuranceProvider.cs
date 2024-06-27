using LabTestAppointments.Data.Entities.Commons;

namespace LabTestAppointments.Data.Entities
{
    public class InsuranceProvider : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public ICollection<HealthInsurance>? HealthInsurances { get; set; }
    }
}