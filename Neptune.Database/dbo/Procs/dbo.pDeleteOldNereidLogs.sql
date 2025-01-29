Create Procedure dbo.pDeleteOldNereidLogs
with execute as owner
As

delete from dbo.NereidLog
where NereidLogID not in
(
    select nl.NereidLogID
    from dbo.NereidLog nl
    join dbo.TreatmentBMP t on nl.NereidLogID = t.LastNereidLogID

    union

    select nl.NereidLogID
    from dbo.NereidLog nl
    join dbo.WaterQualityManagementPlan t on nl.NereidLogID = t.LastNereidLogID
)

GO