
using System;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;
using Microsoft.Ajax.Utilities;

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
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            if (DelineationGeometryLayers != null && DelineationGeometryLayers.Count > 0)
            {
                var stormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetStormwaterJurisdiction(StormwaterJurisdictionID.GetValueOrDefault()); // will never be null due to RequiredAttribute
                Debug.Assert(stormwaterJurisdiction != null, "Stormwater Jurisdiction should not be null. Either the \"Required\" validation is missing, or UpdateModel() was run before validations.");

                var treatmentBMPNames = WktAndAnnotations.Select(x=>x.Annotation).ToList();

                var delineationsToMergeInto =
                    stormwaterJurisdiction.TreatmentBMPs.Where(x => treatmentBMPNames.Contains(x.TreatmentBMPName)).Select(x=>x.Delineation).ToList();


                var delineationsToCreate = stormwaterJurisdiction.TreatmentBMPs.ToList().Join(WktAndAnnotations,
                    x => x.TreatmentBMPName, y => y.Annotation,
                    (x, y) => new Models.Delineation(DbGeometry.FromText(y.Wkt, MapInitJson.CoordinateSystemId),
                            DelineationType.Distributed.DelineationTypeID, true, x.TreatmentBMPID, DateTime.Now)
                     ).ToList();

                delineationsToMergeInto.Merge(delineationsToCreate,
                    HttpRequestStorage.DatabaseEntities.Delineations.Local,
                    (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                    (x, y) => x.DelineationGeometry = y.DelineationGeometry);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (LayerToImportID == null || !DelineationGeometryLayers.Select(x => x.DelineationGeometryStagingID).Contains(LayerToImportID.Value))
            {
                errors.Add(new ValidationResult("Must select one layer to import"));
            }

            var treatmentBMPNamesToUpload = WktAndAnnotations.Select(x => x.Annotation).ToList();
            var delineationGeometryToUpload = WktAndAnnotations.Select(x => x.Wkt).ToList();

            var treatmentBMPNames = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => treatmentBMPNamesToUpload.Contains(x.TreatmentBMPName)).Select(x => x.TreatmentBMPName).ToList();
            var treatmentBMPNameDifference = treatmentBMPNamesToUpload.Except(treatmentBMPNames).ToList();

            if (treatmentBMPNameDifference.Any())
            {
                errors.Add(new ValidationResult(treatmentBMPNameDifference.Count() + " records in the upload did not match BMP Names stored in database"));
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
