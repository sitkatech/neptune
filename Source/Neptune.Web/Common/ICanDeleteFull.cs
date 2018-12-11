using Neptune.Web.Models;

namespace Neptune.Web.Common
{
    public interface ICanDeleteFull
    {
        void DeleteFull(DatabaseEntities dbContext);
    }
}