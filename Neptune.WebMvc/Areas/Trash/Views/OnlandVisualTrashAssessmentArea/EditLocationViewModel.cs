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

using System.ComponentModel.DataAnnotations;
using Neptune.Common.GeoSpatial;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
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

        public async Task UpdateModel(NeptuneDbContext dbContext, EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            if (IsParcelPicker.GetValueOrDefault())
            {
                // since this is parcel picks, we don't need to reproject; the parcels are already in the correct system (State Plane)
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry = ParcelGeometries.UnionAggregateByParcelIDs(dbContext, ParcelIDs);
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326 = ParcelGeometries.UnionAggregate4326ByParcelIDs(dbContext, ParcelIDs);
            }
            else
            {
                var geometries = WktAndAnnotations.Select(x => GeometryHelper.FromWKT(x.Wkt, Proj4NetHelper.WEB_MERCATOR));
                var newGeometry4326 = geometries.ToList().UnionListGeometries();

                // since this is coming from the browser, we have to transform to State Plane
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry4326 = newGeometry4326;
                onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaGeometry = newGeometry4326.ProjectTo2771();
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
                else if (WktAndAnnotations.Select(x => GeometryHelper.FromWKT(x.Wkt, Proj4NetHelper.WEB_MERCATOR))
                    .Any(x => !x.IsValid))
                {
                    yield return new ValidationResult(
                        "The Assessment Area contained invalid (self-intersecting) shapes. Please try again.");
                }
            }
        }
    }
}
