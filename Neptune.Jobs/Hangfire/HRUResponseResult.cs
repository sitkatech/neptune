using Neptune.Jobs.EsriAsynchronousJob;

namespace Neptune.Jobs.Hangfire;

public class HRUResponseResult
{
    public List<HRUResponseFeature> HRUResponseFeatures { get; }
    public IEnumerable<int> LoadGeneratingUnitIDs { get; }

    public HRUResponseResult(List<HRUResponseFeature> hruResponseFeatures, IEnumerable<int> loadGeneratingUnitIDs)
    {
        HRUResponseFeatures = hruResponseFeatures;
        LoadGeneratingUnitIDs = loadGeneratingUnitIDs;
    }
}