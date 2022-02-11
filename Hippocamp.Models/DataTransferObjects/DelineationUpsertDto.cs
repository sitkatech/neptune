namespace Hippocamp.Models.DataTransferObjects
{
    public class DelineationUpsertDto
    {
        public int DelineationID { get; set; }
        public int DelineationTypeID { get; set; }
        public double? DelineationArea { get; set; }
        public string Geometry { get; set; }
        public int TreatmentBMPID { get; set; }
       
    }
}