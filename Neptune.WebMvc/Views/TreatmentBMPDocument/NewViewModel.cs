/*-----------------------------------------------------------------------
<copyright file="NewTreatmentBMPDocumentViewModel.cs" company="Tahoe Regional Planning Agency">
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Services;

namespace Neptune.WebMvc.Views.TreatmentBMPDocument
{
    public class NewViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Treatment BMP Document")]
        [SitkaFileExtensions("pdf|zip|doc|docx|xls|xlsx|jpg|png")]
        public IFormFile FileResourceData { get; set; }

        [StringLength(EFModels.Entities.TreatmentBMPDocument.FieldLengths.DisplayName)]
        [DisplayName("Document Display Name")]
        [Required]
        public string DisplayName { get; set; }

        [StringLength(EFModels.Entities.TreatmentBMPDocument.FieldLengths.DocumentDescription)]
        [DisplayName("Document Description")]
        [Required]
        public string DocumentDescription { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public NewViewModel()
        {
        }

        public NewViewModel(EFModels.Entities.TreatmentBMPDocument treatmentBMPDocument)
        {
            DisplayName = treatmentBMPDocument.DisplayName;
            DocumentDescription = treatmentBMPDocument.DocumentDescription;
        }

        public async Task UpdateModel(EFModels.Entities.TreatmentBMPDocument treatmentBMPDocument, Person currentPerson, FileResourceService fileResourceService)
        {
            treatmentBMPDocument.DisplayName = DisplayName;
            treatmentBMPDocument.DocumentDescription = DocumentDescription;
            treatmentBMPDocument.UploadDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var fileResource = await fileResourceService.CreateNewFromIFormFile(FileResourceData, currentPerson);
            treatmentBMPDocument.FileResource = fileResource;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            FileResourceService.ValidateFileSize(FileResourceData, errors, "FileResourceData");
            return errors;
        }
    }
}
