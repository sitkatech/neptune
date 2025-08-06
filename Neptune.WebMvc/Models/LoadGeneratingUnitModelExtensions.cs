using Neptune.Common;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Models;

public static class LoadGeneratingUnitModelExtensions
{
    public static string GetLoadGeneratingUnitAreaString(this LoadGeneratingUnit? loadGeneratingUnit)
    {
        if (loadGeneratingUnit == null)
        {
            return "-";
        }
        var loadGeneratingUnitAcresAsString = (loadGeneratingUnit.LoadGeneratingUnitGeometry.Area * Constants.SquareMetersToAcres).ToString("0.00");
        return $"{loadGeneratingUnitAcresAsString} ac";
    }
}