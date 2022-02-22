//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasin]
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
        public static ModelBasin GetModelBasin(this IQueryable<ModelBasin> modelBasins, int modelBasinID)
        {
            var modelBasin = modelBasins.SingleOrDefault(x => x.ModelBasinID == modelBasinID);
            Check.RequireNotNullThrowNotFound(modelBasin, "ModelBasin", modelBasinID);
            return modelBasin;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteModelBasin(this IQueryable<ModelBasin> modelBasins, List<int> modelBasinIDList)
        {
            if(modelBasinIDList.Any())
            {
                modelBasins.Where(x => modelBasinIDList.Contains(x.ModelBasinID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteModelBasin(this IQueryable<ModelBasin> modelBasins, ICollection<ModelBasin> modelBasinsToDelete)
        {
            if(modelBasinsToDelete.Any())
            {
                var modelBasinIDList = modelBasinsToDelete.Select(x => x.ModelBasinID).ToList();
                modelBasins.Where(x => modelBasinIDList.Contains(x.ModelBasinID)).Delete();
            }
        }

        public static void DeleteModelBasin(this IQueryable<ModelBasin> modelBasins, int modelBasinID)
        {
            DeleteModelBasin(modelBasins, new List<int> { modelBasinID });
        }

        public static void DeleteModelBasin(this IQueryable<ModelBasin> modelBasins, ModelBasin modelBasinToDelete)
        {
            DeleteModelBasin(modelBasins, new List<ModelBasin> { modelBasinToDelete });
        }
    }
}