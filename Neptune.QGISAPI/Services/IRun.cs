using LtInfo.Common;
using Neptune.Common;

namespace Neptune.QGISAPI.Services;

public interface IRun
{
    public ProcessUtilityResult Run(List<string> arguments);
}