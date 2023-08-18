//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[AuditLogEventType]
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Neptune.Models.DataTransferObjects;


namespace Neptune.EFModels.Entities
{
    public abstract partial class AuditLogEventType : IHavePrimaryKey
    {
        public static readonly AuditLogEventTypeAdded Added = Neptune.EFModels.Entities.AuditLogEventTypeAdded.Instance;
        public static readonly AuditLogEventTypeDeleted Deleted = Neptune.EFModels.Entities.AuditLogEventTypeDeleted.Instance;
        public static readonly AuditLogEventTypeModified Modified = Neptune.EFModels.Entities.AuditLogEventTypeModified.Instance;

        public static readonly List<AuditLogEventType> All;
        public static readonly List<AuditLogEventTypeDto> AllAsDto;
        public static readonly ReadOnlyDictionary<int, AuditLogEventType> AllLookupDictionary;
        public static readonly ReadOnlyDictionary<int, AuditLogEventTypeDto> AllAsDtoLookupDictionary;

        /// <summary>
        /// Static type constructor to coordinate static initialization order
        /// </summary>
        static AuditLogEventType()
        {
            All = new List<AuditLogEventType> { Added, Deleted, Modified };
            AllAsDto = new List<AuditLogEventTypeDto> { Added.AsDto(), Deleted.AsDto(), Modified.AsDto() };
            AllLookupDictionary = new ReadOnlyDictionary<int, AuditLogEventType>(All.ToDictionary(x => x.AuditLogEventTypeID));
            AllAsDtoLookupDictionary = new ReadOnlyDictionary<int, AuditLogEventTypeDto>(AllAsDto.ToDictionary(x => x.AuditLogEventTypeID));
        }

        /// <summary>
        /// Protected constructor only for use in instantiating the set of static lookup values that match database
        /// </summary>
        protected AuditLogEventType(int auditLogEventTypeID, string auditLogEventTypeName, string auditLogEventTypeDisplayName)
        {
            AuditLogEventTypeID = auditLogEventTypeID;
            AuditLogEventTypeName = auditLogEventTypeName;
            AuditLogEventTypeDisplayName = auditLogEventTypeDisplayName;
        }

        [Key]
        public int AuditLogEventTypeID { get; private set; }
        public string AuditLogEventTypeName { get; private set; }
        public string AuditLogEventTypeDisplayName { get; private set; }
        [NotMapped]
        public int PrimaryKey { get { return AuditLogEventTypeID; } }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public bool Equals(AuditLogEventType other)
        {
            if (other == null)
            {
                return false;
            }
            return other.AuditLogEventTypeID == AuditLogEventTypeID;
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as AuditLogEventType);
        }

        /// <summary>
        /// Enum types are equal by primary key
        /// </summary>
        public override int GetHashCode()
        {
            return AuditLogEventTypeID;
        }

        public static bool operator ==(AuditLogEventType left, AuditLogEventType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AuditLogEventType left, AuditLogEventType right)
        {
            return !Equals(left, right);
        }

        public AuditLogEventTypeEnum ToEnum => (AuditLogEventTypeEnum)GetHashCode();

        public static AuditLogEventType ToType(int enumValue)
        {
            return ToType((AuditLogEventTypeEnum)enumValue);
        }

        public static AuditLogEventType ToType(AuditLogEventTypeEnum enumValue)
        {
            switch (enumValue)
            {
                case AuditLogEventTypeEnum.Added:
                    return Added;
                case AuditLogEventTypeEnum.Deleted:
                    return Deleted;
                case AuditLogEventTypeEnum.Modified:
                    return Modified;
                default:
                    throw new ArgumentException("Unable to map Enum: {enumValue}");
            }
        }
    }

    public enum AuditLogEventTypeEnum
    {
        Added = 1,
        Deleted = 2,
        Modified = 3
    }

    public partial class AuditLogEventTypeAdded : AuditLogEventType
    {
        private AuditLogEventTypeAdded(int auditLogEventTypeID, string auditLogEventTypeName, string auditLogEventTypeDisplayName) : base(auditLogEventTypeID, auditLogEventTypeName, auditLogEventTypeDisplayName) {}
        public static readonly AuditLogEventTypeAdded Instance = new AuditLogEventTypeAdded(1, @"Added", @"Added");
    }

    public partial class AuditLogEventTypeDeleted : AuditLogEventType
    {
        private AuditLogEventTypeDeleted(int auditLogEventTypeID, string auditLogEventTypeName, string auditLogEventTypeDisplayName) : base(auditLogEventTypeID, auditLogEventTypeName, auditLogEventTypeDisplayName) {}
        public static readonly AuditLogEventTypeDeleted Instance = new AuditLogEventTypeDeleted(2, @"Deleted", @"Deleted");
    }

    public partial class AuditLogEventTypeModified : AuditLogEventType
    {
        private AuditLogEventTypeModified(int auditLogEventTypeID, string auditLogEventTypeName, string auditLogEventTypeDisplayName) : base(auditLogEventTypeID, auditLogEventTypeName, auditLogEventTypeDisplayName) {}
        public static readonly AuditLogEventTypeModified Instance = new AuditLogEventTypeModified(3, @"Modified", @"Modified");
    }
}