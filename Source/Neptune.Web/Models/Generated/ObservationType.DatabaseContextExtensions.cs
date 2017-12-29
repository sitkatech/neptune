//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationType]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static ObservationType GetObservationType(this IQueryable<ObservationType> observationTypes, int observationTypeID)
        {
            var observationType = observationTypes.SingleOrDefault(x => x.ObservationTypeID == observationTypeID);
            Check.RequireNotNullThrowNotFound(observationType, "ObservationType", observationTypeID);
            return observationType;
        }

        public static void DeleteObservationType(this List<int> observationTypeIDList)
        {
            if(observationTypeIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllObservationTypes.RemoveRange(HttpRequestStorage.DatabaseEntities.ObservationTypes.Where(x => observationTypeIDList.Contains(x.ObservationTypeID)));
            }
        }

        public static void DeleteObservationType(this ICollection<ObservationType> observationTypesToDelete)
        {
            if(observationTypesToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllObservationTypes.RemoveRange(observationTypesToDelete);
            }
        }

        public static void DeleteObservationType(this int observationTypeID)
        {
            DeleteObservationType(new List<int> { observationTypeID });
        }

        public static void DeleteObservationType(this ObservationType observationTypeToDelete)
        {
            DeleteObservationType(new List<ObservationType> { observationTypeToDelete });
        }
    }
}