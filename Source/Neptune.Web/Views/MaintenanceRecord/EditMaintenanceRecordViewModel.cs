/*-----------------------------------------------------------------------
<copyright file="EditMaintenanceRecordViewModel.cs" company="Tahoe Regional Planning Agency">
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
using System.ComponentModel.DataAnnotations;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.MaintenanceRecord
{
    public class EditMaintenanceRecordViewModel : FormViewModel
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditMaintenanceRecordViewModel()
        {
        }

        public EditMaintenanceRecordViewModel(Models.MaintenanceRecord maintenanceRecord)
        {
            MaintenanceRecordDate = maintenanceRecord.MaintenanceRecordDate;
            PerformedByPersonID = maintenanceRecord.PerformedByPersonID;
            MaintenanceRecordTypeID = maintenanceRecord.MaintenanceRecordTypeID;
            MaintenanceRecordDescription = maintenanceRecord.MaintenanceRecordDescription;
        }

        [Required]
        [StringLength(Models.MaintenanceRecord.FieldLengths.MaintenanceRecordDescription)]
        [Display(Name = "Description")]
        public string MaintenanceRecordDescription { get; set; }

        [Required]
        [FieldDefinitionDisplay(FieldDefinitionEnum.MaintenanceRecordType)]
        public int? MaintenanceRecordTypeID { get; set; }

        [Required]
        [Display(Name = "Performed By")]
        public int? PerformedByPersonID { get; set; }

        [Display(Name = "Date")]
        [Required]
        public DateTime? MaintenanceRecordDate { get; set; }

        public void UpdateModel(Models.MaintenanceRecord maintenanceRecord)
        {
            maintenanceRecord.MaintenanceRecordDate = MaintenanceRecordDate.Value;
            maintenanceRecord.PerformedByPersonID = PerformedByPersonID.Value;
            maintenanceRecord.MaintenanceRecordTypeID = MaintenanceRecordTypeID.Value;
            maintenanceRecord.MaintenanceRecordDescription = MaintenanceRecordDescription;
        }
    }
}