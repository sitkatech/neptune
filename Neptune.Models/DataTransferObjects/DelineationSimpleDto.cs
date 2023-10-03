namespace Neptune.Models.DataTransferObjects
{
    public partial class DelineationSimpleDto
    {
        public string Geometry { get; set; }
        public double? DelineationArea { get; set; }
        public string DelineationTypeName { get; set; }
    }
}