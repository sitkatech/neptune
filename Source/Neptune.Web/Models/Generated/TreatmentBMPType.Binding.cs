//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
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
    public abstract partial class TreatmentBMPType : IHavePrimaryKey
    {


        public static readonly List<TreatmentBMPType> All;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentBMPType()
        {
            All = new List<TreatmentBMPType> {  };
            AllLookupDictionary = new ReadOnlyDictionary<int, TreatmentBMPType>(All.ToDictionary(x => x.TreatmentBMPTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected TreatmentBMPType(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType)
        {
            TreatmentBMPTypeID = treatmentBMPTypeID;
            TreatmentBMPTypeName = treatmentBMPTypeName;
            TreatmentBMPTypeDisplayName = treatmentBMPTypeDisplayName;
            SortOrder = sortOrder;
            DisplayColor = displayColor;
            GlyphIconClass = glyphIconClass;
            IsDistributedBMPType = isDistributedBMPType;
        }

        [Key]
        public int TreatmentBMPTypeID { get; private set; }
        public string TreatmentBMPTypeName { get; private set; }
        public string TreatmentBMPTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        public string DisplayColor { get; private set; }
        public string GlyphIconClass { get; private set; }
        public bool IsDistributedBMPType { get; private set; }
        public int PrimaryKey { get { return TreatmentBMPTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(TreatmentBMPType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TreatmentBMPTypeID == TreatmentBMPTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreatmentBMPType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TreatmentBMPTypeID;
        }

        public static bool operator ==(TreatmentBMPType left, TreatmentBMPType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TreatmentBMPType left, TreatmentBMPType right)
        {
            return !Equals(left, right);
        }

        public TreatmentBMPTypeEnum ToEnum { get { return (TreatmentBMPTypeEnum)GetHashCode(); } }

        public static TreatmentBMPType ToType(int enumValue)
        {
            return ToType((TreatmentBMPTypeEnum)enumValue);
        }

        public static TreatmentBMPType ToType(TreatmentBMPTypeEnum enumValue)
        {
            switch (enumValue)
            {

                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TreatmentBMPTypeEnum
    {

    }

}