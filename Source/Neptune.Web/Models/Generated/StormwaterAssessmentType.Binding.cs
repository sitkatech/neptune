//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterAssessmentType]
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
    public abstract partial class StormwaterAssessmentType : IHavePrimaryKey
    {
        public static readonly StormwaterAssessmentTypeRegular Regular = StormwaterAssessmentTypeRegular.Instance;
        public static readonly StormwaterAssessmentTypeValidation Validation = StormwaterAssessmentTypeValidation.Instance;

        public static readonly List<StormwaterAssessmentType> All;
        public static readonly ReadOnlyDictionary<int, StormwaterAssessmentType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static StormwaterAssessmentType()
        {
            All = new List<StormwaterAssessmentType> { Regular, Validation };
            AllLookupDictionary = new ReadOnlyDictionary<int, StormwaterAssessmentType>(All.ToDictionary(x => x.StormwaterAssessmentTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected StormwaterAssessmentType(int stormwaterAssessmentTypeID, string stormwaterAssessmentTypeName, string stormwaterAssessmentTypeDisplayName, int sortOrder)
        {
            StormwaterAssessmentTypeID = stormwaterAssessmentTypeID;
            StormwaterAssessmentTypeName = stormwaterAssessmentTypeName;
            StormwaterAssessmentTypeDisplayName = stormwaterAssessmentTypeDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int StormwaterAssessmentTypeID { get; private set; }
        public string StormwaterAssessmentTypeName { get; private set; }
        public string StormwaterAssessmentTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return StormwaterAssessmentTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(StormwaterAssessmentType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.StormwaterAssessmentTypeID == StormwaterAssessmentTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as StormwaterAssessmentType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return StormwaterAssessmentTypeID;
        }

        public static bool operator ==(StormwaterAssessmentType left, StormwaterAssessmentType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StormwaterAssessmentType left, StormwaterAssessmentType right)
        {
            return !Equals(left, right);
        }

        public StormwaterAssessmentTypeEnum ToEnum { get { return (StormwaterAssessmentTypeEnum)GetHashCode(); } }

        public static StormwaterAssessmentType ToType(int enumValue)
        {
            return ToType((StormwaterAssessmentTypeEnum)enumValue);
        }

        public static StormwaterAssessmentType ToType(StormwaterAssessmentTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case StormwaterAssessmentTypeEnum.Regular:
                    return Regular;
                case StormwaterAssessmentTypeEnum.Validation:
                    return Validation;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum StormwaterAssessmentTypeEnum
    {
        Regular = 1,
        Validation = 2
    }

    public partial class StormwaterAssessmentTypeRegular : StormwaterAssessmentType
    {
        private StormwaterAssessmentTypeRegular(int stormwaterAssessmentTypeID, string stormwaterAssessmentTypeName, string stormwaterAssessmentTypeDisplayName, int sortOrder) : base(stormwaterAssessmentTypeID, stormwaterAssessmentTypeName, stormwaterAssessmentTypeDisplayName, sortOrder) {}
        public static readonly StormwaterAssessmentTypeRegular Instance = new StormwaterAssessmentTypeRegular(1, @"Regular", @"Regular", 10);
    }

    public partial class StormwaterAssessmentTypeValidation : StormwaterAssessmentType
    {
        private StormwaterAssessmentTypeValidation(int stormwaterAssessmentTypeID, string stormwaterAssessmentTypeName, string stormwaterAssessmentTypeDisplayName, int sortOrder) : base(stormwaterAssessmentTypeID, stormwaterAssessmentTypeName, stormwaterAssessmentTypeDisplayName, sortOrder) {}
        public static readonly StormwaterAssessmentTypeValidation Instance = new StormwaterAssessmentTypeValidation(2, @"Validation", @"Validation", 20);
    }
}