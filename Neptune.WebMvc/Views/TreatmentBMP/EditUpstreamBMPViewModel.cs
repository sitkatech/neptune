using Neptune.WebMvc.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class EditUpstreamBMPViewModel: IValidatableObject
    {
        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.UpstreamBMP)]
        public int? UpstreamBMPID { get; set; }

        public int TreatmentBMPID { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditUpstreamBMPViewModel()
        {

        }

        public EditUpstreamBMPViewModel(EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            UpstreamBMPID = treatmentBMP.UpstreamBMPID;
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
        }

        public void UpdateModel(EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            treatmentBMP.UpstreamBMPID = UpstreamBMPID;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            if (dbContext.TreatmentBMPs.AsNoTracking().Any(x => x.UpstreamBMPID == UpstreamBMPID))
                yield return new ValidationResult("The BMP is already set as the Upstream BMP for another BMP");

            if (IsInfiniteLoop(dbContext))
                yield return new ValidationResult("The choice of Upstream BMP would create an infinite loop.");

            if (IsClosedLoop(dbContext))
                yield return new ValidationResult("The choice of Upstream BMP would create a closed loop.");
        }

        private bool IsClosedLoop(NeptuneDbContext dbContext)
        {
            var upstreamBMPChoice = dbContext.TreatmentBMPs.Find(UpstreamBMPID);

            var nextUpstreamBMPID = upstreamBMPChoice.UpstreamBMPID;

            while (nextUpstreamBMPID != null)
            {
                if (nextUpstreamBMPID == UpstreamBMPID)
                {
                    return true;
                }

                nextUpstreamBMPID = dbContext.TreatmentBMPs.Find(nextUpstreamBMPID.Value).UpstreamBMPID;
            }

            return false;
        }

        private bool IsInfiniteLoop(NeptuneDbContext dbContext)
        {
            var upstreamBMPChoice = dbContext.TreatmentBMPs.Find(UpstreamBMPID);

            var nextUpstreamBMPID = upstreamBMPChoice.UpstreamBMPID;

            while (nextUpstreamBMPID != null)
            {
                if (nextUpstreamBMPID == TreatmentBMPID)
                {
                    return true;
                }

                nextUpstreamBMPID = dbContext.TreatmentBMPs.Find(nextUpstreamBMPID.Value).UpstreamBMPID;
            }

            return false;
        }
    }
}