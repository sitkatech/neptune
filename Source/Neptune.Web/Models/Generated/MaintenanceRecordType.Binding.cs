//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordType]
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
    public abstract partial class MaintenanceRecordType : IHavePrimaryKey
    {
        public static readonly MaintenanceRecordTypePreventative Preventative = MaintenanceRecordTypePreventative.Instance;
        public static readonly MaintenanceRecordTypeCorrective Corrective = MaintenanceRecordTypeCorrective.Instance;

        public static readonly List<MaintenanceRecordType> All;
        public static readonly ReadOnlyDictionary<int, MaintenanceRecordType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static MaintenanceRecordType()
        {
            All = new List<MaintenanceRecordType> { Preventative, Corrective };
            AllLookupDictionary = new ReadOnlyDictionary<int, MaintenanceRecordType>(All.ToDictionary(x => x.MaintenanceRecordTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected MaintenanceRecordType(int maintenanceRecordTypeID, string maintenanceRecordTypeName, string maintenanceRecordTypeDisplayName)
        {
            MaintenanceRecordTypeID = maintenanceRecordTypeID;
            MaintenanceRecordTypeName = maintenanceRecordTypeName;
            MaintenanceRecordTypeDisplayName = maintenanceRecordTypeDisplayName;
        }

        [Key]
        public int MaintenanceRecordTypeID { get; private set; }
        public string MaintenanceRecordTypeName { get; private set; }
        public string MaintenanceRecordTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return MaintenanceRecordTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(MaintenanceRecordType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.MaintenanceRecordTypeID == MaintenanceRecordTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as MaintenanceRecordType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return MaintenanceRecordTypeID;
        }

        public static bool operator ==(MaintenanceRecordType left, MaintenanceRecordType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MaintenanceRecordType left, MaintenanceRecordType right)
        {
            return !Equals(left, right);
        }

        public MaintenanceRecordTypeEnum ToEnum { get { return (MaintenanceRecordTypeEnum)GetHashCode(); } }

        public static MaintenanceRecordType ToType(int enumValue)
        {
            return ToType((MaintenanceRecordTypeEnum)enumValue);
        }

        public static MaintenanceRecordType ToType(MaintenanceRecordTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case MaintenanceRecordTypeEnum.Corrective:
                    return Corrective;
                case MaintenanceRecordTypeEnum.Preventative:
                    return Preventative;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum MaintenanceRecordTypeEnum
    {
        Preventative = 1,
        Corrective = 2
    }

    public partial class MaintenanceRecordTypePreventative : MaintenanceRecordType
    {
        private MaintenanceRecordTypePreventative(int maintenanceRecordTypeID, string maintenanceRecordTypeName, string maintenanceRecordTypeDisplayName) : base(maintenanceRecordTypeID, maintenanceRecordTypeName, maintenanceRecordTypeDisplayName) {}
        public static readonly MaintenanceRecordTypePreventative Instance = new MaintenanceRecordTypePreventative(1, @"Preventative", @"Preventative");
    }

    public partial class MaintenanceRecordTypeCorrective : MaintenanceRecordType
    {
        private MaintenanceRecordTypeCorrective(int maintenanceRecordTypeID, string maintenanceRecordTypeName, string maintenanceRecordTypeDisplayName) : base(maintenanceRecordTypeID, maintenanceRecordTypeName, maintenanceRecordTypeDisplayName) {}
        public static readonly MaintenanceRecordTypeCorrective Instance = new MaintenanceRecordTypeCorrective(2, @"Corrective", @"Corrective");
    }
}