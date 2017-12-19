//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationType]
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
    public abstract partial class ObservationType : IHavePrimaryKey
    {


        public static readonly List<ObservationType> All;
        public static readonly ReadOnlyDictionary<int, ObservationType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationType()
        {
            All = new List<ObservationType> {  };
            AllLookupDictionary = new ReadOnlyDictionary<int, ObservationType>(All.ToDictionary(x => x.ObservationTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ObservationType(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation)
        {
            ObservationTypeID = observationTypeID;
            ObservationTypeName = observationTypeName;
            ObservationTypeDisplayName = observationTypeDisplayName;
            SortOrder = sortOrder;
            MeasurementUnitTypeID = measurementUnitTypeID;
            HasBenchmarkAndThreshold = hasBenchmarkAndThreshold;
            ThresholdPercentDecline = thresholdPercentDecline;
            ThresholdPercentDeviation = thresholdPercentDeviation;
        }
        public List<TreatmentBMPObservationDetailType> TreatmentBMPObservationDetailTypes { get { return TreatmentBMPObservationDetailType.All.Where(x => x.ObservationTypeID == ObservationTypeID).ToList(); } }
        public MeasurementUnitType MeasurementUnitType { get { return MeasurementUnitType.AllLookupDictionary[MeasurementUnitTypeID]; } }
        [Key]
        public int ObservationTypeID { get; private set; }
        public string ObservationTypeName { get; private set; }
        public string ObservationTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        public int MeasurementUnitTypeID { get; private set; }
        public bool HasBenchmarkAndThreshold { get; private set; }
        public bool ThresholdPercentDecline { get; private set; }
        public bool ThresholdPercentDeviation { get; private set; }
        public int PrimaryKey { get { return ObservationTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ObservationType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ObservationTypeID == ObservationTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ObservationType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ObservationTypeID;
        }

        public static bool operator ==(ObservationType left, ObservationType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ObservationType left, ObservationType right)
        {
            return !Equals(left, right);
        }

        public ObservationTypeEnum ToEnum { get { return (ObservationTypeEnum)GetHashCode(); } }

        public static ObservationType ToType(int enumValue)
        {
            return ToType((ObservationTypeEnum)enumValue);
        }

        public static ObservationType ToType(ObservationTypeEnum enumValue)
        {
            switch (enumValue)
            {

                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationTypeEnum
    {

    }

}