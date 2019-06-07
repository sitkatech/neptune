using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Diagnostics;
using System.Linq;
using LtInfo.Common;
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

        public List<ModeledCatchmentGeometryLayer> ModeledCatchmentGeometryLayers { get; set; }

        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ApproveDelineationGisUploadViewModel()
        {
        }

        public ApproveDelineationGisUploadViewModel(Person currentPerson)
        {
            ModeledCatchmentGeometryLayers =
                currentPerson.ModeledCatchmentGeometryStagings.Select(
                    x => new ModeledCatchmentGeometryLayer {ModeledCatchmentGeometryStagingID = x.ModeledCatchmentGeometryStagingID, SelectedProperty = x.SelectedProperty}).ToList();
        }

        public void UpdateModel(Person currentPerson)
        {
            var modeledCatchmentGeometryStagings = currentPerson.ModeledCatchmentGeometryStagings.ToList();
            HttpRequestStorage.DatabaseEntities.ModeledCatchmentGeometryStagings.DeleteModeledCatchmentGeometryStaging(modeledCatchmentGeometryStagings);

            currentPerson.ModeledCatchmentGeometryStagings.Clear();
            if (ModeledCatchmentGeometryLayers != null && ModeledCatchmentGeometryLayers.Count > 0)
            {
                var stormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetStormwaterJurisdiction(StormwaterJurisdictionID.GetValueOrDefault()); // will never be null due to RequiredAttribute
                Debug.Assert(stormwaterJurisdiction != null, "Stormwater Jurisdiction should not be null. Either the \"Required\" validation is missing, or UpdateModel() was run before validations.");

                var modeledCatchmentsInDatabase = HttpRequestStorage.DatabaseEntities.ModeledCatchments.Local;
                var modeledCatchmentsToSave =
                    WktAndAnnotations.Select(x => new Models.ModeledCatchment(x.Annotation, stormwaterJurisdiction.StormwaterJurisdictionID) {ModeledCatchmentGeometry = DbGeometry.FromText(x.Wkt)})
                        .ToList();

                // We want to remove candidates that are in bmpRegistrations that would forbid editing of catchment geometry
                var modeledCatchmentNamesToRemove =
                    HttpRequestStorage.DatabaseEntities.ModeledCatchments.ToList()                       
                        .Select(x => x.ModeledCatchmentName);
                modeledCatchmentsToSave = modeledCatchmentsToSave.Where(x => !modeledCatchmentNamesToRemove.Contains(x.ModeledCatchmentName)).ToList();

                var modeledCatchmentsToMerge = stormwaterJurisdiction.ModeledCatchments.ToList();
                modeledCatchmentsToMerge.MergeNew(modeledCatchmentsToSave,
                    (x, y) => x.ModeledCatchmentName == y.ModeledCatchmentName,
                    modeledCatchmentsInDatabase);
                modeledCatchmentsToMerge.MergeUpdate(modeledCatchmentsToSave,
                    (x, y) => x.ModeledCatchmentName == y.ModeledCatchmentName,
                    (x, y) =>
                    {
                        x.ModeledCatchmentGeometry = y.ModeledCatchmentGeometry;
                    });
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (LayerToImportID == null || !ModeledCatchmentGeometryLayers.Select(x => x.ModeledCatchmentGeometryStagingID).Contains(LayerToImportID.Value))
            {
                errors.Add(new ValidationResult("Must select one layer to import"));
            }

            return errors;
        }
    }

    public class ModeledCatchmentGeometryLayer
    {
        [Required]
        public int ModeledCatchmentGeometryStagingID { get; set; }

        public string SelectedProperty { get; set; }
    }
}