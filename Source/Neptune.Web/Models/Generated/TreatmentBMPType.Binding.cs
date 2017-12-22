//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
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
    public abstract partial class TreatmentBMPType : IHavePrimaryKey
    {
        public static readonly TreatmentBMPTypeDryBasin DryBasin = TreatmentBMPTypeDryBasin.Instance;
        public static readonly TreatmentBMPTypeWetBasin WetBasin = TreatmentBMPTypeWetBasin.Instance;
        public static readonly TreatmentBMPTypeInfiltrationBasin InfiltrationBasin = TreatmentBMPTypeInfiltrationBasin.Instance;
        public static readonly TreatmentBMPTypeTreatmentVault TreatmentVault = TreatmentBMPTypeTreatmentVault.Instance;
        public static readonly TreatmentBMPTypeCartridgeFilter CartridgeFilter = TreatmentBMPTypeCartridgeFilter.Instance;
        public static readonly TreatmentBMPTypeBedFilter BedFilter = TreatmentBMPTypeBedFilter.Instance;
        public static readonly TreatmentBMPTypeSettlingBasin SettlingBasin = TreatmentBMPTypeSettlingBasin.Instance;
        public static readonly TreatmentBMPTypeBioFilter BioFilter = TreatmentBMPTypeBioFilter.Instance;
        public static readonly TreatmentBMPTypeInfiltrationFeature InfiltrationFeature = TreatmentBMPTypeInfiltrationFeature.Instance;
        public static readonly TreatmentBMPTypePorousPavement PorousPavement = TreatmentBMPTypePorousPavement.Instance;
        public static readonly TreatmentBMPTypeSedimentTrap SedimentTrap = TreatmentBMPTypeSedimentTrap.Instance;
        public static readonly TreatmentBMPTypeDropInlet DropInlet = TreatmentBMPTypeDropInlet.Instance;

        public static readonly List<TreatmentBMPType> All;
        public static readonly ReadOnlyDictionary<int, TreatmentBMPType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static TreatmentBMPType()
        {
            All = new List<TreatmentBMPType> { DryBasin, WetBasin, InfiltrationBasin, TreatmentVault, CartridgeFilter, BedFilter, SettlingBasin, BioFilter, InfiltrationFeature, PorousPavement, SedimentTrap, DropInlet };
            AllLookupDictionary = new ReadOnlyDictionary<int, TreatmentBMPType>(All.ToDictionary(x => x.TreatmentBMPTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected TreatmentBMPType(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType)
        {
            TreatmentBMPTypeID = treatmentBMPTypeID;
            TreatmentBMPTypeName = treatmentBMPTypeName;
            TreatmentBMPTypeDisplayName = treatmentBMPTypeDisplayName;
            SortOrder = sortOrder;
            DisplayColor = displayColor;
            GlyphIconClass = glyphIconClass;
            IsDistributedBMPType = isDistributedBMPType;
        }

        [Key]
        public int TreatmentBMPTypeID { get; private set; }
        public string TreatmentBMPTypeName { get; private set; }
        public string TreatmentBMPTypeDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        public string DisplayColor { get; private set; }
        public string GlyphIconClass { get; private set; }
        public bool IsDistributedBMPType { get; private set; }
        public int PrimaryKey { get { return TreatmentBMPTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(TreatmentBMPType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TreatmentBMPTypeID == TreatmentBMPTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as TreatmentBMPType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TreatmentBMPTypeID;
        }

        public static bool operator ==(TreatmentBMPType left, TreatmentBMPType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TreatmentBMPType left, TreatmentBMPType right)
        {
            return !Equals(left, right);
        }

        public TreatmentBMPTypeEnum ToEnum { get { return (TreatmentBMPTypeEnum)GetHashCode(); } }

        public static TreatmentBMPType ToType(int enumValue)
        {
            return ToType((TreatmentBMPTypeEnum)enumValue);
        }

        public static TreatmentBMPType ToType(TreatmentBMPTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case TreatmentBMPTypeEnum.BedFilter:
                    return BedFilter;
                case TreatmentBMPTypeEnum.BioFilter:
                    return BioFilter;
                case TreatmentBMPTypeEnum.CartridgeFilter:
                    return CartridgeFilter;
                case TreatmentBMPTypeEnum.DropInlet:
                    return DropInlet;
                case TreatmentBMPTypeEnum.DryBasin:
                    return DryBasin;
                case TreatmentBMPTypeEnum.InfiltrationBasin:
                    return InfiltrationBasin;
                case TreatmentBMPTypeEnum.InfiltrationFeature:
                    return InfiltrationFeature;
                case TreatmentBMPTypeEnum.PorousPavement:
                    return PorousPavement;
                case TreatmentBMPTypeEnum.SedimentTrap:
                    return SedimentTrap;
                case TreatmentBMPTypeEnum.SettlingBasin:
                    return SettlingBasin;
                case TreatmentBMPTypeEnum.TreatmentVault:
                    return TreatmentVault;
                case TreatmentBMPTypeEnum.WetBasin:
                    return WetBasin;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TreatmentBMPTypeEnum
    {
        DryBasin = 1,
        WetBasin = 2,
        InfiltrationBasin = 3,
        TreatmentVault = 4,
        CartridgeFilter = 5,
        BedFilter = 6,
        SettlingBasin = 7,
        BioFilter = 8,
        InfiltrationFeature = 9,
        PorousPavement = 10,
        SedimentTrap = 11,
        DropInlet = 12
    }

    public partial class TreatmentBMPTypeDryBasin : TreatmentBMPType
    {
        private TreatmentBMPTypeDryBasin(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeDryBasin Instance = new TreatmentBMPTypeDryBasin(1, @"DryBasin", @"Dry Basin", 10, @"#935F59", @"wetland", false);
    }

    public partial class TreatmentBMPTypeWetBasin : TreatmentBMPType
    {
        private TreatmentBMPTypeWetBasin(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeWetBasin Instance = new TreatmentBMPTypeWetBasin(2, @"WetBasin", @"Wet Basin", 20, @"#935F59", @"wetland", false);
    }

    public partial class TreatmentBMPTypeInfiltrationBasin : TreatmentBMPType
    {
        private TreatmentBMPTypeInfiltrationBasin(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeInfiltrationBasin Instance = new TreatmentBMPTypeInfiltrationBasin(3, @"InfiltrationBasin", @"Infiltration Basin", 30, @"#935F59", @"wetland", false);
    }

    public partial class TreatmentBMPTypeTreatmentVault : TreatmentBMPType
    {
        private TreatmentBMPTypeTreatmentVault(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeTreatmentVault Instance = new TreatmentBMPTypeTreatmentVault(4, @"TreatmentVault", @"Treatment Vault", 40, @"#935F59", @"water", false);
    }

    public partial class TreatmentBMPTypeCartridgeFilter : TreatmentBMPType
    {
        private TreatmentBMPTypeCartridgeFilter(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeCartridgeFilter Instance = new TreatmentBMPTypeCartridgeFilter(5, @"CartridgeFilter", @"Cartridge Filter", 50, @"#935F59", @"water", false);
    }

    public partial class TreatmentBMPTypeBedFilter : TreatmentBMPType
    {
        private TreatmentBMPTypeBedFilter(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeBedFilter Instance = new TreatmentBMPTypeBedFilter(6, @"BedFilter", @"Bed Filter", 60, @"#935F59", @"water", false);
    }

    public partial class TreatmentBMPTypeSettlingBasin : TreatmentBMPType
    {
        private TreatmentBMPTypeSettlingBasin(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeSettlingBasin Instance = new TreatmentBMPTypeSettlingBasin(7, @"SettlingBasin", @"Settling Basin", 70, @"#935F59", @"wetland", false);
    }

    public partial class TreatmentBMPTypeBioFilter : TreatmentBMPType
    {
        private TreatmentBMPTypeBioFilter(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeBioFilter Instance = new TreatmentBMPTypeBioFilter(8, @"BioFilter", @"Biofilter", 80, @"#935F59", @"wetland", true);
    }

    public partial class TreatmentBMPTypeInfiltrationFeature : TreatmentBMPType
    {
        private TreatmentBMPTypeInfiltrationFeature(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeInfiltrationFeature Instance = new TreatmentBMPTypeInfiltrationFeature(9, @"InfiltrationFeature", @"Infiltration Feature", 90, @"#935F59", @"water", true);
    }

    public partial class TreatmentBMPTypePorousPavement : TreatmentBMPType
    {
        private TreatmentBMPTypePorousPavement(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypePorousPavement Instance = new TreatmentBMPTypePorousPavement(10, @"PorousPavement", @"Porous Pavement", 100, @"#935F59", @"bicycle", true);
    }

    public partial class TreatmentBMPTypeSedimentTrap : TreatmentBMPType
    {
        private TreatmentBMPTypeSedimentTrap(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeSedimentTrap Instance = new TreatmentBMPTypeSedimentTrap(11, @"SedimentTrap", @"Sediment Trap", 110, @"#935F59", @"water", true);
    }

    public partial class TreatmentBMPTypeDropInlet : TreatmentBMPType
    {
        private TreatmentBMPTypeDropInlet(int treatmentBMPTypeID, string treatmentBMPTypeName, string treatmentBMPTypeDisplayName, int sortOrder, string displayColor, string glyphIconClass, bool isDistributedBMPType) : base(treatmentBMPTypeID, treatmentBMPTypeName, treatmentBMPTypeDisplayName, sortOrder, displayColor, glyphIconClass, isDistributedBMPType) {}
        public static readonly TreatmentBMPTypeDropInlet Instance = new TreatmentBMPTypeDropInlet(12, @"DropInlet", @"Drop Inlet", 120, @"#935F59", @"water", false);
    }
}