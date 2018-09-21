namespace Neptune.Web.Models
{
    public class SourceControlBMPSimple
    {
        public int? SourceControlBMPID { get; set; }
        public string SourceControlBMPAttributeCategoryName { get; set; }
        public string SourceControlBMPAttributeName { get; set; }
        public bool IsPresent { get; set; }
        public string SourceControlBMPNote { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public SourceControlBMPSimple()
        {
        }

        public SourceControlBMPSimple(SourceControlBMPAttribute sourceControlBMPAttribute)
        {
            SourceControlBMPID = null;
            SourceControlBMPAttributeCategoryName = sourceControlBMPAttribute
                .SourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryName;
            SourceControlBMPAttributeName = sourceControlBMPAttribute.SourceControlBMPAttributeName;
            IsPresent = false;
            SourceControlBMPNote = null;
        }

        public SourceControlBMPSimple(SourceControlBMP sourceControlBMP)
        {
            SourceControlBMPID = sourceControlBMP.SourceControlBMPID;
            SourceControlBMPAttributeCategoryName = sourceControlBMP.SourceControlBMPAttribute
                .SourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryName;
            SourceControlBMPAttributeName = sourceControlBMP.SourceControlBMPAttribute.SourceControlBMPAttributeName;
            IsPresent = sourceControlBMP.IsPresent;
            SourceControlBMPNote = sourceControlBMP.SourceControlBMPNote;
        }
    }
}

