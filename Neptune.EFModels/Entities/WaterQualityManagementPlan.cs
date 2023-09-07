using Neptune.Common;
using Neptune.Web.Models;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities
{
    public partial class WaterQualityManagementPlan : IAuditableEntity, IHaveHRUCharacteristics
    {
        public string GetAuditDescriptionString()
        {
            return $"Water Quality Management Plan \"{WaterQualityManagementPlanName}\"";
        }

        //private static readonly UrlTemplate<int> DetailUrlTemplate = new UrlTemplate<int>(
        //    SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
        //        c.Detail(UrlTemplate.Parameter1Int)));

        //public string GetDetailUrl()
        //{
        //    return DetailUrlTemplate.ParameterReplace(WaterQualityManagementPlanID);
        //}

        //private static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(
        //    SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
        //        c.Edit(UrlTemplate.Parameter1Int)));
        //public string GetEditUrl()
        //{
        //    return EditUrlTemplate.ParameterReplace(WaterQualityManagementPlanID);
        //}

        //private static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(
        //    SitkaRoute<WaterQualityManagementPlanController>.BuildUrlFromExpression(c =>
        //        c.Delete(UrlTemplate.Parameter1Int)));
        //public string GetDeleteUrl()
        //{
        //    return DeleteUrlTemplate.ParameterReplace(WaterQualityManagementPlanID);
        //}

        //public HtmlString GetDisplayNameAsUrl()
        //{
        //    return UrlTemplate.MakeHrefString(GetDetailUrl(), WaterQualityManagementPlanName);
        //}

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

        public double CalculateTotalAcreage()
        {
            return (WaterQualityManagementPlanBoundary?.GeometryNative?.Area ?? 0) * Constants.SquareMetersToAcres;
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
                return dbContext.HRUCharacteristics.Where(x =>
                    x.LoadGeneratingUnit.WaterQualityManagementPlanID == WaterQualityManagementPlanID);
            }
            else
            {
                var treatmentBMPIDs = TreatmentBMPs.Where(x => x.Delineation != null).Select(x => (int?)x.Delineation.DelineationID).ToList();

                return dbContext.HRUCharacteristics.Where(x =>
                    treatmentBMPIDs.Contains(x.LoadGeneratingUnit.DelineationID));
            }
        }

        public void DeleteFull(NeptuneDbContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
