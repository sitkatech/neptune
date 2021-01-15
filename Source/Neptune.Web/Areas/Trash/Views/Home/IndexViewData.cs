using GeoJSON.Net.Feature;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.Shared;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views.Shared;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using HomeController = Neptune.Web.Areas.Trash.Controllers.HomeController;
using TrashGeneratingUnitController = Neptune.Web.Areas.Trash.Controllers.TrashGeneratingUnitController;
using TreatmentBMPController = Neptune.Web.Controllers.TreatmentBMPController;

namespace Neptune.Web.Areas.Trash.Views.Home
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
        public string RefreshTguUrl { get; }
        public string ScoreDescriptionsUrl { get; }

        public IEnumerable<SelectListItem> JurisdictionSelectList { get; }

        public IndexViewData(Person currentPerson, NeptunePage neptunePage, MapInitJson ovtaBasedMapInitJson,
            MapInitJson areaBasedMapInitJson, MapInitJson loadBasedMapInitJson,
            IEnumerable<Models.TreatmentBMP> treatmentBMPs, List<TrashCaptureStatusType> trashCaptureStatusTypes,
            List<Models.Parcel> parcels, List<StormwaterJurisdiction> stormwaterJurisdictions,
            FeatureCollection geoJsonForJurisdictions) : base(currentPerson, neptunePage)
        {
            OVTABasedMapInitJson = ovtaBasedMapInitJson;
            AreaBasedMapInitJson = areaBasedMapInitJson;
            LoadBasedMapInitJson = loadBasedMapInitJson;

            var stormwaterJurisdictionIDs = stormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID).ToList();
            StormwaterJurisdictionCQLFilter =
                currentPerson.GetStormwaterJurisdictionCqlFilter(stormwaterJurisdictionIDs);
            NegativeStormwaterJurisdictionCQLFilter =
                currentPerson.GetNegativeStormwaterJurisdictionCqlFilter(stormwaterJurisdictionIDs);
            JurisdictionSelectList = stormwaterJurisdictions.OrderBy(x => x.GetOrganizationDisplayName())
                .ToSelectList(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture),
                    x => x.GetOrganizationDisplayName());
            var showDropdown = stormwaterJurisdictions.Count > 1;
            var currentUserIsAnonymousOrUnassigned = CurrentPerson.IsAnonymousOrUnassigned();

            ViewDataForAngular = new ViewDataForAngularClass(ovtaBasedMapInitJson, areaBasedMapInitJson,
                loadBasedMapInitJson,
                treatmentBMPs, trashCaptureStatusTypes, parcels, StormwaterJurisdictionCQLFilter, showDropdown,
                NegativeStormwaterJurisdictionCQLFilter, geoJsonForJurisdictions, currentUserIsAnonymousOrUnassigned);
            EntityName = "Trash Module";
            PageTitle = "Welcome";
            AllOVTAsUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildAbsoluteUrlHttpsFromExpression(x => x.Index(),
                    NeptuneWebConfiguration.CanonicalHostName);
            FindBMPUrl = SitkaRoute<TreatmentBMPController>.BuildAbsoluteUrlHttpsFromExpression(x => x.FindABMP(),
                NeptuneWebConfiguration.CanonicalHostName);
            BeginOVTAUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildAbsoluteUrlHttpsFromExpression(x =>
                        x.Instructions(null),
                    NeptuneWebConfiguration.CanonicalHostName);
            AddBMPUrl = SitkaRoute<TreatmentBMPController>.BuildAbsoluteUrlHttpsFromExpression(x => x.New(),
                NeptuneWebConfiguration.CanonicalHostName);

            RefreshTguUrl = SitkaRoute<HomeController>.BuildUrlFromExpression(x => x.RefreshTrashGeneratingUnits());
            ScoreDescriptionsUrl =
                SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.ScoreDescriptions());

            ProgramOverviewPageContentViewData = new ViewPageContentViewData(
                NeptunePage.GetNeptunePageByPageType(NeptunePageType.TrashModuleProgramOverview), currentPerson);

        }

        public class ViewDataForAngularClass : TrashModuleMapViewDataForAngularBaseClass
        {

            public MapInitJson MapInitJson { get; }

            public MapInitJson OVTABasedMapInitJson { get; }
            public MapInitJson AreaBasedMapInitJson { get; }
            public MapInitJson LoadBasedMapInitJson { get; }

            public string AreaBasedCalculationsUrlTemplate { get; }

            public string OVTABasedResultsUrlTemplate { get; }

            public List<TreatmentBMPSimple> TreatmentBMPs { get; }
            public List<ParcelSimple> Parcels { get; }
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


            public ViewDataForAngularClass(MapInitJson ovtaBasedMapInitJson, MapInitJson areaBasedMapInitJson,
                MapInitJson loadBasedMapInitJson, IEnumerable<Models.TreatmentBMP> treatmentBMPs,
                List<TrashCaptureStatusType> trashCaptureStatusTypeSimples, List<Models.Parcel> parcels,
                string stormwaterJurisdictionCqlFilter, bool showDropdown,
                string negativeStormwaterJurisdictionCqlFilter, FeatureCollection jurisdictionsGeoJson,
                bool currentUserIsAnonymousOrUnassigned)
            {
                OVTABasedMapInitJson = ovtaBasedMapInitJson;
                AreaBasedMapInitJson = areaBasedMapInitJson;
                LoadBasedMapInitJson = loadBasedMapInitJson;
                // it's kind of weird that we need a "global" json that's just a copy of the OVTA-based json, but it works
                MapInitJson = OVTABasedMapInitJson;

                TreatmentBMPs = treatmentBMPs.Select(x => new TreatmentBMPSimple(x)).ToList();
                Parcels = parcels.Select(x => new ParcelSimple(x)).ToList();
                TrashCaptureStatusTypes = trashCaptureStatusTypeSimples;
                StormwaterJurisdictionCqlFilter = stormwaterJurisdictionCqlFilter;
                ShowDropdown = showDropdown;
                CurrentUserIsAnonymousOrUnassigned = currentUserIsAnonymousOrUnassigned;
                NegativeStormwaterJurisdictionCqlFilter = negativeStormwaterJurisdictionCqlFilter;
                AreaBasedCalculationsUrlTemplate =
                    new UrlTemplate<int>(SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(x =>
                        x.AcreBasedCalculations(UrlTemplate.Parameter1Int))).UrlTemplateString;

                JurisdictionsGeoJson = jurisdictionsGeoJson;

                OVTABasedResultsUrlTemplate = new UrlTemplate<int>(
                    SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(x =>
                        x.OVTABasedResultsCalculations(UrlTemplate.Parameter1Int))).UrlTemplateString;

                LoadBasedResultsUrlTemplate =
                    new UrlTemplate<int>(SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(x =>
                        x.LoadBasedResultsCalculations(UrlTemplate.Parameter1Int))).UrlTemplateString;
                //LoadBasedCurrentOrNetChangeTemplate = new UrlTemplate<int>(SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(x => x.LoadBasedCurrentOrNetChange(UrlTemplate.Parameter1Int))).UrlTemplateString;

                // Templates for links in detail pop-up
                OrganizationUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<OrganizationController>.BuildUrlFromExpression(x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;
                BMPUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;
                WQMPUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;
                OVTAAUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;
                OVTAUrlTemplate =
                    new UrlTemplate<int>(
                        SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x =>
                            x.Detail(UrlTemplate.Parameter1Int))).UrlTemplateString;

            }
        }
    }
}