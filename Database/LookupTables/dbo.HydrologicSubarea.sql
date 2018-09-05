DELETE FROM dbo.HydrologicSubarea
GO

INSERT INTO dbo.HydrologicSubarea(HydrologicSubareaID, HydrologicSubareaName, HydrologicSubareaDisplayName, SortOrder)
VALUES
(1, 'LagunaCoastal', 'Laguna Coastal', 10),
(2, 'AlisoCreek', 'Aliso Creek', 20),
(3, 'DanaPointCoastal', 'Dana Point Coastal', 30),
(4, 'SanJuanCreek', 'San Juan Creek', 40),
(5, 'SanClementeCoastal', 'San Clemente Coastal', 50);