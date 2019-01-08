using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public partial class SourceControlBMP : IAuditableEntity
    {

        public SourceControlBMP(SourceControlBMPSimple sourceControlBMPSimple, int waterQualityManagementPlanID)
        {
            SourceControlBMPID = sourceControlBMPSimple.SourceControlBMPID ?? ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            WaterQualityManagementPlanID = waterQualityManagementPlanID;
            SourceControlBMPAttributeID = sourceControlBMPSimple.SourceControlBMPAttributeID;
            IsPresent = sourceControlBMPSimple.IsPresent;
            SourceControlBMPNote = sourceControlBMPSimple.SourceControlBMPNote;
        }


        public string GetAuditDescriptionString()
        {
            return SourceControlBMPID.ToString();
        }
    }
}