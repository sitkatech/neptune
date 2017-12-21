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
        public static readonly ObservationTypeInfiltrationRate InfiltrationRate = ObservationTypeInfiltrationRate.Instance;
        public static readonly ObservationTypeVegetativeCover VegetativeCover = ObservationTypeVegetativeCover.Instance;
        public static readonly ObservationTypeMaterialAccumulation MaterialAccumulation = ObservationTypeMaterialAccumulation.Instance;
        public static readonly ObservationTypeVaultCapacity VaultCapacity = ObservationTypeVaultCapacity.Instance;
        public static readonly ObservationTypeStandingWater StandingWater = ObservationTypeStandingWater.Instance;
        public static readonly ObservationTypeRunoff Runoff = ObservationTypeRunoff.Instance;
        public static readonly ObservationTypeSedimentTrapCapacity SedimentTrapCapacity = ObservationTypeSedimentTrapCapacity.Instance;
        public static readonly ObservationTypeWetBasinVegetativeCover WetBasinVegetativeCover = ObservationTypeWetBasinVegetativeCover.Instance;
        public static readonly ObservationTypeConveyanceFunction ConveyanceFunction = ObservationTypeConveyanceFunction.Instance;
        public static readonly ObservationTypeInstallation Installation = ObservationTypeInstallation.Instance;

        public static readonly List<ObservationType> All;
        public static readonly ReadOnlyDictionary<int, ObservationType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static ObservationType()
        {
            All = new List<ObservationType> { InfiltrationRate, VegetativeCover, MaterialAccumulation, VaultCapacity, StandingWater, Runoff, SedimentTrapCapacity, WetBasinVegetativeCover, ConveyanceFunction, Installation };
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
                case ObservationTypeEnum.ConveyanceFunction:
                    return ConveyanceFunction;
                case ObservationTypeEnum.InfiltrationRate:
                    return InfiltrationRate;
                case ObservationTypeEnum.Installation:
                    return Installation;
                case ObservationTypeEnum.MaterialAccumulation:
                    return MaterialAccumulation;
                case ObservationTypeEnum.Runoff:
                    return Runoff;
                case ObservationTypeEnum.SedimentTrapCapacity:
                    return SedimentTrapCapacity;
                case ObservationTypeEnum.StandingWater:
                    return StandingWater;
                case ObservationTypeEnum.VaultCapacity:
                    return VaultCapacity;
                case ObservationTypeEnum.VegetativeCover:
                    return VegetativeCover;
                case ObservationTypeEnum.WetBasinVegetativeCover:
                    return WetBasinVegetativeCover;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum ObservationTypeEnum
    {
        InfiltrationRate = 1,
        VegetativeCover = 2,
        MaterialAccumulation = 3,
        VaultCapacity = 4,
        StandingWater = 5,
        Runoff = 6,
        SedimentTrapCapacity = 8,
        WetBasinVegetativeCover = 9,
        ConveyanceFunction = 10,
        Installation = 11
    }

    public partial class ObservationTypeInfiltrationRate : ObservationType
    {
        private ObservationTypeInfiltrationRate(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeInfiltrationRate Instance = new ObservationTypeInfiltrationRate(1, @"InfiltrationRate", @"Infiltration Rate", 10, 20, true, true, false);
    }

    public partial class ObservationTypeVegetativeCover : ObservationType
    {
        private ObservationTypeVegetativeCover(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeVegetativeCover Instance = new ObservationTypeVegetativeCover(2, @"VegetativeCover", @"Vegetative Cover", 20, 11, true, false, false);
    }

    public partial class ObservationTypeMaterialAccumulation : ObservationType
    {
        private ObservationTypeMaterialAccumulation(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeMaterialAccumulation Instance = new ObservationTypeMaterialAccumulation(3, @"MaterialAccumulation", @"Material Accumulation", 30, 19, true, false, false);
    }

    public partial class ObservationTypeVaultCapacity : ObservationType
    {
        private ObservationTypeVaultCapacity(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeVaultCapacity Instance = new ObservationTypeVaultCapacity(4, @"VaultCapacity", @"Vault Capacity", 40, 19, true, true, false);
    }

    public partial class ObservationTypeStandingWater : ObservationType
    {
        private ObservationTypeStandingWater(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeStandingWater Instance = new ObservationTypeStandingWater(5, @"StandingWater", @"Standing Water", 50, 22, false, false, false);
    }

    public partial class ObservationTypeRunoff : ObservationType
    {
        private ObservationTypeRunoff(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeRunoff Instance = new ObservationTypeRunoff(6, @"Runoff", @"Runoff", 60, 21, true, false, false);
    }

    public partial class ObservationTypeSedimentTrapCapacity : ObservationType
    {
        private ObservationTypeSedimentTrapCapacity(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeSedimentTrapCapacity Instance = new ObservationTypeSedimentTrapCapacity(8, @"SedimentTrapCapacity", @"Sediment Trap Capacity", 70, 19, true, false, false);
    }

    public partial class ObservationTypeWetBasinVegetativeCover : ObservationType
    {
        private ObservationTypeWetBasinVegetativeCover(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeWetBasinVegetativeCover Instance = new ObservationTypeWetBasinVegetativeCover(9, @"WetBasinVegetativeCover", @"Vegetative Cover", 90, 11, true, false, true);
    }

    public partial class ObservationTypeConveyanceFunction : ObservationType
    {
        private ObservationTypeConveyanceFunction(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeConveyanceFunction Instance = new ObservationTypeConveyanceFunction(10, @"ConveyanceFunction", @"Conveyance Function", 100, 22, false, false, false);
    }

    public partial class ObservationTypeInstallation : ObservationType
    {
        private ObservationTypeInstallation(int observationTypeID, string observationTypeName, string observationTypeDisplayName, int sortOrder, int measurementUnitTypeID, bool hasBenchmarkAndThreshold, bool thresholdPercentDecline, bool thresholdPercentDeviation) : base(observationTypeID, observationTypeName, observationTypeDisplayName, sortOrder, measurementUnitTypeID, hasBenchmarkAndThreshold, thresholdPercentDecline, thresholdPercentDeviation) {}
        public static readonly ObservationTypeInstallation Instance = new ObservationTypeInstallation(11, @"Installation", @"Installation", 91, 22, false, false, false);
    }
}