//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservation]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class OnlandVisualTrashAssessmentObservationDto
    {
        public int OnlandVisualTrashAssessmentObservationID { get; set; }
        public OnlandVisualTrashAssessmentDto OnlandVisualTrashAssessment { get; set; }
        public string Note { get; set; }
        public DateTime ObservationDatetime { get; set; }
    }

    public partial class OnlandVisualTrashAssessmentObservationSimpleDto
    {
        public int OnlandVisualTrashAssessmentObservationID { get; set; }
        public int OnlandVisualTrashAssessmentID { get; set; }
        public string Note { get; set; }
        public DateTime ObservationDatetime { get; set; }
    }

}