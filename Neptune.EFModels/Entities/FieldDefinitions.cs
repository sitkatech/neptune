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

namespace Neptune.EFModels.Entities
{
    public static class FieldDefinitions
    {
        public static FieldDefinition GetFieldDefinitionByFieldDefinitionType(NeptuneDbContext dbContext, FieldDefinitionType fieldDefinitionType)
        {
            return GetFieldDefinitionByFieldDefinitionType(dbContext, fieldDefinitionType.FieldDefinitionTypeID);
        }

        public static FieldDefinition GetFieldDefinitionByFieldDefinitionType(NeptuneDbContext dbContext, int fieldDefinitionTypeID)
        {
            return GetFieldDefintionImpl(dbContext).SingleOrDefault(x => x.FieldDefinitionTypeID == fieldDefinitionTypeID);
        }

        public static FieldDefinition GetFieldDefinitionByFieldDefinitionType(NeptuneDbContext dbContext, FieldDefinitionTypeEnum fieldDefinitionTypeEnum)
        {
            return GetFieldDefintionImpl(dbContext).SingleOrDefault(x => x.FieldDefinitionTypeID == (int)fieldDefinitionTypeEnum);
        }

        private static IQueryable<FieldDefinition> GetFieldDefintionImpl(NeptuneDbContext dbContext)
        {
            return dbContext.FieldDefinitions.AsNoTracking();
        }


        //public static FieldDefinition GetFieldDefinitionByFieldDefinitionType(
        //    this IQueryable<FieldDefinition> fieldDefinitions,
        //    FieldDefinitionTypePrimaryKey fieldDefinitionTypePrimaryKey)
        //{
        //    return fieldDefinitions.SingleOrDefault(x =>
        //        x.FieldDefinitionTypeID == fieldDefinitionTypePrimaryKey.PrimaryKeyValue);
        //}
    }
}
