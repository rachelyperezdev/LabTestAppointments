using LabTestAppointments.Data.Entities.Commons;

namespace LabTestAppointments.Data.Entities
{
    public class HealthInsurance : BaseEntity
    {
        public string Plan { get; set; }
        public int InsuranceProviderId { get; set; }
    }
}
