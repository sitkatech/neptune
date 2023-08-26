﻿using Neptune.Web.Common;
using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.TreatmentBMP
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
            return new List<ValidationResult>();
            //todo:
            //if (_dbContext.TreatmentBMPs.Any(x=>x.UpstreamBMPID == UpstreamBMPID))
            //    yield return new ValidationResult("The BMP is already set as the Upstream BMP for another BMP");

            //if (IsClosedLoop())
            //    yield return new ValidationResult("The choice of Upstream BMP would create a closed loop.");
        }

        //private bool IsClosedLoop()
        //{
        //    var upstreamBMPChoice = _dbContext.TreatmentBMPs.Find(UpstreamBMPID);

        //    var nextUpstream = upstreamBMPChoice.UpstreamBMP;

        //    while (nextUpstream != null)
        //    {
        //        if (nextUpstream.UpstreamBMPID == UpstreamBMPID)
        //        {
        //            return true;
        //        }

        //        nextUpstream = nextUpstream.UpstreamBMP;
        //    }

        //    return false;
        //}
    }
}