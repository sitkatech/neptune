//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeSpecification]
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
    public abstract partial class ObservationTypeSpecification : IHavePrimaryKey
    {
        public static readonly ObservationTypeSpecificationRegular Regular = ObservationTypeSpecificationRegular.Instance;

        public static readonly List<ObservationTypeSpecification> All;
        public static readonly ReadOnlyDictionary<int, ObservationTypeSpecification> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationTypeSpecification()
        {
            All = new List<ObservationTypeSpecification> { Regular };
            AllLookupDictionary = new ReadOnlyDictionary<int, ObservationTypeSpecification>(All.ToDictionary(x => x.ObservationTypeSpecificationID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ObservationTypeSpecification(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID)
        {
            ObservationTypeSpecificationID = observationTypeSpecificationID;
            ObservationTypeSpecificationName = observationTypeSpecificationName;
            ObservationTypeSpecificationDisplayName = observationTypeSpecificationDisplayName;
            SortOrder = sortOrder;
            ObservationTypeCollectionMethodID = observationTypeCollectionMethodID;
            ObservationTargetTypeID = observationTargetTypeID;
            ObservationThresholdTypeID = observationThresholdTypeID;
        }
        public ObservationTypeCollectionMethod ObservationTypeCollectionMethod { get { return ObservationTypeCollectionMethod.AllLookupDictionary[ObservationTypeCollectionMethodID]; } }
        public ObservationTargetType ObservationTargetType { get { return ObservationTargetType.AllLookupDictionary[ObservationTargetTypeID]; } }
        public ObservationThresholdType ObservationThresholdType { get { return ObservationThresholdType.AllLookupDictionary[ObservationThresholdTypeID]; } }
        [Key]
        public int ObservationTypeSpecificationID { get; private set; }
        public string ObservationTypeSpecificationName { get; private set; }
        public string ObservationTypeSpecificationDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        public int ObservationTypeCollectionMethodID { get; private set; }
        public int ObservationTargetTypeID { get; private set; }
        public int ObservationThresholdTypeID { get; private set; }
        public int PrimaryKey { get { return ObservationTypeSpecificationID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ObservationTypeSpecification other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ObservationTypeSpecificationID == ObservationTypeSpecificationID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ObservationTypeSpecification);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ObservationTypeSpecificationID;
        }

        public static bool operator ==(ObservationTypeSpecification left, ObservationTypeSpecification right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ObservationTypeSpecification left, ObservationTypeSpecification right)
        {
            return !Equals(left, right);
        }

        public ObservationTypeSpecificationEnum ToEnum { get { return (ObservationTypeSpecificationEnum)GetHashCode(); } }

        public static ObservationTypeSpecification ToType(int enumValue)
        {
            return ToType((ObservationTypeSpecificationEnum)enumValue);
        }

        public static ObservationTypeSpecification ToType(ObservationTypeSpecificationEnum enumValue)
        {
            switch (enumValue)
            {
                case ObservationTypeSpecificationEnum.Regular:
                    return Regular;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationTypeSpecificationEnum
    {
        Regular = 1
    }

    public partial class ObservationTypeSpecificationRegular : ObservationTypeSpecification
    {
        private ObservationTypeSpecificationRegular(int observationTypeSpecificationID, string observationTypeSpecificationName, string observationTypeSpecificationDisplayName, int sortOrder, int observationTypeCollectionMethodID, int observationTargetTypeID, int observationThresholdTypeID) : base(observationTypeSpecificationID, observationTypeSpecificationName, observationTypeSpecificationDisplayName, sortOrder, observationTypeCollectionMethodID, observationTargetTypeID, observationThresholdTypeID) {}
        public static readonly ObservationTypeSpecificationRegular Instance = new ObservationTypeSpecificationRegular(1, @"Regular", @"Regular", 10, 1, 1, 1);
    }
}