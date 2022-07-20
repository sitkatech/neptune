//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelStaging]
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
        public static ParcelStaging GetParcelStaging(this IQueryable<ParcelStaging> parcelStagings, int parcelStagingID)
        {
            var parcelStaging = parcelStagings.SingleOrDefault(x => x.ParcelStagingID == parcelStagingID);
            Check.RequireNotNullThrowNotFound(parcelStaging, "ParcelStaging", parcelStagingID);
            return parcelStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteParcelStaging(this IQueryable<ParcelStaging> parcelStagings, List<int> parcelStagingIDList)
        {
            if(parcelStagingIDList.Any())
            {
                parcelStagings.Where(x => parcelStagingIDList.Contains(x.ParcelStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteParcelStaging(this IQueryable<ParcelStaging> parcelStagings, ICollection<ParcelStaging> parcelStagingsToDelete)
        {
            if(parcelStagingsToDelete.Any())
            {
                var parcelStagingIDList = parcelStagingsToDelete.Select(x => x.ParcelStagingID).ToList();
                parcelStagings.Where(x => parcelStagingIDList.Contains(x.ParcelStagingID)).Delete();
            }
        }

        public static void DeleteParcelStaging(this IQueryable<ParcelStaging> parcelStagings, int parcelStagingID)
        {
            DeleteParcelStaging(parcelStagings, new List<int> { parcelStagingID });
        }

        public static void DeleteParcelStaging(this IQueryable<ParcelStaging> parcelStagings, ParcelStaging parcelStagingToDelete)
        {
            DeleteParcelStaging(parcelStagings, new List<ParcelStaging> { parcelStagingToDelete });
        }
    }
}