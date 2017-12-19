//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationValueType]
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
    public abstract partial class ObservationValueType : IHavePrimaryKey
    {


        public static readonly List<ObservationValueType> All;
        public static readonly ReadOnlyDictionary<int, ObservationValueType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationValueType()
        {
            All = new List<ObservationValueType> {  };
            AllLookupDictionary = new ReadOnlyDictionary<int, ObservationValueType>(All.ToDictionary(x => x.ObservationValueTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ObservationValueType(int observationValueTypeID, string observationValueTypeName, string observationValueTypeDisplayName, int sortOrder)
        {
            ObservationValueTypeID = observationValueTypeID;
            ObservationValueTypeName = observationValueTypeName;
            ObservationValueTypeDisplayName = observationValueTypeDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int ObservationValueTypeID { get; private set; }
        public string ObservationValueTypeName { get; private set; }
        public string ObservationValueTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        public int PrimaryKey { get { return ObservationValueTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ObservationValueType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ObservationValueTypeID == ObservationValueTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ObservationValueType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ObservationValueTypeID;
        }

        public static bool operator ==(ObservationValueType left, ObservationValueType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ObservationValueType left, ObservationValueType right)
        {
            return !Equals(left, right);
        }

        public ObservationValueTypeEnum ToEnum { get { return (ObservationValueTypeEnum)GetHashCode(); } }

        public static ObservationValueType ToType(int enumValue)
        {
            return ToType((ObservationValueTypeEnum)enumValue);
        }

        public static ObservationValueType ToType(ObservationValueTypeEnum enumValue)
        {
            switch (enumValue)
            {

                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationValueTypeEnum
    {

    }

}