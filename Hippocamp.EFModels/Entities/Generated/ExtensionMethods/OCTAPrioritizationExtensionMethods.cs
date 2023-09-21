//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OCTAPrioritization]

using Hippocamp.Models.DataTransferObjects;

namespace Hippocamp.EFModels.Entities
{
    public static partial class OCTAPrioritizationExtensionMethods
    {
        public static OCTAPrioritizationDto AsDto(this OCTAPrioritization oCTAPrioritization)
        {
            var oCTAPrioritizationDto = new OCTAPrioritizationDto()
            {
                OCTAPrioritizationID = oCTAPrioritization.OCTAPrioritizationID,
                OCTAPrioritizationKey = oCTAPrioritization.OCTAPrioritizationKey,
                LastUpdate = oCTAPrioritization.LastUpdate,
                Watershed = oCTAPrioritization.Watershed,
                CatchIDN = oCTAPrioritization.CatchIDN,
                TPI = oCTAPrioritization.TPI,
                WQNLU = oCTAPrioritization.WQNLU,
                WQNMON = oCTAPrioritization.WQNMON,
                IMPAIR = oCTAPrioritization.IMPAIR,
                MON = oCTAPrioritization.MON,
                SEA = oCTAPrioritization.SEA,
                SEA_PCTL = oCTAPrioritization.SEA_PCTL,
                PC_VOL_PCT = oCTAPrioritization.PC_VOL_PCT,
                PC_NUT_PCT = oCTAPrioritization.PC_NUT_PCT,
                PC_BAC_PCT = oCTAPrioritization.PC_BAC_PCT,
                PC_MET_PCT = oCTAPrioritization.PC_MET_PCT,
                PC_TSS_PCT = oCTAPrioritization.PC_TSS_PCT
            };
            DoCustomMappings(oCTAPrioritization, oCTAPrioritizationDto);
            return oCTAPrioritizationDto;
        }

        static partial void DoCustomMappings(OCTAPrioritization oCTAPrioritization, OCTAPrioritizationDto oCTAPrioritizationDto);

        public static OCTAPrioritizationSimpleDto AsSimpleDto(this OCTAPrioritization oCTAPrioritization)
        {
            var oCTAPrioritizationSimpleDto = new OCTAPrioritizationSimpleDto()
            {
                OCTAPrioritizationID = oCTAPrioritization.OCTAPrioritizationID,
                OCTAPrioritizationKey = oCTAPrioritization.OCTAPrioritizationKey,
                LastUpdate = oCTAPrioritization.LastUpdate,
                Watershed = oCTAPrioritization.Watershed,
                CatchIDN = oCTAPrioritization.CatchIDN,
                TPI = oCTAPrioritization.TPI,
                WQNLU = oCTAPrioritization.WQNLU,
                WQNMON = oCTAPrioritization.WQNMON,
                IMPAIR = oCTAPrioritization.IMPAIR,
                MON = oCTAPrioritization.MON,
                SEA = oCTAPrioritization.SEA,
                SEA_PCTL = oCTAPrioritization.SEA_PCTL,
                PC_VOL_PCT = oCTAPrioritization.PC_VOL_PCT,
                PC_NUT_PCT = oCTAPrioritization.PC_NUT_PCT,
                PC_BAC_PCT = oCTAPrioritization.PC_BAC_PCT,
                PC_MET_PCT = oCTAPrioritization.PC_MET_PCT,
                PC_TSS_PCT = oCTAPrioritization.PC_TSS_PCT
            };
            DoCustomSimpleDtoMappings(oCTAPrioritization, oCTAPrioritizationSimpleDto);
            return oCTAPrioritizationSimpleDto;
        }

        static partial void DoCustomSimpleDtoMappings(OCTAPrioritization oCTAPrioritization, OCTAPrioritizationSimpleDto oCTAPrioritizationSimpleDto);
    }
}