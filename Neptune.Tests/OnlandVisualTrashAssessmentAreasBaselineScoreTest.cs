using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.EFModels.Entities;
using static Neptune.Tests.OnlandVisualTrashAssessmentsTest;

namespace Neptune.Tests
{
    /// <summary>
    /// Covers the baseline-score rule: the rounded average of all completed baseline (non-progress)
    /// assessments, at least 2 required, with ties rounding up (NPT-1136).
    /// </summary>
    [TestClass]
    public class OnlandVisualTrashAssessmentAreasBaselineScoreTest
    {
        [TestMethod]
        [DataRow("A,B", "A", DisplayName = "3.5 (A+B) -> A")]
        [DataRow("B,C", "B", DisplayName = "2.5 (B+C) -> B")]
        [DataRow("C,D", "C", DisplayName = "1.5 (C+D) -> C")]
        [DataRow("A,A,B", "A", DisplayName = "3.67 (A,A,B) -> A")]
        [DataRow("B,C,C", "C", DisplayName = "2.33 (B,C,C) -> C")]
        [DataRow("A,B,B", "B", DisplayName = "3.33 (A,B,B) -> B")]
        public void BaselineScoreRoundsMidpointUp(string letters, string expectedLetter)
        {
            var assessments = letters.Split(',')
                .Select((letter, i) => BuildAssessment(ScoreFromLetter(letter), Today.AddDays(-i), isProgressAssessment: false))
                .ToList();

            var result = OnlandVisualTrashAssessmentAreas.CalculateBaselineScoreFromBackingData(assessments);

            Assert.AreEqual(ScoreFromLetter(expectedLetter), result);
        }

        [TestMethod]
        public void SingleBaselineAssessmentProducesNull()
        {
            var assessments = new List<OnlandVisualTrashAssessment>
            {
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, Today, isProgressAssessment: false),
            };

            Assert.IsNull(OnlandVisualTrashAssessmentAreas.CalculateBaselineScoreFromBackingData(assessments),
                "A baseline score needs at least 2 completed baseline assessments.");
        }

        [TestMethod]
        public void ProgressAndIncompleteAssessmentsAreExcluded()
        {
            // Only the two completed baseline B's count -> B. Including the progress D or the
            // incomplete D would drag the average below 2.5 -> C.
            var assessments = new List<OnlandVisualTrashAssessment>
            {
                BuildAssessment(OnlandVisualTrashAssessmentScore.B, Today, isProgressAssessment: false),
                BuildAssessment(OnlandVisualTrashAssessmentScore.B, Today, isProgressAssessment: false),
                BuildAssessment(OnlandVisualTrashAssessmentScore.D, Today, isProgressAssessment: true),
                BuildAssessment(OnlandVisualTrashAssessmentScore.D, Today, isProgressAssessment: false, isComplete: false),
            };

            Assert.AreEqual(OnlandVisualTrashAssessmentScore.B,
                OnlandVisualTrashAssessmentAreas.CalculateBaselineScoreFromBackingData(assessments));
        }
    }
}
