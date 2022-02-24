//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasinStaging]
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
        public static ModelBasinStaging GetModelBasinStaging(this IQueryable<ModelBasinStaging> modelBasinStagings, int modelBasinStagingID)
        {
            var modelBasinStaging = modelBasinStagings.SingleOrDefault(x => x.ModelBasinStagingID == modelBasinStagingID);
            Check.RequireNotNullThrowNotFound(modelBasinStaging, "ModelBasinStaging", modelBasinStagingID);
            return modelBasinStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteModelBasinStaging(this IQueryable<ModelBasinStaging> modelBasinStagings, List<int> modelBasinStagingIDList)
        {
            if(modelBasinStagingIDList.Any())
            {
                modelBasinStagings.Where(x => modelBasinStagingIDList.Contains(x.ModelBasinStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteModelBasinStaging(this IQueryable<ModelBasinStaging> modelBasinStagings, ICollection<ModelBasinStaging> modelBasinStagingsToDelete)
        {
            if(modelBasinStagingsToDelete.Any())
            {
                var modelBasinStagingIDList = modelBasinStagingsToDelete.Select(x => x.ModelBasinStagingID).ToList();
                modelBasinStagings.Where(x => modelBasinStagingIDList.Contains(x.ModelBasinStagingID)).Delete();
            }
        }

        public static void DeleteModelBasinStaging(this IQueryable<ModelBasinStaging> modelBasinStagings, int modelBasinStagingID)
        {
            DeleteModelBasinStaging(modelBasinStagings, new List<int> { modelBasinStagingID });
        }

        public static void DeleteModelBasinStaging(this IQueryable<ModelBasinStaging> modelBasinStagings, ModelBasinStaging modelBasinStagingToDelete)
        {
            DeleteModelBasinStaging(modelBasinStagings, new List<ModelBasinStaging> { modelBasinStagingToDelete });
        }
    }
}