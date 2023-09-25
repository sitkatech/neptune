//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OCTAPrioritizationStaging]

using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static partial class OCTAPrioritizationStagingExtensionMethods
    {
        public static OCTAPrioritizationStagingDto AsDto(this OCTAPrioritizationStaging oCTAPrioritizationStaging)
        {
            var oCTAPrioritizationStagingDto = new OCTAPrioritizationStagingDto()
            {
                OCTAPrioritizationStagingID = oCTAPrioritizationStaging.OCTAPrioritizationStagingID,
                OCTAPrioritizationKey = oCTAPrioritizationStaging.OCTAPrioritizationKey,
                Watershed = oCTAPrioritizationStaging.Watershed,
                CatchIDN = oCTAPrioritizationStaging.CatchIDN,
                TPI = oCTAPrioritizationStaging.TPI,
                WQNLU = oCTAPrioritizationStaging.WQNLU,
                WQNMON = oCTAPrioritizationStaging.WQNMON,
                IMPAIR = oCTAPrioritizationStaging.IMPAIR,
                MON = oCTAPrioritizationStaging.MON,
                SEA = oCTAPrioritizationStaging.SEA,
                SEA_PCTL = oCTAPrioritizationStaging.SEA_PCTL,
                PC_VOL_PCT = oCTAPrioritizationStaging.PC_VOL_PCT,
                PC_NUT_PCT = oCTAPrioritizationStaging.PC_NUT_PCT,
                PC_BAC_PCT = oCTAPrioritizationStaging.PC_BAC_PCT,
                PC_MET_PCT = oCTAPrioritizationStaging.PC_MET_PCT,
                PC_TSS_PCT = oCTAPrioritizationStaging.PC_TSS_PCT
            };
            DoCustomMappings(oCTAPrioritizationStaging, oCTAPrioritizationStagingDto);
            return oCTAPrioritizationStagingDto;
        }

        static partial void DoCustomMappings(OCTAPrioritizationStaging oCTAPrioritizationStaging, OCTAPrioritizationStagingDto oCTAPrioritizationStagingDto);

        public static OCTAPrioritizationStagingSimpleDto AsSimpleDto(this OCTAPrioritizationStaging oCTAPrioritizationStaging)
        {
            var oCTAPrioritizationStagingSimpleDto = new OCTAPrioritizationStagingSimpleDto()
            {
                OCTAPrioritizationStagingID = oCTAPrioritizationStaging.OCTAPrioritizationStagingID,
                OCTAPrioritizationKey = oCTAPrioritizationStaging.OCTAPrioritizationKey,
                Watershed = oCTAPrioritizationStaging.Watershed,
                CatchIDN = oCTAPrioritizationStaging.CatchIDN,
                TPI = oCTAPrioritizationStaging.TPI,
                WQNLU = oCTAPrioritizationStaging.WQNLU,
                WQNMON = oCTAPrioritizationStaging.WQNMON,
                IMPAIR = oCTAPrioritizationStaging.IMPAIR,
                MON = oCTAPrioritizationStaging.MON,
                SEA = oCTAPrioritizationStaging.SEA,
                SEA_PCTL = oCTAPrioritizationStaging.SEA_PCTL,
                PC_VOL_PCT = oCTAPrioritizationStaging.PC_VOL_PCT,
                PC_NUT_PCT = oCTAPrioritizationStaging.PC_NUT_PCT,
                PC_BAC_PCT = oCTAPrioritizationStaging.PC_BAC_PCT,
                PC_MET_PCT = oCTAPrioritizationStaging.PC_MET_PCT,
                PC_TSS_PCT = oCTAPrioritizationStaging.PC_TSS_PCT
            };
            DoCustomSimpleDtoMappings(oCTAPrioritizationStaging, oCTAPrioritizationStagingSimpleDto);
            return oCTAPrioritizationStagingSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OCTAPrioritizationStaging oCTAPrioritizationStaging, OCTAPrioritizationStagingSimpleDto oCTAPrioritizationStagingSimpleDto);
    }
}