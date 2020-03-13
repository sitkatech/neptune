Create Table LoadGeneratingUnitRefreshArea(
LoadGeneratingUnitRefreshAreaID int not null identity(1,1) constraint PK_LoadGeneratingUnitRefreshArea_LoadGeneratingUnitRefreshAreaID primary key,
LoadGeneratingUnitRefreshAreaGeometry geometry not null,
ProcessDate datetime null
)