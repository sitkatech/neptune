using System;
using System.Collections.Generic;
using System.Web;
using Neptune.Web.Areas.Trash.Controllers;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public abstract partial class OVTASection
    {
        public bool ExpandMenu(OnlandVisualTrashAssessment ovta)
        {
            return false;
        }

        public IEnumerable<OVTASubsectionData> GetSubsections(OnlandVisualTrashAssessment ovta)
        {
            throw new NotImplementedException();
        }

        public abstract string GetSectionUrl(OnlandVisualTrashAssessment ovta);

        public abstract OVTASection GetNextSection();
    }

    public partial class OVTASectionInstructions
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.Instructions(ovta));
        }

        public override OVTASection GetNextSection()
        {
            return RecordObservations;
        }
    }

    public partial class OVTASectionRecordObservations
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.RecordObservations(ovta));
        }

        public override OVTASection GetNextSection()
        {
            return VerifyOVTAArea;
        }
    }

    public partial class OVTASectionVerifyOVTAArea
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.VerifyOVTAArea(ovta));
        }

        public override OVTASection GetNextSection()
        {
            return FinalizeOVTA;
        }
    }

    public partial class OVTASectionFinalizeOVTA
    {
        public override string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return SitkaRoute<OnlandVisualTrashAssessmentController>.BuildUrlFromExpression(x => x.FinalizeOVTA(ovta));
        }

        public override OVTASection GetNextSection()
        {
            throw new NotImplementedException();
        }
    }

    public class OVTASubsectionData
    {
        public string SubsectionName { get; set; }
        public string SubsectionUrl { get; set; }
        public HtmlString SectionCompletionStatusIndicator { get; set; }
    }
}