//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PriorityLandUseType]
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
    public abstract partial class PriorityLandUseType : IHavePrimaryKey
    {
        public static readonly PriorityLandUseTypeCommercial Commercial = PriorityLandUseTypeCommercial.Instance;
        public static readonly PriorityLandUseTypeHighDensity HighDensity = PriorityLandUseTypeHighDensity.Instance;
        public static readonly PriorityLandUseTypeIndustrial Industrial = PriorityLandUseTypeIndustrial.Instance;
        public static readonly PriorityLandUseTypeMixedUrban MixedUrban = PriorityLandUseTypeMixedUrban.Instance;
        public static readonly PriorityLandUseTypeRetail Retail = PriorityLandUseTypeRetail.Instance;
        public static readonly PriorityLandUseTypeTransportation Transportation = PriorityLandUseTypeTransportation.Instance;

        public static readonly List<PriorityLandUseType> All;
        public static readonly ReadOnlyDictionary<int, PriorityLandUseType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static PriorityLandUseType()
        {
            All = new List<PriorityLandUseType> { Commercial, HighDensity, Industrial, MixedUrban, Retail, Transportation };
            AllLookupDictionary = new ReadOnlyDictionary<int, PriorityLandUseType>(All.ToDictionary(x => x.PriorityLandUseTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected PriorityLandUseType(int priorityLandUseTypeID, string priorityLandUseTypeName, string priorityLandUseTypeDisplayName, string mapColorHexCode)
        {
            PriorityLandUseTypeID = priorityLandUseTypeID;
            PriorityLandUseTypeName = priorityLandUseTypeName;
            PriorityLandUseTypeDisplayName = priorityLandUseTypeDisplayName;
            MapColorHexCode = mapColorHexCode;
        }

        [Key]
        public int PriorityLandUseTypeID { get; private set; }
        public string PriorityLandUseTypeName { get; private set; }
        public string PriorityLandUseTypeDisplayName { get; private set; }
        public string MapColorHexCode { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return PriorityLandUseTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(PriorityLandUseType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.PriorityLandUseTypeID == PriorityLandUseTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as PriorityLandUseType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return PriorityLandUseTypeID;
        }

        public static bool operator ==(PriorityLandUseType left, PriorityLandUseType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PriorityLandUseType left, PriorityLandUseType right)
        {
            return !Equals(left, right);
        }

        public PriorityLandUseTypeEnum ToEnum { get { return (PriorityLandUseTypeEnum)GetHashCode(); } }

        public static PriorityLandUseType ToType(int enumValue)
        {
            return ToType((PriorityLandUseTypeEnum)enumValue);
        }

        public static PriorityLandUseType ToType(PriorityLandUseTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case PriorityLandUseTypeEnum.Commercial:
                    return Commercial;
                case PriorityLandUseTypeEnum.HighDensity:
                    return HighDensity;
                case PriorityLandUseTypeEnum.Industrial:
                    return Industrial;
                case PriorityLandUseTypeEnum.MixedUrban:
                    return MixedUrban;
                case PriorityLandUseTypeEnum.Retail:
                    return Retail;
                case PriorityLandUseTypeEnum.Transportation:
                    return Transportation;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum PriorityLandUseTypeEnum
    {
        Commercial = 1,
        HighDensity = 2,
        Industrial = 3,
        MixedUrban = 4,
        Retail = 5,
        Transportation = 6
    }

    public partial class PriorityLandUseTypeCommercial : PriorityLandUseType
    {
        private PriorityLandUseTypeCommercial(int priorityLandUseTypeID, string priorityLandUseTypeName, string priorityLandUseTypeDisplayName, string mapColorHexCode) : base(priorityLandUseTypeID, priorityLandUseTypeName, priorityLandUseTypeDisplayName, mapColorHexCode) {}
        public static readonly PriorityLandUseTypeCommercial Instance = new PriorityLandUseTypeCommercial(1, @"Commercial", @"Commercial", @"#c2fbfc");
    }

    public partial class PriorityLandUseTypeHighDensity : PriorityLandUseType
    {
        private PriorityLandUseTypeHighDensity(int priorityLandUseTypeID, string priorityLandUseTypeName, string priorityLandUseTypeDisplayName, string mapColorHexCode) : base(priorityLandUseTypeID, priorityLandUseTypeName, priorityLandUseTypeDisplayName, mapColorHexCode) {}
        public static readonly PriorityLandUseTypeHighDensity Instance = new PriorityLandUseTypeHighDensity(2, @"HighDensity", @"High Density", @"#c0d6fc");
    }

    public partial class PriorityLandUseTypeIndustrial : PriorityLandUseType
    {
        private PriorityLandUseTypeIndustrial(int priorityLandUseTypeID, string priorityLandUseTypeName, string priorityLandUseTypeDisplayName, string mapColorHexCode) : base(priorityLandUseTypeID, priorityLandUseTypeName, priorityLandUseTypeDisplayName, mapColorHexCode) {}
        public static readonly PriorityLandUseTypeIndustrial Instance = new PriorityLandUseTypeIndustrial(3, @"Industrial", @"Industrial", @"#b4fcb3");
    }

    public partial class PriorityLandUseTypeMixedUrban : PriorityLandUseType
    {
        private PriorityLandUseTypeMixedUrban(int priorityLandUseTypeID, string priorityLandUseTypeName, string priorityLandUseTypeDisplayName, string mapColorHexCode) : base(priorityLandUseTypeID, priorityLandUseTypeName, priorityLandUseTypeDisplayName, mapColorHexCode) {}
        public static readonly PriorityLandUseTypeMixedUrban Instance = new PriorityLandUseTypeMixedUrban(4, @"MixedUrban", @"Mixed Urban", @"#fcb6b9");
    }

    public partial class PriorityLandUseTypeRetail : PriorityLandUseType
    {
        private PriorityLandUseTypeRetail(int priorityLandUseTypeID, string priorityLandUseTypeName, string priorityLandUseTypeDisplayName, string mapColorHexCode) : base(priorityLandUseTypeID, priorityLandUseTypeName, priorityLandUseTypeDisplayName, mapColorHexCode) {}
        public static readonly PriorityLandUseTypeRetail Instance = new PriorityLandUseTypeRetail(5, @"Retail", @"Retail", @"#f2cafc");
    }

    public partial class PriorityLandUseTypeTransportation : PriorityLandUseType
    {
        private PriorityLandUseTypeTransportation(int priorityLandUseTypeID, string priorityLandUseTypeName, string priorityLandUseTypeDisplayName, string mapColorHexCode) : base(priorityLandUseTypeID, priorityLandUseTypeName, priorityLandUseTypeDisplayName, mapColorHexCode) {}
        public static readonly PriorityLandUseTypeTransportation Instance = new PriorityLandUseTypeTransportation(6, @"Transportation", @"Transportation", @"#fcd6b6");
    }
}