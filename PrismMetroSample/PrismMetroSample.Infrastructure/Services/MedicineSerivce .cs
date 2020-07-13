using PrismMetroSample.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;

namespace PrismMetroSample.Infrastructure.Services
{
   public class MedicineSerivce : IMedicineSerivce
    {
       public List<Medicine> GetAllMedicines()
        {
            var allMedicines = new List<Medicine>()
            {
                new Medicine(){Id=1,Name="当归",Type="中药",Unit="克"},
                new Medicine(){Id=2,Name="人参",Type="中药",Unit="克"},
                new Medicine(){Id=3,Name="枸杞",Type="中药",Unit="克"},
                new Medicine(){Id=4,Name="青霉素",Type="西药",Unit="瓶"},
                new Medicine(){Id=5,Name="琼浆玉露",Type="仙药",Unit="滴"}
            };
            return allMedicines;
        }

        public List<Recipe> GetRecipesByPatientId(int patientId)
        {
            var allMedicines = GetAllMedicines();
            var allRecipe = new List<Recipe>()
            {
                new Recipe() {Id=1,LstMedicines=allMedicines.Where(t=>t.Id>2).ToList(),PatientId=2},
                new Recipe() {Id=1,LstMedicines=allMedicines.Where(t=>t.Id==2||t.Id==3).ToList(),PatientId=1},
                new Recipe() {Id=1,LstMedicines=allMedicines.Where(t=>t.Id<4).ToList(),PatientId=3},
                new Recipe() {Id=1,LstMedicines=allMedicines.Where(t=>t.Id==5).ToList(),PatientId=4},
            };
            return allRecipe.Where(t => t.PatientId == patientId).ToList();
        }

    }
}
