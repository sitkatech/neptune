//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OperationMonth]
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
    public abstract partial class OperationMonth : IHavePrimaryKey
    {
        public static readonly OperationMonthSummer Summer = OperationMonthSummer.Instance;
        public static readonly OperationMonthWinter Winter = OperationMonthWinter.Instance;
        public static readonly OperationMonthBoth Both = OperationMonthBoth.Instance;

        public static readonly List<OperationMonth> All;
        public static readonly ReadOnlyDictionary<int, OperationMonth> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static OperationMonth()
        {
            All = new List<OperationMonth> { Summer, Winter, Both };
            AllLookupDictionary = new ReadOnlyDictionary<int, OperationMonth>(All.ToDictionary(x => x.OperationMonthID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected OperationMonth(int operationMonthID, string operationMonthName, string operationMonthDisplayName, string operationMonthNereidAlias)
        {
            OperationMonthID = operationMonthID;
            OperationMonthName = operationMonthName;
            OperationMonthDisplayName = operationMonthDisplayName;
            OperationMonthNereidAlias = operationMonthNereidAlias;
        }

        [Key]
        public int OperationMonthID { get; private set; }
        public string OperationMonthName { get; private set; }
        public string OperationMonthDisplayName { get; private set; }
        public string OperationMonthNereidAlias { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return OperationMonthID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(OperationMonth other)
        {
            if (other == null)
            {
                return false;
            }
            return other.OperationMonthID == OperationMonthID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as OperationMonth);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return OperationMonthID;
        }

        public static bool operator ==(OperationMonth left, OperationMonth right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(OperationMonth left, OperationMonth right)
        {
            return !Equals(left, right);
        }

        public OperationMonthEnum ToEnum { get { return (OperationMonthEnum)GetHashCode(); } }

        public static OperationMonth ToType(int enumValue)
        {
            return ToType((OperationMonthEnum)enumValue);
        }

        public static OperationMonth ToType(OperationMonthEnum enumValue)
        {
            switch (enumValue)
            {
                case OperationMonthEnum.Both:
                    return Both;
                case OperationMonthEnum.Summer:
                    return Summer;
                case OperationMonthEnum.Winter:
                    return Winter;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum OperationMonthEnum
    {
        Summer = 1,
        Winter = 2,
        Both = 3
    }

    public partial class OperationMonthSummer : OperationMonth
    {
        private OperationMonthSummer(int operationMonthID, string operationMonthName, string operationMonthDisplayName, string operationMonthNereidAlias) : base(operationMonthID, operationMonthName, operationMonthDisplayName, operationMonthNereidAlias) {}
        public static readonly OperationMonthSummer Instance = new OperationMonthSummer(1, @"Summer", @"Summer", @"summer");
    }

    public partial class OperationMonthWinter : OperationMonth
    {
        private OperationMonthWinter(int operationMonthID, string operationMonthName, string operationMonthDisplayName, string operationMonthNereidAlias) : base(operationMonthID, operationMonthName, operationMonthDisplayName, operationMonthNereidAlias) {}
        public static readonly OperationMonthWinter Instance = new OperationMonthWinter(2, @"Winter", @"Winter", @"winter");
    }

    public partial class OperationMonthBoth : OperationMonth
    {
        private OperationMonthBoth(int operationMonthID, string operationMonthName, string operationMonthDisplayName, string operationMonthNereidAlias) : base(operationMonthID, operationMonthName, operationMonthDisplayName, operationMonthNereidAlias) {}
        public static readonly OperationMonthBoth Instance = new OperationMonthBoth(3, @"Both", @"Both", @"both");
    }
}