//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPModelingAttribute]
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
        public static TreatmentBMPModelingAttribute GetTreatmentBMPModelingAttribute(this IQueryable<TreatmentBMPModelingAttribute> treatmentBMPModelingAttributes, int treatmentBMPModelingAttributeID)
        {
            var treatmentBMPModelingAttribute = treatmentBMPModelingAttributes.SingleOrDefault(x => x.TreatmentBMPModelingAttributeID == treatmentBMPModelingAttributeID);
            Check.RequireNotNullThrowNotFound(treatmentBMPModelingAttribute, "TreatmentBMPModelingAttribute", treatmentBMPModelingAttributeID);
            return treatmentBMPModelingAttribute;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteTreatmentBMPModelingAttribute(this IQueryable<TreatmentBMPModelingAttribute> treatmentBMPModelingAttributes, List<int> treatmentBMPModelingAttributeIDList)
        {
            if(treatmentBMPModelingAttributeIDList.Any())
            {
                treatmentBMPModelingAttributes.Where(x => treatmentBMPModelingAttributeIDList.Contains(x.TreatmentBMPModelingAttributeID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteTreatmentBMPModelingAttribute(this IQueryable<TreatmentBMPModelingAttribute> treatmentBMPModelingAttributes, ICollection<TreatmentBMPModelingAttribute> treatmentBMPModelingAttributesToDelete)
        {
            if(treatmentBMPModelingAttributesToDelete.Any())
            {
                var treatmentBMPModelingAttributeIDList = treatmentBMPModelingAttributesToDelete.Select(x => x.TreatmentBMPModelingAttributeID).ToList();
                treatmentBMPModelingAttributes.Where(x => treatmentBMPModelingAttributeIDList.Contains(x.TreatmentBMPModelingAttributeID)).Delete();
            }
        }

        public static void DeleteTreatmentBMPModelingAttribute(this IQueryable<TreatmentBMPModelingAttribute> treatmentBMPModelingAttributes, int treatmentBMPModelingAttributeID)
        {
            DeleteTreatmentBMPModelingAttribute(treatmentBMPModelingAttributes, new List<int> { treatmentBMPModelingAttributeID });
        }

        public static void DeleteTreatmentBMPModelingAttribute(this IQueryable<TreatmentBMPModelingAttribute> treatmentBMPModelingAttributes, TreatmentBMPModelingAttribute treatmentBMPModelingAttributeToDelete)
        {
            DeleteTreatmentBMPModelingAttribute(treatmentBMPModelingAttributes, new List<TreatmentBMPModelingAttribute> { treatmentBMPModelingAttributeToDelete });
        }
    }
}