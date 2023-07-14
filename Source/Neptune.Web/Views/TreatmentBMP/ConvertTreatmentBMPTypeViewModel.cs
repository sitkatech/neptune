/*-----------------------------------------------------------------------
<copyright file="ConvertTreatmentBMPTypeViewModel.cs" company="Tahoe Regional Planning Agency">
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

using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class ConvertTreatmentBMPTypeViewModel : FormViewModel
    {
        [Required(ErrorMessage = "Choose a BMP Type to convert to")]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.TreatmentBMPType)]
        public int? TreatmentBMPTypeID { get; set; }


        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public ConvertTreatmentBMPTypeViewModel()
        {
        }


        public void UpdateModel(Models.TreatmentBMP treatmentBMP, Person currentPerson, DatabaseEntities databaseEntities)
        {
            databaseEntities.Database.ExecuteSqlCommand("EXEC dbo.pTreatmentBMPUpdateTreatmentBMPType @treatmentBMPID={0}, @treatmentBMPTypeID={1}",
                treatmentBMP.TreatmentBMPID, TreatmentBMPTypeID.Value);
        }
    }
}
