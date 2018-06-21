/*-----------------------------------------------------------------------
<copyright file="Jurisdiction.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency">
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
using System.Linq;
using LtInfo.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static Parcel GetParcelByParcelNumber(this IQueryable<Parcel> parcels, string parcelNumber)
        {
            return GetParcelByParcelNumber(parcels, parcelNumber, true);
        }

        public static Parcel GetParcelByParcelNumber(this IQueryable<Parcel> parcels, string parcelNumber, bool requireRecord)
        {
            var parcel = parcels.FirstOrDefault(x => x.ParcelNumber.Equals(parcelNumber, StringComparison.InvariantCultureIgnoreCase));
            if (requireRecord && parcel == null)
            {
                throw new SitkaRecordNotFoundException($"Parcel '{parcelNumber}' not found!");
            }
            return parcel;
        }
    }
}
