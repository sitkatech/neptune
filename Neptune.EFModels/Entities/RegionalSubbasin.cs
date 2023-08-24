using Neptune.Web.Models;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class RegionalSubbasin: IHaveHRUCharacteristics, IAuditableEntity
    {
        public Geometry GetCatchmentGeometry()
        {
            return CatchmentGeometry;
        }

        public string GetAuditDescriptionString()
        {
            return $"{Watershed} {DrainID}: {RegionalSubbasinID}";
            
        }

        //public RegionalSubbasin GetOCSurveyDownstreamCatchment()
        //{
        //    return _dbContext.RegionalSubbasins.Single(x =>
        //        x.OCSurveyCatchmentID == OCSurveyDownstreamCatchmentID);
        //}

        //public IEnumerable<HRUCharacteristic> GetHRUCharacteristics()
        //{
        //    return _dbContext.HRUCharacteristics.Where(x =>
        //        x.LoadGeneratingUnit.RegionalSubbasinID == RegionalSubbasinID);
        //}
    }
}