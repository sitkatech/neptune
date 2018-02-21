//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceActivityType]
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
    public abstract partial class MaintenanceActivityType : IHavePrimaryKey
    {
        public static readonly MaintenanceActivityTypePreventative Preventative = MaintenanceActivityTypePreventative.Instance;
        public static readonly MaintenanceActivityTypeCorrective Corrective = MaintenanceActivityTypeCorrective.Instance;

        public static readonly List<MaintenanceActivityType> All;
        public static readonly ReadOnlyDictionary<int, MaintenanceActivityType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static MaintenanceActivityType()
        {
            All = new List<MaintenanceActivityType> { Preventative, Corrective };
            AllLookupDictionary = new ReadOnlyDictionary<int, MaintenanceActivityType>(All.ToDictionary(x => x.MaintenanceActivityTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected MaintenanceActivityType(int maintenanceActivityTypeID, string maintenanceActivityTypeName, string maintenanceActivityTypeDisplayName)
        {
            MaintenanceActivityTypeID = maintenanceActivityTypeID;
            MaintenanceActivityTypeName = maintenanceActivityTypeName;
            MaintenanceActivityTypeDisplayName = maintenanceActivityTypeDisplayName;
        }

        [Key]
        public int MaintenanceActivityTypeID { get; private set; }
        public string MaintenanceActivityTypeName { get; private set; }
        public string MaintenanceActivityTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return MaintenanceActivityTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(MaintenanceActivityType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.MaintenanceActivityTypeID == MaintenanceActivityTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as MaintenanceActivityType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return MaintenanceActivityTypeID;
        }

        public static bool operator ==(MaintenanceActivityType left, MaintenanceActivityType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MaintenanceActivityType left, MaintenanceActivityType right)
        {
            return !Equals(left, right);
        }

        public MaintenanceActivityTypeEnum ToEnum { get { return (MaintenanceActivityTypeEnum)GetHashCode(); } }

        public static MaintenanceActivityType ToType(int enumValue)
        {
            return ToType((MaintenanceActivityTypeEnum)enumValue);
        }

        public static MaintenanceActivityType ToType(MaintenanceActivityTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case MaintenanceActivityTypeEnum.Corrective:
                    return Corrective;
                case MaintenanceActivityTypeEnum.Preventative:
                    return Preventative;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum MaintenanceActivityTypeEnum
    {
        Preventative = 1,
        Corrective = 2
    }

    public partial class MaintenanceActivityTypePreventative : MaintenanceActivityType
    {
        private MaintenanceActivityTypePreventative(int maintenanceActivityTypeID, string maintenanceActivityTypeName, string maintenanceActivityTypeDisplayName) : base(maintenanceActivityTypeID, maintenanceActivityTypeName, maintenanceActivityTypeDisplayName) {}
        public static readonly MaintenanceActivityTypePreventative Instance = new MaintenanceActivityTypePreventative(1, @"Preventative", @"Preventative");
    }

    public partial class MaintenanceActivityTypeCorrective : MaintenanceActivityType
    {
        private MaintenanceActivityTypeCorrective(int maintenanceActivityTypeID, string maintenanceActivityTypeName, string maintenanceActivityTypeDisplayName) : base(maintenanceActivityTypeID, maintenanceActivityTypeName, maintenanceActivityTypeDisplayName) {}
        public static readonly MaintenanceActivityTypeCorrective Instance = new MaintenanceActivityTypeCorrective(2, @"Corrective", @"Corrective");
    }
}