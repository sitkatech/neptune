ALTER TABLE [dbo].[TreatmentBMPAttributeType] DROP CONSTRAINT [CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull]
GO

exec sp_rename 'dbo.PK_TreatmentBMPAttributeValue_TreatmentBMPAttributeValueID', 'PK_CustomAttributeValue_CustomAttributeValueID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttributeValue_Tenant_TenantID', 'FK_CustomAttributeValue_Tenant_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttributeValue_TreatmentBMPAttribute_TreatmentBMPAttributeID', 'FK_CustomAttributeValue_CustomAttribute_CustomAttributeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttributeValue_TreatmentBMPAttribute_TreatmentBMPAttributeID_TenantID', 'FK_CustomAttributeValue_CustomAttribute_CustomAttributeID_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_MaintenanceRecordObservation_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID', 'FK_MaintenanceRecordObservation_CustomAttributeType_CustomAttributeTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_MaintenanceRecordObservation_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID', 'FK_MaintenanceRecordObservation_CustomAttributeType_CustomAttributeTypeID_TenantID', 'OBJECT';
exec sp_rename 'dbo.PK_TreatmentBMPAttribute_TreatmentBMPAttributeID', 'PK_CustomAttribute_CustomAttributeID', 'OBJECT';
exec sp_rename 'dbo.FK_MaintenanceRecordObservation_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID', 'FK_MaintenanceRecordObservation_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPAttribute_TreatmentBMPTypeID_TreatmentBMPTypeAttributeTypeID', 'AK_CustomAttribute_TreatmentBMPTypeID_TreatmentBMPTypeAttributeTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_Tenant_TenantID', 'FK_CustomAttribute_Tenant_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID', 'FK_CustomAttribute_TreatmentBMP_TreatmentBMPID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID', 'FK_CustomAttribute_TreatmentBMPType_TreatmentBMPTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID', 'FK_CustomAttribute_CustomAttributeType_CustomAttributeTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID', 'FK_CustomAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID', 'FK_CustomAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TenantID', 'FK_CustomAttribute_TreatmentBMP_TreatmentBMPID_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID_TenantID', 'FK_CustomAttribute_TreatmentBMPType_TreatmentBMPTypeID_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID', 'FK_CustomAttribute_CustomAttributeType_CustomAttributeTypeID_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID', 'FK_CustomAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID', 'OBJECT';
exec sp_rename 'dbo.PK_TreatmentBMPAttributeTypePurpose_TreatmentBMPAttributeTypePurposeID', 'PK_CustomAttributeTypePurpose_CustomAttributeTypePurposeID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPAttributeTypePurpose_TreatmentBMPAttributeTypePurposeDisplayName', 'AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeDisplayName', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPAttributeTypePurpose_TreatmentBMPAttributeTypePurposeName', 'AK_CustomAttributeTypePurpose_CustomAttributeTypePurposeName', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypePurpose_TreatmentBMPAttributeTypePurposeID', 'FK_CustomAttributeType_CustomAttributeTypePurpose_CustomAttributeTypePurposeID', 'OBJECT';
exec sp_rename 'dbo.PK_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeID', 'PK_CustomAttributeDataType_CustomAttributeDataTypeID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeDisplayName', 'AK_CustomAttributeDataType_CustomAttributeDataTypeDisplayName', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeName', 'AK_CustomAttributeDataType_CustomAttributeDataTypeName', 'OBJECT';
exec sp_rename 'dbo.PK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID', 'PK_CustomAttributeType_CustomAttributeTypeID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID', 'AK_CustomAttributeType_CustomAttributeTypeID_TenantID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeName', 'AK_CustomAttributeType_CustomAttributeTypeName', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttributeType_Tenant_TenantID', 'FK_CustomAttributeType_Tenant_TenantID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttributeType_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeID', 'FK_CustomAttributeType_CustomAttributeDataType_CustomAttributeDataTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPAttributeType_MeasurementUnitType_MeasurementUnitTypeID', 'FK_CustomAttributeType_MeasurementUnitType_MeasurementUnitTypeID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID', 'AK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPTypeAttributeType_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID', 'FK_TreatmentBMPTypeAttributeType_CustomAttributeType_CustomAttributeTypeID', 'OBJECT';
exec sp_rename 'dbo.FK_TreatmentBMPTypeAttributeType_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID', 'FK_TreatmentBMPTypeAttributeType_CustomAttributeType_CustomAttributeTypeID_TenantID', 'OBJECT';
exec sp_rename 'dbo.AK_TreatmentBMPAttribute_TreatmentBMPAttributeID_TenantID', 'AK_CustomAttribute_CustomAttributeID_TenantID', 'OBJECT';
exec sp_rename 'dbo.TreatmentBMPAttributeValue.TreatmentBMPAttributeValueID', 'CustomAttributeValueID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeValue.TreatmentBMPAttributeID', 'CustomAttributeID', 'COLUMN';
exec sp_rename 'dbo.MaintenanceRecordObservation.TreatmentBMPAttributeTypeID', 'CustomAttributeTypeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttribute.TreatmentBMPAttributeID', 'CustomAttributeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttribute.TreatmentBMPAttributeTypeID', 'CustomAttributeTypeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeTypePurpose.TreatmentBMPAttributeTypePurposeID', 'CustomAttributeTypePurposeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeTypePurpose.TreatmentBMPAttributeTypePurposeName', 'CustomAttributeTypePurposeName', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeTypePurpose.TreatmentBMPAttributeTypePurposeDisplayName', 'CustomAttributeTypePurposeDisplayName', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeID', 'CustomAttributeDataTypeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeName', 'CustomAttributeDataTypeName', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeDataType.TreatmentBMPAttributeDataTypeDisplayName', 'CustomAttributeDataTypeDisplayName', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeType.TreatmentBMPAttributeTypeID', 'CustomAttributeTypeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeType.TreatmentBMPAttributeTypeName', 'CustomAttributeTypeName', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeType.TreatmentBMPAttributeDataTypeID', 'CustomAttributeDataTypeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeType.TreatmentBMPAttributeTypeDescription', 'CustomAttributeTypeDescription', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeType.TreatmentBMPAttributeTypePurposeID', 'CustomAttributeTypePurposeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeType.TreatmentBMPAttributeTypeOptionsSchema', 'CustomAttributeTypeOptionsSchema', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPTypeAttributeType.TreatmentBMPAttributeTypeID', 'CustomAttributeTypeID', 'COLUMN';
exec sp_rename 'dbo.TreatmentBMPAttributeValue', 'CustomAttributeValue';
exec sp_rename 'dbo.TreatmentBMPAttribute', 'CustomAttribute';
exec sp_rename 'dbo.TreatmentBMPAttributeTypePurpose', 'CustomAttributeTypePurpose';
exec sp_rename 'dbo.TreatmentBMPAttributeDataType', 'CustomAttributeDataType';
exec sp_rename 'dbo.TreatmentBMPAttributeType', 'CustomAttributeType';

ALTER TABLE [dbo].[CustomAttributeType]  WITH CHECK ADD  CONSTRAINT [CK_CustomAttributeType_PickListTypeOptionSchemaNotNull] CHECK  ((NOT ([CustomAttributeDataTypeID]=(6) OR [CustomAttributeDataTypeID]=(5)) AND [CustomAttributeTypeOptionsSchema] IS NULL OR ([CustomAttributeDataTypeID]=(6) OR [CustomAttributeDataTypeID]=(5)) AND [CustomAttributeTypeOptionsSchema] IS NOT NULL))
GO