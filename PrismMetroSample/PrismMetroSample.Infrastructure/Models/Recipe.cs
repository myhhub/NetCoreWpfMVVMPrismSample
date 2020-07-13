using System;
using System.Collections.Generic;
using System.Text;

namespace PrismMetroSample.Infrastructure.Models
{
   public class Recipe
    {
        public int Id { get; set; }

        public int PatientId { get; set; }

        public List<Medicine> LstMedicines { get; set; }
    }
}
