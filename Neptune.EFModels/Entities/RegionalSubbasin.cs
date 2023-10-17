namespace Neptune.EFModels.Entities
{
    public partial class RegionalSubbasin
    {
        public string GetDisplayName()
        {
            return $"{Watershed} - {DrainID}: {RegionalSubbasinID}";
        }

        public IEnumerable<TreatmentBMP> GetTreatmentBMPs(NeptuneDbContext dbContext)
        {
            return TreatmentBMPs.GetNonPlanningModuleBMPs(dbContext)
                .Where(x => CatchmentGeometry.Contains(x.LocationPoint));
        }
    }
}