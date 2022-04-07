namespace Hippocamp.Models.DataTransferObjects
{
    public class TreatmentBMPDisplayDto
    {
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public string TreatmentBMPTypeName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int? ProjectID { get; set; }
    }
}