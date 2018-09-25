using System.ComponentModel.DataAnnotations;

namespace Neptune.Web.Models
{
    public class SourceControlBMPSimple
    {
        public int? SourceControlBMPID { get; set; }
        public string SourceControlBMPAttributeCategoryName { get; set; }
        public int SourceControlBMPAttributeCategoryID { get; set; }
        public int SourceControlBMPAttributeID { get; set; }
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
            SourceControlBMPAttributeCategoryID = sourceControlBMPAttribute.SourceControlBMPAttributeCategoryID;
            SourceControlBMPAttributeCategoryName = sourceControlBMPAttribute.SourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryName;
            SourceControlBMPAttributeID = sourceControlBMPAttribute.SourceControlBMPAttributeID;
            SourceControlBMPAttributeName = sourceControlBMPAttribute.SourceControlBMPAttributeName;
            IsPresent = false;
            SourceControlBMPNote = null;
        }

        public SourceControlBMPSimple(SourceControlBMP sourceControlBMP)
        {
            SourceControlBMPID = sourceControlBMP.SourceControlBMPID;
            SourceControlBMPAttributeCategoryID = sourceControlBMP.SourceControlBMPAttribute
                .SourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryID;
            SourceControlBMPAttributeCategoryName = sourceControlBMP.SourceControlBMPAttribute
                .SourceControlBMPAttributeCategory.SourceControlBMPAttributeCategoryName;
            SourceControlBMPAttributeID = sourceControlBMP.SourceControlBMPAttributeID;
            SourceControlBMPAttributeName = sourceControlBMP.SourceControlBMPAttribute.SourceControlBMPAttributeName;
            IsPresent = sourceControlBMP.IsPresent;
            SourceControlBMPNote = sourceControlBMP.SourceControlBMPNote;
        }
    }
}

