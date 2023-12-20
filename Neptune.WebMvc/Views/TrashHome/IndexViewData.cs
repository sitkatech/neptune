using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.Shared;
using Neptune.WebMvc.Views.Shared.Trash;
using NetTopologySuite.Features;
using TrashGeneratingUnitController = Neptune.WebMvc.Controllers.TrashGeneratingUnitController;
using TreatmentBMPController = Neptune.WebMvc.Controllers.TreatmentBMPController;

namespace Neptune.WebMvc.Views.TrashHome
{
    public class IndexViewData : TrashModuleViewData
    {
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public MapInitJson OVTABasedMapInitJson { get; }
        public MapInitJson AreaBasedMapInitJson { get; }
        public MapInitJson LoadBasedMapInitJson { get; }

        public string AllOVTAsUrl { get; }
        public string FindBMPUrl { get; }
        public string BeginOVTAUrl { get; }
        public string AddBMPUrl { get; }
        public ViewPageContentViewData ProgramOverviewPageContentViewData { get; }
        public string StormwaterJurisdictionCQLFilter { get; }

        public string NegativeStormwaterJurisdictionCQLFilter { get; }
        public string ScoreDescriptionsUrl { get; }

        public IEnumerable<SelectListItem> JurisdictionSelectList { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, WebConfiguration webConfiguration, EFModels.Entities.NeptunePage neptunePage, MapInitJson ovtaBasedMapInitJson,
            MapInitJson areaBasedMapInitJson, MapInitJson loadBasedMapInitJson,
            IEnumerable<Neptune.EFModels.Entities.TreatmentBMP> treatmentBMPs, List<TrashCaptureStatusType> trashCaptureStatusTypes,
            List<Neptune.EFModels.Entities.Parcel> parcels, List<StormwaterJurisdiction> stormwaterJurisdictions,
            FeatureCollection geoJsonForJurisdictions, EFModels.Entities.NeptunePage neptunePageTrashModuleProgramOverview) : base(httpContext, linkGenerator, currentPerson, webConfiguration, neptunePage)
        {
            OVTABasedMapInitJson = ovtaBasedMapInitJson;
            AreaBasedMapInitJson = areaBasedMapInitJson;
            LoadBasedMapInitJson = loadBasedMapInitJson;

            var stormwaterJurisdictionIDs = stormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID).ToList();
            StormwaterJurisdictionCQLFilter = currentPerson.GetStormwaterJurisdictionCqlFilter(stormwaterJurisdictionIDs);
            NegativeStormwaterJurisdictionCQLFilter = currentPerson.GetNegativeStormwaterJurisdictionCqlFilter(stormwaterJurisdictionIDs);
            JurisdictionSelectList = stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                .ToSelectList(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture),
                    x => x.GetOrganizationDisplayName());
            var showDropdown = stormwaterJurisdictions.Count > 1;
            var currentUserIsAnonymousOrUnassigned = CurrentPerson.IsAnonymousOrUnassigned();

