//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTargetType]
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
    public abstract partial class ObservationTargetType : IHavePrimaryKey
    {
        public static readonly ObservationTargetTypePassFail PassFail = ObservationTargetTypePassFail.Instance;
        public static readonly ObservationTargetTypeHigh High = ObservationTargetTypeHigh.Instance;
        public static readonly ObservationTargetTypeLow Low = ObservationTargetTypeLow.Instance;
        public static readonly ObservationTargetTypeSpecificValue SpecificValue = ObservationTargetTypeSpecificValue.Instance;

        public static readonly List<ObservationTargetType> All;
        public static readonly ReadOnlyDictionary<int, ObservationTargetType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationTargetType()
        {
            All = new List<ObservationTargetType> { PassFail, High, Low, SpecificValue };
            AllLookupDictionary = new ReadOnlyDictionary<int, ObservationTargetType>(All.ToDictionary(x => x.ObservationTargetTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ObservationTargetType(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder)
        {
            ObservationTargetTypeID = observationTargetTypeID;
            ObservationTargetTypeName = observationTargetTypeName;
            ObservationTargetTypeDisplayName = observationTargetTypeDisplayName;
            SortOrder = sortOrder;
        }
        public List<ObservationTypeSpecification> ObservationTypeSpecifications { get { return ObservationTypeSpecification.All.Where(x => x.ObservationTargetTypeID == ObservationTargetTypeID).ToList(); } }
        [Key]
        public int ObservationTargetTypeID { get; private set; }
        public string ObservationTargetTypeName { get; private set; }
        public string ObservationTargetTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return ObservationTargetTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ObservationTargetType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ObservationTargetTypeID == ObservationTargetTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ObservationTargetType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ObservationTargetTypeID;
        }

        public static bool operator ==(ObservationTargetType left, ObservationTargetType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ObservationTargetType left, ObservationTargetType right)
        {
            return !Equals(left, right);
        }

        public ObservationTargetTypeEnum ToEnum { get { return (ObservationTargetTypeEnum)GetHashCode(); } }

        public static ObservationTargetType ToType(int enumValue)
        {
            return ToType((ObservationTargetTypeEnum)enumValue);
        }

        public static ObservationTargetType ToType(ObservationTargetTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case ObservationTargetTypeEnum.High:
                    return High;
                case ObservationTargetTypeEnum.Low:
                    return Low;
                case ObservationTargetTypeEnum.PassFail:
                    return PassFail;
                case ObservationTargetTypeEnum.SpecificValue:
                    return SpecificValue;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationTargetTypeEnum
    {
        PassFail = 1,
        High = 2,
        Low = 3,
        SpecificValue = 4
    }

    public partial class ObservationTargetTypePassFail : ObservationTargetType
    {
        private ObservationTargetTypePassFail(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder) : base(observationTargetTypeID, observationTargetTypeName, observationTargetTypeDisplayName, sortOrder) {}
        public static readonly ObservationTargetTypePassFail Instance = new ObservationTargetTypePassFail(1, @"PassFail", @"Observation is Pass/Fail", 10);
    }

    public partial class ObservationTargetTypeHigh : ObservationTargetType
    {
        private ObservationTargetTypeHigh(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder) : base(observationTargetTypeID, observationTargetTypeName, observationTargetTypeDisplayName, sortOrder) {}
        public static readonly ObservationTargetTypeHigh Instance = new ObservationTargetTypeHigh(2, @"High", @"Higher observed values result in higher score", 20);
    }

    public partial class ObservationTargetTypeLow : ObservationTargetType
    {
        private ObservationTargetTypeLow(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder) : base(observationTargetTypeID, observationTargetTypeName, observationTargetTypeDisplayName, sortOrder) {}
        public static readonly ObservationTargetTypeLow Instance = new ObservationTargetTypeLow(3, @"Low", @"Lower observed values result in higher score", 30);
    }

    public partial class ObservationTargetTypeSpecificValue : ObservationTargetType
    {
        private ObservationTargetTypeSpecificValue(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder) : base(observationTargetTypeID, observationTargetTypeName, observationTargetTypeDisplayName, sortOrder) {}
        public static readonly ObservationTargetTypeSpecificValue Instance = new ObservationTargetTypeSpecificValue(4, @"SpecificValue", @"Observed values exactly equal to the benchmark result in highest score", 40);
    }
}