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
        public static readonly ObservationTargetTypeYesNo YesNo = ObservationTargetTypeYesNo.Instance;
        public static readonly ObservationTargetTypeHighTargetValue HighTargetValue = ObservationTargetTypeHighTargetValue.Instance;
        public static readonly ObservationTargetTypeLowTargetValue LowTargetValue = ObservationTargetTypeLowTargetValue.Instance;
        public static readonly ObservationTargetTypeTargetValue TargetValue = ObservationTargetTypeTargetValue.Instance;

        public static readonly List<ObservationTargetType> All;
        public static readonly ReadOnlyDictionary<int, ObservationTargetType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationTargetType()
        {
            All = new List<ObservationTargetType> { YesNo, HighTargetValue, LowTargetValue, TargetValue };
            AllLookupDictionary = new ReadOnlyDictionary<int, ObservationTargetType>(All.ToDictionary(x => x.ObservationTargetTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ObservationTargetType(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder, string observationTargetTypeDescription)
        {
            ObservationTargetTypeID = observationTargetTypeID;
            ObservationTargetTypeName = observationTargetTypeName;
            ObservationTargetTypeDisplayName = observationTargetTypeDisplayName;
            SortOrder = sortOrder;
            ObservationTargetTypeDescription = observationTargetTypeDescription;
        }
        public List<ObservationTypeSpecification> ObservationTypeSpecifications { get { return ObservationTypeSpecification.All.Where(x => x.ObservationTargetTypeID == ObservationTargetTypeID).ToList(); } }
        [Key]
        public int ObservationTargetTypeID { get; private set; }
        public string ObservationTargetTypeName { get; private set; }
        public string ObservationTargetTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        public string ObservationTargetTypeDescription { get; private set; }
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
                case ObservationTargetTypeEnum.HighTargetValue:
                    return HighTargetValue;
                case ObservationTargetTypeEnum.LowTargetValue:
                    return LowTargetValue;
                case ObservationTargetTypeEnum.TargetValue:
                    return TargetValue;
                case ObservationTargetTypeEnum.YesNo:
                    return YesNo;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationTargetTypeEnum
    {
        YesNo = 1,
        HighTargetValue = 2,
        LowTargetValue = 3,
        TargetValue = 4
    }

    public partial class ObservationTargetTypeYesNo : ObservationTargetType
    {
        private ObservationTargetTypeYesNo(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder, string observationTargetTypeDescription) : base(observationTargetTypeID, observationTargetTypeName, observationTargetTypeDisplayName, sortOrder, observationTargetTypeDescription) {}
        public static readonly ObservationTargetTypeYesNo Instance = new ObservationTargetTypeYesNo(1, @"YesNo", @"Yes/No", 10, @"Observation is pass/fail");
    }

    public partial class ObservationTargetTypeHighTargetValue : ObservationTargetType
    {
        private ObservationTargetTypeHighTargetValue(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder, string observationTargetTypeDescription) : base(observationTargetTypeID, observationTargetTypeName, observationTargetTypeDisplayName, sortOrder, observationTargetTypeDescription) {}
        public static readonly ObservationTargetTypeHighTargetValue Instance = new ObservationTargetTypeHighTargetValue(2, @"HighTargetValue", @"High Target Value", 20, @"Observing a high value is good");
    }

    public partial class ObservationTargetTypeLowTargetValue : ObservationTargetType
    {
        private ObservationTargetTypeLowTargetValue(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder, string observationTargetTypeDescription) : base(observationTargetTypeID, observationTargetTypeName, observationTargetTypeDisplayName, sortOrder, observationTargetTypeDescription) {}
        public static readonly ObservationTargetTypeLowTargetValue Instance = new ObservationTargetTypeLowTargetValue(3, @"LowTargetValue", @"Low Target Value", 30, @"Observing a low value is good");
    }

    public partial class ObservationTargetTypeTargetValue : ObservationTargetType
    {
        private ObservationTargetTypeTargetValue(int observationTargetTypeID, string observationTargetTypeName, string observationTargetTypeDisplayName, int sortOrder, string observationTargetTypeDescription) : base(observationTargetTypeID, observationTargetTypeName, observationTargetTypeDisplayName, sortOrder, observationTargetTypeDescription) {}
        public static readonly ObservationTargetTypeTargetValue Instance = new ObservationTargetTypeTargetValue(4, @"TargetValue", @"Target Value", 40, @"Observing a specific targeted value is good");
    }
}