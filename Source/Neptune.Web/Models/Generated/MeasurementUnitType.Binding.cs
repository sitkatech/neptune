//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MeasurementUnitType]
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
    public abstract partial class MeasurementUnitType : IHavePrimaryKey
    {
        public static readonly MeasurementUnitTypeAcres Acres = MeasurementUnitTypeAcres.Instance;
        public static readonly MeasurementUnitTypeSquareFeet SquareFeet = MeasurementUnitTypeSquareFeet.Instance;
        public static readonly MeasurementUnitTypeKilogram Kilogram = MeasurementUnitTypeKilogram.Instance;
        public static readonly MeasurementUnitTypeCount Count = MeasurementUnitTypeCount.Instance;
        public static readonly MeasurementUnitTypePercent Percent = MeasurementUnitTypePercent.Instance;
        public static readonly MeasurementUnitTypeMilligamsPerLiter MilligamsPerLiter = MeasurementUnitTypeMilligamsPerLiter.Instance;
        public static readonly MeasurementUnitTypeMeters Meters = MeasurementUnitTypeMeters.Instance;
        public static readonly MeasurementUnitTypeFeet Feet = MeasurementUnitTypeFeet.Instance;
        public static readonly MeasurementUnitTypeInches Inches = MeasurementUnitTypeInches.Instance;
        public static readonly MeasurementUnitTypeInchesPerHour InchesPerHour = MeasurementUnitTypeInchesPerHour.Instance;
        public static readonly MeasurementUnitTypeSeconds Seconds = MeasurementUnitTypeSeconds.Instance;
        public static readonly MeasurementUnitTypePercentDecline PercentDecline = MeasurementUnitTypePercentDecline.Instance;
        public static readonly MeasurementUnitTypePercentIncrease PercentIncrease = MeasurementUnitTypePercentIncrease.Instance;
        public static readonly MeasurementUnitTypePercentDeviation PercentDeviation = MeasurementUnitTypePercentDeviation.Instance;
        public static readonly MeasurementUnitTypeCubicFeet CubicFeet = MeasurementUnitTypeCubicFeet.Instance;
        public static readonly MeasurementUnitTypeGallons Gallons = MeasurementUnitTypeGallons.Instance;
        public static readonly MeasurementUnitTypeMinutes Minutes = MeasurementUnitTypeMinutes.Instance;
        public static readonly MeasurementUnitTypeCubicFeetPerSecond CubicFeetPerSecond = MeasurementUnitTypeCubicFeetPerSecond.Instance;

        public static readonly List<MeasurementUnitType> All;
        public static readonly ReadOnlyDictionary<int, MeasurementUnitType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static MeasurementUnitType()
        {
            All = new List<MeasurementUnitType> { Acres, SquareFeet, Kilogram, Count, Percent, MilligamsPerLiter, Meters, Feet, Inches, InchesPerHour, Seconds, PercentDecline, PercentIncrease, PercentDeviation, CubicFeet, Gallons, Minutes, CubicFeetPerSecond };
            AllLookupDictionary = new ReadOnlyDictionary<int, MeasurementUnitType>(All.ToDictionary(x => x.MeasurementUnitTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected MeasurementUnitType(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel)
        {
            MeasurementUnitTypeID = measurementUnitTypeID;
            MeasurementUnitTypeName = measurementUnitTypeName;
            MeasurementUnitTypeDisplayName = measurementUnitTypeDisplayName;
            LegendDisplayName = legendDisplayName;
            SingularDisplayName = singularDisplayName;
            NumberOfSignificantDigits = numberOfSignificantDigits;
            IncludeSpaceBeforeLegendLabel = includeSpaceBeforeLegendLabel;
        }

        [Key]
        public int MeasurementUnitTypeID { get; private set; }
        public string MeasurementUnitTypeName { get; private set; }
        public string MeasurementUnitTypeDisplayName { get; private set; }
        public string LegendDisplayName { get; private set; }
        public string SingularDisplayName { get; private set; }
        public int NumberOfSignificantDigits { get; private set; }
        public bool IncludeSpaceBeforeLegendLabel { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return MeasurementUnitTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(MeasurementUnitType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.MeasurementUnitTypeID == MeasurementUnitTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as MeasurementUnitType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return MeasurementUnitTypeID;
        }

        public static bool operator ==(MeasurementUnitType left, MeasurementUnitType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MeasurementUnitType left, MeasurementUnitType right)
        {
            return !Equals(left, right);
        }

        public MeasurementUnitTypeEnum ToEnum { get { return (MeasurementUnitTypeEnum)GetHashCode(); } }

        public static MeasurementUnitType ToType(int enumValue)
        {
            return ToType((MeasurementUnitTypeEnum)enumValue);
        }

        public static MeasurementUnitType ToType(MeasurementUnitTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case MeasurementUnitTypeEnum.Acres:
                    return Acres;
                case MeasurementUnitTypeEnum.Count:
                    return Count;
                case MeasurementUnitTypeEnum.CubicFeet:
                    return CubicFeet;
                case MeasurementUnitTypeEnum.CubicFeetPerSecond:
                    return CubicFeetPerSecond;
                case MeasurementUnitTypeEnum.Feet:
                    return Feet;
                case MeasurementUnitTypeEnum.Gallons:
                    return Gallons;
                case MeasurementUnitTypeEnum.Inches:
                    return Inches;
                case MeasurementUnitTypeEnum.InchesPerHour:
                    return InchesPerHour;
                case MeasurementUnitTypeEnum.Kilogram:
                    return Kilogram;
                case MeasurementUnitTypeEnum.Meters:
                    return Meters;
                case MeasurementUnitTypeEnum.MilligamsPerLiter:
                    return MilligamsPerLiter;
                case MeasurementUnitTypeEnum.Minutes:
                    return Minutes;
                case MeasurementUnitTypeEnum.Percent:
                    return Percent;
                case MeasurementUnitTypeEnum.PercentDecline:
                    return PercentDecline;
                case MeasurementUnitTypeEnum.PercentDeviation:
                    return PercentDeviation;
                case MeasurementUnitTypeEnum.PercentIncrease:
                    return PercentIncrease;
                case MeasurementUnitTypeEnum.Seconds:
                    return Seconds;
                case MeasurementUnitTypeEnum.SquareFeet:
                    return SquareFeet;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum MeasurementUnitTypeEnum
    {
        Acres = 1,
        SquareFeet = 2,
        Kilogram = 3,
        Count = 4,
        Percent = 5,
        MilligamsPerLiter = 6,
        Meters = 7,
        Feet = 8,
        Inches = 9,
        InchesPerHour = 10,
        Seconds = 11,
        PercentDecline = 12,
        PercentIncrease = 13,
        PercentDeviation = 14,
        CubicFeet = 15,
        Gallons = 16,
        Minutes = 17,
        CubicFeetPerSecond = 18
    }

    public partial class MeasurementUnitTypeAcres : MeasurementUnitType
    {
        private MeasurementUnitTypeAcres(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeAcres Instance = new MeasurementUnitTypeAcres(1, @"Acres", @"acres", @"acres", @"Acre", 2, true);
    }

    public partial class MeasurementUnitTypeSquareFeet : MeasurementUnitType
    {
        private MeasurementUnitTypeSquareFeet(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeSquareFeet Instance = new MeasurementUnitTypeSquareFeet(2, @"SquareFeet", @"square feet", @"sq ft", @"Square Foot", 2, true);
    }

    public partial class MeasurementUnitTypeKilogram : MeasurementUnitType
    {
        private MeasurementUnitTypeKilogram(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeKilogram Instance = new MeasurementUnitTypeKilogram(3, @"Kilogram", @"kg", @"kg", @"Kilogram", 2, true);
    }

    public partial class MeasurementUnitTypeCount : MeasurementUnitType
    {
        private MeasurementUnitTypeCount(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeCount Instance = new MeasurementUnitTypeCount(4, @"Count", @"count", @"count", @"Each Unit", 0, true);
    }

    public partial class MeasurementUnitTypePercent : MeasurementUnitType
    {
        private MeasurementUnitTypePercent(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypePercent Instance = new MeasurementUnitTypePercent(5, @"Percent", @"%", @"%", @"%", 0, false);
    }

    public partial class MeasurementUnitTypeMilligamsPerLiter : MeasurementUnitType
    {
        private MeasurementUnitTypeMilligamsPerLiter(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeMilligamsPerLiter Instance = new MeasurementUnitTypeMilligamsPerLiter(6, @"MilligamsPerLiter", @"mg/L", @"mg/L", @"Milligram Per Liter", 2, true);
    }

    public partial class MeasurementUnitTypeMeters : MeasurementUnitType
    {
        private MeasurementUnitTypeMeters(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeMeters Instance = new MeasurementUnitTypeMeters(7, @"Meters", @"meters", @"meters", @"Meter", 1, true);
    }

    public partial class MeasurementUnitTypeFeet : MeasurementUnitType
    {
        private MeasurementUnitTypeFeet(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeFeet Instance = new MeasurementUnitTypeFeet(8, @"Feet", @"feet", @"ft", @"Foot", 2, true);
    }

    public partial class MeasurementUnitTypeInches : MeasurementUnitType
    {
        private MeasurementUnitTypeInches(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeInches Instance = new MeasurementUnitTypeInches(9, @"Inches", @"inches", @"in", @"inch", 2, true);
    }

    public partial class MeasurementUnitTypeInchesPerHour : MeasurementUnitType
    {
        private MeasurementUnitTypeInchesPerHour(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeInchesPerHour Instance = new MeasurementUnitTypeInchesPerHour(10, @"InchesPerHour", @"in/hr", @"in/hr", @"Inches Per Hour", 2, true);
    }

    public partial class MeasurementUnitTypeSeconds : MeasurementUnitType
    {
        private MeasurementUnitTypeSeconds(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeSeconds Instance = new MeasurementUnitTypeSeconds(11, @"Seconds", @"seconds", @"s", @"Second", 0, true);
    }

    public partial class MeasurementUnitTypePercentDecline : MeasurementUnitType
    {
        private MeasurementUnitTypePercentDecline(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypePercentDecline Instance = new MeasurementUnitTypePercentDecline(12, @"PercentDecline", @"% decline from benchmark", @"% decline from benchmark", @"% decline from benchmark", 0, false);
    }

    public partial class MeasurementUnitTypePercentIncrease : MeasurementUnitType
    {
        private MeasurementUnitTypePercentIncrease(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypePercentIncrease Instance = new MeasurementUnitTypePercentIncrease(13, @"PercentIncrease", @"% increase from benchmark", @"% increase from benchmark", @"% increase from benchmark", 0, false);
    }

    public partial class MeasurementUnitTypePercentDeviation : MeasurementUnitType
    {
        private MeasurementUnitTypePercentDeviation(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypePercentDeviation Instance = new MeasurementUnitTypePercentDeviation(14, @"PercentDeviation", @"% of benchmark", @"% of benchmark", @"% of benchmark", 0, false);
    }

    public partial class MeasurementUnitTypeCubicFeet : MeasurementUnitType
    {
        private MeasurementUnitTypeCubicFeet(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeCubicFeet Instance = new MeasurementUnitTypeCubicFeet(15, @"Cubic Feet", @"cubic feet", @"cu ft", @"cu ft", 0, true);
    }

    public partial class MeasurementUnitTypeGallons : MeasurementUnitType
    {
        private MeasurementUnitTypeGallons(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeGallons Instance = new MeasurementUnitTypeGallons(16, @"Gallons", @"gallons", @"gallons", @"gallon", 0, true);
    }

    public partial class MeasurementUnitTypeMinutes : MeasurementUnitType
    {
        private MeasurementUnitTypeMinutes(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeMinutes Instance = new MeasurementUnitTypeMinutes(17, @"Minutes", @"minutes", @"minutes", @"minute", 0, true);
    }

    public partial class MeasurementUnitTypeCubicFeetPerSecond : MeasurementUnitType
    {
        private MeasurementUnitTypeCubicFeetPerSecond(int measurementUnitTypeID, string measurementUnitTypeName, string measurementUnitTypeDisplayName, string legendDisplayName, string singularDisplayName, int numberOfSignificantDigits, bool includeSpaceBeforeLegendLabel) : base(measurementUnitTypeID, measurementUnitTypeName, measurementUnitTypeDisplayName, legendDisplayName, singularDisplayName, numberOfSignificantDigits, includeSpaceBeforeLegendLabel) {}
        public static readonly MeasurementUnitTypeCubicFeetPerSecond Instance = new MeasurementUnitTypeCubicFeetPerSecond(18, @"CubicFeetPerSecond", @"cubic feet per second", @"cfs", @"cfs", 0, true);
    }
}