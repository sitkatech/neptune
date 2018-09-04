//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPermitTerm]
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
    public abstract partial class WaterQualityManagementPlanPermitTerm : IHavePrimaryKey
    {
        public static readonly WaterQualityManagementPlanPermitTermNorthOC1stTerm NorthOC1stTerm = WaterQualityManagementPlanPermitTermNorthOC1stTerm.Instance;
        public static readonly WaterQualityManagementPlanPermitTermNorthOC2ndTerm NorthOC2ndTerm = WaterQualityManagementPlanPermitTermNorthOC2ndTerm.Instance;
        public static readonly WaterQualityManagementPlanPermitTermNorthOC3rdTerm NorthOC3rdTerm = WaterQualityManagementPlanPermitTermNorthOC3rdTerm.Instance;
        public static readonly WaterQualityManagementPlanPermitTermNorthOC4thTerm NorthOC4thTerm = WaterQualityManagementPlanPermitTermNorthOC4thTerm.Instance;
        public static readonly WaterQualityManagementPlanPermitTermSouthOC1stTerm SouthOC1stTerm = WaterQualityManagementPlanPermitTermSouthOC1stTerm.Instance;
        public static readonly WaterQualityManagementPlanPermitTermSouthOC2ndTerm SouthOC2ndTerm = WaterQualityManagementPlanPermitTermSouthOC2ndTerm.Instance;
        public static readonly WaterQualityManagementPlanPermitTermSouthOC3rdTerm SouthOC3rdTerm = WaterQualityManagementPlanPermitTermSouthOC3rdTerm.Instance;
        public static readonly WaterQualityManagementPlanPermitTermSouthOC4thTerm SouthOC4thTerm = WaterQualityManagementPlanPermitTermSouthOC4thTerm.Instance;
        public static readonly WaterQualityManagementPlanPermitTermSouthOC5thTerm SouthOC5thTerm = WaterQualityManagementPlanPermitTermSouthOC5thTerm.Instance;

        public static readonly List<WaterQualityManagementPlanPermitTerm> All;
        public static readonly ReadOnlyDictionary<int, WaterQualityManagementPlanPermitTerm> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static WaterQualityManagementPlanPermitTerm()
        {
            All = new List<WaterQualityManagementPlanPermitTerm> { NorthOC1stTerm, NorthOC2ndTerm, NorthOC3rdTerm, NorthOC4thTerm, SouthOC1stTerm, SouthOC2ndTerm, SouthOC3rdTerm, SouthOC4thTerm, SouthOC5thTerm };
            AllLookupDictionary = new ReadOnlyDictionary<int, WaterQualityManagementPlanPermitTerm>(All.ToDictionary(x => x.WaterQualityManagementPlanPermitTermID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected WaterQualityManagementPlanPermitTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder)
        {
            WaterQualityManagementPlanPermitTermID = waterQualityManagementPlanPermitTermID;
            WaterQualityManagementPlanPermitTermName = waterQualityManagementPlanPermitTermName;
            WaterQualityManagementPlanPermitTermDisplayName = waterQualityManagementPlanPermitTermDisplayName;
            SortOrder = sortOrder;
        }

        [Key]
        public int WaterQualityManagementPlanPermitTermID { get; private set; }
        public string WaterQualityManagementPlanPermitTermName { get; private set; }
        public string WaterQualityManagementPlanPermitTermDisplayName { get; private set; }
        public int SortOrder { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return WaterQualityManagementPlanPermitTermID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(WaterQualityManagementPlanPermitTerm other)
        {
            if (other == null)
            {
                return false;
            }
            return other.WaterQualityManagementPlanPermitTermID == WaterQualityManagementPlanPermitTermID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as WaterQualityManagementPlanPermitTerm);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return WaterQualityManagementPlanPermitTermID;
        }

        public static bool operator ==(WaterQualityManagementPlanPermitTerm left, WaterQualityManagementPlanPermitTerm right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(WaterQualityManagementPlanPermitTerm left, WaterQualityManagementPlanPermitTerm right)
        {
            return !Equals(left, right);
        }

        public WaterQualityManagementPlanPermitTermEnum ToEnum { get { return (WaterQualityManagementPlanPermitTermEnum)GetHashCode(); } }

        public static WaterQualityManagementPlanPermitTerm ToType(int enumValue)
        {
            return ToType((WaterQualityManagementPlanPermitTermEnum)enumValue);
        }

        public static WaterQualityManagementPlanPermitTerm ToType(WaterQualityManagementPlanPermitTermEnum enumValue)
        {
            switch (enumValue)
            {
                case WaterQualityManagementPlanPermitTermEnum.NorthOC1stTerm:
                    return NorthOC1stTerm;
                case WaterQualityManagementPlanPermitTermEnum.NorthOC2ndTerm:
                    return NorthOC2ndTerm;
                case WaterQualityManagementPlanPermitTermEnum.NorthOC3rdTerm:
                    return NorthOC3rdTerm;
                case WaterQualityManagementPlanPermitTermEnum.NorthOC4thTerm:
                    return NorthOC4thTerm;
                case WaterQualityManagementPlanPermitTermEnum.SouthOC1stTerm:
                    return SouthOC1stTerm;
                case WaterQualityManagementPlanPermitTermEnum.SouthOC2ndTerm:
                    return SouthOC2ndTerm;
                case WaterQualityManagementPlanPermitTermEnum.SouthOC3rdTerm:
                    return SouthOC3rdTerm;
                case WaterQualityManagementPlanPermitTermEnum.SouthOC4thTerm:
                    return SouthOC4thTerm;
                case WaterQualityManagementPlanPermitTermEnum.SouthOC5thTerm:
                    return SouthOC5thTerm;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum WaterQualityManagementPlanPermitTermEnum
    {
        NorthOC1stTerm = 1,
        NorthOC2ndTerm = 2,
        NorthOC3rdTerm = 3,
        NorthOC4thTerm = 4,
        SouthOC1stTerm = 5,
        SouthOC2ndTerm = 6,
        SouthOC3rdTerm = 7,
        SouthOC4thTerm = 8,
        SouthOC5thTerm = 9
    }

    public partial class WaterQualityManagementPlanPermitTermNorthOC1stTerm : WaterQualityManagementPlanPermitTerm
    {
        private WaterQualityManagementPlanPermitTermNorthOC1stTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder) : base(waterQualityManagementPlanPermitTermID, waterQualityManagementPlanPermitTermName, waterQualityManagementPlanPermitTermDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPermitTermNorthOC1stTerm Instance = new WaterQualityManagementPlanPermitTermNorthOC1stTerm(1, @"NorthOC1stTerm", @"North OC 1st Term - 1990", 10);
    }

    public partial class WaterQualityManagementPlanPermitTermNorthOC2ndTerm : WaterQualityManagementPlanPermitTerm
    {
        private WaterQualityManagementPlanPermitTermNorthOC2ndTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder) : base(waterQualityManagementPlanPermitTermID, waterQualityManagementPlanPermitTermName, waterQualityManagementPlanPermitTermDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPermitTermNorthOC2ndTerm Instance = new WaterQualityManagementPlanPermitTermNorthOC2ndTerm(2, @"NorthOC2ndTerm", @"North OC 2nd Term - 1996", 20);
    }

    public partial class WaterQualityManagementPlanPermitTermNorthOC3rdTerm : WaterQualityManagementPlanPermitTerm
    {
        private WaterQualityManagementPlanPermitTermNorthOC3rdTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder) : base(waterQualityManagementPlanPermitTermID, waterQualityManagementPlanPermitTermName, waterQualityManagementPlanPermitTermDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPermitTermNorthOC3rdTerm Instance = new WaterQualityManagementPlanPermitTermNorthOC3rdTerm(3, @"NorthOC3rdTerm", @"North OC 3rd Term – 2002 (2003 Model WQMP)", 30);
    }

    public partial class WaterQualityManagementPlanPermitTermNorthOC4thTerm : WaterQualityManagementPlanPermitTerm
    {
        private WaterQualityManagementPlanPermitTermNorthOC4thTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder) : base(waterQualityManagementPlanPermitTermID, waterQualityManagementPlanPermitTermName, waterQualityManagementPlanPermitTermDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPermitTermNorthOC4thTerm Instance = new WaterQualityManagementPlanPermitTermNorthOC4thTerm(4, @"NorthOC4thTerm", @"North OC 4th Term - 2009 (2011 Model WQMP and TGD)", 40);
    }

    public partial class WaterQualityManagementPlanPermitTermSouthOC1stTerm : WaterQualityManagementPlanPermitTerm
    {
        private WaterQualityManagementPlanPermitTermSouthOC1stTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder) : base(waterQualityManagementPlanPermitTermID, waterQualityManagementPlanPermitTermName, waterQualityManagementPlanPermitTermDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPermitTermSouthOC1stTerm Instance = new WaterQualityManagementPlanPermitTermSouthOC1stTerm(5, @"SouthOC1stTerm", @"South OC 1st Term - 1990", 50);
    }

    public partial class WaterQualityManagementPlanPermitTermSouthOC2ndTerm : WaterQualityManagementPlanPermitTerm
    {
        private WaterQualityManagementPlanPermitTermSouthOC2ndTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder) : base(waterQualityManagementPlanPermitTermID, waterQualityManagementPlanPermitTermName, waterQualityManagementPlanPermitTermDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPermitTermSouthOC2ndTerm Instance = new WaterQualityManagementPlanPermitTermSouthOC2ndTerm(6, @"SouthOC2ndTerm", @"South OC 2nd Term - 1996", 60);
    }

    public partial class WaterQualityManagementPlanPermitTermSouthOC3rdTerm : WaterQualityManagementPlanPermitTerm
    {
        private WaterQualityManagementPlanPermitTermSouthOC3rdTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder) : base(waterQualityManagementPlanPermitTermID, waterQualityManagementPlanPermitTermName, waterQualityManagementPlanPermitTermDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPermitTermSouthOC3rdTerm Instance = new WaterQualityManagementPlanPermitTermSouthOC3rdTerm(7, @"SouthOC3rdTerm", @"South OC 3rd Term – 2002 (2003 Model WQMP)", 70);
    }

    public partial class WaterQualityManagementPlanPermitTermSouthOC4thTerm : WaterQualityManagementPlanPermitTerm
    {
        private WaterQualityManagementPlanPermitTermSouthOC4thTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder) : base(waterQualityManagementPlanPermitTermID, waterQualityManagementPlanPermitTermName, waterQualityManagementPlanPermitTermDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPermitTermSouthOC4thTerm Instance = new WaterQualityManagementPlanPermitTermSouthOC4thTerm(8, @"SouthOC4thTerm", @"South OC 4th Term – 2009 (2013 Model WQMP, TGD, and 2012 HMP)", 80);
    }

    public partial class WaterQualityManagementPlanPermitTermSouthOC5thTerm : WaterQualityManagementPlanPermitTerm
    {
        private WaterQualityManagementPlanPermitTermSouthOC5thTerm(int waterQualityManagementPlanPermitTermID, string waterQualityManagementPlanPermitTermName, string waterQualityManagementPlanPermitTermDisplayName, int sortOrder) : base(waterQualityManagementPlanPermitTermID, waterQualityManagementPlanPermitTermName, waterQualityManagementPlanPermitTermDisplayName, sortOrder) {}
        public static readonly WaterQualityManagementPlanPermitTermSouthOC5thTerm Instance = new WaterQualityManagementPlanPermitTermSouthOC5thTerm(9, @"SouthOC5thTerm", @"South OC 5th Term – 2015 (2017 Model WQMP, TGD, and HMP)", 90);
    }
}