            ViewDataForAngular = new ViewDataForAngularClass(linkGenerator, ovtaBasedMapInitJson, areaBasedMapInitJson,
                loadBasedMapInitJson,
                treatmentBMPs, trashCaptureStatusTypes, parcels, StormwaterJurisdictionCQLFilter, showDropdown,
                NegativeStormwaterJurisdictionCQLFilter, geoJsonForJurisdictions, currentUserIsAnonymousOrUnassigned, webConfiguration.MapServiceUrl);
            EntityName = "Trash Module";
            PageTitle = "Welcome";
            AllOVTAsUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Index());
            FindBMPUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.FindABMP());
            BeginOVTAUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.Instructions(ModelObjectHelpers.NotYetAssignedID));
            AddBMPUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x => x.New());

            ScoreDescriptionsUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x => x.ScoreDescriptions());

            ProgramOverviewPageContentViewData = new ViewPageContentViewData(linkGenerator, neptunePageTrashModuleProgramOverview, currentPerson);

        }

        public class ViewDataForAngularClass : TrashModuleMapViewDataForAngularBaseClass
        {

            public MapInitJson MapInitJson { get; }

            public MapInitJson OVTABasedMapInitJson { get; }
            public MapInitJson AreaBasedMapInitJson { get; }
            public MapInitJson LoadBasedMapInitJson { get; }

            public string AreaBasedCalculationsUrlTemplate { get; }

            public string OVTABasedResultsUrlTemplate { get; }

            public List<TreatmentBMPDisplayDto> TreatmentBMPs { get; }
            public List<ParcelSimpleDto> Parcels { get; }
            public List<TrashCaptureStatusType> TrashCaptureStatusTypes { get; }
            public string StormwaterJurisdictionCqlFilter { get; }
            public string NegativeStormwaterJurisdictionCqlFilter { get; }
            public FeatureCollection JurisdictionsGeoJson { get; }

            public string OrganizationUrlTemplate { get; }
            public string BMPUrlTemplate { get; }
            public string WQMPUrlTemplate { get; }
            public string OVTAAUrlTemplate { get; }
            public string OVTAUrlTemplate { get; }
            public string LoadBasedResultsUrlTemplate { get; }

            public bool ShowDropdown { get; }
            public bool CurrentUserIsAnonymousOrUnassigned { get; set; }


            public ViewDataForAngularClass(LinkGenerator linkGenerator, MapInitJson ovtaBasedMapInitJson,
                MapInitJson areaBasedMapInitJson,
                MapInitJson loadBasedMapInitJson, IEnumerable<EFModels.Entities.TreatmentBMP> treatmentBMPs,
                List<TrashCaptureStatusType> trashCaptureStatusTypeSimples, List<EFModels.Entities.Parcel> parcels,
                string stormwaterJurisdictionCqlFilter, bool showDropdown,
                string negativeStormwaterJurisdictionCqlFilter, FeatureCollection jurisdictionsGeoJson,
                bool currentUserIsAnonymousOrUnassigned, string geoServerUrl) : base(geoServerUrl)
            {
                OVTABasedMapInitJson = ovtaBasedMapInitJson;
                AreaBasedMapInitJson = areaBasedMapInitJson;
                LoadBasedMapInitJson = loadBasedMapInitJson;
                // it's kind of weird that we need a "global" json that's just a copy of the OVTA-based json, but it works
                MapInitJson = OVTABasedMapInitJson;

                TreatmentBMPs = treatmentBMPs.Select(x => x.AsDisplayDto()).ToList();
                Parcels = parcels.Select(x => x.AsSimpleDto()).ToList();
                TrashCaptureStatusTypes = trashCaptureStatusTypeSimples;
                StormwaterJurisdictionCqlFilter = stormwaterJurisdictionCqlFilter;
                ShowDropdown = showDropdown;
                CurrentUserIsAnonymousOrUnassigned = currentUserIsAnonymousOrUnassigned;
                NegativeStormwaterJurisdictionCqlFilter = negativeStormwaterJurisdictionCqlFilter;
                AreaBasedCalculationsUrlTemplate =
                    new UrlTemplate<int>(SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(linkGenerator, x =>
                        x.AcreBasedCalculations(UrlTemplate.Parameter1Int))).UrlTemplateString;

                JurisdictionsGeoJson = jurisdictionsGeoJson;

                OVTABasedResultsUrlTemplate = new UrlTemplate<int>(
                    SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(linkGenerator, x =>
                        x.OVTABasedResultsCalculations(UrlTemplate.Parameter1Int))).UrlTemplateString;

                LoadBasedResultsUrlTemplate =
                    new UrlTemplate<int>(SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(linkGenerator, x =>
                        x.LoadBasedResultsCalculations(UrlTemplate.Parameter1Int))).UrlTemplateString;
                //LoadBasedCurrentOrNetChangeTemplate = new UrlTemplate<int>(SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(_linkGenerator, x => x.LoadBasedCurrentOrNetChange(UrlTemplate.Parameter1Int))).UrlTemplateString;

                // Templates for links in detail pop-up
                OrganizationUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<OrganizationController>.BuildUrlFromExpression(linkGenerator, x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;
                BMPUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(linkGenerator, x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;
                WQMPUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(linkGenerator, x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;
                OVTAAUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(linkGenerator, x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;
                OVTAUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(linkGenerator, x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;

            }
        }
    }
}