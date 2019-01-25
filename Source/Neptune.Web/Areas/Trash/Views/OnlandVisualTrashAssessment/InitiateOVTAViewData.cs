using System.Collections.Generic;
using System.Web.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public class InitiateOVTAViewData : OVTASectionViewData
    {


        public InitiateOVTAViewData(Person currentPerson, StormwaterBreadCrumbEntity stormwaterBreadCrumbEntity, Models.OnlandVisualTrashAssessment ovta, IEnumerable<SelectListItem> jurisdictions)
            : base(currentPerson, stormwaterBreadCrumbEntity, Models.OVTASection.InitiateOVTA, ovta)
        {
            Jurisdictions = jurisdictions;
        }

        public IEnumerable<SelectListItem> Jurisdictions { get; }
    }
}

