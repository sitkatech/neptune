//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhoto]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class OnlandVisualTrashAssessmentObservationPhotoDto
    {
        public int OnlandVisualTrashAssessmentObservationPhotoID { get; set; }
        public FileResourceDto FileResource { get; set; }
        public OnlandVisualTrashAssessmentObservationDto OnlandVisualTrashAssessmentObservation { get; set; }
    }

    public partial class OnlandVisualTrashAssessmentObservationPhotoSimpleDto
    {
        public int OnlandVisualTrashAssessmentObservationPhotoID { get; set; }
        public int FileResourceID { get; set; }
        public int OnlandVisualTrashAssessmentObservationID { get; set; }
    }

}