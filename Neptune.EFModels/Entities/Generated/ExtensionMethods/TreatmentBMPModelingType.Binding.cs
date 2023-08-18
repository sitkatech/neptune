//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPModelingType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class TreatmentBMPModelingType : IHavePrimaryKey
    {
        public static readonly TreatmentBMPModelingTypeBioinfiltrationBioretentionWithRaisedUnderdrain BioinfiltrationBioretentionWithRaisedUnderdrain = Neptune.EFModels.Entities.TreatmentBMPModelingTypeBioinfiltrationBioretentionWithRaisedUnderdrain.Instance;
        public static readonly TreatmentBMPModelingTypeBioretentionWithNoUnderdrain BioretentionWithNoUnderdrain = Neptune.EFModels.Entities.TreatmentBMPModelingTypeBioretentionWithNoUnderdrain.Instance;
        public static readonly TreatmentBMPModelingTypeBioretentionWithUnderdrainAndImperviousLiner BioretentionWithUnderdrainAndImperviousLiner = Neptune.EFModels.Entities.TreatmentBMPModelingTypeBioretentionWithUnderdrainAndImperviousLiner.Instance;
        public static readonly TreatmentBMPModelingTypeCisternsForHarvestAndUse CisternsForHarvestAndUse = Neptune.EFModels.Entities.TreatmentBMPModelingTypeCisternsForHarvestAndUse.Instance;
        public static readonly TreatmentBMPModelingTypeConstructedWetland ConstructedWetland = Neptune.EFModels.Entities.TreatmentBMPModelingTypeConstructedWetland.Instance;
        public static readonly TreatmentBMPModelingTypeDryExtendedDetentionBasin DryExtendedDetentionBasin = Neptune.EFModels.Entities.TreatmentBMPModelingTypeDryExtendedDetentionBasin.Instance;
        public static readonly TreatmentBMPModelingTypeDryWeatherTreatmentSystems DryWeatherTreatmentSystems = Neptune.EFModels.Entities.TreatmentBMPModelingTypeDryWeatherTreatmentSystems.Instance;
        public static readonly TreatmentBMPModelingTypeDrywell Drywell = Neptune.EFModels.Entities.TreatmentBMPModelingTypeDrywell.Instance;
        public static readonly TreatmentBMPModelingTypeFlowDurationControlBasin FlowDurationControlBasin = Neptune.EFModels.Entities.TreatmentBMPModelingTypeFlowDurationControlBasin.Instance;
        public static readonly TreatmentBMPModelingTypeFlowDurationControlTank FlowDurationControlTank = Neptune.EFModels.Entities.TreatmentBMPModelingTypeFlowDurationControlTank.Instance;
        public static readonly TreatmentBMPModelingTypeHydrodynamicSeparator HydrodynamicSeparator = Neptune.EFModels.Entities.TreatmentBMPModelingTypeHydrodynamicSeparator.Instance;
        public static readonly TreatmentBMPModelingTypeInfiltrationBasin InfiltrationBasin = Neptune.EFModels.Entities.TreatmentBMPModelingTypeInfiltrationBasin.Instance;
        public static readonly TreatmentBMPModelingTypeInfiltrationTrench InfiltrationTrench = Neptune.EFModels.Entities.TreatmentBMPModelingTypeInfiltrationTrench.Instance;
        public static readonly TreatmentBMPModelingTypeLowFlowDiversions LowFlowDiversions = Neptune.EFModels.Entities.TreatmentBMPModelingTypeLowFlowDiversions.Instance;
        public static readonly TreatmentBMPModelingTypePermeablePavement PermeablePavement = Neptune.EFModels.Entities.TreatmentBMPModelingTypePermeablePavement.Instance;
        public static readonly TreatmentBMPModelingTypeProprietaryBiotreatment ProprietaryBiotreatment = Neptune.EFModels.Entities.TreatmentBMPModelingTypeProprietaryBiotreatment.Instance;
        public static readonly TreatmentBMPModelingTypeProprietaryTreatmentControl ProprietaryTreatmentControl = Neptune.EFModels.Entities.TreatmentBMPModelingTypeProprietaryTreatmentControl.Instance;
        public static readonly TreatmentBMPModelingTypeSandFilters SandFilters = Neptune.EFModels.Entities.TreatmentBMPModelingTypeSandFilters.Instance;
        public static readonly TreatmentBMPModelingTypeUndergroundInfiltration UndergroundInfiltration = Neptune.EFModels.Entities.TreatmentBMPModelingTypeUndergroundInfiltration.Instance;
        public static readonly TreatmentBMPModelingTypeVegetatedFilterStrip VegetatedFilterStrip = Neptune.EFModels.Entities.TreatmentBMPModelingTypeVegetatedFilterStrip.Instance;
        public static readonly TreatmentBMPModelingTypeVegetatedSwale VegetatedSwale = Neptune.EFModels.Entities.TreatmentBMPModelingTypeVegetatedSwale.Instance;
        public static readonly TreatmentBMPModelingTypeWetDetentionBasin WetDetentionBasin = Neptune.EFModels.Entities.TreatmentBMPModelingTypeWetDetentionBasin.Instance;

        public static readonly List<TreatmentBMPModelingType> All;
        public static readonly List<TreatmentBMPModelingTypeDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPModelingType> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPModelingTypeDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentBMPModelingType()
        {
            All = new List<TreatmentBMPModelingType> { BioinfiltrationBioretentionWithRaisedUnderdrain, BioretentionWithNoUnderdrain, BioretentionWithUnderdrainAndImperviousLiner, CisternsForHarvestAndUse, ConstructedWetland, DryExtendedDetentionBasin, DryWeatherTreatmentSystems, Drywell, FlowDurationControlBasin, FlowDurationControlTank, HydrodynamicSeparator, InfiltrationBasin, InfiltrationTrench, LowFlowDiversions, PermeablePavement, ProprietaryBiotreatment, ProprietaryTreatmentControl, SandFilters, UndergroundInfiltration, VegetatedFilterStrip, VegetatedSwale, WetDetentionBasin };
            AllAsDto = new List<TreatmentBMPModelingTypeDto> { BioinfiltrationBioretentionWithRaisedUnderdrain.AsDto(), BioretentionWithNoUnderdrain.AsDto(), BioretentionWithUnderdrainAndImperviousLiner.AsDto(), CisternsForHarvestAndUse.AsDto(), ConstructedWetland.AsDto(), DryExtendedDetentionBasin.AsDto(), DryWeatherTreatmentSystems.AsDto(), Drywell.AsDto(), FlowDurationControlBasin.AsDto(), FlowDurationControlTank.AsDto(), HydrodynamicSeparator.AsDto(), InfiltrationBasin.AsDto(), InfiltrationTrench.AsDto(), LowFlowDiversions.AsDto(), PermeablePavement.AsDto(), ProprietaryBiotreatment.AsDto(), ProprietaryTreatmentControl.AsDto(), SandFilters.AsDto(), UndergroundInfiltration.AsDto(), VegetatedFilterStrip.AsDto(), VegetatedSwale.AsDto(), WetDetentionBasin.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, TreatmentBMPModelingType>(All.ToDictionary(x => x.TreatmentBMPModelingTypeID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, TreatmentBMPModelingTypeDto>(AllAsDto.ToDictionary(x => x.TreatmentBMPModelingTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected TreatmentBMPModelingType(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName)
        {
            TreatmentBMPModelingTypeID = treatmentBMPModelingTypeID;
            TreatmentBMPModelingTypeName = treatmentBMPModelingTypeName;
            TreatmentBMPModelingTypeDisplayName = treatmentBMPModelingTypeDisplayName;
        }

        [Key]
        public int TreatmentBMPModelingTypeID { get; private set; }
        public string TreatmentBMPModelingTypeName { get; private set; }
        public string TreatmentBMPModelingTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPModelingTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(TreatmentBMPModelingType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TreatmentBMPModelingTypeID == TreatmentBMPModelingTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreatmentBMPModelingType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TreatmentBMPModelingTypeID;
        }

        public static bool operator ==(TreatmentBMPModelingType left, TreatmentBMPModelingType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TreatmentBMPModelingType left, TreatmentBMPModelingType right)
        {
            return !Equals(left, right);
        }

        public TreatmentBMPModelingTypeEnum ToEnum => (TreatmentBMPModelingTypeEnum)GetHashCode();

        public static TreatmentBMPModelingType ToType(int enumValue)
        {
            return ToType((TreatmentBMPModelingTypeEnum)enumValue);
        }

        public static TreatmentBMPModelingType ToType(TreatmentBMPModelingTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case TreatmentBMPModelingTypeEnum.BioinfiltrationBioretentionWithRaisedUnderdrain:
                    return BioinfiltrationBioretentionWithRaisedUnderdrain;
                case TreatmentBMPModelingTypeEnum.BioretentionWithNoUnderdrain:
                    return BioretentionWithNoUnderdrain;
                case TreatmentBMPModelingTypeEnum.BioretentionWithUnderdrainAndImperviousLiner:
                    return BioretentionWithUnderdrainAndImperviousLiner;
                case TreatmentBMPModelingTypeEnum.CisternsForHarvestAndUse:
                    return CisternsForHarvestAndUse;
                case TreatmentBMPModelingTypeEnum.ConstructedWetland:
                    return ConstructedWetland;
                case TreatmentBMPModelingTypeEnum.DryExtendedDetentionBasin:
                    return DryExtendedDetentionBasin;
                case TreatmentBMPModelingTypeEnum.DryWeatherTreatmentSystems:
                    return DryWeatherTreatmentSystems;
                case TreatmentBMPModelingTypeEnum.Drywell:
                    return Drywell;
                case TreatmentBMPModelingTypeEnum.FlowDurationControlBasin:
                    return FlowDurationControlBasin;
                case TreatmentBMPModelingTypeEnum.FlowDurationControlTank:
                    return FlowDurationControlTank;
                case TreatmentBMPModelingTypeEnum.HydrodynamicSeparator:
                    return HydrodynamicSeparator;
                case TreatmentBMPModelingTypeEnum.InfiltrationBasin:
                    return InfiltrationBasin;
                case TreatmentBMPModelingTypeEnum.InfiltrationTrench:
                    return InfiltrationTrench;
                case TreatmentBMPModelingTypeEnum.LowFlowDiversions:
                    return LowFlowDiversions;
                case TreatmentBMPModelingTypeEnum.PermeablePavement:
                    return PermeablePavement;
                case TreatmentBMPModelingTypeEnum.ProprietaryBiotreatment:
                    return ProprietaryBiotreatment;
                case TreatmentBMPModelingTypeEnum.ProprietaryTreatmentControl:
                    return ProprietaryTreatmentControl;
                case TreatmentBMPModelingTypeEnum.SandFilters:
                    return SandFilters;
                case TreatmentBMPModelingTypeEnum.UndergroundInfiltration:
                    return UndergroundInfiltration;
                case TreatmentBMPModelingTypeEnum.VegetatedFilterStrip:
                    return VegetatedFilterStrip;
                case TreatmentBMPModelingTypeEnum.VegetatedSwale:
                    return VegetatedSwale;
                case TreatmentBMPModelingTypeEnum.WetDetentionBasin:
                    return WetDetentionBasin;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum TreatmentBMPModelingTypeEnum
    {
        BioinfiltrationBioretentionWithRaisedUnderdrain = 1,
        BioretentionWithNoUnderdrain = 2,
        BioretentionWithUnderdrainAndImperviousLiner = 3,
        CisternsForHarvestAndUse = 4,
        ConstructedWetland = 5,
        DryExtendedDetentionBasin = 6,
        DryWeatherTreatmentSystems = 7,
        Drywell = 8,
        FlowDurationControlBasin = 9,
        FlowDurationControlTank = 10,
        HydrodynamicSeparator = 11,
        InfiltrationBasin = 12,
        InfiltrationTrench = 13,
        LowFlowDiversions = 14,
        PermeablePavement = 15,
        ProprietaryBiotreatment = 16,
        ProprietaryTreatmentControl = 17,
        SandFilters = 18,
        UndergroundInfiltration = 19,
        VegetatedFilterStrip = 20,
        VegetatedSwale = 21,
        WetDetentionBasin = 22
    }

    public partial class TreatmentBMPModelingTypeBioinfiltrationBioretentionWithRaisedUnderdrain : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeBioinfiltrationBioretentionWithRaisedUnderdrain(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeBioinfiltrationBioretentionWithRaisedUnderdrain Instance = new TreatmentBMPModelingTypeBioinfiltrationBioretentionWithRaisedUnderdrain(1, @"BioinfiltrationBioretentionWithRaisedUnderdrain", @"Bioinfiltration (bioretention with raised underdrain)");
    }

    public partial class TreatmentBMPModelingTypeBioretentionWithNoUnderdrain : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeBioretentionWithNoUnderdrain(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeBioretentionWithNoUnderdrain Instance = new TreatmentBMPModelingTypeBioretentionWithNoUnderdrain(2, @"BioretentionWithNoUnderdrain", @"Bioretention with no Underdrain");
    }

    public partial class TreatmentBMPModelingTypeBioretentionWithUnderdrainAndImperviousLiner : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeBioretentionWithUnderdrainAndImperviousLiner(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeBioretentionWithUnderdrainAndImperviousLiner Instance = new TreatmentBMPModelingTypeBioretentionWithUnderdrainAndImperviousLiner(3, @"BioretentionWithUnderdrainAndImperviousLiner", @"Bioretention with Underdrain and Impervious Liner");
    }

    public partial class TreatmentBMPModelingTypeCisternsForHarvestAndUse : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeCisternsForHarvestAndUse(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeCisternsForHarvestAndUse Instance = new TreatmentBMPModelingTypeCisternsForHarvestAndUse(4, @"CisternsForHarvestAndUse", @"Cisterns for Harvest and Use");
    }

    public partial class TreatmentBMPModelingTypeConstructedWetland : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeConstructedWetland(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeConstructedWetland Instance = new TreatmentBMPModelingTypeConstructedWetland(5, @"ConstructedWetland", @"Constructed Wetland");
    }

    public partial class TreatmentBMPModelingTypeDryExtendedDetentionBasin : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeDryExtendedDetentionBasin(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeDryExtendedDetentionBasin Instance = new TreatmentBMPModelingTypeDryExtendedDetentionBasin(6, @"DryExtendedDetentionBasin", @"Dry Extended Detention Basin");
    }

    public partial class TreatmentBMPModelingTypeDryWeatherTreatmentSystems : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeDryWeatherTreatmentSystems(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeDryWeatherTreatmentSystems Instance = new TreatmentBMPModelingTypeDryWeatherTreatmentSystems(7, @"DryWeatherTreatmentSystems", @"Dry Weather Treatment Systems");
    }

    public partial class TreatmentBMPModelingTypeDrywell : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeDrywell(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeDrywell Instance = new TreatmentBMPModelingTypeDrywell(8, @"Drywell", @"Drywell");
    }

    public partial class TreatmentBMPModelingTypeFlowDurationControlBasin : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeFlowDurationControlBasin(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeFlowDurationControlBasin Instance = new TreatmentBMPModelingTypeFlowDurationControlBasin(9, @"FlowDurationControlBasin", @"Flow Duration Control Basin");
    }

    public partial class TreatmentBMPModelingTypeFlowDurationControlTank : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeFlowDurationControlTank(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeFlowDurationControlTank Instance = new TreatmentBMPModelingTypeFlowDurationControlTank(10, @"FlowDurationControlTank", @"Flow Duration Control Tank");
    }

    public partial class TreatmentBMPModelingTypeHydrodynamicSeparator : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeHydrodynamicSeparator(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeHydrodynamicSeparator Instance = new TreatmentBMPModelingTypeHydrodynamicSeparator(11, @"HydrodynamicSeparator", @"Hydrodynamic Separator");
    }

    public partial class TreatmentBMPModelingTypeInfiltrationBasin : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeInfiltrationBasin(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeInfiltrationBasin Instance = new TreatmentBMPModelingTypeInfiltrationBasin(12, @"InfiltrationBasin", @"Infiltration Basin");
    }

    public partial class TreatmentBMPModelingTypeInfiltrationTrench : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeInfiltrationTrench(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeInfiltrationTrench Instance = new TreatmentBMPModelingTypeInfiltrationTrench(13, @"InfiltrationTrench", @"Infiltration Trench");
    }

    public partial class TreatmentBMPModelingTypeLowFlowDiversions : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeLowFlowDiversions(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeLowFlowDiversions Instance = new TreatmentBMPModelingTypeLowFlowDiversions(14, @"LowFlowDiversions", @"Low Flow Diversions");
    }

    public partial class TreatmentBMPModelingTypePermeablePavement : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypePermeablePavement(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypePermeablePavement Instance = new TreatmentBMPModelingTypePermeablePavement(15, @"PermeablePavement", @"Permeable Pavement");
    }

    public partial class TreatmentBMPModelingTypeProprietaryBiotreatment : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeProprietaryBiotreatment(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeProprietaryBiotreatment Instance = new TreatmentBMPModelingTypeProprietaryBiotreatment(16, @"ProprietaryBiotreatment", @"Proprietary Biotreatment");
    }

    public partial class TreatmentBMPModelingTypeProprietaryTreatmentControl : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeProprietaryTreatmentControl(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeProprietaryTreatmentControl Instance = new TreatmentBMPModelingTypeProprietaryTreatmentControl(17, @"ProprietaryTreatmentControl", @"Proprietary Treatment Control");
    }

    public partial class TreatmentBMPModelingTypeSandFilters : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeSandFilters(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeSandFilters Instance = new TreatmentBMPModelingTypeSandFilters(18, @"SandFilters", @"Sand Filters");
    }

    public partial class TreatmentBMPModelingTypeUndergroundInfiltration : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeUndergroundInfiltration(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeUndergroundInfiltration Instance = new TreatmentBMPModelingTypeUndergroundInfiltration(19, @"UndergroundInfiltration", @"Underground Infiltration");
    }

    public partial class TreatmentBMPModelingTypeVegetatedFilterStrip : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeVegetatedFilterStrip(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeVegetatedFilterStrip Instance = new TreatmentBMPModelingTypeVegetatedFilterStrip(20, @"VegetatedFilterStrip", @"Vegetated Filter Strip");
    }

    public partial class TreatmentBMPModelingTypeVegetatedSwale : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeVegetatedSwale(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeVegetatedSwale Instance = new TreatmentBMPModelingTypeVegetatedSwale(21, @"VegetatedSwale", @"Vegetated Swale");
    }

    public partial class TreatmentBMPModelingTypeWetDetentionBasin : TreatmentBMPModelingType
    {
        private TreatmentBMPModelingTypeWetDetentionBasin(int treatmentBMPModelingTypeID, string treatmentBMPModelingTypeName, string treatmentBMPModelingTypeDisplayName) : base(treatmentBMPModelingTypeID, treatmentBMPModelingTypeName, treatmentBMPModelingTypeDisplayName) {}
        public static readonly TreatmentBMPModelingTypeWetDetentionBasin Instance = new TreatmentBMPModelingTypeWetDetentionBasin(22, @"WetDetentionBasin", @"Wet Detention Basin");
    }
}