delete from dbo.SizingBasisType

Insert into dbo.SizingBasisType (SizingBasisTypeID,SizingBasisTypeName,SizingBasisTypeDisplayName)
values
(1, 'FullTrashCapture', 'Full Trash Capture'),
(2, 'WaterQuality', 'Water Quality'),
(3, 'Other', 'Other (less than Water Quality)'),
(4, 'NotProvided', 'Not Provided')