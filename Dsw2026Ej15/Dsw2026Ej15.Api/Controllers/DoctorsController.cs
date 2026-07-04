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
        public async Task<ActionResult<string>> insertarDoctorAsync([FromBody] DoctorBody doctor)
        {
            Doctor doctor1 = new Doctor();
            doctor1.name = doctor.Name;
            doctor1.isActive = true;
            doctor1.id = Guid.NewGuid();
            doctor1.speciality = await persistenceInMemory.GetSpecialityById(doctor.SpecialityId);
            doctor1.licenseNumber = doctor.LicenseNumber;
            persistenceInMemory.addDoctor(doctor1);
            return Created();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> obtenerDoctoresActivosAsync()
        {
            List<Doctor> doctores = new List<Doctor>();
            doctores = await persistenceInMemory.getActiveDoctors();
            return Ok(doctores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> obtenerDoctorPorIdAsync(Guid id)
        {
            Doctor? doctor = null;
            try
            {
                doctor = await persistenceInMemory.getDoctorById(id);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> deshabilitarDoctorAsync(Guid id)
        {
            try
            {
                await persistenceInMemory.disableDoctor(await persistenceInMemory.getDoctorById(id));
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
