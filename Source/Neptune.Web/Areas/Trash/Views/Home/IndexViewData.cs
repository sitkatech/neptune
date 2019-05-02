using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common;
using LtInfo.Common.Mvc;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Areas.Trash.Views.Shared;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views;
using Neptune.Web.Views.Shared;
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
        public string RefreshTguUrl { get; }

        public IEnumerable<SelectListItem> JurisdictionSelectList { get; }

        public IndexViewData(Person currentPerson, NeptunePage neptunePage, MapInitJson ovtaBasedMapInitJson, MapInitJson areaBasedMapInitJson, MapInitJson loadBasedMapInitJson,
            IEnumerable<Models.TreatmentBMP> treatmentBMPs, List<TrashCaptureStatusType> trashCaptureStatusTypes,
            List<Models.Parcel> parcels) : base(currentPerson, neptunePage)
        {
            OVTABasedMapInitJson = ovtaBasedMapInitJson;
            AreaBasedMapInitJson = areaBasedMapInitJson;
            LoadBasedMapInitJson = loadBasedMapInitJson;

            var stormwaterJurisdictionsPersonCanEdit = currentPerson.GetStormwaterJurisdictionsPersonCanEdit().ToList();
            StormwaterJurisdictionCQLFilter = currentPerson.GetStormwaterJurisdictionCqlFilter();
            JurisdictionSelectList = stormwaterJurisdictionsPersonCanEdit.OrderBy(x => x.GetOrganizationDisplayName()).ToSelectList(x => x.StormwaterJurisdictionID.ToString(CultureInfo.InvariantCulture), x => x.GetOrganizationDisplayName());
            var showDropdown = stormwaterJurisdictionsPersonCanEdit.Count() > 1;

            ViewDataForAngular = new ViewDataForAngularClass(ovtaBasedMapInitJson, areaBasedMapInitJson, loadBasedMapInitJson,
                treatmentBMPs, trashCaptureStatusTypes, parcels, StormwaterJurisdictionCQLFilter, showDropdown);
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

            ProgramOverviewPageContentViewData = new ViewPageContentViewData(NeptunePage.GetNeptunePageByPageType(NeptunePageType.TrashModuleProgramOverview), currentPerson);

        }

        public class ViewDataForAngularClass : TrashModuleMapViewDataForAngularBaseClass
        {
            public MapInitJson MapInitJson { get; }
            public MapInitJson AreaBasedMapInitJson { get; }
            public MapInitJson LoadBasedMapInitJson { get; }

            public string AreaBasedCalculationsUrlTemplate { get; }

            public string OVTABasedResultsUrlTemplate { get; }

            public List<TreatmentBMPSimple> TreatmentBMPs { get; }
            public List<ParcelSimple> Parcels { get; }
            public List<TrashCaptureStatusType> TrashCaptureStatusTypes { get; }
            public string StormwaterJurisdictionCqlFilter { get; }

            public bool ShowDropdown { get; }

            public ViewDataForAngularClass(MapInitJson mapInitJson, MapInitJson areaBasedMapInitJson, MapInitJson loadBasedMapInitJson, IEnumerable<Models.TreatmentBMP> treatmentBMPs,
                List<TrashCaptureStatusType> trashCaptureStatusTypeSimples, List<Models.Parcel> parcels,
                string stormwaterJurisdictionCqlFilter, bool showDropdown)
            {
                MapInitJson = mapInitJson;
                AreaBasedMapInitJson = areaBasedMapInitJson;
                LoadBasedMapInitJson = loadBasedMapInitJson;
                
                TreatmentBMPs = treatmentBMPs.Select(x => new TreatmentBMPSimple(x)).ToList();
                Parcels = parcels.Select(x => new ParcelSimple(x)).ToList();
                TrashCaptureStatusTypes = trashCaptureStatusTypeSimples;
                StormwaterJurisdictionCqlFilter = stormwaterJurisdictionCqlFilter;
                ShowDropdown = showDropdown;
                AreaBasedCalculationsUrlTemplate =
                    new UrlTemplate<int>(SitkaRoute<TrashGeneratingUnitController>.BuildUrlFromExpression(x =>
                        x.AcreBasedCalculations(UrlTemplate.Parameter1Int))).UrlTemplateString;

                OVTABasedResultsUrlTemplate = new UrlTemplate<int>(SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(x =>
                    x.OVTABasedResultsCalculations(UrlTemplate.Parameter1Int))).UrlTemplateString;
            }
        }
    }
}