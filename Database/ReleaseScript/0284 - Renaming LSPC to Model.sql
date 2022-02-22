alter table dbo.LoadGeneratingUnit
drop constraint FK_LoadGeneratingUnit_LSPCBasin_LSPCBasinID

alter table dbo.RegionalSubbasin
drop constraint FK_RegionalSubbasin_LSPCBasin_LSPCBasinID

alter table dbo.TreatmentBMP
drop constraint FK_TreatmentBMP_LSPCBasin_LSPCBasinID

create table dbo.ModelBasin (
	ModelBasinID int not null identity(1,1) constraint PK_ModelBasin_ModelBasinID primary key,
	ModelBasinKey int not null constraint AK_ModelBasin_ModelBasinKey unique,
	ModelBasinName varchar(100) not null,
	ModelBasinGeometry geometry not null,
	LastUpdate datetime not null
)

SET IDENTITY_INSERT dbo.ModelBasin ON
insert into dbo.ModelBasin(ModelBasinID, ModelBasinKey, ModelBasinName, ModelBasinGeometry, LastUpdate)
select LSPCBasinID, LSPCBasinKey, LSPCBasinName, LSPCBasinGeometry, LastUpdate
from dbo.LSPCBasin
SET IDENTITY_INSERT dbo.ModelBasin OFF

create table dbo.ModelBasinStaging (
	ModelBasinStagingID int not null identity(1,1) constraint PK_ModelBasinStaging_ModelBasinStagingID primary key,
	ModelBasinKey int not null constraint AK_ModelBasinStaging_ModelBasinKey unique,
	ModelBasinName varchar(100) not null,
	ModelBasinGeometry geometry not null
)

SET IDENTITY_INSERT dbo.ModelBasinStaging ON
insert into dbo.ModelBasinStaging (ModelBasinStagingID, ModelBasinKey, ModelBasinName, ModelBasinGeometry)
select LSPCBasinStagingID, LSPCBasinKey, LSPCBasinName, LSPCBasinGeometry
from dbo.LSPCBasinStaging
SET IDENTITY_INSERT dbo.ModelBasinStaging OFF

exec sp_rename 'dbo.LoadGeneratingUnit.LSPCBasinID', 'ModelBasinID', 'COLUMN'
exec sp_rename 'dbo.RegionalSubbasin.LSPCBasinID', 'ModelBasinID', 'COLUMN'
exec sp_rename 'dbo.TreatmentBMP.LSPCBasinID', 'ModelBasinID', 'COLUMN'
exec sp_rename 'dbo.RegionalSubbasin.IsInLSPCBasin', 'IsInModelBasin', 'COLUMN'

alter table dbo.LoadGeneratingUnit
add constraint FK_LoadGeneratingUnit_ModelBasin_ModelBasinID foreign key (ModelBasinID) references dbo.ModelBasin (ModelBasinID)

alter table dbo.RegionalSubbasin
add constraint FK_RegionalSubbasin_ModelBasin_ModelBasinID foreign key (ModelBasinID) references dbo.ModelBasin (ModelBasinID)

alter table dbo.TreatmentBMP
add constraint FK_TreatmentBMP_ModelBasin_ModelBasinID foreign key (ModelBasinID) references dbo.ModelBasin (ModelBasinID)

drop procedure dbo.pLSPCBasinUpdateFromStaging
drop procedure dbo.pTreatmentBMPUpdateLSPCBasin

drop view dbo.vLSPCBasinLGUInput

drop table dbo.LSPCBasin
drop table dbo.LSPCBasinStaging