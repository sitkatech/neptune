//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZoneStaging]
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
        public static PrecipitationZoneStaging GetPrecipitationZoneStaging(this IQueryable<PrecipitationZoneStaging> precipitationZoneStagings, int precipitationZoneStagingID)
        {
            var precipitationZoneStaging = precipitationZoneStagings.SingleOrDefault(x => x.PrecipitationZoneStagingID == precipitationZoneStagingID);
            Check.RequireNotNullThrowNotFound(precipitationZoneStaging, "PrecipitationZoneStaging", precipitationZoneStagingID);
            return precipitationZoneStaging;
        }

        // Delete using an IDList (Firma style)
        public static void DeletePrecipitationZoneStaging(this IQueryable<PrecipitationZoneStaging> precipitationZoneStagings, List<int> precipitationZoneStagingIDList)
        {
            if(precipitationZoneStagingIDList.Any())
            {
                precipitationZoneStagings.Where(x => precipitationZoneStagingIDList.Contains(x.PrecipitationZoneStagingID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeletePrecipitationZoneStaging(this IQueryable<PrecipitationZoneStaging> precipitationZoneStagings, ICollection<PrecipitationZoneStaging> precipitationZoneStagingsToDelete)
        {
            if(precipitationZoneStagingsToDelete.Any())
            {
                var precipitationZoneStagingIDList = precipitationZoneStagingsToDelete.Select(x => x.PrecipitationZoneStagingID).ToList();
                precipitationZoneStagings.Where(x => precipitationZoneStagingIDList.Contains(x.PrecipitationZoneStagingID)).Delete();
            }
        }

        public static void DeletePrecipitationZoneStaging(this IQueryable<PrecipitationZoneStaging> precipitationZoneStagings, int precipitationZoneStagingID)
        {
            DeletePrecipitationZoneStaging(precipitationZoneStagings, new List<int> { precipitationZoneStagingID });
        }

        public static void DeletePrecipitationZoneStaging(this IQueryable<PrecipitationZoneStaging> precipitationZoneStagings, PrecipitationZoneStaging precipitationZoneStagingToDelete)
        {
            DeletePrecipitationZoneStaging(precipitationZoneStagings, new List<PrecipitationZoneStaging> { precipitationZoneStagingToDelete });
        }
    }
}