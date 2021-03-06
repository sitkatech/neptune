﻿/*-----------------------------------------------------------------------
<copyright file="AuditLog.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;

namespace Neptune.Web.Models
{
    public partial class AuditLog
    {
        private const string EntityContainerName = "DatabaseEntities";
        private const string PrimaryKeyName = "PrimaryKey";

        public static readonly List<string> IgnoredTables = new List<string>
        {
            "NeptunePage",
            "NeptunePageImage",
            "FileResource",
            "FieldDefinitionData",
            "FieldDefinitionDataImage",
            "Notification",
            "SupportRequestLog",
            "MaintenanceRecord",
            "MaintenanceRecordObservation",
            "MaintenanceRecordObservationValue",
            "FundingEventFundingSource",
            "OnlandVisualTrashAssessmentObservationPhoto",
            "OnlandVisualTrashAssessmentPreliminarySourceIdentificationType",
            "OnlandVisualTrashAssessmentObservationPhotoStaging",
            "DelineationStaging",
            "RegionalSubbasin",
            "RegionalSubbasinStaging",
            "HRUCharacteristic",
            "DelineationOverlap",
            "LoadGeneratingUnit"
        };

        public string GetAuditDescriptionDisplay()
        {
            
            if (string.IsNullOrWhiteSpace(AuditDescription))
            {
                return AuditLogEventType.GetAuditStringForOperationType(ColumnName, OriginalValue, NewValue);
            }

            return AuditDescription;
        }

        public static List<AuditLog> GetAuditLogRecordsForModifiedOrDeleted(DbEntityEntry dbEntry, Person person, ObjectContext objectContext)
        {
            var result = new List<AuditLog>();
            var tableName = EntityFrameworkHelpers.GetTableName(dbEntry.Entity.GetType(), objectContext);
            if (!IgnoredTables.Contains(tableName))
            {
                switch (dbEntry.State)
                {
                    case EntityState.Deleted:
                        var newAuditLog = CreateAuditLogEntryForDeleted(dbEntry, tableName, person, DateTime.Now, AuditLogEventType.Deleted);
                        result.Add(newAuditLog);
                        break;

                    case EntityState.Modified:
                        var modifiedProperties = dbEntry.CurrentValues.PropertyNames.Where(p => dbEntry.Property(p).IsModified).Select(dbEntry.Property).ToList();
                        var auditLogs =
                            modifiedProperties.Select(
                                modifiedProperty => CreateAuditLogEntryForAddedOrModified(objectContext, dbEntry, tableName, person, DateTime.Now, AuditLogEventType.Modified, modifiedProperty))
                                .Where(x => x != null)
                                .ToList();
                        result.AddRange(auditLogs);
                        break;
                }
            }
            // Otherwise, don't do anything, we don't care about Unchanged or Detached entities 
            return result;
        }

        /// <summary>
        /// This has its own path because we need to call this after the initial savechanges to grab the new primary key id from the database
        /// </summary>
        public static List<AuditLog> GetAuditLogRecordsForAdded(DbEntityEntry dbEntry, Person person, ObjectContext objectContext)
        {
            var result = new List<AuditLog>();
            var tableName = EntityFrameworkHelpers.GetTableName(dbEntry.Entity.GetType(), objectContext);
            if (!IgnoredTables.Contains(tableName))
            {
                var modifiedProperties = dbEntry.CurrentValues.PropertyNames.Select(dbEntry.Property).Where(currentProperty => !IsPropertyChangeOfNullToNull(currentProperty)).ToList();
                var auditLogs =
                    modifiedProperties.Select(
                        modifiedProperty => CreateAuditLogEntryForAddedOrModified(objectContext, dbEntry, tableName, person, DateTime.Now, AuditLogEventType.Added, modifiedProperty))
                        .Where(x => x != null)
                        .ToList();
                result.AddRange(auditLogs);
            }
            return result;
        }

        /// <summary>
        /// Remove uninteresting properties from Add operations, to reduce noisy meaningless AuditLogs
        /// </summary>
        private static bool IsPropertyChangeOfNullToNull(DbPropertyEntry currentProperty)
        {
            return currentProperty.CurrentValue == null && currentProperty.OriginalValue == null;
        }

        /// <summary>
        /// Creates an audit log entry for a <see cref="DbEntityEntry"/> that has an <see cref="EntityState"/> of <see cref="EntityState.Deleted"/>
        /// Deleted log entries do not have columns/property names, so there will just be one record created
        /// </summary>
        private static AuditLog CreateAuditLogEntryForDeleted(DbEntityEntry dbEntry,
            string tableName,
            Person person,
            DateTime changeDate,
            AuditLogEventType auditLogEventType)
        {
            var auditableEntityDeleted = GetIAuditableEntityFromEntity(dbEntry.Entity, tableName);
            var optionalAuditDescriptionString = auditLogEventType.GetAuditStringForOperationType(tableName, null, auditableEntityDeleted.GetAuditDescriptionString());
            var auditLogEntry = CreateAuditLogEntryImpl(dbEntry,
                tableName,
                person,
                changeDate,
                auditLogEventType,
                "*ALL",
                AuditLogEventType.Deleted.AuditLogEventTypeDisplayName,
                null,
                optionalAuditDescriptionString);
            return auditLogEntry;
        }

        /// <summary>
        /// Creates an audit log entry for a <see cref="DbEntityEntry"/> that has an <see cref="EntityState"/> of <see cref="EntityState.Added"/> or <see cref="EntityState.Modified"/>
        /// It will create an audit log entry for each property that has changed
        /// </summary>
        private static AuditLog CreateAuditLogEntryForAddedOrModified(ObjectContext objectContext,
            DbEntityEntry dbEntry,
            string tableName,
            Person person,
            DateTime changeDate,
            AuditLogEventType auditLogEventType,
            DbPropertyEntry modifiedProperty)
        {
            var propertyName = modifiedProperty.Name;
            if (!string.Equals(propertyName, $"{tableName}ID", StringComparison.InvariantCultureIgnoreCase) && !string.Equals(propertyName, "TenantID", StringComparison.InvariantCultureIgnoreCase))
            {
                var optionalAuditDescriptionString = GetAuditDescriptionStringIfAnyForProperty(objectContext, dbEntry, propertyName, auditLogEventType);
                var auditLogEntry = CreateAuditLogEntryImpl(dbEntry,
                    tableName,
                    person,
                    changeDate,
                    auditLogEventType,
                    propertyName,
                    modifiedProperty.CurrentValue,
                    modifiedProperty.OriginalValue,
                    optionalAuditDescriptionString);
                return auditLogEntry;
            }
            return null;
        }

        private static AuditLog CreateAuditLogEntryImpl(DbEntityEntry dbEntry,
            string tableName,
            Person person,
            DateTime changeDate,
            AuditLogEventType auditLogEventType,
            string propertyName,
            object newValue,
            object originalValue,
            string optionalAuditDescriptionString)
        {
            var recordID = (int) dbEntry.Property(PrimaryKeyName).CurrentValue;
            var newValueString = newValue != null ? newValue.ToString() : string.Empty;
            var auditLog = new AuditLog(person, changeDate, auditLogEventType, tableName, recordID, propertyName, newValueString)
            {
                OriginalValue = originalValue?.ToString(),
                AuditDescription = optionalAuditDescriptionString
            };
            return auditLog;
        }

        /// <summary>
        /// Gets the audit description string for a property that came from a <see cref="DbEntityEntry"/> that has an <see cref="EntityState"/> of <see cref="EntityState.Added"/> or <see cref="EntityState.Modified"/>
        /// This will attempt to look up a foreign key and return a more descriptive string for that fk property
        /// </summary>
        public static string GetAuditDescriptionStringIfAnyForProperty(ObjectContext objectContext, DbEntityEntry dbEntry, string propertyName, AuditLogEventType auditLogEventType)
        {
            var objectStateEntry = objectContext.ObjectStateManager.GetObjectStateEntry(dbEntry.Entity);
            // find foreign key relationships for given propertyname
            var relatedEnds = GetDependentForeignKeyRelatedEndsForProperty(objectStateEntry, propertyName);

            foreach (var end in relatedEnds)
            {
                if (!(end.RelationshipSet.ElementType is AssociationType elementType) || !elementType.IsForeignKey)
                {
                    continue;
                }

                foreach (var constraint in elementType.ReferentialConstraints)
                {
                    // Multiplicity many means we are looking at a foreign key in a dependent entity
                    // I assume that ToRole will point to a dependent entity, don't know if it can be FromRole
                    Check.Require(constraint.ToRole.RelationshipMultiplicity == RelationshipMultiplicity.Many);
                    // If not 1 then it is a composite key I guess. Becomes a lot more difficult to handle.
                    Check.Require(constraint.ToProperties.Count == 1);

                    var entityName = constraint.FromRole.Name;
                    string auditDescriptionStringForOriginalValue = null;
                    if (!IgnoredTables.Contains(entityName))
                    {
                        var constraintProperty = constraint.ToProperties[0];
                        var principalEntity = (EntityReference) end;

                        var newEntityKey = principalEntity.EntityKey;
                        var auditDescriptionStringForNewValue = GetAuditDescriptionStringForEntityKey(objectContext, newEntityKey, entityName);

                        if (newEntityKey != null)
                        {
                            var oldEntityKey = CreateEntityKeyForValue(newEntityKey.EntitySetName,
                                principalEntity.EntityKey.EntityKeyValues[0].Key,
                                objectStateEntry.OriginalValues[constraintProperty.Name]);
                            auditDescriptionStringForOriginalValue = GetAuditDescriptionStringForEntityKey(objectContext, oldEntityKey, entityName);
                        }
                        return auditLogEventType.GetAuditStringForOperationType(entityName, auditDescriptionStringForOriginalValue, auditDescriptionStringForNewValue);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Creates an entity key for the given entitySet and primaryKeyValue
        /// Used to look up the Entity from the Original Value
        /// </summary>
        private static EntityKey CreateEntityKeyForValue(string entitySetName, string entityKeyName, object primaryKeyValue)
        {
            EntityKey oldEntityKey = null;
            // Create an EntityKey for the old foreign key value if there was one
            if (!(primaryKeyValue is DBNull))
            {
                var oldPrimaryKeyValue = primaryKeyValue;
                oldEntityKey = new EntityKey
                {
                    EntityContainerName = EntityContainerName,
                    EntitySetName = entitySetName,
                    EntityKeyValues = new[] {new EntityKeyMember(entityKeyName, oldPrimaryKeyValue)}
                };
            }
            return oldEntityKey;
        }

        /// <summary>
        /// Gets the <see cref="IAuditableEntity.GetAuditDescriptionString"/> for a given entityKey
        /// </summary>
        private static string GetAuditDescriptionStringForEntityKey(ObjectContext objectContext, EntityKey entityKey, string entityName)
        {
            if (entityKey != null)
            {
                var auditableEntity = GetIAuditableEntityFromEntityKey(objectContext, entityKey, entityName);
                return auditableEntity.GetAuditDescriptionString();
            }
            return null;
        }

        /// <summary>
        /// Finds the foreign key related ends for a property, if any
        /// </summary>
        /// <param name="objectStateEntry"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static IEnumerable<IRelatedEnd> GetDependentForeignKeyRelatedEndsForProperty(ObjectStateEntry objectStateEntry, string propertyName)
        {
            return objectStateEntry.RelationshipManager.GetAllRelatedEnds().Where(x =>
            {
                if (!(x is EntityReference)) // we only handle entity references; we can't handle entity collections
                {
                    return false;
                }
                var associationType = ((AssociationType) x.RelationshipSet.ElementType);
                if (associationType == null)
                {
                    return false;
                }
                var referentialConstraints = associationType.ReferentialConstraints;

                return associationType.IsForeignKey && referentialConstraints.Any(c => c.ToProperties.Contains(propertyName));
            }).ToList();
        }

        /// <summary>
        /// Given an objectcontext and entityKey, return an IAuditableEntity
        /// </summary>
        private static IAuditableEntity GetIAuditableEntityFromEntityKey(ObjectContext objectContext, EntityKey entityKey, string tableName)
        {
            var entity = objectContext.GetObjectByKey(entityKey);
            return GetIAuditableEntityFromEntity(entity, tableName);
        }

        /// <summary>
        /// Given an entity, try to cast it to an IAuditableEntity and return it
        /// </summary>
        private static IAuditableEntity GetIAuditableEntityFromEntity(object entity, string tableName)
        {
            var auditableEntity = entity as IAuditableEntity;
            Check.RequireNotNull(auditableEntity, $"{tableName} needs to implement {typeof(IAuditableEntity).Name}");
            return auditableEntity;
        }
    }
}
