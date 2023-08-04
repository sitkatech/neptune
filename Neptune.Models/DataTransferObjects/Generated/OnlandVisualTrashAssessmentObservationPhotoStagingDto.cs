//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class OnlandVisualTrashAssessmentObservationPhotoStagingDto
    {
        public int OnlandVisualTrashAssessmentObservationPhotoStagingID { get; set; }
        public FileResourceDto FileResource { get; set; }
        public OnlandVisualTrashAssessmentDto OnlandVisualTrashAssessment { get; set; }
    }

    public partial class OnlandVisualTrashAssessmentObservationPhotoStagingSimpleDto
    {
        public int OnlandVisualTrashAssessmentObservationPhotoStagingID { get; set; }
        public System.Int32 FileResourceID { get; set; }
        public System.Int32 OnlandVisualTrashAssessmentID { get; set; }
    }

}