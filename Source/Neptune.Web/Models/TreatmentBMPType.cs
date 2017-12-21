/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPType.cs" company="Tahoe Regional Planning Agency">
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

using System;
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class TreatmentBMPType
    {
        public static List<int> GetTreatmentBMPTypeIDsWhereDesignDepthIsRequired()
        {
            var treatmentBMPTypes = All.Where(x => x.GetTreatmentBMPTypeObservationTypeOrDefault(ObservationType.MaterialAccumulation) != null);
            return treatmentBMPTypes.Select(x => x.TreatmentBMPTypeID).ToList();
        }

        public static bool RequiresDesignDepth(int treatmentBMPTypeID)
        {
            return GetTreatmentBMPTypeIDsWhereDesignDepthIsRequired().Contains(treatmentBMPTypeID);
        }

        public List<ObservationType> GetObservationTypes()
        {
            var treatmentBMPTypeObservationTypes = HttpRequestStorage.DatabaseEntities.AllTreatmentBMPTypeObservationTypes.Where(x => x.TreatmentBMPTypeID == TreatmentBMPTypeID).ToList();
            return treatmentBMPTypeObservationTypes.Select(x => x.ObservationType).ToList();
        }
        
    }

    public partial class TreatmentBMPTypeDryBasin
    {
       
    }

    public partial class TreatmentBMPTypeWetBasin
    {
        public double CalculateVegetativeCoverMaxThresholdFromBenchmarkAndDeviation(double benchmark, double deviation)
        {
            if (benchmark <= 0)
            {
                throw new ArgumentException("Benchmark value was less than or equal to zero");
            }
            if (deviation <= 0 || deviation > 100)
            {
                throw new ArgumentException("Threshold Deviation value was less than or equal to zero");
            }

            return benchmark + deviation;
        }

        public double CalculateVegetativeCoverMinThresholdFromBenchmarkAndDeviation(double benchmark, double deviation)
        {
            if (benchmark <= 0)
            {
                throw new ArgumentException("Benchmark value was less than or equal to zero");
            }
            if (deviation <= 0 || deviation > 100)
            {
                throw new ArgumentException("Threshold Deviation value was less than or equal to zero");
            }

            return benchmark - deviation;        
        }

        public double CalculateVegetativeCoverBenchmarkFromMinAndMaxThresholds(double minThreshold, double maxThreshold)
        {
            return (minThreshold + maxThreshold)/2;
        }

        public double CalculateVegetativeCoverDeviationFromMinAndMaxThresholds(double minThreshold, double maxThreshold)
        {
            if (maxThreshold <= minThreshold)
            {
                throw new ArgumentException("Maximum Threshold value was greater than or equal to the Minimum Threshold value");
            }
            var benchmark = CalculateVegetativeCoverBenchmarkFromMinAndMaxThresholds(minThreshold, maxThreshold);

            //return (benchmark - minThreshold)/benchmark*100;
            return benchmark - minThreshold;
        }
        
    }

    public partial class TreatmentBMPTypeInfiltrationBasin
    {
       
    }

    public partial class TreatmentBMPTypeTreatmentVault
    {

    }

    public partial class TreatmentBMPTypeCartridgeFilter
    {
        
    }

    public partial class TreatmentBMPTypeBedFilter
    {
       
    }

    public partial class TreatmentBMPTypeBioFilter
    {
       
    }

    public partial class TreatmentBMPTypePorousPavement
    {
        
    }

    public partial class TreatmentBMPTypeSedimentTrap
    {
        
    }

    public partial class TreatmentBMPTypeDropInlet
    {
        
    }

    public partial class TreatmentBMPTypeSettlingBasin
    {
        
    }

    public partial class TreatmentBMPTypeInfiltrationFeature
    {
        
    }
}
