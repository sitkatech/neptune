
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

namespace Neptune.Web.Views.LandUseBlockUpload
{
    public class ApproveLandUseBlockGisUploadViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public int? StormwaterJurisdictionID { get; set; }

        [Required]
        public int? LayerToImportID { get; set; }

        public List<LandUseBlockGeometryLayer> LandUseBlockGeometryLayers { get; set; }

        public List<WktAndAnnotation> WktAndAnnotations { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public ApproveLandUseBlockGisUploadViewModel()
        {
        }

        public ApproveLandUseBlockGisUploadViewModel(Person currentPerson)
        {
            LandUseBlockGeometryLayers =
                currentPerson.LandUseBlockGeometryStagings.Select(
                    x => new LandUseBlockGeometryLayer {LandUseBlockGeometryStagingID = x.LandUseBlockGeometryStagingID, SelectedProperty = x.SelectedProperty}).ToList();
        }

        public void UpdateModel(Person currentPerson)
        {
            var landUseBlockGeometryStagings = currentPerson.LandUseBlockGeometryStagings.ToList();
            HttpRequestStorage.DatabaseEntities.LandUseBlockGeometryStagings.DeleteLandUseBlockGeometryStaging(landUseBlockGeometryStagings);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            if (LandUseBlockGeometryLayers != null && LandUseBlockGeometryLayers.Count > 0)
            {
                var stormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetStormwaterJurisdiction(StormwaterJurisdictionID.GetValueOrDefault()); // will never be null due to RequiredAttribute
                Debug.Assert(stormwaterJurisdiction != null, "Stormwater Jurisdiction should not be null. Either the \"Required\" validation is missing, or UpdateModel() was run before validations.");

                var treatmentBMPNames = WktAndAnnotations.Select(x=>x.Annotation).ToList();

                //var landUseBlocksToMergeInto =
                //    stormwaterJurisdiction.TreatmentBMPs.Where(x => treatmentBMPNames.Contains(x.TreatmentBMPName)).Select(x=>x.LandUseBlock).ToList();


                //var landUseBlocksToCreate = stormwaterJurisdiction.TreatmentBMPs.ToList().Join(WktAndAnnotations,
                //    x => x.TreatmentBMPName, y => y.Annotation,
                //    (x, y) => new Models.LandUseBlock(DbGeometry.FromText(y.Wkt, MapInitJson.CoordinateSystemId),
                //            LandUseBlockType.Distributed.LandUseBlockTypeID, true, x.TreatmentBMPID, DateTime.Now)
                //     ).ToList();

                //landUseBlocksToMergeInto.Merge(landUseBlocksToCreate,
                //    HttpRequestStorage.DatabaseEntities.LandUseBlocks.Local,
                //    (x, y) => x.TreatmentBMPID == y.TreatmentBMPID,
                //    (x, y) => x.LandUseBlockGeometry = y.LandUseBlockGeometry);
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            if (LayerToImportID == null || !LandUseBlockGeometryLayers.Select(x => x.LandUseBlockGeometryStagingID).Contains(LayerToImportID.Value))
            {
                errors.Add(new ValidationResult("Must select one layer to import"));
            }

            var treatmentBMPNamesToUpload = WktAndAnnotations.Select(x => x.Annotation).ToList();
            var landUseBlockGeometryToUpload = WktAndAnnotations.Select(x => x.Wkt).ToList();

            var treatmentBMPNames = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => treatmentBMPNamesToUpload.Contains(x.TreatmentBMPName)).Select(x => x.TreatmentBMPName).ToList();
            var treatmentBMPNameDifference = treatmentBMPNamesToUpload.Except(treatmentBMPNames).ToList();

            if (treatmentBMPNameDifference.Any())
            {
                errors.Add(new ValidationResult(treatmentBMPNameDifference.Count() + " records in the upload did not match BMP Names stored in database"));
            }

            return errors;
        }
    }

    public class LandUseBlockGeometryLayer
    {
        [Required]
        public int LandUseBlockGeometryStagingID { get; set; }

        public string SelectedProperty { get; set; }
    }
}
