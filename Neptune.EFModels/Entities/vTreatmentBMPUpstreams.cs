using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

public static class vTreatmentBMPUpstreams
{
    public static Dictionary<int, Delineation?> ListWithDelineationAsDictionary(NeptuneDbContext dbContext)
    {
        var results = new Dictionary<int, Delineation?>();
        var treatmentBMPUpstreams = dbContext.vTreatmentBMPUpstreams.AsNoTracking()
            .ToList();
        var delineations = dbContext.Delineations.Include(x => x.TreatmentBMP).AsNoTracking().ToDictionary(x => x.TreatmentBMPID);
        foreach (var vTreatmentBMPUpstream in treatmentBMPUpstreams)
        {
            var treatmentBMPIDForDelineation =
                vTreatmentBMPUpstream.UpstreamBMPID ?? vTreatmentBMPUpstream.TreatmentBMPID.Value;
            results.Add(vTreatmentBMPUpstream.TreatmentBMPID.Value, delineations.ContainsKey(treatmentBMPIDForDelineation) ? delineations[treatmentBMPIDForDelineation] : null);
        }
        return results;

    }

    public static Dictionary<int, Delineation?> ListWithDelineationAsDictionaryForTreatmentBMPIDList(NeptuneDbContext dbContext, List<int> treatmentBMPIDList)
    {
        var results = new Dictionary<int, Delineation?>();
        var treatmentBMPUpstreams = dbContext.vTreatmentBMPUpstreams.AsNoTracking().Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID.Value)).ToList();
        var delineations = dbContext.Delineations.Include(x => x.TreatmentBMP).AsNoTracking().Where(x => treatmentBMPIDList.Contains(x.TreatmentBMPID)).ToDictionary(x => x.TreatmentBMPID);
        foreach (var vTreatmentBMPUpstream in treatmentBMPUpstreams)
        {
            var treatmentBMPIDForDelineation =
                vTreatmentBMPUpstream.UpstreamBMPID ?? vTreatmentBMPUpstream.TreatmentBMPID.Value;
            results.Add(vTreatmentBMPUpstream.TreatmentBMPID.Value, delineations.ContainsKey(treatmentBMPIDForDelineation) ? delineations[treatmentBMPIDForDelineation] : null);
        }
        return results;

    }
}