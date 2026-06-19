namespace Dsw2026Ej15.Api.Entities
{
    public record DoctorBody
    {
        public string Name { get; set; }
        public string LicenseNumber { get; set; }
        public Guid SpecialityId { get; set; }
    }
}
