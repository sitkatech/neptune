//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LandUseBlock]
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
        public static LandUseBlock GetLandUseBlock(this IQueryable<LandUseBlock> landUseBlocks, int landUseBlockID)
        {
            var landUseBlock = landUseBlocks.SingleOrDefault(x => x.LandUseBlockID == landUseBlockID);
            Check.RequireNotNullThrowNotFound(landUseBlock, "LandUseBlock", landUseBlockID);
            return landUseBlock;
        }

        // Delete using an IDList (Firma style)
        public static void DeleteLandUseBlock(this IQueryable<LandUseBlock> landUseBlocks, List<int> landUseBlockIDList)
        {
            if(landUseBlockIDList.Any())
            {
                landUseBlocks.Where(x => landUseBlockIDList.Contains(x.LandUseBlockID)).Delete();
            }
        }

        // Delete using an object list (Firma style)
        public static void DeleteLandUseBlock(this IQueryable<LandUseBlock> landUseBlocks, ICollection<LandUseBlock> landUseBlocksToDelete)
        {
            if(landUseBlocksToDelete.Any())
            {
                var landUseBlockIDList = landUseBlocksToDelete.Select(x => x.LandUseBlockID).ToList();
                landUseBlocks.Where(x => landUseBlockIDList.Contains(x.LandUseBlockID)).Delete();
            }
        }

        public static void DeleteLandUseBlock(this IQueryable<LandUseBlock> landUseBlocks, int landUseBlockID)
        {
            DeleteLandUseBlock(landUseBlocks, new List<int> { landUseBlockID });
        }

        public static void DeleteLandUseBlock(this IQueryable<LandUseBlock> landUseBlocks, LandUseBlock landUseBlockToDelete)
        {
            DeleteLandUseBlock(landUseBlocks, new List<LandUseBlock> { landUseBlockToDelete });
        }
    }
}