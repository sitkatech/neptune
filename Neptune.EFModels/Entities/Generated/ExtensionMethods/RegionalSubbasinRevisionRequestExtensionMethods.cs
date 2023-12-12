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
            var regionalSubbasinRevisionRequestSimpleDto = new RegionalSubbasinRevisionRequestSimpleDto()
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
            DoCustomSimpleDtoMappings(regionalSubbasinRevisionRequest, regionalSubbasinRevisionRequestSimpleDto);
            return regionalSubbasinRevisionRequestSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest, RegionalSubbasinRevisionRequestSimpleDto regionalSubbasinRevisionRequestSimpleDto);
    }
}