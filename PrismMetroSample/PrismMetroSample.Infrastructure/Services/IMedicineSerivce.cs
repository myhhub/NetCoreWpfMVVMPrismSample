using PrismMetroSample.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismMetroSample.Infrastructure.Services
{
   public interface IMedicineSerivce
    {
        List<Medicine> GetAllMedicines();

        List<Recipe> GetRecipesByPatientId(int patientId);
    }
}
