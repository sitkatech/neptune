//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterBreadCrumbEntity]
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
    public abstract partial class StormwaterBreadCrumbEntity : IHavePrimaryKey
    {


        public static readonly List<StormwaterBreadCrumbEntity> All;
        public static readonly ReadOnlyDictionary<int, StormwaterBreadCrumbEntity> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static StormwaterBreadCrumbEntity()
        {
            All = new List<StormwaterBreadCrumbEntity> {  };
            AllLookupDictionary = new ReadOnlyDictionary<int, StormwaterBreadCrumbEntity>(All.ToDictionary(x => x.StormwaterBreadCrumbEntityID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected StormwaterBreadCrumbEntity(int stormwaterBreadCrumbEntityID, int tenantID, string stormwaterBreadCrumbEntityName, string stormwaterBreadCrumbEntityDisplayName, string glyphIconClass, string colorClass)
        {
            StormwaterBreadCrumbEntityID = stormwaterBreadCrumbEntityID;
            TenantID = tenantID;
            StormwaterBreadCrumbEntityName = stormwaterBreadCrumbEntityName;
            StormwaterBreadCrumbEntityDisplayName = stormwaterBreadCrumbEntityDisplayName;
            GlyphIconClass = glyphIconClass;
            ColorClass = colorClass;
        }
        public Tenant Tenant { get { return Tenant.AllLookupDictionary[TenantID]; } }
        [Key]
        public int StormwaterBreadCrumbEntityID { get; private set; }
        public int TenantID { get; private set; }
        public string StormwaterBreadCrumbEntityName { get; private set; }
        public string StormwaterBreadCrumbEntityDisplayName { get; private set; }
        public string GlyphIconClass { get; private set; }
        public string ColorClass { get; private set; }
        public int PrimaryKey { get { return StormwaterBreadCrumbEntityID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(StormwaterBreadCrumbEntity other)
        {
            if (other == null)
            {
                return false;
            }
            return other.StormwaterBreadCrumbEntityID == StormwaterBreadCrumbEntityID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as StormwaterBreadCrumbEntity);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return StormwaterBreadCrumbEntityID;
        }

        public static bool operator ==(StormwaterBreadCrumbEntity left, StormwaterBreadCrumbEntity right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StormwaterBreadCrumbEntity left, StormwaterBreadCrumbEntity right)
        {
            return !Equals(left, right);
        }

        public StormwaterBreadCrumbEntityEnum ToEnum { get { return (StormwaterBreadCrumbEntityEnum)GetHashCode(); } }

        public static StormwaterBreadCrumbEntity ToType(int enumValue)
        {
            return ToType((StormwaterBreadCrumbEntityEnum)enumValue);
        }

        public static StormwaterBreadCrumbEntity ToType(StormwaterBreadCrumbEntityEnum enumValue)
        {
            switch (enumValue)
            {

                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum StormwaterBreadCrumbEntityEnum
    {

    }

}