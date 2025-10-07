namespace Neptune.Models.DataTransferObjects
{
    public class HRULogDto
    {
        public int HRULogID { get; set; }
        public DateTime RequestDate { get; set; }
        public bool Success { get; set; }
        public string? HRURequest { get; set; }
        public string? HRUResponse { get; set; }
    }
}
