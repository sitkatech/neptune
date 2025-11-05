namespace Neptune.Models.DataTransferObjects
{
    public class AreaBasedAcreCalculationsDto
    {
        public double FullTrashCaptureAcreagePLU { get; set; }
        public double PartialTrashCaptureAcreagePLU { get; set; }
        public double UntreatedAcreagePLU { get; set; }


        public double FullTrashCaptureAcreageALU { get; set; }
        public double PartialTrashCaptureAcreageALU { get; set; }
        public double UntreatedAcreageALU { get; set; }

    }
}