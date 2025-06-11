using Neptune.Common;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Models;

public static class RegionalSubbasinModelExtensions
{
    public static string GetRegionalSubbasinAreaString(this RegionalSubbasin? regionalSubbasin)
    {
        if (regionalSubbasin == null)
        {
            return "-";
        }
        var regionalSubbasinAcresAsString = (regionalSubbasin.CatchmentGeometry.Area * Constants.SquareMetersToAcres).ToString("0.00");
        return $"{regionalSubbasinAcresAsString} ac";
    }
}