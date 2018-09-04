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
        public static readonly HydrologicSubareatest test = HydrologicSubareatest.Instance;

        public static readonly List<HydrologicSubarea> All;
        public static readonly ReadOnlyDictionary<int, HydrologicSubarea> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static HydrologicSubarea()
        {
            All = new List<HydrologicSubarea> { test };
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
                case HydrologicSubareaEnum.test:
                    return test;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum HydrologicSubareaEnum
    {
        test = 1
    }

    public partial class HydrologicSubareatest : HydrologicSubarea
    {
        private HydrologicSubareatest(int hydrologicSubareaID, string hydrologicSubareaName, string hydrologicSubareaDisplayName, int sortOrder) : base(hydrologicSubareaID, hydrologicSubareaName, hydrologicSubareaDisplayName, sortOrder) {}
        public static readonly HydrologicSubareatest Instance = new HydrologicSubareatest(1, @"test", @"testing", 10);
    }
}