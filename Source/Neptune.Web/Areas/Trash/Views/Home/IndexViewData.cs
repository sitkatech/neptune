using System.Collections.Generic;
using System.Linq;
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
        public MapInitJson MapInitJson { get; }
        public string AllOVTAsUrl { get; }
        public string FindBMPUrl { get; }
        public string BeginOVTAUrl { get; }
        public string AddBMPUrl { get; }
        public ViewPageContentViewData ProgramOverviewPageContentViewData { get; }
        public string StormwaterJurisdictionCQLFilter { get; }
        public string RefreshTguUrl { get; }


        public IndexViewData(Person currentPerson, NeptunePage neptunePage, MapInitJson mapInitJson,
            IEnumerable<Models.TreatmentBMP> treatmentBMPs, List<TrashCaptureStatusType> trashCaptureStatusTypes,
            List<Models.Parcel> parcels) : base(currentPerson, neptunePage)
        {
            MapInitJson = mapInitJson;
            StormwaterJurisdictionCQLFilter = currentPerson.GetStormwaterJurisdictionCqlFilter();

            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson,
                treatmentBMPs, trashCaptureStatusTypes, parcels, StormwaterJurisdictionCQLFilter);
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
            public List<TreatmentBMPSimple> TreatmentBMPs { get; }
            public List<ParcelSimple> Parcels { get; }
            public List<TrashCaptureStatusType> TrashCaptureStatusTypes { get; }
            public string StormwaterJurisdictionCqlFilter { get; }

            public ViewDataForAngularClass(MapInitJson mapInitJson, IEnumerable<Models.TreatmentBMP> treatmentBMPs,
                List<TrashCaptureStatusType> trashCaptureStatusTypeSimples, List<Models.Parcel> parcels,
                string stormwaterJurisdictionCqlFilter)
            {
                MapInitJson = mapInitJson;
                TreatmentBMPs = treatmentBMPs.Select(x => new TreatmentBMPSimple(x)).ToList();
                Parcels = parcels.Select(x => new ParcelSimple(x)).ToList();
                TrashCaptureStatusTypes = trashCaptureStatusTypeSimples;
                StormwaterJurisdictionCqlFilter = stormwaterJurisdictionCqlFilter;
            }
        }
    }
}