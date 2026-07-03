using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain
{
    public interface IPersistence
    {
        Task addDoctor(Doctor doctor);

        Task<List<Doctor>> getDoctors();

        Task<List<Doctor>> getActiveDoctors();

        Task<Doctor?> getDoctorById(Guid id);

        Task disableDoctor(Doctor doctor);

        Task removeDoctor(Doctor doctor);

        Task<Speciality> GetSpecialityById(Guid id);
    }
}
