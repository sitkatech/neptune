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
        public static readonly TreatmentBMPAttributeTypePurposeDesignParameter DesignParameter = TreatmentBMPAttributeTypePurposeDesignParameter.Instance;
        public static readonly TreatmentBMPAttributeTypePurposeOther Other = TreatmentBMPAttributeTypePurposeOther.Instance;

        public static readonly List<TreatmentBMPAttributeTypePurpose> All;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPAttributeTypePurpose> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentBMPAttributeTypePurpose()
        {
            All = new List<TreatmentBMPAttributeTypePurpose> { DesignParameter, Other };
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
                case TreatmentBMPAttributeTypePurposeEnum.DesignParameter:
                    return DesignParameter;
                case TreatmentBMPAttributeTypePurposeEnum.Other:
                    return Other;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TreatmentBMPAttributeTypePurposeEnum
    {
        DesignParameter = 1,
        Other = 2
    }

    public partial class TreatmentBMPAttributeTypePurposeDesignParameter : TreatmentBMPAttributeTypePurpose
    {
        private TreatmentBMPAttributeTypePurposeDesignParameter(int treatmentBMPAttributeTypePurposeID, string treatmentBMPAttributeTypePurposeName, string treatmentBMPAttributeTypePurposeDisplayName) : base(treatmentBMPAttributeTypePurposeID, treatmentBMPAttributeTypePurposeName, treatmentBMPAttributeTypePurposeDisplayName) {}
        public static readonly TreatmentBMPAttributeTypePurposeDesignParameter Instance = new TreatmentBMPAttributeTypePurposeDesignParameter(1, @"DesignParameter", @"Design Parameter");
    }

    public partial class TreatmentBMPAttributeTypePurposeOther : TreatmentBMPAttributeTypePurpose
    {
        private TreatmentBMPAttributeTypePurposeOther(int treatmentBMPAttributeTypePurposeID, string treatmentBMPAttributeTypePurposeName, string treatmentBMPAttributeTypePurposeDisplayName) : base(treatmentBMPAttributeTypePurposeID, treatmentBMPAttributeTypePurposeName, treatmentBMPAttributeTypePurposeDisplayName) {}
        public static readonly TreatmentBMPAttributeTypePurposeOther Instance = new TreatmentBMPAttributeTypePurposeOther(2, @"Other", @"Other");
    }
}