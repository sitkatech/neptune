//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeSpecification]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public abstract partial class ObservationTypeSpecification : IHavePrimaryKey
    {
        public static readonly ObservationTypeSpecificationYesNo_YesNo_None YesNo_YesNo_None = ObservationTypeSpecificationYesNo_YesNo_None.Instance;
        public static readonly ObservationTypeSpecificationSingleValue_HighTargetValue_Absolute SingleValue_HighTargetValue_Absolute = ObservationTypeSpecificationSingleValue_HighTargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_Absolute MultipleTimeValue_HighTargetValue_Absolute = ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationYesNo_HighTargetValue_Absolute YesNo_HighTargetValue_Absolute = ObservationTypeSpecificationYesNo_HighTargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_HighTargetValue_Absolute PercentValue_HighTargetValue_Absolute = ObservationTypeSpecificationPercentValue_HighTargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationSingleValue_HighTargetValue_PercentFromBenchmark SingleValue_HighTargetValue_PercentFromBenchmark = ObservationTypeSpecificationSingleValue_HighTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_PercentFromBenchmark MultipleTimeValue_HighTargetValue_PercentFromBenchmark = ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark PercentValue_HighTargetValue_PercentFromBenchmark = ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationSingleValue_LowTargetValue_Absolute SingleValue_LowTargetValue_Absolute = ObservationTypeSpecificationSingleValue_LowTargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_Absolute MultipleTimeValue_LowTargetValue_Absolute = ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationYesNo_LowTargetValue_Absolute YesNo_LowTargetValue_Absolute = ObservationTypeSpecificationYesNo_LowTargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_LowTargetValue_Absolute PercentValue_LowTargetValue_Absolute = ObservationTypeSpecificationPercentValue_LowTargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationSingleValue_LowTargetValue_PercentFromBenchmark SingleValue_LowTargetValue_PercentFromBenchmark = ObservationTypeSpecificationSingleValue_LowTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_PercentFromBenchmark MultipleTimeValue_LowTargetValue_PercentFromBenchmark = ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark PercentValue_LowTargetValue_PercentFromBenchmark = ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationSingleValue_TargetValue_Absolute SingleValue_TargetValue_Absolute = ObservationTypeSpecificationSingleValue_TargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationMultipleTimeValue_TargetValue_Absolute MultipleTimeValue_TargetValue_Absolute = ObservationTypeSpecificationMultipleTimeValue_TargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationYesNo_TargetValue_Absolute YesNo_TargetValue_Absolute = ObservationTypeSpecificationYesNo_TargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_TargetValue_Absolute PercentValue_TargetValue_Absolute = ObservationTypeSpecificationPercentValue_TargetValue_Absolute.Instance;
        public static readonly ObservationTypeSpecificationSingleValue_TargetValue_PercentFromBenchmark SingleValue_TargetValue_PercentFromBenchmark = ObservationTypeSpecificationSingleValue_TargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationMultipleTimeValue_TargetValue_PercentFromBenchmark MultipleTimeValue_TargetValue_PercentFromBenchmark = ObservationTypeSpecificationMultipleTimeValue_TargetValue_PercentFromBenchmark.Instance;
        public static readonly ObservationTypeSpecificationPercentValue_TargetValue_PercentFromBenchmark PercentValue_TargetValue_PercentFromBenchmark = ObservationTypeSpecificationPercentValue_TargetValue_PercentFromBenchmark.Instance;

        public static readonly List<ObservationTypeSpecification> All;
        public static readonly ReadOnlyDictionary<int, ObservationTypeSpecification> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationTypeSpecification()
        {
            All = new List<ObservationTypeSpecification> { YesNo_YesNo_None, SingleValue_HighTargetValue_Absolute, MultipleTimeValue_HighTargetValue_Absolute, YesNo_HighTargetValue_Absolute, PercentValue_HighTargetValue_Absolute, SingleValue_HighTargetValue_PercentFromBenchmark, MultipleTimeValue_HighTargetValue_PercentFromBenchmark, PercentValue_HighTargetValue_PercentFromBenchmark, SingleValue_LowTargetValue_Absolute, MultipleTimeValue_LowTargetValue_Absolute, YesNo_LowTargetValue_Absolute, PercentValue_LowTargetValue_Absolute, SingleValue_LowTargetValue_PercentFromBenchmark, MultipleTimeValue_LowTargetValue_PercentFromBenchmark, PercentValue_LowTargetValue_PercentFromBenchmark, SingleValue_TargetValue_Absolute, MultipleTimeValue_TargetValue_Absolute, YesNo_TargetValue_Absolute, PercentValue_TargetValue_Absolute, SingleValue_TargetValue_PercentFromBenchmark, MultipleTimeValue_TargetValue_PercentFromBenchmark, PercentValue_TargetValue_PercentFromBenchmark };
            AllLookupDictionary = new ReadOnlyDictionary<int, ObservationTypeSpecification>(All.ToDictionary(x => x.ObservationTypeSpecificationID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ObservationTypeSpecification(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID)
        {
            ObservationTypeSpecificationID = observationTypeSpecificationID;
            ObservationTypeSpecificationName = observationTypeSpecificationName;
            ObservationTypeSpecificationDisplayName = observationTypeSpecificationDisplayName;
            SortOrder = sortOrder;
            ObservationTypeCollectionMethodID = observationTypeCollectionMethodID;
            ObservationTargetTypeID = observationTargetTypeID;
            ObservationThresholdTypeID = observationThresholdTypeID;
        }
        public ObservationTypeCollectionMethod ObservationTypeCollectionMethod { get { return ObservationTypeCollectionMethod.AllLookupDictionary[ObservationTypeCollectionMethodID]; } }
        public ObservationTargetType ObservationTargetType { get { return ObservationTargetType.AllLookupDictionary[ObservationTargetTypeID]; } }
        public ObservationThresholdType ObservationThresholdType { get { return ObservationThresholdType.AllLookupDictionary[ObservationThresholdTypeID]; } }
        [Key]
        public int ObservationTypeSpecificationID { get; private set; }
        public string ObservationTypeSpecificationName { get; private set; }
        public string ObservationTypeSpecificationDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        public int ObservationTypeCollectionMethodID { get; private set; }
        public int ObservationTargetTypeID { get; private set; }
        public int ObservationThresholdTypeID { get; private set; }
        public int PrimaryKey { get { return ObservationTypeSpecificationID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ObservationTypeSpecification other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ObservationTypeSpecificationID == ObservationTypeSpecificationID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ObservationTypeSpecification);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ObservationTypeSpecificationID;
        }

        public static bool operator ==(ObservationTypeSpecification left, ObservationTypeSpecification right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ObservationTypeSpecification left, ObservationTypeSpecification right)
        {
            return !Equals(left, right);
        }

        public ObservationTypeSpecificationEnum ToEnum { get { return (ObservationTypeSpecificationEnum)GetHashCode(); } }

        public static ObservationTypeSpecification ToType(int enumValue)
        {
            return ToType((ObservationTypeSpecificationEnum)enumValue);
        }

        public static ObservationTypeSpecification ToType(ObservationTypeSpecificationEnum enumValue)
        {
            switch (enumValue)
            {
                case ObservationTypeSpecificationEnum.MultipleTimeValue_HighTargetValue_Absolute:
                    return MultipleTimeValue_HighTargetValue_Absolute;
                case ObservationTypeSpecificationEnum.MultipleTimeValue_HighTargetValue_PercentFromBenchmark:
                    return MultipleTimeValue_HighTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.MultipleTimeValue_LowTargetValue_Absolute:
                    return MultipleTimeValue_LowTargetValue_Absolute;
                case ObservationTypeSpecificationEnum.MultipleTimeValue_LowTargetValue_PercentFromBenchmark:
                    return MultipleTimeValue_LowTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.MultipleTimeValue_TargetValue_Absolute:
                    return MultipleTimeValue_TargetValue_Absolute;
                case ObservationTypeSpecificationEnum.MultipleTimeValue_TargetValue_PercentFromBenchmark:
                    return MultipleTimeValue_TargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.PercentValue_HighTargetValue_Absolute:
                    return PercentValue_HighTargetValue_Absolute;
                case ObservationTypeSpecificationEnum.PercentValue_HighTargetValue_PercentFromBenchmark:
                    return PercentValue_HighTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.PercentValue_LowTargetValue_Absolute:
                    return PercentValue_LowTargetValue_Absolute;
                case ObservationTypeSpecificationEnum.PercentValue_LowTargetValue_PercentFromBenchmark:
                    return PercentValue_LowTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.PercentValue_TargetValue_Absolute:
                    return PercentValue_TargetValue_Absolute;
                case ObservationTypeSpecificationEnum.PercentValue_TargetValue_PercentFromBenchmark:
                    return PercentValue_TargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.SingleValue_HighTargetValue_Absolute:
                    return SingleValue_HighTargetValue_Absolute;
                case ObservationTypeSpecificationEnum.SingleValue_HighTargetValue_PercentFromBenchmark:
                    return SingleValue_HighTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.SingleValue_LowTargetValue_Absolute:
                    return SingleValue_LowTargetValue_Absolute;
                case ObservationTypeSpecificationEnum.SingleValue_LowTargetValue_PercentFromBenchmark:
                    return SingleValue_LowTargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.SingleValue_TargetValue_Absolute:
                    return SingleValue_TargetValue_Absolute;
                case ObservationTypeSpecificationEnum.SingleValue_TargetValue_PercentFromBenchmark:
                    return SingleValue_TargetValue_PercentFromBenchmark;
                case ObservationTypeSpecificationEnum.YesNo_HighTargetValue_Absolute:
                    return YesNo_HighTargetValue_Absolute;
                case ObservationTypeSpecificationEnum.YesNo_LowTargetValue_Absolute:
                    return YesNo_LowTargetValue_Absolute;
                case ObservationTypeSpecificationEnum.YesNo_TargetValue_Absolute:
                    return YesNo_TargetValue_Absolute;
                case ObservationTypeSpecificationEnum.YesNo_YesNo_None:
                    return YesNo_YesNo_None;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationTypeSpecificationEnum
    {
        YesNo_YesNo_None = 1,
        SingleValue_HighTargetValue_Absolute = 2,
        MultipleTimeValue_HighTargetValue_Absolute = 3,
        YesNo_HighTargetValue_Absolute = 4,
        PercentValue_HighTargetValue_Absolute = 5,
        SingleValue_HighTargetValue_PercentFromBenchmark = 6,
        MultipleTimeValue_HighTargetValue_PercentFromBenchmark = 7,
        PercentValue_HighTargetValue_PercentFromBenchmark = 8,
        SingleValue_LowTargetValue_Absolute = 9,
        MultipleTimeValue_LowTargetValue_Absolute = 10,
        YesNo_LowTargetValue_Absolute = 11,
        PercentValue_LowTargetValue_Absolute = 12,
        SingleValue_LowTargetValue_PercentFromBenchmark = 13,
        MultipleTimeValue_LowTargetValue_PercentFromBenchmark = 14,
        PercentValue_LowTargetValue_PercentFromBenchmark = 15,
        SingleValue_TargetValue_Absolute = 16,
        MultipleTimeValue_TargetValue_Absolute = 17,
        YesNo_TargetValue_Absolute = 18,
        PercentValue_TargetValue_Absolute = 19,
        SingleValue_TargetValue_PercentFromBenchmark = 20,
        MultipleTimeValue_TargetValue_PercentFromBenchmark = 21,
        PercentValue_TargetValue_PercentFromBenchmark = 22
    }

    public partial class ObservationTypeSpecificationYesNo_YesNo_None : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationYesNo_YesNo_None(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationYesNo_YesNo_None Instance = new ObservationTypeSpecificationYesNo_YesNo_None(1, @"YesNo_YesNo_None", @" YesNo_YesNo_None", 10, 3, 1, 3);
    }

    public partial class ObservationTypeSpecificationSingleValue_HighTargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationSingleValue_HighTargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationSingleValue_HighTargetValue_Absolute Instance = new ObservationTypeSpecificationSingleValue_HighTargetValue_Absolute(2, @"SingleValue_HighTargetValue_Absolute", @" SingleValue_HighTargetValue_Absolute", 20, 1, 2, 1);
    }

    public partial class ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_Absolute Instance = new ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_Absolute(3, @"MultipleTimeValue_HighTargetValue_Absolute", @" MultipleTimeValue_HighTargetValue_Absolute", 30, 2, 2, 1);
    }

    public partial class ObservationTypeSpecificationYesNo_HighTargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationYesNo_HighTargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationYesNo_HighTargetValue_Absolute Instance = new ObservationTypeSpecificationYesNo_HighTargetValue_Absolute(4, @"YesNo_HighTargetValue_Absolute", @" YesNo_HighTargetValue_Absolute", 40, 3, 2, 1);
    }

    public partial class ObservationTypeSpecificationPercentValue_HighTargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_HighTargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_HighTargetValue_Absolute Instance = new ObservationTypeSpecificationPercentValue_HighTargetValue_Absolute(5, @"PercentValue_HighTargetValue_Absolute", @" PercentValue_HighTargetValue_Absolute", 50, 4, 2, 1);
    }

    public partial class ObservationTypeSpecificationSingleValue_HighTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationSingleValue_HighTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationSingleValue_HighTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationSingleValue_HighTargetValue_PercentFromBenchmark(6, @"SingleValue_HighTargetValue_PercentFromBenchmark", @" SingleValue_HighTargetValue_PercentFromBenchmark", 60, 1, 2, 2);
    }

    public partial class ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationMultipleTimeValue_HighTargetValue_PercentFromBenchmark(7, @"MultipleTimeValue_HighTargetValue_PercentFromBenchmark", @" MultipleTimeValue_HighTargetValue_PercentFromBenchmark", 70, 2, 2, 2);
    }

    public partial class ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationPercentValue_HighTargetValue_PercentFromBenchmark(8, @"PercentValue_HighTargetValue_PercentFromBenchmark", @" PercentValue_HighTargetValue_PercentFromBenchmark", 80, 4, 2, 2);
    }

    public partial class ObservationTypeSpecificationSingleValue_LowTargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationSingleValue_LowTargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationSingleValue_LowTargetValue_Absolute Instance = new ObservationTypeSpecificationSingleValue_LowTargetValue_Absolute(9, @"SingleValue_LowTargetValue_Absolute", @" SingleValue_LowTargetValue_Absolute", 90, 1, 3, 1);
    }

    public partial class ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_Absolute Instance = new ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_Absolute(10, @"MultipleTimeValue_LowTargetValue_Absolute", @" MultipleTimeValue_LowTargetValue_Absolute", 100, 2, 3, 1);
    }

    public partial class ObservationTypeSpecificationYesNo_LowTargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationYesNo_LowTargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationYesNo_LowTargetValue_Absolute Instance = new ObservationTypeSpecificationYesNo_LowTargetValue_Absolute(11, @"YesNo_LowTargetValue_Absolute", @" YesNo_LowTargetValue_Absolute", 110, 3, 3, 1);
    }

    public partial class ObservationTypeSpecificationPercentValue_LowTargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_LowTargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_LowTargetValue_Absolute Instance = new ObservationTypeSpecificationPercentValue_LowTargetValue_Absolute(12, @"PercentValue_LowTargetValue_Absolute", @" PercentValue_LowTargetValue_Absolute", 120, 4, 3, 1);
    }

    public partial class ObservationTypeSpecificationSingleValue_LowTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationSingleValue_LowTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationSingleValue_LowTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationSingleValue_LowTargetValue_PercentFromBenchmark(13, @"SingleValue_LowTargetValue_PercentFromBenchmark", @" SingleValue_LowTargetValue_PercentFromBenchmark", 130, 1, 3, 2);
    }

    public partial class ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationMultipleTimeValue_LowTargetValue_PercentFromBenchmark(14, @"MultipleTimeValue_LowTargetValue_PercentFromBenchmark", @" MultipleTimeValue_LowTargetValue_PercentFromBenchmark", 140, 2, 3, 2);
    }

    public partial class ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationPercentValue_LowTargetValue_PercentFromBenchmark(15, @"PercentValue_LowTargetValue_PercentFromBenchmark", @" PercentValue_LowTargetValue_PercentFromBenchmark", 150, 4, 3, 2);
    }

    public partial class ObservationTypeSpecificationSingleValue_TargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationSingleValue_TargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationSingleValue_TargetValue_Absolute Instance = new ObservationTypeSpecificationSingleValue_TargetValue_Absolute(16, @"SingleValue_TargetValue_Absolute", @" SingleValue_TargetValue_Absolute", 160, 1, 4, 1);
    }

    public partial class ObservationTypeSpecificationMultipleTimeValue_TargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationMultipleTimeValue_TargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationMultipleTimeValue_TargetValue_Absolute Instance = new ObservationTypeSpecificationMultipleTimeValue_TargetValue_Absolute(17, @"MultipleTimeValue_TargetValue_Absolute", @" MultipleTimeValue_TargetValue_Absolute", 170, 2, 4, 1);
    }

    public partial class ObservationTypeSpecificationYesNo_TargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationYesNo_TargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationYesNo_TargetValue_Absolute Instance = new ObservationTypeSpecificationYesNo_TargetValue_Absolute(18, @"YesNo_TargetValue_Absolute", @" YesNo_TargetValue_Absolute", 180, 3, 4, 1);
    }

    public partial class ObservationTypeSpecificationPercentValue_TargetValue_Absolute : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_TargetValue_Absolute(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_TargetValue_Absolute Instance = new ObservationTypeSpecificationPercentValue_TargetValue_Absolute(19, @"PercentValue_TargetValue_Absolute", @" PercentValue_TargetValue_Absolute", 190, 4, 4, 1);
    }

    public partial class ObservationTypeSpecificationSingleValue_TargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationSingleValue_TargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationSingleValue_TargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationSingleValue_TargetValue_PercentFromBenchmark(20, @"SingleValue_TargetValue_PercentFromBenchmark", @" SingleValue_TargetValue_PercentFromBenchmark", 200, 1, 4, 2);
    }

    public partial class ObservationTypeSpecificationMultipleTimeValue_TargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationMultipleTimeValue_TargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationMultipleTimeValue_TargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationMultipleTimeValue_TargetValue_PercentFromBenchmark(21, @"MultipleTimeValue_TargetValue_PercentFromBenchmark", @" MultipleTimeValue_TargetValue_PercentFromBenchmark", 210, 2, 4, 2);
    }

    public partial class ObservationTypeSpecificationPercentValue_TargetValue_PercentFromBenchmark : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationPercentValue_TargetValue_PercentFromBenchmark(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationPercentValue_TargetValue_PercentFromBenchmark Instance = new ObservationTypeSpecificationPercentValue_TargetValue_PercentFromBenchmark(22, @"PercentValue_TargetValue_PercentFromBenchmark", @" PercentValue_TargetValue_PercentFromBenchmark", 220, 4, 4, 2);
    }
}