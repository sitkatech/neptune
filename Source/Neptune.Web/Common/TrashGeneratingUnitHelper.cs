using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public static class TrashGeneratingUnitHelper
    {
        public const string DelineationObjectType = "Delineation";
        public const string OnlandVisualTrashAssessmentAreaObjectType = "OnlandVisualTrashAssessmentArea";

        public static bool UpdateTrashGeneratingUnits(this Delineation delineation)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place
            //// calling save changes here so the caller can't forget to
            //HttpRequestStorage.DatabaseEntities.SaveChanges();

            //try
            //{
            //    var objectIDs =
            //        new SqlParameter("@ObjectIDs", FormatIDString(new List<int> { delineation.DelineationID }));
            //    var objectType = new SqlParameter("@ObjectType", DelineationObjectType);

            //    HttpRequestStorage.DatabaseEntities.Database.CommandTimeout = 600;
            //    HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
            //        "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);

            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }
        public static bool UpdateTrashGeneratingUnitsAfterDelete(this Delineation delineation)
        {

            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place
            
            //var wellKnownText = delineation.DelineationGeometry.ToString();
            //wellKnownText = wellKnownText.Substring(wellKnownText.IndexOf("POLYGON", StringComparison.InvariantCulture));

            //// calling save changes here so the caller can't forget to
            //HttpRequestStorage.DatabaseEntities.SaveChanges();

            //try
            //{
            //    var geometryWKT = new SqlParameter("@GeometryWKT", wellKnownText);

            //    HttpRequestStorage.DatabaseEntities.Database.CommandTimeout = 600;
            //    HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
            //        "dbo.pRebuildTrashGeneratingUnitTableRelative @GeometryWKT", geometryWKT);

            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public static bool UpdateTrashGeneratingUnits(this IEnumerable<Delineation> delineations)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place
            
            //// calling save changes here so the caller can't forget to
            //HttpRequestStorage.DatabaseEntities.SaveChanges();

            //try
            //{
            //    var objectIDs =
            //        new SqlParameter("@ObjectIDs", FormatIDString(delineations.Select(x => x.DelineationID)));
            //    var objectType = new SqlParameter("@ObjectType", DelineationObjectType);

            //    HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
            //        "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public static bool UpdateTrashGeneratingUnits(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            return true;
            // TODO: neutered under 367. Will bring back once job scheduling is in place

            //// calling save changes here so the caller can't forget to
            //HttpRequestStorage.DatabaseEntities.SaveChanges();

            //try
            //{
            //    var objectIDs = new SqlParameter("@ObjectIDs", FormatIDString(new List<int> { onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID }));
            //    var objectType = new SqlParameter("@ObjectType", OnlandVisualTrashAssessmentAreaObjectType);

            //    HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
            //        "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public static string FormatIDString(IEnumerable<int> idList)
        {
            return string.Join(",", idList);
        }
    }
}