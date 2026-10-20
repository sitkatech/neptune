/*-----------------------------------------------------------------------
<copyright file="OnlandVisualTrashAssessmentsTest.cs" company="Sitka Technology Group">
Copyright (c) Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.EFModels.Entities;

namespace Neptune.Tests
{
    /// <summary>
    /// Covers the NPT-1065 progress-score rule: a progress score requires at least 1 completed
    /// progress OVTA within the last 5 years (previously 2 within 4 years). The score is the rounded
    /// average of the top 3 most recent qualifying assessments, with ties rounding up (NPT-1136).
    /// </summary>
    [TestClass]
    public class OnlandVisualTrashAssessmentsTest
    {
        // NumericValue mapping from OnlandVisualTrashAssessmentScore.Binding.cs: A=4, B=3, C=2, D=1
        internal static OnlandVisualTrashAssessment BuildAssessment(
            OnlandVisualTrashAssessmentScore score,
            DateOnly completedDate,
            bool isProgressAssessment = true,
            bool isComplete = true,
            int id = 0)
        {
            return new OnlandVisualTrashAssessment
            {
                OnlandVisualTrashAssessmentID = id,
                OnlandVisualTrashAssessmentStatusID = isComplete
                    ? OnlandVisualTrashAssessmentStatus.Complete.OnlandVisualTrashAssessmentStatusID
                    : OnlandVisualTrashAssessmentStatus.InProgress.OnlandVisualTrashAssessmentStatusID,
                IsProgressAssessment = isProgressAssessment,
                CompletedDate = completedDate,
                OnlandVisualTrashAssessmentScoreID = score.OnlandVisualTrashAssessmentScoreID,
            };
        }

        internal static DateOnly Today => DateOnly.FromDateTime(DateTime.UtcNow);

        internal static OnlandVisualTrashAssessmentScore ScoreFromLetter(string letter) =>
            OnlandVisualTrashAssessmentScore.All.Single(x => x.OnlandVisualTrashAssessmentScoreDisplayName == letter);

        [TestMethod]
        public void SingleCompletedProgressAssessmentWithinFiveYearsProducesScore()
        {
            // New rule: a single completed progress OVTA within 5 years is now enough (was 2 within 4 years).
            var fiveYearsAgo = Today.AddYears(-5);
            var assessments = new List<OnlandVisualTrashAssessment>
            {
                BuildAssessment(OnlandVisualTrashAssessmentScore.B, fiveYearsAgo),
            };

            var result = OnlandVisualTrashAssessments.CalculateProgressScore(assessments);

            Assert.IsNotNull(result, "A single completed progress assessment within 5 years should yield a score.");
            Assert.AreEqual(OnlandVisualTrashAssessmentScore.B, result);
        }

        [TestMethod]
        public void NoCompletedProgressAssessmentsProducesNull()
        {
            var result = OnlandVisualTrashAssessments.CalculateProgressScore(new List<OnlandVisualTrashAssessment>());

            Assert.IsNull(result, "With zero qualifying assessments there is no progress score.");
        }

        [TestMethod]
        public void ProgressAssessmentOlderThanFiveYearsIsExcluded()
        {
            // Just outside the 5-year window -> filtered out -> no score.
            var fiveYearsAgo = Today.AddYears(-5);
            var assessments = new List<OnlandVisualTrashAssessment>
            {
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, fiveYearsAgo.AddDays(-1)),
            };

            var result = OnlandVisualTrashAssessments.CalculateProgressScore(assessments);

            Assert.IsNull(result, "A progress assessment completed more than 5 years ago must not count.");
        }

        [TestMethod]
        public void NonProgressAndIncompleteAssessmentsAreExcluded()
        {
            var assessments = new List<OnlandVisualTrashAssessment>
            {
                // Completed but a baseline (non-progress) assessment -> excluded.
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, Today, isProgressAssessment: false),
                // Progress but not completed -> excluded.
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, Today, isComplete: false),
            };

            var result = OnlandVisualTrashAssessments.CalculateProgressScore(assessments);

            Assert.IsNull(result, "Only completed progress assessments should contribute to the progress score.");
        }

        [TestMethod]
        public void OnlyTopThreeMostRecentAssessmentsAreAveraged()
        {
            // 3 most recent are A (numeric 4); the oldest is D (numeric 1).
            // Top-3 average = 4 -> A. Averaging all 4 would be (4+4+4+1)/4 = 3.25 -> round 3 -> B.
            // Asserting A proves only the top 3 most recent are used.
            var assessments = new List<OnlandVisualTrashAssessment>
            {
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, Today),
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, Today.AddDays(-10)),
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, Today.AddDays(-20)),
                BuildAssessment(OnlandVisualTrashAssessmentScore.D, Today.AddDays(-30)),
            };

            var result = OnlandVisualTrashAssessments.CalculateProgressScore(assessments);

            Assert.AreEqual(OnlandVisualTrashAssessmentScore.A, result,
                "Only the top 3 most recent assessments should be averaged.");
        }

        [TestMethod]
        // NPT-1136: ties round up (toward the better letter), not banker's rounding.
        [DataRow("A,B", "A", DisplayName = "3.5 (A+B) -> A")]
        [DataRow("B,C", "B", DisplayName = "2.5 (B+C) -> B")]
        [DataRow("C,D", "C", DisplayName = "1.5 (C+D) -> C")]
        // Non-ties still round to the nearest letter.
        [DataRow("A,A,B", "A", DisplayName = "3.67 (A,A,B) -> A")]
        [DataRow("B,C,C", "C", DisplayName = "2.33 (B,C,C) -> C")]
        [DataRow("A,B,B", "B", DisplayName = "3.33 (A,B,B) -> B")]
        public void ProgressScoreRoundsMidpointUp(string letters, string expectedLetter)
        {
            var assessments = letters.Split(',')
                .Select((letter, i) => BuildAssessment(ScoreFromLetter(letter), Today.AddDays(-i)))
                .ToList();

            var result = OnlandVisualTrashAssessments.CalculateProgressScore(assessments);

            Assert.AreEqual(ScoreFromLetter(expectedLetter), result);
        }

        [TestMethod]
        public void SameDayAssessmentsAtTheTopThreeBoundaryBreakTiesOnID()
        {
            // The 3rd and 4th most recent share a CompletedDate; the higher ID is the newer one and wins
            // (matches the ROW_NUMBER order in dbo.vOnlandVisualTrashAssessmentAreaProgress).
            // Top 3 = A, A, A (ID 4) -> A. If the D (ID 3) were picked instead: (4+4+1)/3 = 3 -> B.
            var sameDay = Today.AddDays(-20);
            var assessments = new List<OnlandVisualTrashAssessment>
            {
                BuildAssessment(OnlandVisualTrashAssessmentScore.D, sameDay, id: 3),
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, sameDay, id: 4),
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, Today, id: 1),
                BuildAssessment(OnlandVisualTrashAssessmentScore.A, Today.AddDays(-10), id: 2),
            };

            var result = OnlandVisualTrashAssessments.CalculateProgressScore(assessments);

            Assert.AreEqual(OnlandVisualTrashAssessmentScore.A, result);
        }

        [TestMethod]
        [DataRow(3.5, "A")]
        [DataRow(2.5, "B")]
        [DataRow(1.5, "C")]
        [DataRow(11.0 / 3, "A")]
        [DataRow(7.0 / 3, "C")]
        public void FromAverageNumericValueRoundsMidpointUp(double average, string expectedLetter)
        {
            Assert.AreEqual(ScoreFromLetter(expectedLetter), OnlandVisualTrashAssessmentScore.FromAverageNumericValue(average));
        }
    }
}
