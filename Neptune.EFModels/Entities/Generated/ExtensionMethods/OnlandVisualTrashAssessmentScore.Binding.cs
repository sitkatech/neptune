//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentScore]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class OnlandVisualTrashAssessmentScore : IHavePrimaryKey
    {
        public static readonly OnlandVisualTrashAssessmentScoreA A = Neptune.EFModels.Entities.OnlandVisualTrashAssessmentScoreA.Instance;
        public static readonly OnlandVisualTrashAssessmentScoreB B = Neptune.EFModels.Entities.OnlandVisualTrashAssessmentScoreB.Instance;
        public static readonly OnlandVisualTrashAssessmentScoreC C = Neptune.EFModels.Entities.OnlandVisualTrashAssessmentScoreC.Instance;
        public static readonly OnlandVisualTrashAssessmentScoreD D = Neptune.EFModels.Entities.OnlandVisualTrashAssessmentScoreD.Instance;

        public static readonly List<OnlandVisualTrashAssessmentScore> All;
        public static readonly List<OnlandVisualTrashAssessmentScoreDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, OnlandVisualTrashAssessmentScore> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, OnlandVisualTrashAssessmentScoreDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static OnlandVisualTrashAssessmentScore()
        {
            All = new List<OnlandVisualTrashAssessmentScore> { A, B, C, D };
            AllAsDto = new List<OnlandVisualTrashAssessmentScoreDto> { A.AsDto(), B.AsDto(), C.AsDto(), D.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, OnlandVisualTrashAssessmentScore>(All.ToDictionary(x => x.OnlandVisualTrashAssessmentScoreID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, OnlandVisualTrashAssessmentScoreDto>(AllAsDto.ToDictionary(x => x.OnlandVisualTrashAssessmentScoreID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected OnlandVisualTrashAssessmentScore(int onlandVisualTrashAssessmentScoreID, string onlandVisualTrashAssessmentScoreName, string onlandVisualTrashAssessmentScoreDisplayName, int numericValue, decimal trashGenerationRate)
        {
            OnlandVisualTrashAssessmentScoreID = onlandVisualTrashAssessmentScoreID;
            OnlandVisualTrashAssessmentScoreName = onlandVisualTrashAssessmentScoreName;
            OnlandVisualTrashAssessmentScoreDisplayName = onlandVisualTrashAssessmentScoreDisplayName;
            NumericValue = numericValue;
            TrashGenerationRate = trashGenerationRate;
        }

        [Key]
        public int OnlandVisualTrashAssessmentScoreID { get; private set; }
        public string OnlandVisualTrashAssessmentScoreName { get; private set; }
        public string OnlandVisualTrashAssessmentScoreDisplayName { get; private set; }
        public int NumericValue { get; private set; }
        public decimal TrashGenerationRate { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return OnlandVisualTrashAssessmentScoreID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(OnlandVisualTrashAssessmentScore other)
        {
            if (other == null)
            {
                return false;
            }
            return other.OnlandVisualTrashAssessmentScoreID == OnlandVisualTrashAssessmentScoreID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as OnlandVisualTrashAssessmentScore);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return OnlandVisualTrashAssessmentScoreID;
        }

        public static bool operator ==(OnlandVisualTrashAssessmentScore left, OnlandVisualTrashAssessmentScore right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OnlandVisualTrashAssessmentScore left, OnlandVisualTrashAssessmentScore right)
        {
            return !Equals(left, right);
        }

        public OnlandVisualTrashAssessmentScoreEnum ToEnum => (OnlandVisualTrashAssessmentScoreEnum)GetHashCode();

        public static OnlandVisualTrashAssessmentScore ToType(int enumValue)
        {
            return ToType((OnlandVisualTrashAssessmentScoreEnum)enumValue);
        }

        public static OnlandVisualTrashAssessmentScore ToType(OnlandVisualTrashAssessmentScoreEnum enumValue)
        {
            switch (enumValue)
            {
                case OnlandVisualTrashAssessmentScoreEnum.A:
                    return A;
                case OnlandVisualTrashAssessmentScoreEnum.B:
                    return B;
                case OnlandVisualTrashAssessmentScoreEnum.C:
                    return C;
                case OnlandVisualTrashAssessmentScoreEnum.D:
                    return D;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum OnlandVisualTrashAssessmentScoreEnum
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4
    }

    public partial class OnlandVisualTrashAssessmentScoreA : OnlandVisualTrashAssessmentScore
    {
        private OnlandVisualTrashAssessmentScoreA(int onlandVisualTrashAssessmentScoreID, string onlandVisualTrashAssessmentScoreName, string onlandVisualTrashAssessmentScoreDisplayName, int numericValue, decimal trashGenerationRate) : base(onlandVisualTrashAssessmentScoreID, onlandVisualTrashAssessmentScoreName, onlandVisualTrashAssessmentScoreDisplayName, numericValue, trashGenerationRate) {}
        public static readonly OnlandVisualTrashAssessmentScoreA Instance = new OnlandVisualTrashAssessmentScoreA(1, @"A", @"A", 4, 2.5m);
    }

    public partial class OnlandVisualTrashAssessmentScoreB : OnlandVisualTrashAssessmentScore
    {
        private OnlandVisualTrashAssessmentScoreB(int onlandVisualTrashAssessmentScoreID, string onlandVisualTrashAssessmentScoreName, string onlandVisualTrashAssessmentScoreDisplayName, int numericValue, decimal trashGenerationRate) : base(onlandVisualTrashAssessmentScoreID, onlandVisualTrashAssessmentScoreName, onlandVisualTrashAssessmentScoreDisplayName, numericValue, trashGenerationRate) {}
        public static readonly OnlandVisualTrashAssessmentScoreB Instance = new OnlandVisualTrashAssessmentScoreB(2, @"B", @"B", 3, 7.5m);
    }

    public partial class OnlandVisualTrashAssessmentScoreC : OnlandVisualTrashAssessmentScore
    {
        private OnlandVisualTrashAssessmentScoreC(int onlandVisualTrashAssessmentScoreID, string onlandVisualTrashAssessmentScoreName, string onlandVisualTrashAssessmentScoreDisplayName, int numericValue, decimal trashGenerationRate) : base(onlandVisualTrashAssessmentScoreID, onlandVisualTrashAssessmentScoreName, onlandVisualTrashAssessmentScoreDisplayName, numericValue, trashGenerationRate) {}
        public static readonly OnlandVisualTrashAssessmentScoreC Instance = new OnlandVisualTrashAssessmentScoreC(3, @"C", @"C", 2, 30.0m);
    }

    public partial class OnlandVisualTrashAssessmentScoreD : OnlandVisualTrashAssessmentScore
    {
        private OnlandVisualTrashAssessmentScoreD(int onlandVisualTrashAssessmentScoreID, string onlandVisualTrashAssessmentScoreName, string onlandVisualTrashAssessmentScoreDisplayName, int numericValue, decimal trashGenerationRate) : base(onlandVisualTrashAssessmentScoreID, onlandVisualTrashAssessmentScoreName, onlandVisualTrashAssessmentScoreDisplayName, numericValue, trashGenerationRate) {}
        public static readonly OnlandVisualTrashAssessmentScoreD Instance = new OnlandVisualTrashAssessmentScoreD(4, @"D", @"D", 1, 100.0m);
    }
}