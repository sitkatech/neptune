using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.Delineation
{
    public class DelineationMapInitJson : StormwaterMapInitJson
    {
        public LayerGeoJson TreatmentBMPLayerGeoJson { get; set; }

        public DelineationMapInitJson(string mapDivID, IEnumerable<EFModels.Entities.TreatmentBMP> databaseEntitiesTreatmentBMPs,
            BoundingBoxDto boundingBox, List<LayerGeoJson> layerGeoJsons, LinkGenerator linkGenerator) : base(mapDivID, DefaultZoomLevel,
            layerGeoJsons, boundingBox)
        {
            var delineationUrlTemplate = new UrlTemplate<int>(SitkaRoute<DelineationController>.BuildUrlFromExpression(linkGenerator, x => x.ForTreatmentBMP(UrlTemplate.Parameter1Int)));
            TreatmentBMPLayerGeoJson = MakeTreatmentBMPLayerGeoJson(databaseEntitiesTreatmentBMPs,
                (feature, treatmentBMP) =>
                {
                    feature.Attributes.Add("TreatmentBMPType", treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName);
                    if (treatmentBMP.UpstreamBMPID != null)
                    {
                        feature.Attributes.Add("UpstreamBMPID", treatmentBMP.UpstreamBMPID);
                        feature.Attributes.Add("UpstreamBMPName", treatmentBMP.UpstreamBMP.TreatmentBMPName);
                    }

                    var delineation = treatmentBMP.Delineation;
                    if (delineation != null)
                    {
                        var delineationUrl = delineationUrlTemplate.ParameterReplace(delineation.TreatmentBMPID);
                        feature.Attributes.Add("DelineationURL", delineationUrl);
                        feature.Attributes.Add("DelineationID", delineation.DelineationID);
                        feature.Attributes.Add("IsDelineationVerified", delineation.IsVerified);
                        feature.Attributes.Add("DelineationType", delineation?.DelineationType.DelineationTypeDisplayName);
                    }
                }, false);
        }

        // needed by serializer
        public DelineationMapInitJson()
        {
        }
    }
}