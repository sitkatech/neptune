//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPInfiltrationReading]
using System.Collections.Generic;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static TreatmentBMPInfiltrationReading GetTreatmentBMPInfiltrationReading(this IQueryable<TreatmentBMPInfiltrationReading> treatmentBMPInfiltrationReadings, int treatmentBMPInfiltrationReadingID)
        {
            var treatmentBMPInfiltrationReading = treatmentBMPInfiltrationReadings.SingleOrDefault(x => x.TreatmentBMPInfiltrationReadingID == treatmentBMPInfiltrationReadingID);
            Check.RequireNotNullThrowNotFound(treatmentBMPInfiltrationReading, "TreatmentBMPInfiltrationReading", treatmentBMPInfiltrationReadingID);
            return treatmentBMPInfiltrationReading;
        }

        public static void DeleteTreatmentBMPInfiltrationReading(this List<int> treatmentBMPInfiltrationReadingIDList)
        {
            if(treatmentBMPInfiltrationReadingIDList.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPInfiltrationReadings.RemoveRange(HttpRequestStorage.DatabaseEntities.TreatmentBMPInfiltrationReadings.Where(x => treatmentBMPInfiltrationReadingIDList.Contains(x.TreatmentBMPInfiltrationReadingID)));
            }
        }

        public static void DeleteTreatmentBMPInfiltrationReading(this ICollection<TreatmentBMPInfiltrationReading> treatmentBMPInfiltrationReadingsToDelete)
        {
            if(treatmentBMPInfiltrationReadingsToDelete.Any())
            {
                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPInfiltrationReadings.RemoveRange(treatmentBMPInfiltrationReadingsToDelete);
            }
        }

        public static void DeleteTreatmentBMPInfiltrationReading(this int treatmentBMPInfiltrationReadingID)
        {
            DeleteTreatmentBMPInfiltrationReading(new List<int> { treatmentBMPInfiltrationReadingID });
        }

        public static void DeleteTreatmentBMPInfiltrationReading(this TreatmentBMPInfiltrationReading treatmentBMPInfiltrationReadingToDelete)
        {
            DeleteTreatmentBMPInfiltrationReading(new List<TreatmentBMPInfiltrationReading> { treatmentBMPInfiltrationReadingToDelete });
        }
    }
}