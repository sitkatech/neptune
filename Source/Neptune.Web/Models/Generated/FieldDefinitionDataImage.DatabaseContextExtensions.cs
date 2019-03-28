//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinitionDataImage]
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
        public static FieldDefinitionDataImage GetFieldDefinitionDataImage(this IQueryable<FieldDefinitionDataImage> fieldDefinitionDataImages, int fieldDefinitionDataImageID)
        {
            var fieldDefinitionDataImage = fieldDefinitionDataImages.SingleOrDefault(x => x.FieldDefinitionDataImageID == fieldDefinitionDataImageID);
            Check.RequireNotNullThrowNotFound(fieldDefinitionDataImage, "FieldDefinitionDataImage", fieldDefinitionDataImageID);
            return fieldDefinitionDataImage;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteFieldDefinitionDataImage(this IQueryable<FieldDefinitionDataImage> fieldDefinitionDataImages, List<int> fieldDefinitionDataImageIDList)
        {
            if(fieldDefinitionDataImageIDList.Any())
            {
                fieldDefinitionDataImages.Where(x => fieldDefinitionDataImageIDList.Contains(x.FieldDefinitionDataImageID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteFieldDefinitionDataImage(this IQueryable<FieldDefinitionDataImage> fieldDefinitionDataImages, ICollection<FieldDefinitionDataImage> fieldDefinitionDataImagesToDelete)
        {
            if(fieldDefinitionDataImagesToDelete.Any())
            {
                var fieldDefinitionDataImageIDList = fieldDefinitionDataImagesToDelete.Select(x => x.FieldDefinitionDataImageID).ToList();
                fieldDefinitionDataImages.Where(x => fieldDefinitionDataImageIDList.Contains(x.FieldDefinitionDataImageID)).Delete();
            }
        }

        public static void DeleteFieldDefinitionDataImage(this IQueryable<FieldDefinitionDataImage> fieldDefinitionDataImages, int fieldDefinitionDataImageID)
        {
            DeleteFieldDefinitionDataImage(fieldDefinitionDataImages, new List<int> { fieldDefinitionDataImageID });
        }

        public static void DeleteFieldDefinitionDataImage(this IQueryable<FieldDefinitionDataImage> fieldDefinitionDataImages, FieldDefinitionDataImage fieldDefinitionDataImageToDelete)
        {
            DeleteFieldDefinitionDataImage(fieldDefinitionDataImages, new List<FieldDefinitionDataImage> { fieldDefinitionDataImageToDelete });
        }
    }
}