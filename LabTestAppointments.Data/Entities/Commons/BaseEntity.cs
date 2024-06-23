namespace LabTestAppointments.Data.Entities.Commons
{
    public class BaseEntity : AuditableBaseEntity
    {
        public virtual int Id { get; set; }
    }
}
