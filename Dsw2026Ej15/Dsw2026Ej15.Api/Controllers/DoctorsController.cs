using Dsw2026Ej15.Api.Entities;
using Dsw2026Ej15.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Dsw2026Ej15.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IPersistence persistenceInMemory;
        public DoctorsController(IPersistence persistence)
        {
            persistenceInMemory = persistence;
        }
        [HttpPost]
        public ActionResult<string> insertarDoctor([FromBody] DoctorBody doctor)
        {
            Doctor doctor1 = new Doctor();
            doctor1.name = doctor.Name;
            doctor1.isActive = true;
            doctor1.id = Guid.NewGuid();
            doctor1.speciality = persistenceInMemory.GetSpecialityById(doctor.SpecialityId).Result;
            doctor1.licenseNumber = doctor.LicenseNumber;
            persistenceInMemory.addDoctor(doctor1);
            return Created();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Doctor>> obtenerDoctoresActivos()
        {
            List<Doctor> doctores = new List<Doctor>();
            doctores = persistenceInMemory.getActiveDoctors().Result;
            return Ok(doctores);
        }

        [HttpGet("{id}")]
        public ActionResult<Doctor> obtenerDoctorPorId(Guid id)
        {
            Doctor? doctor = null;
            try
            {
                doctor = persistenceInMemory.getDoctorById(id).Result;
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public ActionResult<string> deshabilitarDoctor(Guid id)
        {
            try
            {
                persistenceInMemory.disableDoctor(persistenceInMemory.getDoctorById(id).Result);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
