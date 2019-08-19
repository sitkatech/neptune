/*-----------------------------------------------------------------------
<copyright file="EditVitalSignLocationViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Collections.Generic;
using LtInfo.Common.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.DbSpatial;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
{
    public class EditLocationViewModel : FormViewModel, IValidatableObject
    {
        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        public bool? IsParcelPicker { get; set; }

        public List<int> ParcelIDs { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditLocationViewModel()
        {
        }

        public EditLocationViewModel(Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {

        }

        public void UpdateModel(Models.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            if (IsParcelPicker.GetValueOrDefault())
            {
                // since this is parcel picks, we don't need to reproject; the parcels are already in the correct system (State Plane)
                var unionListGeometries = HttpRequestStorage.DatabaseEntities.Parcels.Where(x => ParcelIDs.Contains(x.ParcelID)).Select(x => x.ParcelGeometry).ToList().UnionListGeometries();
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry = unionListGeometries.FixSrid();
            }
            else
            {
                var dbGeometrys = WktAndAnnotations.Select(x =>
                    DbGeometry.FromText(x.Wkt, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID).ToSqlGeometry().MakeValid()
                        .ToDbGeometry());
                var unionListGeometries = dbGeometrys.ToList().UnionListGeometries();

                // since this is coming from the browser, we have to transform to State Plane
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry =
                    CoordinateSystemHelper.ProjectWebMercatorToCaliforniaStatePlaneVI(unionListGeometries);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsParcelPicker.GetValueOrDefault())
            {

                if (ParcelIDs == null)
                {
                    yield return new ValidationResult("Assessment Area Geometry is required.");
                }
            }
            else
            {
                if (WktAndAnnotations == null)
                {
                    yield return new ValidationResult("Assessment Area Geometry is required.");
                }
                else if (WktAndAnnotations.Select(x => DbGeometry.FromText(x.Wkt, CoordinateSystemHelper.NAD_83_HARN_CA_ZONE_VI_SRID))
                    .Any(x => !x.IsValid))
                {
                    yield return new ValidationResult(
                        "The Assessment Area contained invalid (self-intersecting) shapes. Please try again.");
                }
            }
        }
    }
}
