﻿using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class RecordObservationsViewData : OVTASectionViewData
    {
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public OVTAObservationsMapInitJson MapInitJson { get; }

        public RecordObservationsViewData(Person currentPerson, Models.OnlandVisualTrashAssessment ovta, OVTAObservationsMapInitJson mapInitJson)
            : base(currentPerson, Models.OVTASection.RecordObservations, ovta)
        {
            MapInitJson = mapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson, ovta);
        }

        public class ViewDataForAngularClass
        {
            public ViewDataForAngularClass(OVTAObservationsMapInitJson mapInitJson, Models.OnlandVisualTrashAssessment ovta)
            {
                MapInitJson = mapInitJson;
                ovtaID = ovta.OnlandVisualTrashAssessmentID;
            }

            public OVTAObservationsMapInitJson MapInitJson { get; }
            public int ovtaID { get; }
        }
    }
}