namespace Neptune.Web.Models
{
    public class TreatmentBMPSimple
    {
        public int TreatmentBMPID { get; set; }
        public string DisplayName { get; set; }
        public string TreatmentBMPTypeName { get; set; }

        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public TreatmentBMPSimple()
        {
        }

        public TreatmentBMPSimple(TreatmentBMP treatmentBMP)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            DisplayName = treatmentBMP.TreatmentBMPName;
            TreatmentBMPTypeName = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName;
        }

        public TreatmentBMPSimple(int treatmentBMPID, string displayName, string treatmentBMPTypeName)
        {
            TreatmentBMPID = treatmentBMPID;
            DisplayName = displayName;
            TreatmentBMPTypeName = treatmentBMPTypeName;
        }
    }
}