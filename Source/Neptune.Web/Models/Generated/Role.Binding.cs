//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Role]
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
    public abstract partial class Role : IHavePrimaryKey
    {
        public static readonly RoleAdmin Admin = RoleAdmin.Instance;
        public static readonly RoleUnassigned Unassigned = RoleUnassigned.Instance;
        public static readonly RoleSitkaAdmin SitkaAdmin = RoleSitkaAdmin.Instance;
        public static readonly RoleJurisdictionManager JurisdictionManager = RoleJurisdictionManager.Instance;
        public static readonly RoleJurisdictionEditor JurisdictionEditor = RoleJurisdictionEditor.Instance;

        public static readonly List<Role> All;
        public static readonly ReadOnlyDictionary<int, Role> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static Role()
        {
            All = new List<Role> { Admin, Unassigned, SitkaAdmin, JurisdictionManager, JurisdictionEditor };
            AllLookupDictionary = new ReadOnlyDictionary<int, Role>(All.ToDictionary(x => x.RoleID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected Role(int roleID, string roleName, string roleDisplayName, string roleDescription)
        {
            RoleID = roleID;
            RoleName = roleName;
            RoleDisplayName = roleDisplayName;
            RoleDescription = roleDescription;
        }

        [Key]
        public int RoleID { get; private set; }
        public string RoleName { get; private set; }
        public string RoleDisplayName { get; private set; }
        public string RoleDescription { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return RoleID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(Role other)
        {
            if (other == null)
            {
                return false;
            }
            return other.RoleID == RoleID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as Role);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return RoleID;
        }

        public static bool operator ==(Role left, Role right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Role left, Role right)
        {
            return !Equals(left, right);
        }

        public RoleEnum ToEnum { get { return (RoleEnum)GetHashCode(); } }

        public static Role ToType(int enumValue)
        {
            return ToType((RoleEnum)enumValue);
        }

        public static Role ToType(RoleEnum enumValue)
        {
            switch (enumValue)
            {
                case RoleEnum.Admin:
                    return Admin;
                case RoleEnum.JurisdictionEditor:
                    return JurisdictionEditor;
                case RoleEnum.JurisdictionManager:
                    return JurisdictionManager;
                case RoleEnum.SitkaAdmin:
                    return SitkaAdmin;
                case RoleEnum.Unassigned:
                    return Unassigned;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum RoleEnum
    {
        Admin = 1,
        Unassigned = 3,
        SitkaAdmin = 4,
        JurisdictionManager = 5,
        JurisdictionEditor = 6
    }

    public partial class RoleAdmin : Role
    {
        private RoleAdmin(int roleID, string roleName, string roleDisplayName, string roleDescription) : base(roleID, roleName, roleDisplayName, roleDescription) {}
        public static readonly RoleAdmin Instance = new RoleAdmin(1, @"Admin", @"Administrator", @"");
    }

    public partial class RoleUnassigned : Role
    {
        private RoleUnassigned(int roleID, string roleName, string roleDisplayName, string roleDescription) : base(roleID, roleName, roleDisplayName, roleDescription) {}
        public static readonly RoleUnassigned Instance = new RoleUnassigned(3, @"Unassigned", @"Unassigned", @"");
    }

    public partial class RoleSitkaAdmin : Role
    {
        private RoleSitkaAdmin(int roleID, string roleName, string roleDisplayName, string roleDescription) : base(roleID, roleName, roleDisplayName, roleDescription) {}
        public static readonly RoleSitkaAdmin Instance = new RoleSitkaAdmin(4, @"SitkaAdmin", @"Sitka Administrator", @"");
    }

    public partial class RoleJurisdictionManager : Role
    {
        private RoleJurisdictionManager(int roleID, string roleName, string roleDisplayName, string roleDescription) : base(roleID, roleName, roleDisplayName, roleDescription) {}
        public static readonly RoleJurisdictionManager Instance = new RoleJurisdictionManager(5, @"JurisdictionManager", @"Jurisdication Manager", @"");
    }

    public partial class RoleJurisdictionEditor : Role
    {
        private RoleJurisdictionEditor(int roleID, string roleName, string roleDisplayName, string roleDescription) : base(roleID, roleName, roleDisplayName, roleDescription) {}
        public static readonly RoleJurisdictionEditor Instance = new RoleJurisdictionEditor(6, @"JurisdictionEditor", @"Jurisdication Editor", @"");
    }
}