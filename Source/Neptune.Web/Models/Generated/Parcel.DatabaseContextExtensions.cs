//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Parcel]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static Parcel GetParcel(this IQueryable<Parcel> parcels, int parcelID)
        {
            var parcel = parcels.SingleOrDefault(x => x.ParcelID == parcelID);
            Check.RequireNotNullThrowNotFound(parcel, "Parcel", parcelID);
            return parcel;
        }

        public static void DeleteParcel(this List<int> parcelIDList)
        {
            if(parcelIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllParcels.RemoveRange(HttpRequestStorage.DatabaseEntities.Parcels.Where(x => parcelIDList.Contains(x.ParcelID)));
            }
        }

        public static void DeleteParcel(this ICollection<Parcel> parcelsToDelete)
        {
            if(parcelsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllParcels.RemoveRange(parcelsToDelete);
            }
        }

        public static void DeleteParcel(this int parcelID)
        {
            DeleteParcel(new List<int> { parcelID });
        }

        public static void DeleteParcel(this Parcel parcelToDelete)
        {
            DeleteParcel(new List<Parcel> { parcelToDelete });
        }
    }
}