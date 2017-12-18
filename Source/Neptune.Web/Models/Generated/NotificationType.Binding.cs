//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NotificationType]
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
    public abstract partial class NotificationType : IHavePrimaryKey
    {
        public static readonly NotificationTypeCustom Custom = NotificationTypeCustom.Instance;

        public static readonly List<NotificationType> All;
        public static readonly ReadOnlyDictionary<int, NotificationType> AllLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static NotificationType()
        {
            All = new List<NotificationType> { Custom };
            AllLookupDictionary = new ReadOnlyDictionary<int, NotificationType>(All.ToDictionary(x => x.NotificationTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected NotificationType(int notificationTypeID, string notificationTypeName, string notificationTypeDisplayName)
        {
            NotificationTypeID = notificationTypeID;
            NotificationTypeName = notificationTypeName;
            NotificationTypeDisplayName = notificationTypeDisplayName;
        }

        [Key]
        public int NotificationTypeID { get; private set; }
        public string NotificationTypeName { get; private set; }
        public string NotificationTypeDisplayName { get; private set; }
        public int PrimaryKey { get { return NotificationTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(NotificationType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.NotificationTypeID == NotificationTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as NotificationType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return NotificationTypeID;
        }

        public static bool operator ==(NotificationType left, NotificationType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(NotificationType left, NotificationType right)
        {
            return !Equals(left, right);
        }

        public NotificationTypeEnum ToEnum { get { return (NotificationTypeEnum)GetHashCode(); } }

        public static NotificationType ToType(int enumValue)
        {
            return ToType((NotificationTypeEnum)enumValue);
        }

        public static NotificationType ToType(NotificationTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case NotificationTypeEnum.Custom:
                    return Custom;
                default:
                    throw new ArgumentException(string.Format("Unable to map Enum: {0}", enumValue));
            }
        }
    }

    public enum NotificationTypeEnum
    {
        Custom = 1
    }

    public partial class NotificationTypeCustom : NotificationType
    {
        private NotificationTypeCustom(int notificationTypeID, string notificationTypeName, string notificationTypeDisplayName) : base(notificationTypeID, notificationTypeName, notificationTypeDisplayName) {}
        public static readonly NotificationTypeCustom Instance = new NotificationTypeCustom(1, @"Custom", @"Custom Notification");
    }
}