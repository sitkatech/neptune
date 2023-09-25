//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequest]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class RegionalSubbasinRevisionRequestExtensionMethods
    {
        public static RegionalSubbasinRevisionRequestDto AsDto(this RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest)
        {
            var regionalSubbasinRevisionRequestDto = new RegionalSubbasinRevisionRequestDto()
            {
                RegionalSubbasinRevisionRequestID = regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestID,
                TreatmentBMP = regionalSubbasinRevisionRequest.TreatmentBMP.AsDto(),
                RequestPerson = regionalSubbasinRevisionRequest.RequestPerson.AsDto(),
                RegionalSubbasinRevisionRequestStatus = regionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestStatus.AsDto(),
                RequestDate = regionalSubbasinRevisionRequest.RequestDate,
                ClosedByPerson = regionalSubbasinRevisionRequest.ClosedByPerson?.AsDto(),
                ClosedDate = regionalSubbasinRevisionRequest.ClosedDate,
                Notes = regionalSubbasinRevisionRequest.Notes,
                CloseNotes = regionalSubbasinRevisionRequest.CloseNotes
            };
            DoCustomMappings(regionalSubbasinRevisionRequest, regionalSubbasinRevisionRequestDto);
            return regionalSubbasinRevisionRequestDto;
        }

        static partial void DoCustomMappings(RegionalSubbasinRevisionRequest regionalSubbasinRevisionRequest, RegionalSubbasinRevisionRequestDto regionalSubbasinRevisionRequestDto);

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