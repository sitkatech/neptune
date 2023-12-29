/*-----------------------------------------------------------------------
<copyright file="NewViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Services;

namespace Neptune.WebMvc.Views.NeptuneHomePageImage
{
    public class NewViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.PhotoCaption)]
        [StringLength(EFModels.Entities.NeptuneHomePageImage.FieldLengths.Caption)]
        public string Caption { get; set; }

        [Required]
        [DisplayName("Sort Order")]
        public int SortOrder { get; set; }

        [Required]
        [DisplayName("Image File")]
        [SitkaFileExtensions("jpg|jpeg|gif|png|tiff|bmp")]
        public IFormFile FileResourceData { get; set; }

        public async Task UpdateModel(EFModels.Entities.NeptuneHomePageImage neptuneHomePageImage, Person person, FileResourceService fileResourceService)
        {
            neptuneHomePageImage.Caption = Caption;
            neptuneHomePageImage.SortOrder = SortOrder;

            var fileResource = await fileResourceService.CreateNewFromIFormFile(FileResourceData, person);
            neptuneHomePageImage.FileResource = fileResource;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            FileResourceService.ValidateFileSize(FileResourceData, errors, "FileResourceData");
            return errors;
        }
    }
}
