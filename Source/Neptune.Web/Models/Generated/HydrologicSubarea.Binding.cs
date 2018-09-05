//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydrologicSubarea]
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
    public abstract partial class HydrologicSubarea : IHavePrimaryKey
    {
        public static readonly HydrologicSubareaLagunaCoastal LagunaCoastal = HydrologicSubareaLagunaCoastal.Instance;
        public static readonly HydrologicSubareaAlisoCreek AlisoCreek = HydrologicSubareaAlisoCreek.Instance;
        public static readonly HydrologicSubareaDanaPointCoastal DanaPointCoastal = HydrologicSubareaDanaPointCoastal.Instance;
        public static readonly HydrologicSubareaSanJuanCreek SanJuanCreek = HydrologicSubareaSanJuanCreek.Instance;
        public static readonly HydrologicSubareaSanClementeCoastal SanClementeCoastal = HydrologicSubareaSanClementeCoastal.Instance;

        public static readonly List<HydrologicSubarea> All;
        public static readonly ReadOnlyDictionary<int, HydrologicSubarea> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static HydrologicSubarea()
        {
            All = new List<HydrologicSubarea> { LagunaCoastal, AlisoCreek, DanaPointCoastal, SanJuanCreek, SanClementeCoastal };
            AllLookupDictionary = new ReadOnlyDictionary<int, HydrologicSubarea>(All.ToDictionary(x => x.HydrologicSubareaID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected HydrologicSubarea(int hydrologicSubareaID, string hydrologicSubareaName, string hydrologicSubareaDisplayName, int sortOrder)
        {
            HydrologicSubareaID = hydrologicSubareaID;
            HydrologicSubareaName = hydrologicSubareaName;
            HydrologicSubareaDisplayName = hydrologicSubareaDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int HydrologicSubareaID { get; private set; }
        public string HydrologicSubareaName { get; private set; }
        public string HydrologicSubareaDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return HydrologicSubareaID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(HydrologicSubarea other)
        {
            if (other == null)
            {
                return false;
            }
            return other.HydrologicSubareaID == HydrologicSubareaID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as HydrologicSubarea);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return HydrologicSubareaID;
        }

        public static bool operator ==(HydrologicSubarea left, HydrologicSubarea right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HydrologicSubarea left, HydrologicSubarea right)
        {
            return !Equals(left, right);
        }

        public HydrologicSubareaEnum ToEnum { get { return (HydrologicSubareaEnum)GetHashCode(); } }

        public static HydrologicSubarea ToType(int enumValue)
        {
            return ToType((HydrologicSubareaEnum)enumValue);
        }

        public static HydrologicSubarea ToType(HydrologicSubareaEnum enumValue)
        {
            switch (enumValue)
            {
                case HydrologicSubareaEnum.AlisoCreek:
                    return AlisoCreek;
                case HydrologicSubareaEnum.DanaPointCoastal:
                    return DanaPointCoastal;
                case HydrologicSubareaEnum.LagunaCoastal:
                    return LagunaCoastal;
                case HydrologicSubareaEnum.SanClementeCoastal:
                    return SanClementeCoastal;
                case HydrologicSubareaEnum.SanJuanCreek:
                    return SanJuanCreek;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum HydrologicSubareaEnum
    {
        LagunaCoastal = 1,
        AlisoCreek = 2,
        DanaPointCoastal = 3,
        SanJuanCreek = 4,
        SanClementeCoastal = 5
    }

    public partial class HydrologicSubareaLagunaCoastal : HydrologicSubarea
    {
        private HydrologicSubareaLagunaCoastal(int hydrologicSubareaID, string hydrologicSubareaName, string hydrologicSubareaDisplayName, int sortOrder) : base(hydrologicSubareaID, hydrologicSubareaName, hydrologicSubareaDisplayName, sortOrder) {}
        public static readonly HydrologicSubareaLagunaCoastal Instance = new HydrologicSubareaLagunaCoastal(1, @"LagunaCoastal", @"Laguna Coastal", 10);
    }

    public partial class HydrologicSubareaAlisoCreek : HydrologicSubarea
    {
        private HydrologicSubareaAlisoCreek(int hydrologicSubareaID, string hydrologicSubareaName, string hydrologicSubareaDisplayName, int sortOrder) : base(hydrologicSubareaID, hydrologicSubareaName, hydrologicSubareaDisplayName, sortOrder) {}
        public static readonly HydrologicSubareaAlisoCreek Instance = new HydrologicSubareaAlisoCreek(2, @"AlisoCreek", @"Aliso Creek", 20);
    }

    public partial class HydrologicSubareaDanaPointCoastal : HydrologicSubarea
    {
        private HydrologicSubareaDanaPointCoastal(int hydrologicSubareaID, string hydrologicSubareaName, string hydrologicSubareaDisplayName, int sortOrder) : base(hydrologicSubareaID, hydrologicSubareaName, hydrologicSubareaDisplayName, sortOrder) {}
        public static readonly HydrologicSubareaDanaPointCoastal Instance = new HydrologicSubareaDanaPointCoastal(3, @"DanaPointCoastal", @"Dana Point Coastal", 30);
    }

    public partial class HydrologicSubareaSanJuanCreek : HydrologicSubarea
    {
        private HydrologicSubareaSanJuanCreek(int hydrologicSubareaID, string hydrologicSubareaName, string hydrologicSubareaDisplayName, int sortOrder) : base(hydrologicSubareaID, hydrologicSubareaName, hydrologicSubareaDisplayName, sortOrder) {}
        public static readonly HydrologicSubareaSanJuanCreek Instance = new HydrologicSubareaSanJuanCreek(4, @"SanJuanCreek", @"San Juan Creek", 40);
    }

    public partial class HydrologicSubareaSanClementeCoastal : HydrologicSubarea
    {
        private HydrologicSubareaSanClementeCoastal(int hydrologicSubareaID, string hydrologicSubareaName, string hydrologicSubareaDisplayName, int sortOrder) : base(hydrologicSubareaID, hydrologicSubareaName, hydrologicSubareaDisplayName, sortOrder) {}
        public static readonly HydrologicSubareaSanClementeCoastal Instance = new HydrologicSubareaSanClementeCoastal(5, @"SanClementeCoastal", @"San Clemente Coastal", 50);
    }
}