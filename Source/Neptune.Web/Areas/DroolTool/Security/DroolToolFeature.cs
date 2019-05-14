using System.Collections.Generic;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Areas.DroolTool.Security
{
    public class DroolToolFeature : NeptuneBaseFeature
    {
        public DroolToolFeature(IList<IRole> roles)
            : base(roles, NeptuneArea.DroolTool)
        {
        }
    }
}