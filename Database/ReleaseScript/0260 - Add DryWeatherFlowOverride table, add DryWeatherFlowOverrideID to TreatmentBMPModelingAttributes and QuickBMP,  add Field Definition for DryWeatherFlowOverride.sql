create table dbo.DryWeatherFlowOverride
(
	DryWeatherFlowOverrideID int not null constraint PK_DryWeatherFlowOverride_DryWeatherFlowOverrideID primary key,
	DryWeatherFlowOverrideName varchar(50) not null constraint AK_DryWeatherFlowOverride_DryWeatherFlowOverrideName unique,
	DryWeatherFlowOverrideDisplayName varchar(50) not null constraint AK_DryWeatherFlowOverride_DryWeatherFlowOverrideDisplayName unique
)

alter table dbo.TreatmentBMPModelingAttribute add DryWeatherFlowOverrideID int null constraint FK_TreatmentBMPModelingAttribute_DryWeatherFlowOverride_DryWeatherFlowOverrideID foreign key references dbo.DryWeatherFlowOverride(DryWeatherFlowOverrideID)
GO

insert into dbo.DryWeatherFlowOverride(DryWeatherFlowOverrideID, DryWeatherFlowOverrideName, DryWeatherFlowOverrideDisplayName)
values
(1, 'No', 'No - As Modeled'),
(2, 'Yes', 'Yes - DWF Effectively Eliminated')

-- defaulting them all to Detailed
update dbo.TreatmentBMPModelingAttribute
set DryWeatherFlowOverrideID = 1

alter table dbo.QuickBMP add DryWeatherFlowOverrideID int null constraint FK_QuickBMP_DryWeatherFlowOverride_DryWeatherFlowOverrideID foreign key references dbo.DryWeatherFlowOverride(DryWeatherFlowOverrideID)
GO

update dbo.QuickBMP
set DryWeatherFlowOverrideID = 1

INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(115, 'DryWeatherFlowOverride', 'Dry Weather Flow Override?', 'Indicates if the modeled values for Dry Weather Flow should be overridden during modeling.', 1)

INSERT [dbo].[FieldDefinitionData] (FieldDefinitionID) 
VALUES 
(115)