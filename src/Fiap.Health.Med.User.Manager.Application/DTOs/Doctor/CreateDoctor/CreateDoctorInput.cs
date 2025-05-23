﻿using Fiap.Health.Med.User.Manager.Domain.Enum;

namespace Fiap.Health.Med.User.Manager.Application.DTOs.Doctor.CreateDoctor
{
    public class CreateDoctorInput
    {
        public int CrmNumber { get; set; }
        public string CrmUf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EMedicalSpecialty MedicalSpecialty { get; set; }
    }
}
