using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Models
{
    public static class FieldVisitModelExtensions
    {
        public static void MarkFieldVisitAsProvisionalIfNonManager(this EFModels.Entities.FieldVisit fieldVisit, Person person)
        {
            var isAssignedToStormwaterJurisdiction = person.CanManageStormwaterJurisdiction(fieldVisit.TreatmentBMP.StormwaterJurisdictionID);
            if (!isAssignedToStormwaterJurisdiction)
            {
                fieldVisit.IsFieldVisitVerified = false;
            }
        }

        public static void VerifyFieldVisit(this EFModels.Entities.FieldVisit fieldVisit)
        {
            fieldVisit.IsFieldVisitVerified = true;
            fieldVisit.FieldVisitStatusID = (int) FieldVisitStatusEnum.Complete;
        }

        public static void MarkFieldVisitAsProvisional(this EFModels.Entities.FieldVisit fieldVisit)
        {
            fieldVisit.IsFieldVisitVerified = false;
        }

        public static void ReturnFieldVisitToEdit(this EFModels.Entities.FieldVisit fieldVisit)
        {
            fieldVisit.IsFieldVisitVerified = false;
            fieldVisit.FieldVisitStatusID = (int) FieldVisitStatusEnum.ReturnedToEdit;
        }

        //public static readonly UrlTemplate<int> WorkflowUrlTemplate = new UrlTemplate<int>(
        //    SitkaRoute<FieldVisitController>.BuildUrlFromExpression(t => t.Inventory(UrlTemplate.Parameter1Int)));

        //public static string GetEditUrl(this FieldVisit fieldVisit)
        //{
        //    return WorkflowUrlTemplate.ParameterReplace(fieldVisit.FieldVisitID);
        //}

        //public static HtmlString GetStatusAsWorkflowUrl(this EFModels.Entities.FieldVisit fieldVisit)
        //{
        //    var fieldVisitStatus = fieldVisit.FieldVisitStatus;
        //    return fieldVisitStatus != FieldVisitStatus.InProgress
        //        ? new HtmlString(fieldVisitStatus.FieldVisitStatusDisplayName)
        //        : UrlTemplate.MakeHrefString(WorkflowUrlTemplate.ParameterReplace(fieldVisit.FieldVisitID),
        //            fieldVisitStatus.FieldVisitStatusDisplayName);
        //}
    }
}