using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Areas.Trash.Views.Home
{
    public class IndexViewData : NeptuneViewData
    {
        public ViewDataForAngularClass ViewDataForAngular { get; }
        public MapInitJson MapInitJson { get; }
        public string AllBMPsUrl { get; }

        public IndexViewData(Person currentPerson, NeptunePage neptunePage, MapInitJson mapInitJson, DbSet<Models.TreatmentBMP> treatmentBMPs, List<TrashCaptureStatusType> trashCaptureStatusTypes) : base(currentPerson, neptunePage)
        {
            MapInitJson = mapInitJson;
            ViewDataForAngular = new ViewDataForAngularClass(mapInitJson,
                treatmentBMPs, trashCaptureStatusTypes);
            EntityName = "Trash Module";
            PageTitle = "Welcome";
            AllBMPsUrl =
                SitkaRoute<TreatmentBMPController>.BuildAbsoluteUrlHttpsFromExpression(x => x.Index(),
                    NeptuneWebConfiguration.CanonicalHostName);

            ;
        }

        public class ViewDataForAngularClass
        {
            public MapInitJson MapInitJson { get; }
            public List<TreatmentBMPSimple> TreatmentBMPs { get; }
            public List<TrashCaptureStatusType> TrashCaptureStatusTypes { get; }

            public ViewDataForAngularClass(MapInitJson mapInitJson, IEnumerable<Models.TreatmentBMP> treatmentBMPs, List<TrashCaptureStatusType> trashCaptureStatusTypeSimples)
            {
                MapInitJson = mapInitJson;
                TreatmentBMPs = treatmentBMPs.Select(x => new TreatmentBMPSimple(x)).ToList();
                TrashCaptureStatusTypes = trashCaptureStatusTypeSimples;
            }
        }
    }
}