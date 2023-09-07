using Neptune.Web.Models;

namespace Neptune.EFModels.Entities
{
    public partial class QuickBMP : IAuditableEntity
    {

        public string GetAuditDescriptionString()
        {
            return QuickBMPName;
        }
    }
}