/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPObservationDetailType.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPObservationDetailType
    {
        public static List<TreatmentBMPObservationDetailType> GetRelevantDetailTypesForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            return All.Where(y => y.IsRelevantForTreatmentBMPType(treatmentBMPType)).ToList();
        }

        public bool IsRelevantForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            return treatmentBMPType.GetObservationTypes().Contains(ObservationType);
        }


        public static List<TreatmentBMPObservationDetailType> GetScoredDetailTypesForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            return All.Where(y => y.IsUsedForScoringForTreatmentBMPType(treatmentBMPType)).ToList();
        }

        public virtual bool IsUsedForScoringForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            return IsRelevantForTreatmentBMPType(treatmentBMPType);
        }
    }

    public partial class TreatmentBMPObservationDetailTypeInlet
    {
    }

    public partial class TreatmentBMPObservationDetailTypeOutlet
    {
    }

    public partial class TreatmentBMPObservationDetailTypeStaffPlate
    {
    }

    public partial class TreatmentBMPObservationDetailTypeTreatmentVaultCapacityStadiaRod
    {
    }

    public partial class TreatmentBMPObservationDetailTypeSedimentTrapCapacityStadiaRod
    {
    }

    public partial class TreatmentBMPObservationDetailTypeDurationOfInfiltration
    {
    }

    public partial class TreatmentBMPObservationDetailTypeConstantHeadPermeameter
    {
    }

    public partial class TreatmentBMPObservationDetailTypeInfiltrometer
    {
    }

    public partial class TreatmentBMPObservationDetailTypeUserDefinedInfiltrationMeasurement
    {
    }

    public partial class TreatmentBMPObservationDetailTypeStandingWater
    {
    }

    public partial class TreatmentBMPObservationDetailTypeVegetativeCoverWetlandAndRiparianSpecies
    {
        public override bool IsUsedForScoringForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            switch (treatmentBMPType.ToEnum)
            {
                case TreatmentBMPTypeEnum.BioFilter:
                case TreatmentBMPTypeEnum.DryBasin:
                case TreatmentBMPTypeEnum.InfiltrationBasin:
                case TreatmentBMPTypeEnum.InfiltrationFeature:
                    return true;
                default:
                    return false;
            }
        }
    }

    public partial class TreatmentBMPObservationDetailTypeVegetativeCoverTreeSpecies
    {
        public override bool IsUsedForScoringForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            switch (treatmentBMPType.ToEnum)
            {
                case TreatmentBMPTypeEnum.InfiltrationFeature:
                    return true;
                default:
                    return false;
            }
        }

    }

    public partial class TreatmentBMPObservationDetailTypeVegetativeCoverGrassSpecies
    {
        public override bool IsUsedForScoringForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            switch (treatmentBMPType.ToEnum)
            {
                case TreatmentBMPTypeEnum.BioFilter:
                case TreatmentBMPTypeEnum.InfiltrationFeature:
                    return true;
                default:
                    return false;
            }
        }
    }

    public partial class TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverWetlandAndRiparianSpecies
    {
        public override bool IsUsedForScoringForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            switch (treatmentBMPType.ToEnum)
            {
                case TreatmentBMPTypeEnum.WetBasin:                
                    return true;
                default:
                    return false;
            }
        }
    }

    public partial class TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverTreeSpecies
    {
        public override bool IsUsedForScoringForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            return false;
        }
    }

    public partial class TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverGrassSpecies
    {
        public override bool IsUsedForScoringForTreatmentBMPType(TreatmentBMPType treatmentBMPType)
        {
            return false;
        }
       
    }
}
