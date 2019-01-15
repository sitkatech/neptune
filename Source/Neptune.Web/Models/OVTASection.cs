using System;
using System.Collections.Generic;
using System.Web;

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

        public string GetSectionUrl(OnlandVisualTrashAssessment ovta)
        {
            return "lol you forgot to code the section urls";
        }
    }



    public class OVTASubsectionData
    {
        public string SubsectionName { get; set; }
        public string SubsectionUrl { get; set; }
        public HtmlString SectionCompletionStatusIndicator { get; set; }
    }
}