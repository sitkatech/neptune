using System.Collections.Generic;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.DroolTool.Security
{
    [SecurityFeatureDescription("View Urban Drool Tool")]
    public class DroolToolViewFeature : DroolToolFeature
    {
        public DroolToolViewFeature()
            : base(new List<IRole> { DroolToolRole.Admin, DroolToolRole.Editor })
        {
        }
    }
}