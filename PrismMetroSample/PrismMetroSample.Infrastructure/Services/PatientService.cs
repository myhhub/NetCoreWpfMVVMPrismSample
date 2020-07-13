using PrismMetroSample.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismMetroSample.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        public List<Patient> GetAllPatients()
        {
            var allPatient = new List<Patient>()
            {
                new Patient(){Id=1,Name="Lina",Age=18,Sex="女",RoomNo="A-501"},
                new Patient(){Id=2,Name="Ryzen",Age=24,Sex="男",RoomNo="B-610"},
                new Patient(){Id=3,Name="Joy",Age=20,Sex="男",RoomNo="C-620"},
                new Patient(){Id=4,Name="Jack",Age=40,Sex="男",RoomNo="D-520"},
            };
            return allPatient;
        }
    }
}
