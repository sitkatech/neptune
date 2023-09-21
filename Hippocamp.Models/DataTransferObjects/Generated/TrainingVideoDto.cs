//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrainingVideo]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class TrainingVideoDto
    {
        public int TrainingVideoID { get; set; }
        public string VideoName { get; set; }
        public string VideoDescription { get; set; }
        public string VideoURL { get; set; }
    }

    public partial class TrainingVideoSimpleDto
    {
        public int TrainingVideoID { get; set; }
        public string VideoName { get; set; }
        public string VideoDescription { get; set; }
        public string VideoURL { get; set; }
    }

}