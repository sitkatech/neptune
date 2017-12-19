//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModeledCatchmentGeometryStaging]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ModeledCatchmentGeometryStaging GetModeledCatchmentGeometryStaging(this IQueryable<ModeledCatchmentGeometryStaging> modeledCatchmentGeometryStagings, int modeledCatchmentGeometryStagingID)
        {
            var modeledCatchmentGeometryStaging = modeledCatchmentGeometryStagings.SingleOrDefault(x => x.ModeledCatchmentGeometryStagingID == modeledCatchmentGeometryStagingID);
            Check.RequireNotNullThrowNotFound(modeledCatchmentGeometryStaging, "ModeledCatchmentGeometryStaging", modeledCatchmentGeometryStagingID);
            return modeledCatchmentGeometryStaging;
        }

        public static void DeleteModeledCatchmentGeometryStaging(this List<int> modeledCatchmentGeometryStagingIDList)
        {
            if(modeledCatchmentGeometryStagingIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllModeledCatchmentGeometryStagings.RemoveRange(HttpRequestStorage.DatabaseEntities.ModeledCatchmentGeometryStagings.Where(x => modeledCatchmentGeometryStagingIDList.Contains(x.ModeledCatchmentGeometryStagingID)));
            }
        }

        public static void DeleteModeledCatchmentGeometryStaging(this ICollection<ModeledCatchmentGeometryStaging> modeledCatchmentGeometryStagingsToDelete)
        {
            if(modeledCatchmentGeometryStagingsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllModeledCatchmentGeometryStagings.RemoveRange(modeledCatchmentGeometryStagingsToDelete);
            }
        }

        public static void DeleteModeledCatchmentGeometryStaging(this int modeledCatchmentGeometryStagingID)
        {
            DeleteModeledCatchmentGeometryStaging(new List<int> { modeledCatchmentGeometryStagingID });
        }

        public static void DeleteModeledCatchmentGeometryStaging(this ModeledCatchmentGeometryStaging modeledCatchmentGeometryStagingToDelete)
        {
            DeleteModeledCatchmentGeometryStaging(new List<ModeledCatchmentGeometryStaging> { modeledCatchmentGeometryStagingToDelete });
        }
    }
}