using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class FundingSource : IAuditableEntity
    {
        public string GetDisplayName()
        {
            return
                $"{FundingSourceName} ({Organization.GetOrganizationShortNameIfAvailable()}){(!IsActive ? " (Inactive)" : string.Empty)}";
        }

        public string GetAuditDescriptionString()
        {
            return FundingSourceName;
        }

        public List<TreatmentBMP> GetAssociatedTreatmentBMPs()
        {
            return FundingEventFundingSources.Select(x => x.FundingEvent.TreatmentBMP).Distinct().ToList();
        }
    }
}