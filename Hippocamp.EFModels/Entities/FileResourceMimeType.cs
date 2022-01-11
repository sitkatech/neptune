using System.Linq;

namespace Hippocamp.EFModels.Entities
{
    public partial class FileResourceMimeType
    {
        public static FileResourceMimeType GetFileResourceMimeTypeByContentTypeName(HippocampDbContext dbContext, string contentTypeName)
        {
            return dbContext.FileResourceMimeTypes.Single(x => x.FileResourceMimeTypeContentTypeName == contentTypeName);
        }
    }
}