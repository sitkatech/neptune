using System.Collections.Generic;
using System.Data.SqlClient;
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

        public static string FormatIDString(IEnumerable<int> idList)
        {
            return string.Join(",", idList);
        }
    }
}