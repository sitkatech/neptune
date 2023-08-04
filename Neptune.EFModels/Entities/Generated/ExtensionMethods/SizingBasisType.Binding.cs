//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SizingBasisType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class SizingBasisType
    {
        public static readonly SizingBasisTypeFullTrashCapture FullTrashCapture = Neptune.EFModels.Entities.SizingBasisTypeFullTrashCapture.Instance;
        public static readonly SizingBasisTypeWaterQuality WaterQuality = Neptune.EFModels.Entities.SizingBasisTypeWaterQuality.Instance;
        public static readonly SizingBasisTypeOther Other = Neptune.EFModels.Entities.SizingBasisTypeOther.Instance;
        public static readonly SizingBasisTypeNotProvided NotProvided = Neptune.EFModels.Entities.SizingBasisTypeNotProvided.Instance;

        public static readonly List<SizingBasisType> All;
        public static readonly List<SizingBasisTypeDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, SizingBasisType> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, SizingBasisTypeDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static SizingBasisType()
        {
            All = new List<SizingBasisType> { FullTrashCapture, WaterQuality, Other, NotProvided };
            AllAsDto = new List<SizingBasisTypeDto> { FullTrashCapture.AsDto(), WaterQuality.AsDto(), Other.AsDto(), NotProvided.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, SizingBasisType>(All.ToDictionary(x => x.SizingBasisTypeID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, SizingBasisTypeDto>(AllAsDto.ToDictionary(x => x.SizingBasisTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected SizingBasisType(int sizingBasisTypeID, string sizingBasisTypeName, string sizingBasisTypeDisplayName)
        {
            SizingBasisTypeID = sizingBasisTypeID;
            SizingBasisTypeName = sizingBasisTypeName;
            SizingBasisTypeDisplayName = sizingBasisTypeDisplayName;
        }

        [Key]
        public int SizingBasisTypeID { get; private set; }
        public string SizingBasisTypeName { get; private set; }
        public string SizingBasisTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return SizingBasisTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(SizingBasisType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.SizingBasisTypeID == SizingBasisTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as SizingBasisType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return SizingBasisTypeID;
        }

        public static bool operator ==(SizingBasisType left, SizingBasisType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SizingBasisType left, SizingBasisType right)
        {
            return !Equals(left, right);
        }

        public SizingBasisTypeEnum ToEnum => (SizingBasisTypeEnum)GetHashCode();

        public static SizingBasisType ToType(int enumValue)
        {
            return ToType((SizingBasisTypeEnum)enumValue);
        }

        public static SizingBasisType ToType(SizingBasisTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case SizingBasisTypeEnum.FullTrashCapture:
                    return FullTrashCapture;
                case SizingBasisTypeEnum.NotProvided:
                    return NotProvided;
                case SizingBasisTypeEnum.Other:
                    return Other;
                case SizingBasisTypeEnum.WaterQuality:
                    return WaterQuality;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum SizingBasisTypeEnum
    {
        FullTrashCapture = 1,
        WaterQuality = 2,
        Other = 3,
        NotProvided = 4
    }

    public partial class SizingBasisTypeFullTrashCapture : SizingBasisType
    {
        private SizingBasisTypeFullTrashCapture(int sizingBasisTypeID, string sizingBasisTypeName, string sizingBasisTypeDisplayName) : base(sizingBasisTypeID, sizingBasisTypeName, sizingBasisTypeDisplayName) {}
        public static readonly SizingBasisTypeFullTrashCapture Instance = new SizingBasisTypeFullTrashCapture(1, @"FullTrashCapture", @"Full Trash Capture");
    }

    public partial class SizingBasisTypeWaterQuality : SizingBasisType
    {
        private SizingBasisTypeWaterQuality(int sizingBasisTypeID, string sizingBasisTypeName, string sizingBasisTypeDisplayName) : base(sizingBasisTypeID, sizingBasisTypeName, sizingBasisTypeDisplayName) {}
        public static readonly SizingBasisTypeWaterQuality Instance = new SizingBasisTypeWaterQuality(2, @"WaterQuality", @"Water Quality");
    }

    public partial class SizingBasisTypeOther : SizingBasisType
    {
        private SizingBasisTypeOther(int sizingBasisTypeID, string sizingBasisTypeName, string sizingBasisTypeDisplayName) : base(sizingBasisTypeID, sizingBasisTypeName, sizingBasisTypeDisplayName) {}
        public static readonly SizingBasisTypeOther Instance = new SizingBasisTypeOther(3, @"Other", @"Other (less than Water Quality)");
    }

    public partial class SizingBasisTypeNotProvided : SizingBasisType
    {
        private SizingBasisTypeNotProvided(int sizingBasisTypeID, string sizingBasisTypeName, string sizingBasisTypeDisplayName) : base(sizingBasisTypeID, sizingBasisTypeName, sizingBasisTypeDisplayName) {}
        public static readonly SizingBasisTypeNotProvided Instance = new SizingBasisTypeNotProvided(4, @"NotProvided", @"Not Provided");
    }
}