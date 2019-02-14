//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModeledCatchmentGeometryStaging]
using System.Collections.Generic;
using System.Linq;
using Z.EntityFramework.Plus;
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

        // Delete using an IDList (Firma style)
        public static void DeleteModeledCatchmentGeometryStaging(this IQueryable<ModeledCatchmentGeometryStaging> modeledCatchmentGeometryStagings, List<int> modeledCatchmentGeometryStagingIDList)
        {
            if(modeledCatchmentGeometryStagingIDList.Any())
            {
                modeledCatchmentGeometryStagings.Where(x => modeledCatchmentGeometryStagingIDList.Contains(x.ModeledCatchmentGeometryStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteModeledCatchmentGeometryStaging(this IQueryable<ModeledCatchmentGeometryStaging> modeledCatchmentGeometryStagings, ICollection<ModeledCatchmentGeometryStaging> modeledCatchmentGeometryStagingsToDelete)
        {
            if(modeledCatchmentGeometryStagingsToDelete.Any())
            {
                var modeledCatchmentGeometryStagingIDList = modeledCatchmentGeometryStagingsToDelete.Select(x => x.ModeledCatchmentGeometryStagingID).ToList();
                modeledCatchmentGeometryStagings.Where(x => modeledCatchmentGeometryStagingIDList.Contains(x.ModeledCatchmentGeometryStagingID)).Delete();
            }
        }

        public static void DeleteModeledCatchmentGeometryStaging(this IQueryable<ModeledCatchmentGeometryStaging> modeledCatchmentGeometryStagings, int modeledCatchmentGeometryStagingID)
        {
            DeleteModeledCatchmentGeometryStaging(modeledCatchmentGeometryStagings, new List<int> { modeledCatchmentGeometryStagingID });
        }

        public static void DeleteModeledCatchmentGeometryStaging(this IQueryable<ModeledCatchmentGeometryStaging> modeledCatchmentGeometryStagings, ModeledCatchmentGeometryStaging modeledCatchmentGeometryStagingToDelete)
        {
            DeleteModeledCatchmentGeometryStaging(modeledCatchmentGeometryStagings, new List<ModeledCatchmentGeometryStaging> { modeledCatchmentGeometryStagingToDelete });
        }
    }
}