//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterRole]
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
    public abstract partial class StormwaterRole : IHavePrimaryKey
    {


        public static readonly List<StormwaterRole> All;
        public static readonly ReadOnlyDictionary<int, StormwaterRole> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static StormwaterRole()
        {
            All = new List<StormwaterRole> {  };
            AllLookupDictionary = new ReadOnlyDictionary<int, StormwaterRole>(All.ToDictionary(x => x.StormwaterRoleID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected StormwaterRole(int stormwaterRoleID, int tenantID, string stormwaterRoleName, string stormwaterRoleDisplayName, string stormwaterRoleDescription)
        {
            StormwaterRoleID = stormwaterRoleID;
            TenantID = tenantID;
            StormwaterRoleName = stormwaterRoleName;
            StormwaterRoleDisplayName = stormwaterRoleDisplayName;
            StormwaterRoleDescription = stormwaterRoleDescription;
        }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        [Key]
        public int StormwaterRoleID { get; private set; }
        public int TenantID { get; private set; }
        public string StormwaterRoleName { get; private set; }
        public string StormwaterRoleDisplayName { get; private set; }
        public string StormwaterRoleDescription { get; private set; }
        public int PrimaryKey { get { return StormwaterRoleID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(StormwaterRole other)
        {
            if (other == null)
            {
                return false;
            }
            return other.StormwaterRoleID == StormwaterRoleID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as StormwaterRole);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return StormwaterRoleID;
        }

        public static bool operator ==(StormwaterRole left, StormwaterRole right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StormwaterRole left, StormwaterRole right)
        {
            return !Equals(left, right);
        }

        public StormwaterRoleEnum ToEnum { get { return (StormwaterRoleEnum)GetHashCode(); } }

        public static StormwaterRole ToType(int enumValue)
        {
            return ToType((StormwaterRoleEnum)enumValue);
        }

        public static StormwaterRole ToType(StormwaterRoleEnum enumValue)
        {
            switch (enumValue)
            {

                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum StormwaterRoleEnum
    {

    }

}