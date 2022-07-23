using System.Data.SqlClient;

namespace Neptune.Web.Models
{
    public static partial class DatabaseContextExtensions
    {
        public static void pParcelStagingDeleteByPersonID(this DatabaseEntities dbContext, int personID)
        {
            dbContext.Database.ExecuteSqlCommand("EXEC dbo.pParcelStagingDeleteByPersonID @personID", new SqlParameter("@personID", personID));
        }
    }
}
