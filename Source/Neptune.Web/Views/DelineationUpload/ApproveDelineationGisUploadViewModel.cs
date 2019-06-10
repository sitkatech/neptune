﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.GeoJson;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.DelineationUpload
{
    public class ApproveDelineationGisUploadViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public int? StormwaterJurisdictionID { get; set; }

        [Required]
        public int? LayerToImportID { get; set; }

        public List<DelineationGeometryLayer> DelineationGeometryLayers { get; set; }

        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ApproveDelineationGisUploadViewModel()
        {
        }

        public ApproveDelineationGisUploadViewModel(Person currentPerson)
        {
            DelineationGeometryLayers =
                currentPerson.DelineationGeometryStagings.Select(
                    x => new DelineationGeometryLayer {DelineationGeometryStagingID = x.DelineationGeometryStagingID, SelectedProperty = x.SelectedProperty}).ToList();
        }

        public void UpdateModel(Person currentPerson)
        {
            var delineationGeometryStagings = currentPerson.DelineationGeometryStagings.ToList();
            HttpRequestStorage.DatabaseEntities.DelineationGeometryStagings.DeleteDelineationGeometryStaging(delineationGeometryStagings);

            currentPerson.DelineationGeometryStagings.Clear();
            if (DelineationGeometryLayers != null && DelineationGeometryLayers.Count > 0)
            {
                var stormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetStormwaterJurisdiction(StormwaterJurisdictionID.GetValueOrDefault()); // will never be null due to RequiredAttribute
                Debug.Assert(stormwaterJurisdiction != null, "Treatment BMP should not be null. Either the \"Required\" validation is missing, or UpdateModel() was run before validations.");

                var delineationsInDatabase = HttpRequestStorage.DatabaseEntities.Delineations.Local;
                var delineationsToSave = 
                    WktAndAnnotations.Select(x =>
                        {
                            var delineationGeometry = DbGeometry.FromText(x.Wkt, MapInitJson.CoordinateSystemId);
                            var delineationType = DelineationType.Distributed.DelineationTypeID;
                            //var delineationType = DelineationType.ToType( (DelineationTypeEnum) Enum.Parse(typeof(DelineationTypeEnum), x.Annotation) ).DelineationTypeID;
                            return new Models.Delineation(
                                    delineationGeometry,
                                    delineationType, true);
                        })
                        .ToList();

                // We want to remove candidates that are in bmpRegistrations that would forbid editing of catchment geometry
                var delineationNamesToRemove =
                    HttpRequestStorage.DatabaseEntities.Delineations.ToList()                       
                        .Select(x => x.DelineationID);

                delineationsToSave = delineationsToSave.Where(x => !delineationNamesToRemove.Contains(x.DelineationID)).ToList();


                var delineationsToMerge = stormwaterJurisdiction.TreatmentBMPs.Where(x => x.Delineation != null).Select(x => x.Delineation).ToList();

                delineationsToMerge.MergeNew(delineationsToSave,
                    (x, y) => x.DelineationID == y.DelineationID,
                    delineationsInDatabase);


                delineationsToMerge.MergeUpdate(delineationsToSave,
                    (x, y) => x.DelineationID == y.DelineationID,
                    (x, y) =>
                    {
                        x.DelineationGeometry = y.DelineationGeometry;
                    });
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (LayerToImportID == null || !DelineationGeometryLayers.Select(x => x.DelineationGeometryStagingID).Contains(LayerToImportID.Value))
            {
                errors.Add(new ValidationResult("Must select one layer to import"));
            }

            return errors;
        }
    }

    public class DelineationGeometryLayer
    {
        [Required]
        public int DelineationGeometryStagingID { get; set; }

        public string SelectedProperty { get; set; }
    }
}