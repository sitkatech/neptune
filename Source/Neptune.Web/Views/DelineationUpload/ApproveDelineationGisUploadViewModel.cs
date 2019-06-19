
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
            //DelineationGeometryLayers =
            //    currentPerson.DelineationGeometryStagings.Select(
            //        x => new DelineationGeometryLayer {DelineationGeometryStagingID = x.DelineationGeometryStagingID, SelectedProperty = x.SelectedProperty}).ToList();
        }

        public int? UpdateModel(Person currentPerson)
        {
            //var delineationGeometryStagings = currentPerson.DelineationGeometryStagings.ToList();
            //HttpRequestStorage.DatabaseEntities.DelineationGeometryStagings.DeleteDelineationGeometryStaging(delineationGeometryStagings);
            //HttpRequestStorage.DatabaseEntities.SaveChanges();

            //var successfulUpdateCount = 0;

            //if (DelineationGeometryLayers != null && DelineationGeometryLayers.Count > 0)
            //{
            //    var stormwaterJurisdiction = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetStormwaterJurisdiction(StormwaterJurisdictionID.GetValueOrDefault()); // will never be null due to RequiredAttribute
            //    Debug.Assert(stormwaterJurisdiction != null, "Stormwater Jurisdiction should not be null. Either the \"Required\" validation is missing, or UpdateModel() was run before validations.");

            //    var treatmentBMPNames = WktAndAnnotations.Select(x=>x.Annotation).ToList();

            //    var treatmentBMPsToUpdate = stormwaterJurisdiction.TreatmentBMPs.Where(x => treatmentBMPNames.Contains(x.TreatmentBMPName)).ToList();

            //    foreach (var treatmentBMP in treatmentBMPsToUpdate)
            //    {
            //        var wktAndAnnotation = WktAndAnnotations.SingleOrDefault(z => treatmentBMP.TreatmentBMPName == z.Annotation);

            //        treatmentBMP.Delineation?.Delete(HttpRequestStorage.DatabaseEntities);

            //        treatmentBMP.Delineation = new Models.Delineation(
            //            DbGeometry.FromText(wktAndAnnotation.Wkt, MapInitJson.CoordinateSystemId),
            //            DelineationType.Distributed.DelineationTypeID, true, treatmentBMP.TreatmentBMPID, DateTime.Now);
            //    }
            //    successfulUpdateCount = treatmentBMPsToUpdate.Count;
            //}


            return -83; //return successfulUpdateCount;
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
