using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlan
    {
        public string MaintenanceContactAddressToString()
        {
            return string.Join(" ",
                new List<string>
                {
                    MaintenanceContactAddress1,
                    MaintenanceContactAddress2,
                    MaintenanceContactCity,
                    MaintenanceContactState,
                    MaintenanceContactZip
                }.Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public string GetLatestOandMVerificationDate()
        {
            return this.WaterQualityManagementPlanVerifies.Count > 0 ? WaterQualityManagementPlanVerifies.Select(x => x.LastEditedDate).Max().ToString("MM/dd/yyyy") : "";
        }

        //public string GetLatestOandMVerificationUrl()
        //{
        //    return WaterQualityManagementPlanVerifies.Count > 0 ? WaterQualityManagementPlanVerifies.Single(x => x.LastEditedDate == this.WaterQualityManagementPlanVerifies.Select(y => y.LastEditedDate).Max()).GetDetailUrl() : string.Empty;
        //}


        public bool HasAllRequiredDocuments()
        {
            foreach (var documentType in WaterQualityManagementPlanDocumentType.All.Where(x => x.IsRequired))
            {
                if (WaterQualityManagementPlanDocuments.All(x => x.WaterQualityManagementPlanDocumentType != documentType))
                {
                    return false;
                }
            }

            return true;
        }

        public Geometry GetCatchmentGeometry()
        {
            return WaterQualityManagementPlanBoundary?.GeometryNative;
        }

        public IEnumerable<HRUCharacteristic> GetHRUCharacteristics(NeptuneDbContext dbContext)
        {
            if (WaterQualityManagementPlanModelingApproachID == WaterQualityManagementPlanModelingApproach.Simplified
                    .WaterQualityManagementPlanModelingApproachID)
            {
                return dbContext.HRUCharacteristics.Include(x => x.LoadGeneratingUnit).AsNoTracking().Where(x =>
                    x.LoadGeneratingUnit.WaterQualityManagementPlanID == WaterQualityManagementPlanID);
            }

            var delineationIDs = Delineations
                .ListByTreatmentBMPIDList(dbContext, TreatmentBMPs.Select(x => x.TreatmentBMPID))
                .Select(x => x.DelineationID).ToList();

            return dbContext.HRUCharacteristics.Include(x => x.LoadGeneratingUnit).AsNoTracking().Where(x =>
                x.LoadGeneratingUnit.DelineationID.HasValue &&
                delineationIDs.Contains(x.LoadGeneratingUnit.DelineationID.Value));
        }

        public void DeleteFull(NeptuneDbContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
