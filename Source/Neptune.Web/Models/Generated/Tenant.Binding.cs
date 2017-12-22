//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Tenant]
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
    public abstract partial class Tenant : IHavePrimaryKey
    {
        public static readonly TenantOCStormwater OCStormwater = TenantOCStormwater.Instance;

        public static readonly List<Tenant> All;
        public static readonly ReadOnlyDictionary<int, Tenant> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static Tenant()
        {
            All = new List<Tenant> { OCStormwater };
            AllLookupDictionary = new ReadOnlyDictionary<int, Tenant>(All.ToDictionary(x => x.TenantID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected Tenant(int tenantID, string tenantName, string tenantDomain, string tenantSubdomain)
        {
            TenantID = tenantID;
            TenantName = tenantName;
            TenantDomain = tenantDomain;
            TenantSubdomain = tenantSubdomain;
        }

        [Key]
        public int TenantID { get; private set; }
        public string TenantName { get; private set; }
        public string TenantDomain { get; private set; }
        public string TenantSubdomain { get; private set; }
        public int PrimaryKey { get { return TenantID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(Tenant other)
        {
            if (other == null)
            {
                return false;
            }
            return other.TenantID == TenantID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as Tenant);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return TenantID;
        }

        public static bool operator ==(Tenant left, Tenant right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Tenant left, Tenant right)
        {
            return !Equals(left, right);
        }

        public TenantEnum ToEnum { get { return (TenantEnum)GetHashCode(); } }

        public static Tenant ToType(int enumValue)
        {
            return ToType((TenantEnum)enumValue);
        }

        public static Tenant ToType(TenantEnum enumValue)
        {
            switch (enumValue)
            {
                case TenantEnum.OCStormwater:
                    return OCStormwater;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum TenantEnum
    {
        OCStormwater = 2
    }

    public partial class TenantOCStormwater : Tenant
    {
        private TenantOCStormwater(int tenantID, string tenantName, string tenantDomain, string tenantSubdomain) : base(tenantID, tenantName, tenantDomain, tenantSubdomain) {}
        public static readonly TenantOCStormwater Instance = new TenantOCStormwater(2, @"OCStormwater", @"ocstormwatertools.org", null);
    }
}