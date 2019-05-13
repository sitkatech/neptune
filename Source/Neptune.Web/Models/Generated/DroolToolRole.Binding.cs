//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DroolToolRole]
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
    public abstract partial class DroolToolRole : IHavePrimaryKey
    {
        public static readonly DroolToolRoleAdmin Admin = DroolToolRoleAdmin.Instance;
        public static readonly DroolToolRoleEditor Editor = DroolToolRoleEditor.Instance;
        public static readonly DroolToolRoleUnassigned Unassigned = DroolToolRoleUnassigned.Instance;

        public static readonly List<DroolToolRole> All;
        public static readonly ReadOnlyDictionary<int, DroolToolRole> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static DroolToolRole()
        {
            All = new List<DroolToolRole> { Admin, Editor, Unassigned };
            AllLookupDictionary = new ReadOnlyDictionary<int, DroolToolRole>(All.ToDictionary(x => x.DroolToolRoleID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected DroolToolRole(int droolToolRoleID, string droolToolRoleName, string droolToolRoleDisplayName, string droolToolRoleDescription)
        {
            DroolToolRoleID = droolToolRoleID;
            DroolToolRoleName = droolToolRoleName;
            DroolToolRoleDisplayName = droolToolRoleDisplayName;
            DroolToolRoleDescription = droolToolRoleDescription;
        }

        [Key]
        public int DroolToolRoleID { get; private set; }
        public string DroolToolRoleName { get; private set; }
        public string DroolToolRoleDisplayName { get; private set; }
        public string DroolToolRoleDescription { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return DroolToolRoleID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(DroolToolRole other)
        {
            if (other == null)
            {
                return false;
            }
            return other.DroolToolRoleID == DroolToolRoleID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as DroolToolRole);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return DroolToolRoleID;
        }

        public static bool operator ==(DroolToolRole left, DroolToolRole right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DroolToolRole left, DroolToolRole right)
        {
            return !Equals(left, right);
        }

        public DroolToolRoleEnum ToEnum { get { return (DroolToolRoleEnum)GetHashCode(); } }

        public static DroolToolRole ToType(int enumValue)
        {
            return ToType((DroolToolRoleEnum)enumValue);
        }

        public static DroolToolRole ToType(DroolToolRoleEnum enumValue)
        {
            switch (enumValue)
            {
                case DroolToolRoleEnum.Admin:
                    return Admin;
                case DroolToolRoleEnum.Editor:
                    return Editor;
                case DroolToolRoleEnum.Unassigned:
                    return Unassigned;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum DroolToolRoleEnum
    {
        Admin = 1,
        Editor = 2,
        Unassigned = 3
    }

    public partial class DroolToolRoleAdmin : DroolToolRole
    {
        private DroolToolRoleAdmin(int droolToolRoleID, string droolToolRoleName, string droolToolRoleDisplayName, string droolToolRoleDescription) : base(droolToolRoleID, droolToolRoleName, droolToolRoleDisplayName, droolToolRoleDescription) {}
        public static readonly DroolToolRoleAdmin Instance = new DroolToolRoleAdmin(1, @"Admin", @"Administrator", @"");
    }

    public partial class DroolToolRoleEditor : DroolToolRole
    {
        private DroolToolRoleEditor(int droolToolRoleID, string droolToolRoleName, string droolToolRoleDisplayName, string droolToolRoleDescription) : base(droolToolRoleID, droolToolRoleName, droolToolRoleDisplayName, droolToolRoleDescription) {}
        public static readonly DroolToolRoleEditor Instance = new DroolToolRoleEditor(2, @"Editor", @"Editor", @"");
    }

    public partial class DroolToolRoleUnassigned : DroolToolRole
    {
        private DroolToolRoleUnassigned(int droolToolRoleID, string droolToolRoleName, string droolToolRoleDisplayName, string droolToolRoleDescription) : base(droolToolRoleID, droolToolRoleName, droolToolRoleDisplayName, droolToolRoleDescription) {}
        public static readonly DroolToolRoleUnassigned Instance = new DroolToolRoleUnassigned(3, @"Unassigned", @"Unassigned", @"");
    }
}