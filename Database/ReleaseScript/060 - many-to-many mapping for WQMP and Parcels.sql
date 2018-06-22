alter table dbo.Parcel add constraint AK_Parcel_ParcelID_TenantID unique (ParcelID, TenantID)

create table dbo.WaterQualityManagementPlanParcel(
	WaterQualityManagementPlanParcelID int not null identity(1, 1) constraint PK_WaterQualityManagementPlanParcel_WaterQualityManagementPlanParcelID primary key,

	TenantID int not null
		constraint FK_WaterQualityManagementPlanParcel_Tenant_TenantID foreign key references dbo.Tenant(TenantID),
	WaterQualityManagementPlanID int not null
		constraint FK_WaterQualityManagementPlanParcel_WaterQualityManagementPlan_WaterQualityManagementPlanID foreign key references dbo.WaterQualityManagementPlan(WaterQualityManagementPlanID),
	ParcelID int not null
		constraint FK_WaterQualityManagementPlanParcel_Parcel_ParcelID foreign key references dbo.Parcel(ParcelID),

	constraint AK_WaterQualityManagementPlanParcel_WaterQualityManagementPlanID_ParcelID unique (WaterQualityManagementPlanID, ParcelID),
	constraint AK_WaterQualityManagementPlanParcel_WaterQualityManagementPlanParcelID_TenantID unique (WaterQualityManagementPlanParcelID, TenantID),
	constraint FK_WaterQualityManagementPlanParcel_WaterQualityManagementPlan_WaterQualityManagementPlanID_TenantID foreign key (WaterQualityManagementPlanID, TenantID) references dbo.WaterQualityManagementPlan(WaterQualityManagementPlanID, TenantID),
	constraint FK_WaterQualityManagementPlanParcel_Parcel_ParcelID_TenantID foreign key (ParcelID, TenantID) references dbo.Parcel(ParcelID, TenantID)
)

alter table dbo.TenantAttribute add MapServiceUrl varchar(255) null
alter table dbo.TenantAttribute add ParcelLayerName varchar(255) null

go

update dbo.TenantAttribute
set
	ParcelLayerName = 'OCStormwater:Parcels'
