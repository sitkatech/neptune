namespace Neptune.Web.Models
{
    public class TreatmentBMPSimple
    {
        public int TreatmentBMPID { get; set; }
        public string DisplayName { get; set; }
        public string Type { get; set; }

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
            Type = treatmentBMP.TreatmentBMPType.TreatmentBMPTypeName;
        }

        public TreatmentBMPSimple(int treatmentBMPID, string displayName, string type)
        {
            TreatmentBMPID = treatmentBMPID;
            DisplayName = displayName;
            Type = type;
        }
    }
}