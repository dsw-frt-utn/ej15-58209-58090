using Dsw2026Ej15.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data
{
    public class PersistenceEf : IPersistence
    {
        private readonly Dsw2026Ej15DbContext _context;

        public PersistenceEf(Dsw2026Ej15DbContext context)
        {
            _context = context;

        }
        async Task IPersistence.addDoctor(Doctor doctor)
        {
            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }

        async Task IPersistence.disableDoctor(Doctor doctor)
        {
            await _context.Doctors.Where(d => d.id == doctor.id).FirstOrDefaultAsync<Doctor>();
        }

        async Task<List<Doctor>> IPersistence.getActiveDoctors()
        {
            return await _context.Doctors.Where(d => d.isActive).ToListAsync<Doctor>();
        }

        async Task<Doctor?> IPersistence.getDoctorById(Guid id)
        {
            return await _context.Doctors.Where(d => d.id == id).FirstOrDefaultAsync<Doctor>();
        }

        async Task<List<Doctor>> IPersistence.getDoctors()
        {
            return await _context.Doctors.ToListAsync<Doctor>();
        }

        Task<Speciality> IPersistence.GetSpecialityById(Guid id)
        {
            return _context.Specialities.FirstOrDefaultAsync(s => s.id == id);
        }

        async Task IPersistence.removeDoctor(Doctor doctor)
        {

            _context.Remove(doctor);
            await _context.SaveChangesAsync();
        }


    }
}