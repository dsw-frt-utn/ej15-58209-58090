using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain
{
    public class Doctor : BaseEntity
    {
        public string name { get; set; }
        public string licenseNumber { get; set; }
        public bool isActive { get; set; }
        public Speciality speciality { get; set; }
    }
}
