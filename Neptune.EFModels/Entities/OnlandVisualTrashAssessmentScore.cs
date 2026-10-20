namespace Neptune.EFModels.Entities;

public abstract partial class OnlandVisualTrashAssessmentScore
{
    /// <summary>
    /// Rounds an average of assessment NumericValues (A=4, B=3, C=2, D=1) to the nearest score.
    /// </summary>
    public static OnlandVisualTrashAssessmentScore? FromAverageNumericValue(double average)
    {
        // NPT-1136: midpoint-up is deliberate. Do NOT switch back to the default Math.Round (banker's
        // rounding), which sends B+C (2.5) to C. All values are positive, so AwayFromZero rounds ties up,
        // toward the better letter. Keep in sync with dbo.vOnlandVisualTrashAssessmentAreaProgress and
        // release script 042, which ROUND a decimal AVG (also half away from zero).
        var rounded = (int)Math.Round(average, MidpointRounding.AwayFromZero);
        return All.SingleOrDefault(x => x.NumericValue == rounded);
    }
}
