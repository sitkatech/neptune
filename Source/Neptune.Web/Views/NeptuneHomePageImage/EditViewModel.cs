/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using Neptune.Web.Common;
using Neptune.Web.Models;
using LtInfo.Common.Models;

namespace Neptune.Web.Views.NeptuneHomePageImage
{
    public class EditViewModel : FormViewModel
    {
        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.PhotoCaption)]
        [StringLength(Models.NeptuneHomePageImage.FieldLengths.Caption)]
        public string Caption { get; set; }

        [Required]
        [DisplayName("Sort Order")]
        public int SortOrder { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.NeptuneHomePageImage neptuneHomePageImage)
        {
            Caption = neptuneHomePageImage.Caption;
            SortOrder = neptuneHomePageImage.SortOrder;
        }

        public virtual void UpdateModel(Models.NeptuneHomePageImage neptuneHomePageImage, Person person)
        {
            neptuneHomePageImage.Caption = Caption;
            neptuneHomePageImage.SortOrder = SortOrder;
        }
    }
}
