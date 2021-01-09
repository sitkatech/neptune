create table dbo.StormwaterJurisdictionPublicBMPVisibilityType (
	StormwaterJurisdictionPublicBMPVisibilityTypeID int not null constraint PK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeID primary key,
	StormwaterJurisdictionPublicBMPVisibilityTypeName varchar(80) not null constraint AK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeName unique,
	StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName varchar(80) not null constraint AK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName unique
)

insert into dbo.StormwaterJurisdictionPublicBMPVisibilityType (StormwaterJurisdictionPublicBMPVisibilityTypeID, StormwaterJurisdictionPublicBMPVisibilityTypeName, StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName)
values (1, 'VerifiedOnly', 'Verified Only'),
(2, 'None', 'None')

alter table dbo.StormwaterJurisdiction
add StormwaterJurisdictionPublicBMPVisibilityTypeID int null
go

update dbo.StormwaterJurisdiction
set StormwaterJurisdictionPublicBMPVisibilityTypeID = 1

alter table dbo.StormwaterJurisdiction
alter column StormwaterJurisdictionPublicBMPVisibilityTypeID int not null 

alter table dbo.StormwaterJurisdiction
add constraint FK_StormwaterJurisdiction_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeID foreign key (StormwaterJurisdictionPublicBMPVisibilityTypeID) references dbo.StormwaterJurisdictionPublicBMPVisibilityType (StormwaterJurisdictionPublicBMPVisibilityTypeID)