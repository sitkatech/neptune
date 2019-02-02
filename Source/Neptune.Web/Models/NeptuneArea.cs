
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public abstract partial class NeptuneArea
    {
        public abstract string GetHomeUrl();
        public abstract bool IsAreaVisibleToPerson(Person person);
        public abstract string GetIconUrl();
    }

    public partial class NeptuneAreaTrash
    {
        public override string GetHomeUrl()
        {
            return SitkaRoute<Areas.Trash.Controllers.HomeController>.BuildUrlFromExpression(hc => hc.Index());
        }

        public override bool IsAreaVisibleToPerson(Person person)
        {
            return true;
        }

        public override string GetIconUrl()
        {
            return "/Content/img/icons/trashIcon.png";
        }
    }

    public partial class NeptuneAreaOCStormwaterTools
    {
        public override string GetHomeUrl()
        {
            return SitkaRoute<Controllers.HomeController>.BuildUrlFromExpression(hc => hc.Index()) + "#welcome";
        }

        public override bool IsAreaVisibleToPerson(Person person)
        {
            return true;
        }

        public override string GetIconUrl()
        {
            return "/Content/img/icons/inventoryIcon.png";
        }
    }

    public partial class NeptuneAreaModeling
    {
        public override string GetHomeUrl()
        {
            return SitkaRoute<Areas.Modeling.Controllers.HomeController>.BuildUrlFromExpression(hc => hc.Index());
        }

        public override bool IsAreaVisibleToPerson(Person person)
        {
            return true;
        }

        public override string GetIconUrl()
        {
            return "/Content/img/icons/modelingIcon.png";
        }
    }
}