//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ObservationTypeCollectionMethod]
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
    public abstract partial class ObservationTypeCollectionMethod : IHavePrimaryKey
    {
        public static readonly ObservationTypeCollectionMethodDiscreteValue DiscreteValue = ObservationTypeCollectionMethodDiscreteValue.Instance;
        public static readonly ObservationTypeCollectionMethodRate Rate = ObservationTypeCollectionMethodRate.Instance;
        public static readonly ObservationTypeCollectionMethodPassFail PassFail = ObservationTypeCollectionMethodPassFail.Instance;
        public static readonly ObservationTypeCollectionMethodPercentage Percentage = ObservationTypeCollectionMethodPercentage.Instance;

        public static readonly List<ObservationTypeCollectionMethod> All;
        public static readonly ReadOnlyDictionary<int, ObservationTypeCollectionMethod> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationTypeCollectionMethod()
        {
            All = new List<ObservationTypeCollectionMethod> { DiscreteValue, Rate, PassFail, Percentage };
            AllLookupDictionary = new ReadOnlyDictionary<int, ObservationTypeCollectionMethod>(All.ToDictionary(x => x.ObservationTypeCollectionMethodID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected ObservationTypeCollectionMethod(int observationTypeCollectionMethodID, string observationTypeCollectionMethodName, string observationTypeCollectionMethodDisplayName, int sortOrder)
        {
            ObservationTypeCollectionMethodID = observationTypeCollectionMethodID;
            ObservationTypeCollectionMethodName = observationTypeCollectionMethodName;
            ObservationTypeCollectionMethodDisplayName = observationTypeCollectionMethodDisplayName;
            SortOrder = sortOrder;
        }
        public List<ObservationTypeSpecification> ObservationTypeSpecifications { get { return ObservationTypeSpecification.All.Where(x => x.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethodID).ToList(); } }
        [Key]
        public int ObservationTypeCollectionMethodID { get; private set; }
        public string ObservationTypeCollectionMethodName { get; private set; }
        public string ObservationTypeCollectionMethodDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        public int PrimaryKey { get { return ObservationTypeCollectionMethodID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(ObservationTypeCollectionMethod other)
        {
            if (other == null)
            {
                return false;
            }
            return other.ObservationTypeCollectionMethodID == ObservationTypeCollectionMethodID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ObservationTypeCollectionMethod);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return ObservationTypeCollectionMethodID;
        }

        public static bool operator ==(ObservationTypeCollectionMethod left, ObservationTypeCollectionMethod right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ObservationTypeCollectionMethod left, ObservationTypeCollectionMethod right)
        {
            return !Equals(left, right);
        }

        public ObservationTypeCollectionMethodEnum ToEnum { get { return (ObservationTypeCollectionMethodEnum)GetHashCode(); } }

        public static ObservationTypeCollectionMethod ToType(int enumValue)
        {
            return ToType((ObservationTypeCollectionMethodEnum)enumValue);
        }

        public static ObservationTypeCollectionMethod ToType(ObservationTypeCollectionMethodEnum enumValue)
        {
            switch (enumValue)
            {
                case ObservationTypeCollectionMethodEnum.DiscreteValue:
                    return DiscreteValue;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    return PassFail;
                case ObservationTypeCollectionMethodEnum.Percentage:
                    return Percentage;
                case ObservationTypeCollectionMethodEnum.Rate:
                    return Rate;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationTypeCollectionMethodEnum
    {
        DiscreteValue = 1,
        Rate = 2,
        PassFail = 3,
        Percentage = 4
    }

    public partial class ObservationTypeCollectionMethodDiscreteValue : ObservationTypeCollectionMethod
    {
        private ObservationTypeCollectionMethodDiscreteValue(int observationTypeCollectionMethodID, string observationTypeCollectionMethodName, string observationTypeCollectionMethodDisplayName, int sortOrder) : base(observationTypeCollectionMethodID, observationTypeCollectionMethodName, observationTypeCollectionMethodDisplayName, sortOrder) {}
        public static readonly ObservationTypeCollectionMethodDiscreteValue Instance = new ObservationTypeCollectionMethodDiscreteValue(1, @"DiscreteValue", @"Measure one or many discrete values", 10);
    }

    public partial class ObservationTypeCollectionMethodRate : ObservationTypeCollectionMethod
    {
        private ObservationTypeCollectionMethodRate(int observationTypeCollectionMethodID, string observationTypeCollectionMethodName, string observationTypeCollectionMethodDisplayName, int sortOrder) : base(observationTypeCollectionMethodID, observationTypeCollectionMethodName, observationTypeCollectionMethodDisplayName, sortOrder) {}
        public static readonly ObservationTypeCollectionMethodRate Instance = new ObservationTypeCollectionMethodRate(2, @"Rate", @"Measure one or many rates as discrete values or time/value pairs", 20);
    }

    public partial class ObservationTypeCollectionMethodPassFail : ObservationTypeCollectionMethod
    {
        private ObservationTypeCollectionMethodPassFail(int observationTypeCollectionMethodID, string observationTypeCollectionMethodName, string observationTypeCollectionMethodDisplayName, int sortOrder) : base(observationTypeCollectionMethodID, observationTypeCollectionMethodName, observationTypeCollectionMethodDisplayName, sortOrder) {}
        public static readonly ObservationTypeCollectionMethodPassFail Instance = new ObservationTypeCollectionMethodPassFail(3, @"PassFail", @"Record Observation as Pass/Fail", 30);
    }

    public partial class ObservationTypeCollectionMethodPercentage : ObservationTypeCollectionMethod
    {
        private ObservationTypeCollectionMethodPercentage(int observationTypeCollectionMethodID, string observationTypeCollectionMethodName, string observationTypeCollectionMethodDisplayName, int sortOrder) : base(observationTypeCollectionMethodID, observationTypeCollectionMethodName, observationTypeCollectionMethodDisplayName, sortOrder) {}
        public static readonly ObservationTypeCollectionMethodPercentage Instance = new ObservationTypeCollectionMethodPercentage(4, @"Percentage", @"Measure one or many percent values", 40);
    }
}