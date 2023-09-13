/*-----------------------------------------------------------------------
<copyright file="FieldDefinitionData.DatabaseContextExtensions.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class CustomAttributeTypes
    {
        public static IQueryable<CustomAttributeType> GetImpl(NeptuneDbContext dbContext)
        {
            return dbContext.CustomAttributeTypes;
        }

        public static CustomAttributeType GetByIDWithChangeTracking(NeptuneDbContext dbContext,
            int customAttributeTypeID)
        {
            var customAttributeType = GetImpl(dbContext)
                .SingleOrDefault(x => x.CustomAttributeTypeID == customAttributeTypeID);
            Check.RequireNotNull(customAttributeType,
                $"CustomAttributeType with ID {customAttributeTypeID} not found!");
            return customAttributeType;
        }

        public static CustomAttributeType GetByIDWithChangeTracking(NeptuneDbContext dbContext,
            CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            return GetByIDWithChangeTracking(dbContext, customAttributeTypePrimaryKey.PrimaryKeyValue);
        }

        public static CustomAttributeType GetByID(NeptuneDbContext dbContext, int customAttributeTypeID)
        {
            var customAttributeType = GetImpl(dbContext).AsNoTracking()
                .SingleOrDefault(x => x.CustomAttributeTypeID == customAttributeTypeID);
            Check.RequireNotNull(customAttributeType,
                $"CustomAttributeType with ID {customAttributeTypeID} not found!");
            return customAttributeType;
        }

        public static CustomAttributeType GetByID(NeptuneDbContext dbContext,
            CustomAttributeTypePrimaryKey customAttributeTypePrimaryKey)
        {
            return GetByID(dbContext, customAttributeTypePrimaryKey.PrimaryKeyValue);
        }

        public static List<CustomAttributeType> List(NeptuneDbContext dbContext)
        {
            return GetImpl(dbContext).AsNoTracking().OrderBy(x => x.CustomAttributeTypeName).ToList();
        }

        public static List<CustomAttributeType> GetCustomAttributeTypes(NeptuneDbContext dbContext, List<CustomAttributeUpsertDto> customAttributes)
        {
            var customAttributeTypeIDs = customAttributes.Select(x => x.CustomAttributeTypeID).ToList();
            return GetImpl(dbContext).AsNoTracking().Where(x => customAttributeTypeIDs.Contains(x.CustomAttributeTypeID)).OrderBy(x => x.CustomAttributeTypeName).ToList();
        }
    }
}
