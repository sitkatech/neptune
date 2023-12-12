//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrainingVideo]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class TrainingVideoExtensionMethods
    {
        public static TrainingVideoSimpleDto AsSimpleDto(this TrainingVideo trainingVideo)
        {
            var dto = new TrainingVideoSimpleDto()
            {
                TrainingVideoID = trainingVideo.TrainingVideoID,
                VideoName = trainingVideo.VideoName,
                VideoDescription = trainingVideo.VideoDescription,
                VideoURL = trainingVideo.VideoURL
            };
            return dto;
        }
    }
}