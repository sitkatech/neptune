namespace Neptune.Models.DataTransferObjects
{
    public class TreatmentBMPImageDto
    {
        public int TreatmentBMPImageID { get; set; }
        public int FileResourceID { get; set; }
        public string FileResourceGUID { get; set; }
        public string? Caption { get; set; }
        public int TreatmentBMPID { get; set; }
        public DateOnly UploadDate { get; set; }
    }
}
