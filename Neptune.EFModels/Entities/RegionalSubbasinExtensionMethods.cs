using Neptune.Common;

namespace Neptune.EFModels.Entities;

public static partial class RegionalSubbasinExtensionMethods
{
    public static double? GetRegionalSubbasinArea(this RegionalSubbasin regionalSubbasin)
    {
        return regionalSubbasin?.CatchmentGeometry.Area != null
            ? Math.Round(regionalSubbasin.CatchmentGeometry.Area * Constants.SquareMetersToAcres, 2)
            : null;
    }
}