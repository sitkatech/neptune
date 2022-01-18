//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PreliminarySourceIdentificationType]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class PreliminarySourceIdentificationTypeDto
    {
        public int PreliminarySourceIdentificationTypeID { get; set; }
        public string PreliminarySourceIdentificationTypeName { get; set; }
        public string PreliminarySourceIdentificationTypeDisplayName { get; set; }
        public PreliminarySourceIdentificationCategoryDto PreliminarySourceIdentificationCategory { get; set; }
    }

    public partial class PreliminarySourceIdentificationTypeSimpleDto
    {
        public int PreliminarySourceIdentificationTypeID { get; set; }
        public string PreliminarySourceIdentificationTypeName { get; set; }
        public string PreliminarySourceIdentificationTypeDisplayName { get; set; }
        public int PreliminarySourceIdentificationCategoryID { get; set; }
    }

}