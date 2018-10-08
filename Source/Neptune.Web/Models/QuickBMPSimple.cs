
namespace Neptune.Web.Models
{
    public class QuickBMPSimple
    {
        
        public int? QuickBMPID { get; set; }
        public string DisplayName { get; set; }
        public int QuickTreatmentBMPTypeID { get; set; }
        public string QuickBMPTypeName { get; set; }
        public string QuickBMPNote { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public QuickBMPSimple()
        {
        }

        public QuickBMPSimple(QuickBMP quickBMP)
        {
            QuickBMPID = quickBMP.QuickBMPID;
            DisplayName = quickBMP.QuickBMPName;
            QuickTreatmentBMPTypeID = quickBMP.TreatmentBMPTypeID;
            QuickBMPTypeName = quickBMP.TreatmentBMPType.TreatmentBMPTypeName;
            QuickBMPNote = quickBMP.QuickBMPNote;
        }
    }
}