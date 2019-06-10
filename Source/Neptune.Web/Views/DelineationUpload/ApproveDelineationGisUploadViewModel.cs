using System;
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
                Debug.Assert(stormwaterJurisdiction != null, "Stormwater Jurisdiction should not be null. Either the \"Required\" validation is missing, or UpdateModel() was run before validations.");

                var delineationsToCreate = stormwaterJurisdiction.TreatmentBMPs.ToList().Join(WktAndAnnotations,
                    x => x.TreatmentBMPName, y => y.Annotation,
                    (x, y) => new
                    {
                        Delineation = new Models.Delineation(DbGeometry.FromText(y.Wkt, MapInitJson.CoordinateSystemId),
                            DelineationType.Distributed.DelineationTypeID, true, x.TreatmentBMPID),
                       x.TreatmentBMPID
                    }).ToList();

                var treatmentBMPIDsToReplaceDelineation = delineationsToCreate.Select(y => y.TreatmentBMPID);

                var delineationsToDelete = HttpRequestStorage.DatabaseEntities.Delineations.Where(x => treatmentBMPIDsToReplaceDelineation.Contains(x.TreatmentBMPID));

                HttpRequestStorage.DatabaseEntities.Delineations.DeleteDelineation(delineationsToDelete.ToList());
                HttpRequestStorage.DatabaseEntities.SaveChanges();
                HttpRequestStorage.DatabaseEntities.Delineations.AddRange(delineationsToCreate.Select(x=>x.Delineation));
                HttpRequestStorage.DatabaseEntities.SaveChanges();
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
