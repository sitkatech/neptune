namespace Neptune.Models.DataTransferObjects
{
    public class TreatmentBMPHRUCharacteristicsSummarySimpleDto
    {
        public int ProjectHRUCharacteristicID { get; set; }
        public int TreatmentBMPID { get; set; }
        public string LandUse { get; set; }
        public double Area { get; set; }
        public double ImperviousCover { get; set; }
    }
}