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
        public static readonly TreatmentBMPObservationDetailTypeInlet Inlet = TreatmentBMPObservationDetailTypeInlet.Instance;
        public static readonly TreatmentBMPObservationDetailTypeOutlet Outlet = TreatmentBMPObservationDetailTypeOutlet.Instance;
        public static readonly TreatmentBMPObservationDetailTypeStaffPlate StaffPlate = TreatmentBMPObservationDetailTypeStaffPlate.Instance;
        public static readonly TreatmentBMPObservationDetailTypeVaultCapacityStadiaRod VaultCapacityStadiaRod = TreatmentBMPObservationDetailTypeVaultCapacityStadiaRod.Instance;
        public static readonly TreatmentBMPObservationDetailTypeSedimentTrapCapacityStadiaRod SedimentTrapCapacityStadiaRod = TreatmentBMPObservationDetailTypeSedimentTrapCapacityStadiaRod.Instance;
        public static readonly TreatmentBMPObservationDetailTypeDurationOfInfiltration DurationOfInfiltration = TreatmentBMPObservationDetailTypeDurationOfInfiltration.Instance;
        public static readonly TreatmentBMPObservationDetailTypeConstantHeadPermeameter ConstantHeadPermeameter = TreatmentBMPObservationDetailTypeConstantHeadPermeameter.Instance;
        public static readonly TreatmentBMPObservationDetailTypeInfiltrometer Infiltrometer = TreatmentBMPObservationDetailTypeInfiltrometer.Instance;
        public static readonly TreatmentBMPObservationDetailTypeUserDefinedInfiltrationMeasurement UserDefinedInfiltrationMeasurement = TreatmentBMPObservationDetailTypeUserDefinedInfiltrationMeasurement.Instance;
        public static readonly TreatmentBMPObservationDetailTypeStandingWater StandingWater = TreatmentBMPObservationDetailTypeStandingWater.Instance;
        public static readonly TreatmentBMPObservationDetailTypeVegetativeCoverWetlandAndRiparianSpecies VegetativeCoverWetlandAndRiparianSpecies = TreatmentBMPObservationDetailTypeVegetativeCoverWetlandAndRiparianSpecies.Instance;
        public static readonly TreatmentBMPObservationDetailTypeVegetativeCoverTreeSpecies VegetativeCoverTreeSpecies = TreatmentBMPObservationDetailTypeVegetativeCoverTreeSpecies.Instance;
        public static readonly TreatmentBMPObservationDetailTypeVegetativeCoverGrassSpecies VegetativeCoverGrassSpecies = TreatmentBMPObservationDetailTypeVegetativeCoverGrassSpecies.Instance;
        public static readonly TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverWetlandAndRiparianSpecies WetBasinVegetativeCoverWetlandAndRiparianSpecies = TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverWetlandAndRiparianSpecies.Instance;
        public static readonly TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverTreeSpecies WetBasinVegetativeCoverTreeSpecies = TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverTreeSpecies.Instance;
        public static readonly TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverGrassSpecies WetBasinVegetativeCoverGrassSpecies = TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverGrassSpecies.Instance;
        public static readonly TreatmentBMPObservationDetailTypeInstallation Installation = TreatmentBMPObservationDetailTypeInstallation.Instance;

        public static readonly List<TreatmentBMPObservationDetailType> All;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPObservationDetailType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentBMPObservationDetailType()
        {
            All = new List<TreatmentBMPObservationDetailType> { Inlet, Outlet, StaffPlate, VaultCapacityStadiaRod, SedimentTrapCapacityStadiaRod, DurationOfInfiltration, ConstantHeadPermeameter, Infiltrometer, UserDefinedInfiltrationMeasurement, StandingWater, VegetativeCoverWetlandAndRiparianSpecies, VegetativeCoverTreeSpecies, VegetativeCoverGrassSpecies, WetBasinVegetativeCoverWetlandAndRiparianSpecies, WetBasinVegetativeCoverTreeSpecies, WetBasinVegetativeCoverGrassSpecies, Installation };
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
                case TreatmentBMPObservationDetailTypeEnum.ConstantHeadPermeameter:
                    return ConstantHeadPermeameter;
                case TreatmentBMPObservationDetailTypeEnum.DurationOfInfiltration:
                    return DurationOfInfiltration;
                case TreatmentBMPObservationDetailTypeEnum.Infiltrometer:
                    return Infiltrometer;
                case TreatmentBMPObservationDetailTypeEnum.Inlet:
                    return Inlet;
                case TreatmentBMPObservationDetailTypeEnum.Installation:
                    return Installation;
                case TreatmentBMPObservationDetailTypeEnum.Outlet:
                    return Outlet;
                case TreatmentBMPObservationDetailTypeEnum.SedimentTrapCapacityStadiaRod:
                    return SedimentTrapCapacityStadiaRod;
                case TreatmentBMPObservationDetailTypeEnum.StaffPlate:
                    return StaffPlate;
                case TreatmentBMPObservationDetailTypeEnum.StandingWater:
                    return StandingWater;
                case TreatmentBMPObservationDetailTypeEnum.UserDefinedInfiltrationMeasurement:
                    return UserDefinedInfiltrationMeasurement;
                case TreatmentBMPObservationDetailTypeEnum.VaultCapacityStadiaRod:
                    return VaultCapacityStadiaRod;
                case TreatmentBMPObservationDetailTypeEnum.VegetativeCoverGrassSpecies:
                    return VegetativeCoverGrassSpecies;
                case TreatmentBMPObservationDetailTypeEnum.VegetativeCoverTreeSpecies:
                    return VegetativeCoverTreeSpecies;
                case TreatmentBMPObservationDetailTypeEnum.VegetativeCoverWetlandAndRiparianSpecies:
                    return VegetativeCoverWetlandAndRiparianSpecies;
                case TreatmentBMPObservationDetailTypeEnum.WetBasinVegetativeCoverGrassSpecies:
                    return WetBasinVegetativeCoverGrassSpecies;
                case TreatmentBMPObservationDetailTypeEnum.WetBasinVegetativeCoverTreeSpecies:
                    return WetBasinVegetativeCoverTreeSpecies;
                case TreatmentBMPObservationDetailTypeEnum.WetBasinVegetativeCoverWetlandAndRiparianSpecies:
                    return WetBasinVegetativeCoverWetlandAndRiparianSpecies;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TreatmentBMPObservationDetailTypeEnum
    {
        Inlet = 1,
        Outlet = 2,
        StaffPlate = 3,
        VaultCapacityStadiaRod = 4,
        SedimentTrapCapacityStadiaRod = 5,
        DurationOfInfiltration = 6,
        ConstantHeadPermeameter = 7,
        Infiltrometer = 8,
        UserDefinedInfiltrationMeasurement = 9,
        StandingWater = 10,
        VegetativeCoverWetlandAndRiparianSpecies = 11,
        VegetativeCoverTreeSpecies = 12,
        VegetativeCoverGrassSpecies = 13,
        WetBasinVegetativeCoverWetlandAndRiparianSpecies = 14,
        WetBasinVegetativeCoverTreeSpecies = 15,
        WetBasinVegetativeCoverGrassSpecies = 16,
        Installation = 17
    }

    public partial class TreatmentBMPObservationDetailTypeInlet : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeInlet(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeInlet Instance = new TreatmentBMPObservationDetailTypeInlet(1, @"Inlet", @"Inlet", 10, 10);
    }

    public partial class TreatmentBMPObservationDetailTypeOutlet : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeOutlet(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeOutlet Instance = new TreatmentBMPObservationDetailTypeOutlet(2, @"Outlet", @"Outlet", 10, 20);
    }

    public partial class TreatmentBMPObservationDetailTypeStaffPlate : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeStaffPlate(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeStaffPlate Instance = new TreatmentBMPObservationDetailTypeStaffPlate(3, @"StaffPlate", @"Staff Plate", 3, 30);
    }

    public partial class TreatmentBMPObservationDetailTypeVaultCapacityStadiaRod : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeVaultCapacityStadiaRod(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeVaultCapacityStadiaRod Instance = new TreatmentBMPObservationDetailTypeVaultCapacityStadiaRod(4, @"VaultCapacityStadiaRod", @"Stadia Rod", 4, 40);
    }

    public partial class TreatmentBMPObservationDetailTypeSedimentTrapCapacityStadiaRod : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeSedimentTrapCapacityStadiaRod(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeSedimentTrapCapacityStadiaRod Instance = new TreatmentBMPObservationDetailTypeSedimentTrapCapacityStadiaRod(5, @"SedimentTrapCapacityStadiaRod", @"Stadia Rod", 8, 50);
    }

    public partial class TreatmentBMPObservationDetailTypeDurationOfInfiltration : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeDurationOfInfiltration(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeDurationOfInfiltration Instance = new TreatmentBMPObservationDetailTypeDurationOfInfiltration(6, @"DurationOfInfiltration", @"Duration of Infiltration", 6, 60);
    }

    public partial class TreatmentBMPObservationDetailTypeConstantHeadPermeameter : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeConstantHeadPermeameter(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeConstantHeadPermeameter Instance = new TreatmentBMPObservationDetailTypeConstantHeadPermeameter(7, @"ConstantHeadPermeameter", @"Constant Head Permeameter (CHP)", 1, 70);
    }

    public partial class TreatmentBMPObservationDetailTypeInfiltrometer : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeInfiltrometer(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeInfiltrometer Instance = new TreatmentBMPObservationDetailTypeInfiltrometer(8, @"Infiltrometer", @"Infiltrometer", 1, 80);
    }

    public partial class TreatmentBMPObservationDetailTypeUserDefinedInfiltrationMeasurement : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeUserDefinedInfiltrationMeasurement(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeUserDefinedInfiltrationMeasurement Instance = new TreatmentBMPObservationDetailTypeUserDefinedInfiltrationMeasurement(9, @"UserDefinedInfiltrationMeasurement", @"User Defined Infiltration Measurement", 1, 90);
    }

    public partial class TreatmentBMPObservationDetailTypeStandingWater : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeStandingWater(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeStandingWater Instance = new TreatmentBMPObservationDetailTypeStandingWater(10, @"StandingWater", @"Standing Water", 5, 100);
    }

    public partial class TreatmentBMPObservationDetailTypeVegetativeCoverWetlandAndRiparianSpecies : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeVegetativeCoverWetlandAndRiparianSpecies(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeVegetativeCoverWetlandAndRiparianSpecies Instance = new TreatmentBMPObservationDetailTypeVegetativeCoverWetlandAndRiparianSpecies(11, @"VegetativeCoverWetlandAndRiparianSpecies", @"Wetland & Riparian Species", 2, 110);
    }

    public partial class TreatmentBMPObservationDetailTypeVegetativeCoverTreeSpecies : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeVegetativeCoverTreeSpecies(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeVegetativeCoverTreeSpecies Instance = new TreatmentBMPObservationDetailTypeVegetativeCoverTreeSpecies(12, @"VegetativeCoverTreeSpecies", @"Tree Species", 2, 120);
    }

    public partial class TreatmentBMPObservationDetailTypeVegetativeCoverGrassSpecies : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeVegetativeCoverGrassSpecies(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeVegetativeCoverGrassSpecies Instance = new TreatmentBMPObservationDetailTypeVegetativeCoverGrassSpecies(13, @"VegetativeCoverGrassSpecies", @"Grass Species", 2, 130);
    }

    public partial class TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverWetlandAndRiparianSpecies : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverWetlandAndRiparianSpecies(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverWetlandAndRiparianSpecies Instance = new TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverWetlandAndRiparianSpecies(14, @"WetBasinVegetativeCoverWetlandAndRiparianSpecies", @"Wetland & Riparian Species", 9, 140);
    }

    public partial class TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverTreeSpecies : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverTreeSpecies(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverTreeSpecies Instance = new TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverTreeSpecies(15, @"WetBasinVegetativeCoverTreeSpecies", @"Tree Species", 9, 150);
    }

    public partial class TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverGrassSpecies : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverGrassSpecies(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverGrassSpecies Instance = new TreatmentBMPObservationDetailTypeWetBasinVegetativeCoverGrassSpecies(16, @"WetBasinVegetativeCoverGrassSpecies", @"Grass Species", 9, 160);
    }

    public partial class TreatmentBMPObservationDetailTypeInstallation : TreatmentBMPObservationDetailType
    {
        private TreatmentBMPObservationDetailTypeInstallation(int treatmentBMPObservationDetailTypeID, string treatmentBMPObservationDetailTypeName, string treatmentBMPObservationDetailTypeDisplayName, int observationTypeID, int sortOrder) : base(treatmentBMPObservationDetailTypeID, treatmentBMPObservationDetailTypeName, treatmentBMPObservationDetailTypeDisplayName, observationTypeID, sortOrder) {}
        public static readonly TreatmentBMPObservationDetailTypeInstallation Instance = new TreatmentBMPObservationDetailTypeInstallation(17, @"Installation", @"Installation", 11, 170);
    }
}