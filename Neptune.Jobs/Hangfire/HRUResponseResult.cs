using Neptune.Jobs.EsriAsynchronousJob;

namespace Neptune.Jobs.Hangfire;

public class HRUResponseResult
{
    public List<HRUResponseFeature> HRUResponseFeatures { get; set; }

    public HRUResponseResult()
    {
    }

    public HRUResponseResult(List<HRUResponseFeature> hruResponseFeatures)
    {
        HRUResponseFeatures = hruResponseFeatures;
    }
}