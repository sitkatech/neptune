using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static class vTrashGeneratingUnitLoadStatisticModelExtensions
    {
        public static OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea(
            this vTrashGeneratingUnitLoadStatistic vTrashGeneratingUnitLoadStatistic)
        {
            return HttpRequestStorage.DatabaseEntities.OnlandVisualTrashAssessmentAreas.Find(
                vTrashGeneratingUnitLoadStatistic.OnlandVisualTrashAssessmentAreaID);
        }
    public static WaterQualityManagementPlan WaterQualityManagementPlan(
        this vTrashGeneratingUnitLoadStatistic vTrashGeneratingUnitLoadStatistic)
    {
        return HttpRequestStorage.DatabaseEntities.WaterQualityManagementPlans.Find(vTrashGeneratingUnitLoadStatistic
            .WaterQualityManagementPlanID);
    }
        public static TreatmentBMP TreatmentBMP(
            this vTrashGeneratingUnitLoadStatistic vTrashGeneratingUnitLoadStatistic)
        {
            return HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Find(vTrashGeneratingUnitLoadStatistic
                .TreatmentBMPID);
        }

        public static StormwaterJurisdiction StormwaterJurisdiction(
            this vTrashGeneratingUnitLoadStatistic vTrashGeneratingUnitLoadStatistic)
        {
            return HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.Find(vTrashGeneratingUnitLoadStatistic
                .StormwaterJurisdictionID);
        }

        public static LandUseBlock LandUseBlock(
            this vTrashGeneratingUnitLoadStatistic vTrashGeneratingUnitLoadStatistic)
        {
            return HttpRequestStorage.DatabaseEntities.LandUseBlocks.Find(vTrashGeneratingUnitLoadStatistic
                .LandUseBlockID);
        }
        
    }
}
