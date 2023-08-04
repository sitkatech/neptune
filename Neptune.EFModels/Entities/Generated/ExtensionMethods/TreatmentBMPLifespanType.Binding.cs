//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPLifespanType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class TreatmentBMPLifespanType
    {
        public static readonly TreatmentBMPLifespanTypeUnspecified Unspecified = Neptune.EFModels.Entities.TreatmentBMPLifespanTypeUnspecified.Instance;
        public static readonly TreatmentBMPLifespanTypePerpetuity Perpetuity = Neptune.EFModels.Entities.TreatmentBMPLifespanTypePerpetuity.Instance;
        public static readonly TreatmentBMPLifespanTypeFixedEndDate FixedEndDate = Neptune.EFModels.Entities.TreatmentBMPLifespanTypeFixedEndDate.Instance;

        public static readonly List<TreatmentBMPLifespanType> All;
        public static readonly List<TreatmentBMPLifespanTypeDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPLifespanType> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPLifespanTypeDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentBMPLifespanType()
        {
            All = new List<TreatmentBMPLifespanType> { Unspecified, Perpetuity, FixedEndDate };
            AllAsDto = new List<TreatmentBMPLifespanTypeDto> { Unspecified.AsDto(), Perpetuity.AsDto(), FixedEndDate.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, TreatmentBMPLifespanType>(All.ToDictionary(x => x.TreatmentBMPLifespanTypeID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, TreatmentBMPLifespanTypeDto>(AllAsDto.ToDictionary(x => x.TreatmentBMPLifespanTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected TreatmentBMPLifespanType(int treatmentBMPLifespanTypeID, string treatmentBMPLifespanTypeName, string treatmentBMPLifespanTypeDisplayName)
        {
            TreatmentBMPLifespanTypeID = treatmentBMPLifespanTypeID;
            TreatmentBMPLifespanTypeName = treatmentBMPLifespanTypeName;
            TreatmentBMPLifespanTypeDisplayName = treatmentBMPLifespanTypeDisplayName;
        }

        [Key]
        public int TreatmentBMPLifespanTypeID { get; private set; }
        public string TreatmentBMPLifespanTypeName { get; private set; }
        public string TreatmentBMPLifespanTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPLifespanTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(TreatmentBMPLifespanType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TreatmentBMPLifespanTypeID == TreatmentBMPLifespanTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreatmentBMPLifespanType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TreatmentBMPLifespanTypeID;
        }

        public static bool operator ==(TreatmentBMPLifespanType left, TreatmentBMPLifespanType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TreatmentBMPLifespanType left, TreatmentBMPLifespanType right)
        {
            return !Equals(left, right);
        }

        public TreatmentBMPLifespanTypeEnum ToEnum => (TreatmentBMPLifespanTypeEnum)GetHashCode();

        public static TreatmentBMPLifespanType ToType(int enumValue)
        {
            return ToType((TreatmentBMPLifespanTypeEnum)enumValue);
        }

        public static TreatmentBMPLifespanType ToType(TreatmentBMPLifespanTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case TreatmentBMPLifespanTypeEnum.FixedEndDate:
                    return FixedEndDate;
                case TreatmentBMPLifespanTypeEnum.Perpetuity:
                    return Perpetuity;
                case TreatmentBMPLifespanTypeEnum.Unspecified:
                    return Unspecified;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum TreatmentBMPLifespanTypeEnum
    {
        Unspecified = 1,
        Perpetuity = 2,
        FixedEndDate = 3
    }

    public partial class TreatmentBMPLifespanTypeUnspecified : TreatmentBMPLifespanType
    {
        private TreatmentBMPLifespanTypeUnspecified(int treatmentBMPLifespanTypeID, string treatmentBMPLifespanTypeName, string treatmentBMPLifespanTypeDisplayName) : base(treatmentBMPLifespanTypeID, treatmentBMPLifespanTypeName, treatmentBMPLifespanTypeDisplayName) {}
        public static readonly TreatmentBMPLifespanTypeUnspecified Instance = new TreatmentBMPLifespanTypeUnspecified(1, @"Unspecified", @"Unspecified/Voluntary");
    }

    public partial class TreatmentBMPLifespanTypePerpetuity : TreatmentBMPLifespanType
    {
        private TreatmentBMPLifespanTypePerpetuity(int treatmentBMPLifespanTypeID, string treatmentBMPLifespanTypeName, string treatmentBMPLifespanTypeDisplayName) : base(treatmentBMPLifespanTypeID, treatmentBMPLifespanTypeName, treatmentBMPLifespanTypeDisplayName) {}
        public static readonly TreatmentBMPLifespanTypePerpetuity Instance = new TreatmentBMPLifespanTypePerpetuity(2, @"Perpetuity", @"Perpetuity/Life of Project");
    }

    public partial class TreatmentBMPLifespanTypeFixedEndDate : TreatmentBMPLifespanType
    {
        private TreatmentBMPLifespanTypeFixedEndDate(int treatmentBMPLifespanTypeID, string treatmentBMPLifespanTypeName, string treatmentBMPLifespanTypeDisplayName) : base(treatmentBMPLifespanTypeID, treatmentBMPLifespanTypeName, treatmentBMPLifespanTypeDisplayName) {}
        public static readonly TreatmentBMPLifespanTypeFixedEndDate Instance = new TreatmentBMPLifespanTypeFixedEndDate(3, @"FixedEndDate", @"Fixed End Date");
    }
}