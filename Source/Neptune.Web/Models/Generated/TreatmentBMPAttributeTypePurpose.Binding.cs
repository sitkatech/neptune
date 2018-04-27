//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAttributeTypePurpose]
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
    public abstract partial class TreatmentBMPAttributeTypePurpose : IHavePrimaryKey
    {
        public static readonly TreatmentBMPAttributeTypePurposePerformanceAndModelingAttributes PerformanceAndModelingAttributes = TreatmentBMPAttributeTypePurposePerformanceAndModelingAttributes.Instance;
        public static readonly TreatmentBMPAttributeTypePurposeOtherDesignAttributes OtherDesignAttributes = TreatmentBMPAttributeTypePurposeOtherDesignAttributes.Instance;

        public static readonly List<TreatmentBMPAttributeTypePurpose> All;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPAttributeTypePurpose> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentBMPAttributeTypePurpose()
        {
            All = new List<TreatmentBMPAttributeTypePurpose> { PerformanceAndModelingAttributes, OtherDesignAttributes };
            AllLookupDictionary = new ReadOnlyDictionary<int, TreatmentBMPAttributeTypePurpose>(All.ToDictionary(x => x.TreatmentBMPAttributeTypePurposeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected TreatmentBMPAttributeTypePurpose(int treatmentBMPAttributeTypePurposeID, string treatmentBMPAttributeTypePurposeName, string treatmentBMPAttributeTypePurposeDisplayName)
        {
            TreatmentBMPAttributeTypePurposeID = treatmentBMPAttributeTypePurposeID;
            TreatmentBMPAttributeTypePurposeName = treatmentBMPAttributeTypePurposeName;
            TreatmentBMPAttributeTypePurposeDisplayName = treatmentBMPAttributeTypePurposeDisplayName;
        }

        [Key]
        public int TreatmentBMPAttributeTypePurposeID { get; private set; }
        public string TreatmentBMPAttributeTypePurposeName { get; private set; }
        public string TreatmentBMPAttributeTypePurposeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPAttributeTypePurposeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(TreatmentBMPAttributeTypePurpose other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TreatmentBMPAttributeTypePurposeID == TreatmentBMPAttributeTypePurposeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreatmentBMPAttributeTypePurpose);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TreatmentBMPAttributeTypePurposeID;
        }

        public static bool operator ==(TreatmentBMPAttributeTypePurpose left, TreatmentBMPAttributeTypePurpose right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TreatmentBMPAttributeTypePurpose left, TreatmentBMPAttributeTypePurpose right)
        {
            return !Equals(left, right);
        }

        public TreatmentBMPAttributeTypePurposeEnum ToEnum { get { return (TreatmentBMPAttributeTypePurposeEnum)GetHashCode(); } }

        public static TreatmentBMPAttributeTypePurpose ToType(int enumValue)
        {
            return ToType((TreatmentBMPAttributeTypePurposeEnum)enumValue);
        }

        public static TreatmentBMPAttributeTypePurpose ToType(TreatmentBMPAttributeTypePurposeEnum enumValue)
        {
            switch (enumValue)
            {
                case TreatmentBMPAttributeTypePurposeEnum.OtherDesignAttributes:
                    return OtherDesignAttributes;
                case TreatmentBMPAttributeTypePurposeEnum.PerformanceAndModelingAttributes:
                    return PerformanceAndModelingAttributes;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TreatmentBMPAttributeTypePurposeEnum
    {
        PerformanceAndModelingAttributes = 1,
        OtherDesignAttributes = 2
    }

    public partial class TreatmentBMPAttributeTypePurposePerformanceAndModelingAttributes : TreatmentBMPAttributeTypePurpose
    {
        private TreatmentBMPAttributeTypePurposePerformanceAndModelingAttributes(int treatmentBMPAttributeTypePurposeID, string treatmentBMPAttributeTypePurposeName, string treatmentBMPAttributeTypePurposeDisplayName) : base(treatmentBMPAttributeTypePurposeID, treatmentBMPAttributeTypePurposeName, treatmentBMPAttributeTypePurposeDisplayName) {}
        public static readonly TreatmentBMPAttributeTypePurposePerformanceAndModelingAttributes Instance = new TreatmentBMPAttributeTypePurposePerformanceAndModelingAttributes(1, @"PerformanceAndModelingAttributes", @"Performance / Modeling Attributes");
    }

    public partial class TreatmentBMPAttributeTypePurposeOtherDesignAttributes : TreatmentBMPAttributeTypePurpose
    {
        private TreatmentBMPAttributeTypePurposeOtherDesignAttributes(int treatmentBMPAttributeTypePurposeID, string treatmentBMPAttributeTypePurposeName, string treatmentBMPAttributeTypePurposeDisplayName) : base(treatmentBMPAttributeTypePurposeID, treatmentBMPAttributeTypePurposeName, treatmentBMPAttributeTypePurposeDisplayName) {}
        public static readonly TreatmentBMPAttributeTypePurposeOtherDesignAttributes Instance = new TreatmentBMPAttributeTypePurposeOtherDesignAttributes(2, @"OtherDesignAttributes", @"Other Design Attributes");
    }
}