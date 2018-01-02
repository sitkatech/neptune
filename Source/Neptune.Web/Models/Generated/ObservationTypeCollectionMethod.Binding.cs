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
        public static readonly ObservationTypeCollectionMethodMultipleTimeValue MultipleTimeValue = ObservationTypeCollectionMethodMultipleTimeValue.Instance;
        public static readonly ObservationTypeCollectionMethodPassFail PassFail = ObservationTypeCollectionMethodPassFail.Instance;
        public static readonly ObservationTypeCollectionMethodPercentValue PercentValue = ObservationTypeCollectionMethodPercentValue.Instance;

        public static readonly List<ObservationTypeCollectionMethod> All;
        public static readonly ReadOnlyDictionary<int, ObservationTypeCollectionMethod> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationTypeCollectionMethod()
        {
            All = new List<ObservationTypeCollectionMethod> { DiscreteValue, MultipleTimeValue, PassFail, PercentValue };
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
                case ObservationTypeCollectionMethodEnum.MultipleTimeValue:
                    return MultipleTimeValue;
                case ObservationTypeCollectionMethodEnum.PassFail:
                    return PassFail;
                case ObservationTypeCollectionMethodEnum.PercentValue:
                    return PercentValue;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationTypeCollectionMethodEnum
    {
        DiscreteValue = 1,
        MultipleTimeValue = 2,
        PassFail = 3,
        PercentValue = 4
    }

    public partial class ObservationTypeCollectionMethodDiscreteValue : ObservationTypeCollectionMethod
    {
        private ObservationTypeCollectionMethodDiscreteValue(int observationTypeCollectionMethodID, string observationTypeCollectionMethodName, string observationTypeCollectionMethodDisplayName, int sortOrder) : base(observationTypeCollectionMethodID, observationTypeCollectionMethodName, observationTypeCollectionMethodDisplayName, sortOrder) {}
        public static readonly ObservationTypeCollectionMethodDiscreteValue Instance = new ObservationTypeCollectionMethodDiscreteValue(1, @"DiscreteValue", @"Measure one or many discrete values", 10);
    }

    public partial class ObservationTypeCollectionMethodMultipleTimeValue : ObservationTypeCollectionMethod
    {
        private ObservationTypeCollectionMethodMultipleTimeValue(int observationTypeCollectionMethodID, string observationTypeCollectionMethodName, string observationTypeCollectionMethodDisplayName, int sortOrder) : base(observationTypeCollectionMethodID, observationTypeCollectionMethodName, observationTypeCollectionMethodDisplayName, sortOrder) {}
        public static readonly ObservationTypeCollectionMethodMultipleTimeValue Instance = new ObservationTypeCollectionMethodMultipleTimeValue(2, @"MultipleTimeValue", @"Measure one or many time/value pairs", 20);
    }

    public partial class ObservationTypeCollectionMethodPassFail : ObservationTypeCollectionMethod
    {
        private ObservationTypeCollectionMethodPassFail(int observationTypeCollectionMethodID, string observationTypeCollectionMethodName, string observationTypeCollectionMethodDisplayName, int sortOrder) : base(observationTypeCollectionMethodID, observationTypeCollectionMethodName, observationTypeCollectionMethodDisplayName, sortOrder) {}
        public static readonly ObservationTypeCollectionMethodPassFail Instance = new ObservationTypeCollectionMethodPassFail(3, @"PassFail", @"Record Obervation as Pass/Fail", 30);
    }

    public partial class ObservationTypeCollectionMethodPercentValue : ObservationTypeCollectionMethod
    {
        private ObservationTypeCollectionMethodPercentValue(int observationTypeCollectionMethodID, string observationTypeCollectionMethodName, string observationTypeCollectionMethodDisplayName, int sortOrder) : base(observationTypeCollectionMethodID, observationTypeCollectionMethodName, observationTypeCollectionMethodDisplayName, sortOrder) {}
        public static readonly ObservationTypeCollectionMethodPercentValue Instance = new ObservationTypeCollectionMethodPercentValue(4, @"PercentValue", @"Measure a single percent value", 40);
    }
}