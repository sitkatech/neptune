﻿using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessment.MapInitJson;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessment
{
    public class RefineAssessmentAreaViewData : OVTASectionViewData
    {
        public string MapFormID { get;  }
        public RefineAssessmentAreaMapInitJson MapInitJson { get; }
        public string GeoServerUrl { get;  }

        public RefineAssessmentAreaViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.OVTASection ovtaSection, EFModels.Entities.OnlandVisualTrashAssessment ovta, RefineAssessmentAreaMapInitJson mapInitJson, string geoServerUrl) : base(httpContext, linkGenerator, currentPerson, webConfiguration, ovtaSection, ovta)
        {
            MapInitJson = mapInitJson;
            GeoServerUrl = geoServerUrl;
            MapFormID = "refineAssessmentAreaForm";
        }
    }
}