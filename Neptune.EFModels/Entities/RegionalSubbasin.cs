using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    public partial class RegionalSubbasin
    {
        //public static HtmlString GetDisplayNameAsUrl(this RegionalSubbasin regionalSubbasin)
        //{

        //    return new HtmlString($"<a href='{DetailUrlTemplate.ParameterReplace(regionalSubbasin.RegionalSubbasinID)}'>{regionalSubbasin.Watershed} - {regionalSubbasin.DrainID}: {regionalSubbasin.RegionalSubbasinID}</a>"); 
        //}

        public IEnumerable<TreatmentBMP> GetTreatmentBMPs(NeptuneDbContext dbContext)
        {
            return TreatmentBMPs.GetNonPlanningModuleBMPs(dbContext)
                .Where(x => CatchmentGeometry.Contains(x.LocationPoint));
        }

        public IEnumerable<HRUCharacteristic> GetHRUCharacteristics(NeptuneDbContext dbContext)
        {
            return dbContext.HRUCharacteristics
                .Include(x => x.LoadGeneratingUnit)
                .AsNoTracking()
                .Where(x => x.LoadGeneratingUnit.RegionalSubbasinID == RegionalSubbasinID);
        }
    }
}