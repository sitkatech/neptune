//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRUCharacteristic]
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
        public static HRUCharacteristic GetHRUCharacteristic(this IQueryable<HRUCharacteristic> hRUCharacteristics, int hRUCharacteristicID)
        {
            var hRUCharacteristic = hRUCharacteristics.SingleOrDefault(x => x.HRUCharacteristicID == hRUCharacteristicID);
            Check.RequireNotNullThrowNotFound(hRUCharacteristic, "HRUCharacteristic", hRUCharacteristicID);
            return hRUCharacteristic;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteHRUCharacteristic(this IQueryable<HRUCharacteristic> hRUCharacteristics, List<int> hRUCharacteristicIDList)
        {
            if(hRUCharacteristicIDList.Any())
            {
                hRUCharacteristics.Where(x => hRUCharacteristicIDList.Contains(x.HRUCharacteristicID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteHRUCharacteristic(this IQueryable<HRUCharacteristic> hRUCharacteristics, ICollection<HRUCharacteristic> hRUCharacteristicsToDelete)
        {
            if(hRUCharacteristicsToDelete.Any())
            {
                var hRUCharacteristicIDList = hRUCharacteristicsToDelete.Select(x => x.HRUCharacteristicID).ToList();
                hRUCharacteristics.Where(x => hRUCharacteristicIDList.Contains(x.HRUCharacteristicID)).Delete();
            }
        }

        public static void DeleteHRUCharacteristic(this IQueryable<HRUCharacteristic> hRUCharacteristics, int hRUCharacteristicID)
        {
            DeleteHRUCharacteristic(hRUCharacteristics, new List<int> { hRUCharacteristicID });
        }

        public static void DeleteHRUCharacteristic(this IQueryable<HRUCharacteristic> hRUCharacteristics, HRUCharacteristic hRUCharacteristicToDelete)
        {
            DeleteHRUCharacteristic(hRUCharacteristics, new List<HRUCharacteristic> { hRUCharacteristicToDelete });
        }
    }
}