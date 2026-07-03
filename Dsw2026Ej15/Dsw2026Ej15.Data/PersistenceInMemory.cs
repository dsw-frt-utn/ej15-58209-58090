using Dsw2026Ej15.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Doctor> doctors;

        private List<Speciality> specialities;

        public PersistenceInMemory()
        {
            doctors = new List<Doctor>();
            specialities = LoadSpecialities().Result;
        }
        public async Task addDoctor(Doctor doctor)
        {
            doctors.Add(doctor);
        }

        public async Task disableDoctor(Doctor doctor)
        {
            Doctor? doctor1 = getDoctorById(doctor.id).Result;

            doctors.Remove(doctor1);

            doctor1.isActive = false;

            doctors.Add(doctor1);
        }

        public async Task<List<Doctor>> getActiveDoctors()
        {
            return doctors.Where(x => x.isActive == true).ToList();
        }

        public async Task<Doctor?> getDoctorById(Guid id)
        {
            Doctor? doctor = null;

            doctor = doctors.FirstOrDefault(x => x.id == id);

            if (doctor == null)
            {
                throw new ArgumentNullException("id");
            }

            return doctor;
        }

        public async Task<List<Doctor>> getDoctors()
        {
            return doctors;
        }

        public async Task<Speciality> GetSpecialityById(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task<List<Speciality>> LoadSpecialities()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "specialities.json");

            if (!File.Exists(path))
            {
                return new List<Speciality>();
            }

            var json = File.ReadAllText(path);

            return JsonSerializer.Deserialize<List<Speciality>>(json)
                   ?? new List<Speciality>();
        }

        public async Task removeDoctor(Doctor doctor)
        {
            doctors.Remove(doctor);
        }
    }
}
