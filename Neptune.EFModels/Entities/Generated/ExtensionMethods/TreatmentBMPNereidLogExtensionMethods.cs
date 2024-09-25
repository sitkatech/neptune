//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPNereidLog]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TreatmentBMPNereidLogExtensionMethods
    {
        public static TreatmentBMPNereidLogSimpleDto AsSimpleDto(this TreatmentBMPNereidLog treatmentBMPNereidLog)
        {
            var dto = new TreatmentBMPNereidLogSimpleDto()
            {
                TreatmentBMPNereidLogID = treatmentBMPNereidLog.TreatmentBMPNereidLogID,
                TreatmentBMPID = treatmentBMPNereidLog.TreatmentBMPID,
                LastRequestDate = treatmentBMPNereidLog.LastRequestDate,
                NereidRequest = treatmentBMPNereidLog.NereidRequest,
                NereidResponse = treatmentBMPNereidLog.NereidResponse
            };
            return dto;
        }
    }
}