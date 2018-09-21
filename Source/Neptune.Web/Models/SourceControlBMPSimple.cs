namespace Neptune.Web.Models
{
    public class SourceControlBMPSimple
    {
        public int SourceControlBMPID { get; set; }
        public string SourceControlBMPAttributeName { get; set; }
        public bool IsPresent { get; set; }
        public string SourceControlBMPNote { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public SourceControlBMPSimple()
        {
        }

        public SourceControlBMPSimple(SourceControlBMP sourceControlBMP)
        {
            SourceControlBMPID = sourceControlBMP.SourceControlBMPID;
            SourceControlBMPAttributeName = sourceControlBMP.SourceControlBMPAttribute.SourceControlBMPAttributeName;
            IsPresent = sourceControlBMP.IsPresent;
            SourceControlBMPNote = sourceControlBMP.SourceControlBMPNote;
        }
    }
}

