//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZone]
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
        public static PrecipitationZone GetPrecipitationZone(this IQueryable<PrecipitationZone> precipitationZones, int precipitationZoneID)
        {
            var precipitationZone = precipitationZones.SingleOrDefault(x => x.PrecipitationZoneID == precipitationZoneID);
            Check.RequireNotNullThrowNotFound(precipitationZone, "PrecipitationZone", precipitationZoneID);
            return precipitationZone;
        }

        // Delete using an IDList (Firma style)
        public static void DeletePrecipitationZone(this IQueryable<PrecipitationZone> precipitationZones, List<int> precipitationZoneIDList)
        {
            if(precipitationZoneIDList.Any())
            {
                precipitationZones.Where(x => precipitationZoneIDList.Contains(x.PrecipitationZoneID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeletePrecipitationZone(this IQueryable<PrecipitationZone> precipitationZones, ICollection<PrecipitationZone> precipitationZonesToDelete)
        {
            if(precipitationZonesToDelete.Any())
            {
                var precipitationZoneIDList = precipitationZonesToDelete.Select(x => x.PrecipitationZoneID).ToList();
                precipitationZones.Where(x => precipitationZoneIDList.Contains(x.PrecipitationZoneID)).Delete();
            }
        }

        public static void DeletePrecipitationZone(this IQueryable<PrecipitationZone> precipitationZones, int precipitationZoneID)
        {
            DeletePrecipitationZone(precipitationZones, new List<int> { precipitationZoneID });
        }

        public static void DeletePrecipitationZone(this IQueryable<PrecipitationZone> precipitationZones, PrecipitationZone precipitationZoneToDelete)
        {
            DeletePrecipitationZone(precipitationZones, new List<PrecipitationZone> { precipitationZoneToDelete });
        }
    }
}