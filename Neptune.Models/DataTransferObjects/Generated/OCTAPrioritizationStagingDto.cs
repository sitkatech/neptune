//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OCTAPrioritizationStaging]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class OCTAPrioritizationStagingDto
    {
        public int OCTAPrioritizationStagingID { get; set; }
        public int OCTAPrioritizationKey { get; set; }
        public string Watershed { get; set; }
        public string CatchIDN { get; set; }
        public double TPI { get; set; }
        public double WQNLU { get; set; }
        public double WQNMON { get; set; }
        public double IMPAIR { get; set; }
        public double MON { get; set; }
        public double SEA { get; set; }
        public string SEA_PCTL { get; set; }
        public double PC_VOL_PCT { get; set; }
        public double PC_NUT_PCT { get; set; }
        public double PC_BAC_PCT { get; set; }
        public double PC_MET_PCT { get; set; }
        public double PC_TSS_PCT { get; set; }
    }

    public partial class OCTAPrioritizationStagingSimpleDto
    {
        public int OCTAPrioritizationStagingID { get; set; }
        public int OCTAPrioritizationKey { get; set; }
        public string Watershed { get; set; }
        public string CatchIDN { get; set; }
        public double TPI { get; set; }
        public double WQNLU { get; set; }
        public double WQNMON { get; set; }
        public double IMPAIR { get; set; }
        public double MON { get; set; }
        public double SEA { get; set; }
        public string SEA_PCTL { get; set; }
        public double PC_VOL_PCT { get; set; }
        public double PC_NUT_PCT { get; set; }
        public double PC_BAC_PCT { get; set; }
        public double PC_MET_PCT { get; set; }
        public double PC_TSS_PCT { get; set; }
    }

}