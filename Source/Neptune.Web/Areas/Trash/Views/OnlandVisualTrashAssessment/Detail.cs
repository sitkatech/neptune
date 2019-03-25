using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LtInfo.Common.Mvc;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Areas.Trash.Views.OnlandVisualTrashAssessment
{
    public abstract class Detail : TypedWebViewPage<DetailViewData>
    {
        
    }

    public class DetailViewData : TrashModuleViewData
    {
        public Models.OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; }

        public DetailViewData(Person currentPerson, Models.OnlandVisualTrashAssessment onlandVisualTrashAssessment) : base(currentPerson)
        {
            OnlandVisualTrashAssessment = onlandVisualTrashAssessment;
            EntityName = "On-land Visual Trash Assessment";
            EntityUrl = SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Index());
        }
    }
}
