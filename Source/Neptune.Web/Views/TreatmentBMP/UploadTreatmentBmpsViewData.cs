﻿using System.Collections.Generic;
using System.Web.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class UploadTreatmentBMPsViewData : NeptuneViewData
    {
        public string TreatmentBMPsUploadUrl { get; }
        public IEnumerable<SelectListItem> BMPTypes { get; }



        public UploadTreatmentBMPsViewData(Person currentPerson, IEnumerable<SelectListItem> bmpTypes, string treatmentBMPsUploadUrl) : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            PageTitle = "BMP Bulk Upload";
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            TreatmentBMPsUploadUrl = treatmentBMPsUploadUrl;
            BMPTypes = bmpTypes;
        }
    }
}