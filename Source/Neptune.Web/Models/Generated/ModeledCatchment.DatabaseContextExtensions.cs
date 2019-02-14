//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModeledCatchment]
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
        public static ModeledCatchment GetModeledCatchment(this IQueryable<ModeledCatchment> modeledCatchments, int modeledCatchmentID)
        {
            var modeledCatchment = modeledCatchments.SingleOrDefault(x => x.ModeledCatchmentID == modeledCatchmentID);
            Check.RequireNotNullThrowNotFound(modeledCatchment, "ModeledCatchment", modeledCatchmentID);
            return modeledCatchment;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteModeledCatchment(this IQueryable<ModeledCatchment> modeledCatchments, List<int> modeledCatchmentIDList)
        {
            if(modeledCatchmentIDList.Any())
            {
                modeledCatchments.Where(x => modeledCatchmentIDList.Contains(x.ModeledCatchmentID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteModeledCatchment(this IQueryable<ModeledCatchment> modeledCatchments, ICollection<ModeledCatchment> modeledCatchmentsToDelete)
        {
            if(modeledCatchmentsToDelete.Any())
            {
                var modeledCatchmentIDList = modeledCatchmentsToDelete.Select(x => x.ModeledCatchmentID).ToList();
                modeledCatchments.Where(x => modeledCatchmentIDList.Contains(x.ModeledCatchmentID)).Delete();
            }
        }

        public static void DeleteModeledCatchment(this IQueryable<ModeledCatchment> modeledCatchments, int modeledCatchmentID)
        {
            DeleteModeledCatchment(modeledCatchments, new List<int> { modeledCatchmentID });
        }

        public static void DeleteModeledCatchment(this IQueryable<ModeledCatchment> modeledCatchments, ModeledCatchment modeledCatchmentToDelete)
        {
            DeleteModeledCatchment(modeledCatchments, new List<ModeledCatchment> { modeledCatchmentToDelete });
        }
    }
}