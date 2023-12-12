namespace Neptune.Models.DataTransferObjects
{
    public class DelineationDto
    {
        public int DelineationID { get; set; }
        public int DelineationTypeID { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? DateLastVerified { get; set; }
        public int? VerifiedByPersonID { get; set; }
        public int TreatmentBMPID { get; set; }
        public DateTime DateLastModified { get; set; }
        public bool HasDiscrepancies { get; set; }
        public string Geometry { get; set; }
        public double? DelineationArea { get; set; }
        public string DelineationTypeName { get; set; }
    }
}