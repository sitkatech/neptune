/*-----------------------------------------------------------------------
<copyright file="MeasurementUnitType.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
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
using LtInfo.Common;
using LtInfo.Common.Views;

namespace Neptune.Web.Models
{
    public partial class MeasurementUnitType
    {
        public abstract bool IncludeSpaceBeforeLegendLabel();
    }

    public partial class MeasurementUnitTypeAcres : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypeSquareFeet : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypeKilogram : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypeCount : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypePercent : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return true;
        }
    }

    public partial class MeasurementUnitTypeMilligamsPerLiter : MeasurementUnitType
    {
       public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypeMeters : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypeFeet : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypeInches : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypeInchesPerHour : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypeSeconds : MeasurementUnitType
    {
       public override bool IncludeSpaceBeforeLegendLabel()
        {
            return false;
        }
    }

    public partial class MeasurementUnitTypePercentDecline : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return true;
        }
    }

    public partial class MeasurementUnitTypePercentIncrease : MeasurementUnitType
    {
        public override bool IncludeSpaceBeforeLegendLabel()
        {
            return true;
        }
    }

    public partial class MeasurementUnitTypePercentDeviation : MeasurementUnitType
    {
       public override bool IncludeSpaceBeforeLegendLabel()
        {
            return true;
        }
    }

}
