create table dbo.StormwaterJurisdictionPublicWQMPVisibilityType (
	StormwaterJurisdictionPublicWQMPVisibilityTypeID int not null constraint PK_StormwaterJurisdictionPublicWQMPVisibilityType_StormwaterJurisdictionPublicWQMPVisibilityTypeID primary key,
	StormwaterJurisdictionPublicWQMPVisibilityTypeName varchar(80) not null constraint AK_StormwaterJurisdictionPublicWQMPVisibilityType_StormwaterJurisdictionPublicWQMPVisibilityTypeName unique,
	StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName varchar(80) not null constraint AK_StormwaterJurisdictionPublicWQMPVisibilityType_StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName unique
)

insert into dbo.StormwaterJurisdictionPublicWQMPVisibilityType (StormwaterJurisdictionPublicWQMPVisibilityTypeID, StormwaterJurisdictionPublicWQMPVisibilityTypeName, StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName)
values (1, 'ActiveAndInactive', 'Active and Inactive'),
(2, 'ActiveOnly', 'Active Only'),
(3, 'None', 'None')

alter table dbo.StormwaterJurisdiction
add StormwaterJurisdictionPublicWQMPVisibilityTypeID int null
go

--Should be North Stormwater Jurisdictions
update dbo.StormwaterJurisdiction
set StormwaterJurisdictionPublicWQMPVisibilityTypeID = 2
where StormwaterJurisdictionID between 1 and 12

--Shouldbe South Stormwater Jurisdictions
update dbo.StormwaterJurisdiction
set StormwaterJurisdictionPublicWQMPVisibilityTypeID = 3
where StormwaterJurisdictionID between 13 and 35

alter table dbo.StormwaterJurisdiction
alter column StormwaterJurisdictionPublicWQMPVisibilityTypeID int not null 

alter table dbo.StormwaterJurisdiction
add constraint FK_StormwaterJurisdiction_StormwaterJurisdictionPublicWQMPVisibilityType_StormwaterJurisdictionPublicWQMPVisibilityTypeID foreign key (StormwaterJurisdictionPublicWQMPVisibilityTypeID) references dbo.StormwaterJurisdictionPublicWQMPVisibilityType (StormwaterJurisdictionPublicWQMPVisibilityTypeID)