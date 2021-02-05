alter table dbo.RegionalSubbasin
Add LSPCBasinID int null
constraint FK_RegionalSubbasin_LSPCBasin_LSPCBasinID foreign key references dbo.LSPCBasin(LSPCBasinID)