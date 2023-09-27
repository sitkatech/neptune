/*-----------------------------------------------------------------------
<copyright file="FieldDefinitionData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
    public partial class FieldDefinition
    {
        public FieldDefinition(int fieldDefinitionID, string fieldDefinitionDataValue)
        {
            FieldDefinitionID = fieldDefinitionID;
            FieldDefinitionValue = fieldDefinitionDataValue;
        }

        public FieldDefinition()
        {
        }
        public static List<FieldDefinitionDto> List(NeptuneDbContext dbContext)
        {
            return dbContext.FieldDefinitions.Include(x => x.FieldDefinitionType).Select(x => x.AsDto()).ToList();
        }

        public static FieldDefinitionDto GetByFieldDefinitionTypeID(NeptuneDbContext dbContext, int FieldDefinitionTypeID)
        {
            var fieldDefinition = dbContext.FieldDefinitions
                .SingleOrDefault(x => x.FieldDefinitionTypeID == FieldDefinitionTypeID);

            return fieldDefinition?.AsDto();
        }

        public static FieldDefinitionDto UpdateFieldDefinition(NeptuneDbContext dbContext, int FieldDefinitionTypeID,
            FieldDefinitionDto FieldDefinitionUpdateDto)
        {
            var fieldDefinition = dbContext.FieldDefinitions
                .Include(x => x.FieldDefinitionType)
                .SingleOrDefault(x => x.FieldDefinitionTypeID == FieldDefinitionTypeID);

            // null check occurs in calling endpoint method.
            fieldDefinition.FieldDefinitionValue = FieldDefinitionUpdateDto.FieldDefinitionValue;

            dbContext.SaveChanges();

            return fieldDefinition.AsDto();
        }
    }
}
