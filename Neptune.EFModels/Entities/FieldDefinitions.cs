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
using Neptune.Models.DataTransferObjects;

namespace Neptune.EFModels.Entities
{
    public static class FieldDefinitions
    {
        public static FieldDefinition? GetByFieldDefinitionType(NeptuneDbContext dbContext, FieldDefinitionType fieldDefinitionType)
        {
            return GetByFieldDefinitionType(dbContext, fieldDefinitionType.FieldDefinitionTypeID);
        }

        public static FieldDefinition? GetByFieldDefinitionType(NeptuneDbContext dbContext, int fieldDefinitionTypeID)
        {
            return GetImpl(dbContext).AsNoTracking().SingleOrDefault(x => x.FieldDefinitionTypeID == fieldDefinitionTypeID);
        }

        public static FieldDefinition? GetByFieldDefinitionTypeWithChangeTracking(NeptuneDbContext dbContext, int fieldDefinitionTypeID)
        {
            return GetImpl(dbContext).SingleOrDefault(x => x.FieldDefinitionTypeID == fieldDefinitionTypeID);
        }

        public static FieldDefinition? GetByFieldDefinitionType(NeptuneDbContext dbContext, FieldDefinitionTypeEnum fieldDefinitionTypeEnum)
        {
            return GetImpl(dbContext).AsNoTracking().SingleOrDefault(x => x.FieldDefinitionTypeID == (int)fieldDefinitionTypeEnum);
        }

        private static IQueryable<FieldDefinition> GetImpl(NeptuneDbContext dbContext)
        {
            return dbContext.FieldDefinitions;
        }

        public static List<FieldDefinitionDto> List(NeptuneDbContext dbContext)
        {
            return GetImpl(dbContext).AsNoTracking().Select(x => x.AsDto()).ToList();
        }

        public static FieldDefinitionDto? GetByFieldDefinitionTypeID(NeptuneDbContext dbContext, int fieldDefinitionTypeID)
        {
            var fieldDefinition = GetImpl(dbContext).AsNoTracking()
                .SingleOrDefault(x => x.FieldDefinitionTypeID == fieldDefinitionTypeID);

            return fieldDefinition?.AsDto();
        }

        public static async Task<FieldDefinitionDto> Update(NeptuneDbContext dbContext, int fieldDefinitionTypeID,
            FieldDefinitionDto fieldDefinitionUpdateDto)
        {
            var fieldDefinition = dbContext.FieldDefinitions
                .Single(x => x.FieldDefinitionTypeID == fieldDefinitionTypeID);

            // null check occurs in calling endpoint method.
            fieldDefinition.FieldDefinitionValue = fieldDefinitionUpdateDto.FieldDefinitionValue;

            await dbContext.SaveChangesAsync();

            return fieldDefinition.AsDto();
        }
    }
}
