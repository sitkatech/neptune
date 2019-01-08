exec sp_rename 'dbo.FK_TenantAttribute_FileResource_TenantBannerLogoFileResourceID_FileResourceID', 'FK_SystemAttribute_FileResource_TenantBannerLogoFileResourceID_FileResourceID', 'OBJECT';
exec sp_rename 'dbo.FK_TenantAttribute_FileResource_TenantSquareLogoFileResourceID_FileResourceID', 'FK_SystemAttribute_FileResource_TenantSquareLogoFileResourceID_FileResourceID', 'OBJECT';
exec sp_rename 'dbo.FK_TenantAttribute_FileResource_TenantStyleSheetFileResourceID_FileResourceID', 'FK_SystemAttribute_FileResource_TenantStyleSheetFileResourceID_FileResourceID', 'OBJECT';
exec sp_rename 'dbo.FK_TenantAttribute_Person_PrimaryContactPersonID_PersonID', 'FK_SystemAttribute_Person_PrimaryContactPersonID_PersonID', 'OBJECT';
exec sp_rename 'dbo.PK_TenantAttribute_TenantAttributeID', 'PK_SystemAttribute_SystemAttributeID', 'OBJECT';
exec sp_rename 'dbo.AK_TenantAttribute_TenantDisplayName', 'AK_SystemAttribute_TenantDisplayName', 'OBJECT';
exec sp_rename 'dbo.TenantAttribute.TenantAttributeID', 'SystemAttributeID', 'COLUMN';
exec sp_rename 'dbo.TenantAttribute', 'SystemAttribute';