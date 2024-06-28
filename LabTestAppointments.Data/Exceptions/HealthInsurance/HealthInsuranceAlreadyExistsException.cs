namespace LabTestAppointments.Data.Exceptions.HealthInsurance
{
    public class HealthInsuranceAlreadyExistsException : Exception
    {
        public HealthInsuranceAlreadyExistsException(string message) : base(message) { }
    }
}