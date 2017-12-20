//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModeledCatchment]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ModeledCatchment GetModeledCatchment(this IQueryable<ModeledCatchment> modeledCatchments, int modeledCatchmentID)
        {
            var modeledCatchment = modeledCatchments.SingleOrDefault(x => x.ModeledCatchmentID == modeledCatchmentID);
            Check.RequireNotNullThrowNotFound(modeledCatchment, "ModeledCatchment", modeledCatchmentID);
            return modeledCatchment;
        }

        public static void DeleteModeledCatchment(this List<int> modeledCatchmentIDList)
        {
            if(modeledCatchmentIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllModeledCatchments.RemoveRange(HttpRequestStorage.DatabaseEntities.ModeledCatchments.Where(x => modeledCatchmentIDList.Contains(x.ModeledCatchmentID)));
            }
        }

        public static void DeleteModeledCatchment(this ICollection<ModeledCatchment> modeledCatchmentsToDelete)
        {
            if(modeledCatchmentsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllModeledCatchments.RemoveRange(modeledCatchmentsToDelete);
            }
        }

        public static void DeleteModeledCatchment(this int modeledCatchmentID)
        {
            DeleteModeledCatchment(new List<int> { modeledCatchmentID });
        }

        public static void DeleteModeledCatchment(this ModeledCatchment modeledCatchmentToDelete)
        {
            DeleteModeledCatchment(new List<ModeledCatchment> { modeledCatchmentToDelete });
        }
    }
}