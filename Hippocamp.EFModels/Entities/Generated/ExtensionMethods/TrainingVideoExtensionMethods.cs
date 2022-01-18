//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrainingVideo]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class TrainingVideoExtensionMethods
    {
        public static TrainingVideoDto AsDto(this TrainingVideo trainingVideo)
        {
            var trainingVideoDto = new TrainingVideoDto()
            {
                TrainingVideoID = trainingVideo.TrainingVideoID,
                VideoName = trainingVideo.VideoName,
                VideoDescription = trainingVideo.VideoDescription,
                VideoURL = trainingVideo.VideoURL
            };
            DoCustomMappings(trainingVideo, trainingVideoDto);
            return trainingVideoDto;
        }

        static partial void DoCustomMappings(TrainingVideo trainingVideo, TrainingVideoDto trainingVideoDto);

        public static TrainingVideoSimpleDto AsSimpleDto(this TrainingVideo trainingVideo)
        {
            var trainingVideoSimpleDto = new TrainingVideoSimpleDto()
            {
                TrainingVideoID = trainingVideo.TrainingVideoID,
                VideoName = trainingVideo.VideoName,
                VideoDescription = trainingVideo.VideoDescription,
                VideoURL = trainingVideo.VideoURL
            };
            DoCustomSimpleDtoMappings(trainingVideo, trainingVideoSimpleDto);
            return trainingVideoSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(TrainingVideo trainingVideo, TrainingVideoSimpleDto trainingVideoSimpleDto);
    }
}