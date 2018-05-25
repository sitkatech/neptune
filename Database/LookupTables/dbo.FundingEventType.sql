delete from dbo.FundingEventType
insert into dbo.FundingEventType (FundingEventTypeID, FundingEventTypeName, FundingEventTypeDisplayName, SortOrder) values
(1, 'PlanningAndDesign', 'Planning & Design', 1),
(2, 'CapitalConstruction', 'Capital Construction', 2),
(3, 'RoutineMaintenance', 'Routine Assessment and Maintenance', 3),
(4, 'RehabilitativeMaintenance', 'Rehabilitative Maintenance', 4),
(5, 'Retrofit', 'Retrofit', 5)