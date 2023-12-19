namespace Neptune.Models.DataTransferObjects
{
    public class LoadResultsDto
    {
        public double LoadFullCapture { get; set; }
        public double LoadPartialCapture { get; set; }
        public double LoadOVTA { get; set; }
        public double TotalAchieved { get; set; }
        public double TargetLoadReduction { get; set; }
    }
}