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

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditUpstreamBMPViewModel()
        {

        }

        public EditUpstreamBMPViewModel(EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            UpstreamBMPID = treatmentBMP.UpstreamBMPID;
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

            if (IsClosedLoop(dbContext))
                yield return new ValidationResult("The choice of Upstream BMP would create a closed loop.");
        }

        private bool IsClosedLoop(NeptuneDbContext dbContext)
        {
            //todo: need to fix this reference to upstream bmp
            var upstreamBMPChoice = dbContext.TreatmentBMPs.Find(UpstreamBMPID);

            var nextUpstream = upstreamBMPChoice.UpstreamBMP;

            while (nextUpstream != null)
            {
                if (nextUpstream.UpstreamBMPID == UpstreamBMPID)
                {
                    return true;
                }

                nextUpstream = nextUpstream.UpstreamBMP;
            }

            return false;
        }
    }
}