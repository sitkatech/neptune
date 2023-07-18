namespace Neptune.Web.Models
{
    public partial class SourceControlBMPAttribute : IAuditableEntity
    {



        public string GetAuditDescriptionString()
        {
            return SourceControlBMPAttributeName;
        }
    }
}