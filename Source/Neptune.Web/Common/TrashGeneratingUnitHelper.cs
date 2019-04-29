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

        public static void UpdateTrashGeneratingUnits(this Delineation delineation)
        {
            var objectIDs = new SqlParameter("@ObjectIDs", FormatIDString(new List<int> {delineation.DelineationID}));
            var objectType = new SqlParameter("@ObjectType", DelineationObjectType);

            HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
                "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);
        }
        public static void UpdateTrashGeneratingUnits(this IEnumerable<Delineation> delineations)
        {
            var objectIDs = new SqlParameter("@ObjectIDs", FormatIDString(delineations.Select(x=>x.DelineationID)));
            var objectType = new SqlParameter("@ObjectType", DelineationObjectType);

            HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
                "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);
        }

        public static void UpdateTrashGeneratingUnits(this OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            var objectIDs = new SqlParameter("@ObjectIDs", FormatIDString(new List<int> {onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID}));
            var objectType = new SqlParameter("@ObjectType", OnlandVisualTrashAssessmentAreaObjectType);

            HttpRequestStorage.DatabaseEntities.Database.ExecuteSqlCommand(
                "dbo.pRebuildTrashGeneratingUnitTableRelative @ObjectIDs, @ObjectType", objectIDs, objectType);
        }

        public static string FormatIDString(IEnumerable<int> idList)
        {
            return string.Join(",", idList);
        }
    }
}