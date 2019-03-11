using System.Data.Entity;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationMapInitJson : StormwaterMapInitJson
    {
        public LayerGeoJson TreatmentBMPLayerGeoJson { get; set; }

        public DelineationMapInitJson(string mapDivID, DbSet<Models.TreatmentBMP> databaseEntitiesTreatmentBMPs) : base(mapDivID)
        {
            TreatmentBMPLayerGeoJson = MakeTreatmentBMPLayerGeoJson(databaseEntitiesTreatmentBMPs,
                (feature, treatmentBMP) =>
                {
                    if (treatmentBMP.Delineation != null)
                    {
                        feature.Properties.Add("DelineationURL", treatmentBMP.GetDelineationUrl());
                    }
                }, false);
        }
    }
}