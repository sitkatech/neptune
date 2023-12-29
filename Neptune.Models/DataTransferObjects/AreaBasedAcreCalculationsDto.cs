namespace Neptune.Models.DataTransferObjects
{
    public class AreaBasedAcreCalculationsDto
    {
        public double FullTrashCaptureAcreage { get; set; }
        public double EquivalentAreaAcreage { get; set; }
        public double TotalAcresCaptured { get; set; }
        public double TotalPLUAcres { get; set; }

        public double PercentTreated { get; set; }

    }
}