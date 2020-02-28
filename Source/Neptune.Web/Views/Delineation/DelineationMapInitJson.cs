using Neptune.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationMapInitJson : StormwaterMapInitJson
    {
        public LayerGeoJson TreatmentBMPLayerGeoJson { get; set; }

        public DelineationMapInitJson(string mapDivID, IEnumerable<Models.TreatmentBMP> databaseEntitiesTreatmentBMPs,
            BoundingBox boundingBox) : base(mapDivID, DefaultZoomLevel,
            MapInitJsonHelpers.GetJurisdictionMapLayers().ToList(), boundingBox)
        {
            TreatmentBMPLayerGeoJson = MakeTreatmentBMPLayerGeoJson(databaseEntitiesTreatmentBMPs,
                (feature, treatmentBMP) =>
                {
                    feature.Properties.Add("TreatmentBMPType", treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName);
                    if (treatmentBMP.UpstreamBMPID != null)
                    {
                        feature.Properties.Add("UpstreamBMPID", treatmentBMP.UpstreamBMPID);
                        feature.Properties.Add("UpstreamBMPName", treatmentBMP.UpstreamBMP.TreatmentBMPName);
                    }
                    if (treatmentBMP.Delineation != null)
                    {
                        feature.Properties.Add("DelineationURL", treatmentBMP.GetDelineationUrl());
                        if (treatmentBMP.Delineation != null)
                        {
                            feature.Properties.Add("DelineationID", treatmentBMP.Delineation.DelineationID);
                            feature.Properties.Add("IsDelineationVerified", treatmentBMP.Delineation.IsVerified);
                        }
                        feature.Properties.Add("DelineationType", treatmentBMP.Delineation?.DelineationType.DelineationTypeDisplayName);
                    }
                }, false);
        }
    }
}