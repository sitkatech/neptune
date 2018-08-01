using System.Linq;

namespace Neptune.Web.Models
{
    public partial class CustomAttribute : IAuditableEntity
    {
        public string GetAuditDescriptionString()
        {
            return "Custom Attribute deleted";
        }
    }
}