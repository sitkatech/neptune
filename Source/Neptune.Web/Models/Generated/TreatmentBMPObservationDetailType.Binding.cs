//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservationDetailType]
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
    public abstract partial class TreatmentBMPObservationDetailType : IHavePrimaryKey
    {


        public static readonly List<TreatmentBMPObservationDetailType> All;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPObservationDetailType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentBMPObservationDetailType()
        {
            All = new List<TreatmentBMPObservationDetailType> {  };
            AllLookupDictionary = new ReadOnlyDictionary<int, TreatmentBMPObservationDetailType>(All.ToDictionary(x => x.TreatmentBMPObservationDetailTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected TreatmentBMPObservationDetailType(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder)
        {
            TreatmentBMPObservationDetailTypeID = treatmentBMPObservationDetailTypeID;
            TreatmentBMPObservationDetailTypeName = treatmentBMPObservationDetailTypeName;
            TreatmentBMPObservationDetailTypeDisplayName = treatmentBMPObservationDetailTypeDisplayName;
            ObservationTypeID = observationTypeID;
            SortOrder = sortOrder;
        }
        public ObservationType ObservationType { get { return ObservationType.AllLookupDictionary[ObservationTypeID]; } }
        [Key]
        public int TreatmentBMPObservationDetailTypeID { get; private set; }
        public string TreatmentBMPObservationDetailTypeName { get; private set; }
        public string TreatmentBMPObservationDetailTypeDisplayName { get; private set; }
        public int ObservationTypeID { get; private set; }
        public int SortOrder { get; private set; }
        public int PrimaryKey { get { return TreatmentBMPObservationDetailTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(TreatmentBMPObservationDetailType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TreatmentBMPObservationDetailTypeID == TreatmentBMPObservationDetailTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreatmentBMPObservationDetailType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TreatmentBMPObservationDetailTypeID;
        }

        public static bool operator ==(TreatmentBMPObservationDetailType left, TreatmentBMPObservationDetailType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TreatmentBMPObservationDetailType left, TreatmentBMPObservationDetailType right)
        {
            return !Equals(left, right);
        }

        public TreatmentBMPObservationDetailTypeEnum ToEnum { get { return (TreatmentBMPObservationDetailTypeEnum)GetHashCode(); } }

        public static TreatmentBMPObservationDetailType ToType(int enumValue)
        {
            return ToType((TreatmentBMPObservationDetailTypeEnum)enumValue);
        }

        public static TreatmentBMPObservationDetailType ToType(TreatmentBMPObservationDetailTypeEnum enumValue)
        {
            switch (enumValue)
            {

                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TreatmentBMPObservationDetailTypeEnum
    {

    }

}