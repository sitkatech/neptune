using System.Collections.Generic;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.DroolTool.Security
{
    [SecurityFeatureDescription("Edit Urban Drool Tool")]
    public class DroolToolEditFeature : DroolToolFeature
    {
        public DroolToolEditFeature()
            : base(new List<IRole> { DroolToolRole.Admin })
        {
        }
    }
}