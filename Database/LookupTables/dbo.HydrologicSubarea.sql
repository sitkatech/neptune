DELETE FROM dbo.HydrologicSubarea
GO

INSERT INTO dbo.HydrologicSubarea(HydrologicSubareaID, HydrologicSubareaName, HydrologicSubareaDisplayName, SortOrder)
VALUES
(1, 'AlisoCreek', 'Aliso Creek', 10),
(2, 'DanaPointCoastal', 'Dana Point Coastal', 20),
(3, 'LagunaCoastal', 'Laguna Coastal', 30),
(4, 'SanClementeCoastal', 'San Clemente Coastal', 40),
(5, 'SanJuanCreek', 'San Juan Creek', 50);
