//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationThresholdType]
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
    public abstract partial class ObservationThresholdType : IHavePrimaryKey
    {
        public static readonly ObservationThresholdTypeAbsolute Absolute = ObservationThresholdTypeAbsolute.Instance;
        public static readonly ObservationThresholdTypePercentFromBenchmark PercentFromBenchmark = ObservationThresholdTypePercentFromBenchmark.Instance;
        public static readonly ObservationThresholdTypeNone None = ObservationThresholdTypeNone.Instance;

        public static readonly List<ObservationThresholdType> All;
        public static readonly ReadOnlyDictionary<int, ObservationThresholdType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationThresholdType()
        {
            All = new List<ObservationThresholdType> { Absolute, PercentFromBenchmark, None };
            AllLookupDictionary = new ReadOnlyDictionary<int, ObservationThresholdType>(All.ToDictionary(x => x.ObservationThresholdTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ObservationThresholdType(int observationThresholdTypeID, string observationThresholdTypeName, string observationThresholdTypeDisplayName, int sortOrder, string observationThresholdTypeDescription)
        {
            ObservationThresholdTypeID = observationThresholdTypeID;
            ObservationThresholdTypeName = observationThresholdTypeName;
            ObservationThresholdTypeDisplayName = observationThresholdTypeDisplayName;
            SortOrder = sortOrder;
            ObservationThresholdTypeDescription = observationThresholdTypeDescription;
        }
        public List<ObservationTypeSpecification> ObservationTypeSpecifications { get { return ObservationTypeSpecification.All.Where(x => x.ObservationThresholdTypeID == ObservationThresholdTypeID).ToList(); } }
        [Key]
        public int ObservationThresholdTypeID { get; private set; }
        public string ObservationThresholdTypeName { get; private set; }
        public string ObservationThresholdTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        public string ObservationThresholdTypeDescription { get; private set; }
        public int PrimaryKey { get { return ObservationThresholdTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ObservationThresholdType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ObservationThresholdTypeID == ObservationThresholdTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ObservationThresholdType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ObservationThresholdTypeID;
        }

        public static bool operator ==(ObservationThresholdType left, ObservationThresholdType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ObservationThresholdType left, ObservationThresholdType right)
        {
            return !Equals(left, right);
        }

        public ObservationThresholdTypeEnum ToEnum { get { return (ObservationThresholdTypeEnum)GetHashCode(); } }

        public static ObservationThresholdType ToType(int enumValue)
        {
            return ToType((ObservationThresholdTypeEnum)enumValue);
        }

        public static ObservationThresholdType ToType(ObservationThresholdTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case ObservationThresholdTypeEnum.Absolute:
                    return Absolute;
                case ObservationThresholdTypeEnum.None:
                    return None;
                case ObservationThresholdTypeEnum.PercentFromBenchmark:
                    return PercentFromBenchmark;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationThresholdTypeEnum
    {
        Absolute = 1,
        PercentFromBenchmark = 2,
        None = 3
    }

    public partial class ObservationThresholdTypeAbsolute : ObservationThresholdType
    {
        private ObservationThresholdTypeAbsolute(int observationThresholdTypeID, string observationThresholdTypeName, string observationThresholdTypeDisplayName, int sortOrder, string observationThresholdTypeDescription) : base(observationThresholdTypeID, observationThresholdTypeName, observationThresholdTypeDisplayName, sortOrder, observationThresholdTypeDescription) {}
        public static readonly ObservationThresholdTypeAbsolute Instance = new ObservationThresholdTypeAbsolute(1, @"Absolute", @"Absolute", 10, @"Threshold is measured as an absolute value (e.g. 3 ft of sediment accumulation)");
    }

    public partial class ObservationThresholdTypePercentFromBenchmark : ObservationThresholdType
    {
        private ObservationThresholdTypePercentFromBenchmark(int observationThresholdTypeID, string observationThresholdTypeName, string observationThresholdTypeDisplayName, int sortOrder, string observationThresholdTypeDescription) : base(observationThresholdTypeID, observationThresholdTypeName, observationThresholdTypeDisplayName, sortOrder, observationThresholdTypeDescription) {}
        public static readonly ObservationThresholdTypePercentFromBenchmark Instance = new ObservationThresholdTypePercentFromBenchmark(2, @"PercentFromBenchmark", @"Percent From Benchmark", 20, @"Threshold is measured as a departure from the Benchmark value (e.g. 10% less vegetative cover than the Benchmark value)");
    }

    public partial class ObservationThresholdTypeNone : ObservationThresholdType
    {
        private ObservationThresholdTypeNone(int observationThresholdTypeID, string observationThresholdTypeName, string observationThresholdTypeDisplayName, int sortOrder, string observationThresholdTypeDescription) : base(observationThresholdTypeID, observationThresholdTypeName, observationThresholdTypeDisplayName, sortOrder, observationThresholdTypeDescription) {}
        public static readonly ObservationThresholdTypeNone Instance = new ObservationThresholdTypeNone(3, @"None", @"None", 30, @"No Threshold value for this Observation type");
    }
}