/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPController.cs" company="Tahoe Regional Planning Agency">
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
using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Models;
using LtInfo.Common.Models;
using MoreLinq;

namespace Neptune.Web.Views.TreatmentBMPImage
{
    public class EditViewModel : FormViewModel
    {
        public List<TreatmentBMPImageSimple> TreatmentBMPImageSimples { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.TreatmentBMP treatmentBMP)
        {
            TreatmentBMPImageSimples = treatmentBMP.TreatmentBMPImages.OrderBy(x => x.TreatmentBMPImageID).Select(x => new TreatmentBMPImageSimple(x)).ToList();
        }

        public void UpdateModel(Models.TreatmentBMP treatmentBMP)
        {
            if (TreatmentBMPImageSimples == null || TreatmentBMPImageSimples.Count == 0)
            {
                return;
            }

            TreatmentBMPImageSimples.Where(x => !x.ToDelete)
                .ForEach(x =>
                {
                    var treatmentBMPImage = treatmentBMP.TreatmentBMPImages.Single(y => y.TreatmentBMPImageID == x.TreatmentBMPImageID);
                    treatmentBMPImage.Caption = x.Caption;
                });

            var treatmentBMPImageIDsToDelete = TreatmentBMPImageSimples.Where(x => x.ToDelete).Select(x => x.TreatmentBMPImageID).ToList();
            var treatmentBMPImagesToDelete = treatmentBMP.TreatmentBMPImages.Where(x => treatmentBMPImageIDsToDelete.Contains(x.TreatmentBMPImageID)).ToList();
            HttpRequestStorage.DatabaseEntities.AllFileResources.RemoveRange(treatmentBMPImagesToDelete.Select(x => x.FileResource).ToList());
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPImages.RemoveRange(treatmentBMPImagesToDelete);
        }
    }
}
