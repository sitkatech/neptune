﻿using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RecordObservationsViewData : OVTASectionViewData
    {
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public OVTAObservationsMapInitJson MapInitJson { get; }

        public RecordObservationsViewData(Person currentPerson, Models.OnlandVisualTrashAssessment ovta,
            OVTAObservationsMapInitJson mapInitJson, string geoServerUrl)
            : base(currentPerson, Models.OVTASection.RecordObservations, ovta)
        {
            MapInitJson = mapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson, ovta, geoServerUrl);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(OVTAObservationsMapInitJson mapInitJson, Models.OnlandVisualTrashAssessment ovta, string geoServerUrl)
            {
                MapInitJson = mapInitJson;
                GeoServerUrl = geoServerUrl;
                ovtaID = ovta.OnlandVisualTrashAssessmentID;

            }

            public OVTAObservationsMapInitJson MapInitJson { get; }
            public int ovtaID { get; }
            public string GeoServerUrl { get; }
        }
    }
}