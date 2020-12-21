//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionBMPPublicVisibilityType]
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
    public abstract partial class StormwaterJurisdictionBMPPublicVisibilityType : IHavePrimaryKey
    {
        public static readonly StormwaterJurisdictionBMPPublicVisibilityTypeVerifiedOnly VerifiedOnly = StormwaterJurisdictionBMPPublicVisibilityTypeVerifiedOnly.Instance;
        public static readonly StormwaterJurisdictionBMPPublicVisibilityTypeNone None = StormwaterJurisdictionBMPPublicVisibilityTypeNone.Instance;

        public static readonly List<StormwaterJurisdictionBMPPublicVisibilityType> All;
        public static readonly ReadOnlyDictionary<int, StormwaterJurisdictionBMPPublicVisibilityType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static StormwaterJurisdictionBMPPublicVisibilityType()
        {
            All = new List<StormwaterJurisdictionBMPPublicVisibilityType> { VerifiedOnly, None };
            AllLookupDictionary = new ReadOnlyDictionary<int, StormwaterJurisdictionBMPPublicVisibilityType>(All.ToDictionary(x => x.StormwaterJurisdictionBMPPublicVisibilityTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected StormwaterJurisdictionBMPPublicVisibilityType(int stormwaterJurisdictionBMPPublicVisibilityTypeID, string stormwaterJurisdictionBMPPublicVisibilityTypeName, string stormwaterJurisdictionBMPPublicVisibilityTypeDisplayName)
        {
            StormwaterJurisdictionBMPPublicVisibilityTypeID = stormwaterJurisdictionBMPPublicVisibilityTypeID;
            StormwaterJurisdictionBMPPublicVisibilityTypeName = stormwaterJurisdictionBMPPublicVisibilityTypeName;
            StormwaterJurisdictionBMPPublicVisibilityTypeDisplayName = stormwaterJurisdictionBMPPublicVisibilityTypeDisplayName;
        }

        [Key]
        public int StormwaterJurisdictionBMPPublicVisibilityTypeID { get; private set; }
        public string StormwaterJurisdictionBMPPublicVisibilityTypeName { get; private set; }
        public string StormwaterJurisdictionBMPPublicVisibilityTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return StormwaterJurisdictionBMPPublicVisibilityTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(StormwaterJurisdictionBMPPublicVisibilityType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.StormwaterJurisdictionBMPPublicVisibilityTypeID == StormwaterJurisdictionBMPPublicVisibilityTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as StormwaterJurisdictionBMPPublicVisibilityType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return StormwaterJurisdictionBMPPublicVisibilityTypeID;
        }

        public static bool operator ==(StormwaterJurisdictionBMPPublicVisibilityType left, StormwaterJurisdictionBMPPublicVisibilityType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StormwaterJurisdictionBMPPublicVisibilityType left, StormwaterJurisdictionBMPPublicVisibilityType right)
        {
            return !Equals(left, right);
        }

        public StormwaterJurisdictionBMPPublicVisibilityTypeEnum ToEnum { get { return (StormwaterJurisdictionBMPPublicVisibilityTypeEnum)GetHashCode(); } }

        public static StormwaterJurisdictionBMPPublicVisibilityType ToType(int enumValue)
        {
            return ToType((StormwaterJurisdictionBMPPublicVisibilityTypeEnum)enumValue);
        }

        public static StormwaterJurisdictionBMPPublicVisibilityType ToType(StormwaterJurisdictionBMPPublicVisibilityTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case StormwaterJurisdictionBMPPublicVisibilityTypeEnum.None:
                    return None;
                case StormwaterJurisdictionBMPPublicVisibilityTypeEnum.VerifiedOnly:
                    return VerifiedOnly;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum StormwaterJurisdictionBMPPublicVisibilityTypeEnum
    {
        VerifiedOnly = 1,
        None = 2
    }

    public partial class StormwaterJurisdictionBMPPublicVisibilityTypeVerifiedOnly : StormwaterJurisdictionBMPPublicVisibilityType
    {
        private StormwaterJurisdictionBMPPublicVisibilityTypeVerifiedOnly(int stormwaterJurisdictionBMPPublicVisibilityTypeID, string stormwaterJurisdictionBMPPublicVisibilityTypeName, string stormwaterJurisdictionBMPPublicVisibilityTypeDisplayName) : base(stormwaterJurisdictionBMPPublicVisibilityTypeID, stormwaterJurisdictionBMPPublicVisibilityTypeName, stormwaterJurisdictionBMPPublicVisibilityTypeDisplayName) {}
        public static readonly StormwaterJurisdictionBMPPublicVisibilityTypeVerifiedOnly Instance = new StormwaterJurisdictionBMPPublicVisibilityTypeVerifiedOnly(1, @"VerifiedOnly", @"Verified Only");
    }

    public partial class StormwaterJurisdictionBMPPublicVisibilityTypeNone : StormwaterJurisdictionBMPPublicVisibilityType
    {
        private StormwaterJurisdictionBMPPublicVisibilityTypeNone(int stormwaterJurisdictionBMPPublicVisibilityTypeID, string stormwaterJurisdictionBMPPublicVisibilityTypeName, string stormwaterJurisdictionBMPPublicVisibilityTypeDisplayName) : base(stormwaterJurisdictionBMPPublicVisibilityTypeID, stormwaterJurisdictionBMPPublicVisibilityTypeName, stormwaterJurisdictionBMPPublicVisibilityTypeDisplayName) {}
        public static readonly StormwaterJurisdictionBMPPublicVisibilityTypeNone Instance = new StormwaterJurisdictionBMPPublicVisibilityTypeNone(2, @"None", @"None");
    }
}