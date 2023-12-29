//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequest]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class RegionalSubbasinRevisionRequestExtensionMethods
    {
        public static RegionalSubbasinRevisionRequestSimpleDto AsSimpleDto(this RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest)
        {
            var dto = new RegionalSubbasinRevisionRequestSimpleDto()
            {
                RegionalSubbasinRevisionRequestID = regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestID,
                TreatmentBMPID = regionalSubbasinRevisionRequest.TreatmentBMPID,
                RequestPersonID = regionalSubbasinRevisionRequest.RequestPersonID,
                RegionalSubbasinRevisionRequestStatusID = regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestStatusID,
                RequestDate = regionalSubbasinRevisionRequest.RequestDate,
                ClosedByPersonID = regionalSubbasinRevisionRequest.ClosedByPersonID,
                ClosedDate = regionalSubbasinRevisionRequest.ClosedDate,
                Notes = regionalSubbasinRevisionRequest.Notes,
                CloseNotes = regionalSubbasinRevisionRequest.CloseNotes
            };
            return dto;
        }
    }
}