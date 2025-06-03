using Neptune.Jobs.EsriAsynchronousJob;

namespace Neptune.Jobs.Hangfire;

public class HRUResponseResult
{
    public List<HRUResponseFeature> HRUResponseFeatures { get; set; }
    public IEnumerable<int> LoadGeneratingUnitIDs { get; set; }
    public int? HRULogID { get; set; }

    public HRUResponseResult()
    {
    }

    public HRUResponseResult(List<HRUResponseFeature> hruResponseFeatures, IEnumerable<int> loadGeneratingUnitIDs)
    {
        HRUResponseFeatures = hruResponseFeatures;
        LoadGeneratingUnitIDs = loadGeneratingUnitIDs;
    }
}