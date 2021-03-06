﻿/*-----------------------------------------------------------------------
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Neptune.Web.Models;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMPDocument
{
    public class NewViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        [DisplayName("Treatment BMP Document")]
        [SitkaFileExtensions("pdf|zip|doc|docx|xls|xlsx|jpg|png")]
        public HttpPostedFileBase FileResourceData { get; set; }

        [StringLength(Models.TreatmentBMPDocument.FieldLengths.DisplayName)]
        [DisplayName("Document Display Name")]
        [Required]
        public string DisplayName { get; set; }

        [StringLength(Models.TreatmentBMPDocument.FieldLengths.DocumentDescription)]
        [DisplayName("Document Description")]
        [Required]
        public string DocumentDescription { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public NewViewModel()
        {
        }

        public NewViewModel(Models.TreatmentBMPDocument monitoringProgramDocument)
        {
            DisplayName = monitoringProgramDocument.DisplayName;
            DocumentDescription = monitoringProgramDocument.DocumentDescription;
        }

        public void UpdateModel(Models.TreatmentBMPDocument monitoringProgramDocument, Person currentPerson)
        {
            monitoringProgramDocument.DisplayName = DisplayName;
            monitoringProgramDocument.DocumentDescription = DocumentDescription;
            monitoringProgramDocument.UploadDate = DateTime.Now;
            monitoringProgramDocument.FileResource = FileResource.CreateNewFromHttpPostedFileAndSave(FileResourceData, currentPerson);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            FileResource.ValidateFileSize(FileResourceData, errors, GeneralUtility.NameOf(() => FileResourceData));
            return errors;
        }
    }
}
