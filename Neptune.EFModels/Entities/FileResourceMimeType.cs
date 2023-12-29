using System.Linq;

namespace Neptune.EFModels.Entities
{
    public partial class FileResourceMimeType
    {
        public static FileResourceMimeType GetFileResourceMimeTypeByContentTypeName(NeptuneDbContext dbContext, string contentTypeName)
        {
            return All.Single(x => x.FileResourceMimeTypeContentTypeName == contentTypeName);
        }
    }
}