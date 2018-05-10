delete from dbo.CustomAttributeTypePurpose
go
Insert Into dbo.CustomAttributeTypePurpose(CustomAttributeTypePurposeID, CustomAttributeTypePurposeName, CustomAttributeTypePurposeDisplayName)
values
(1, 'PerformanceAndModelingAttributes', 'Performance / Modeling Attributes'),
(2, 'OtherDesignAttributes', 'Other Design Attributes'),
(3, 'Maintenance', 'Maintenance Attributes')