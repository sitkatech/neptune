Create Procedure dbo.pDeleteNereidResults (
@isBaselineCondition bit
)

As
Delete from dbo.NereidResult where IsBaselineCondition = @isBaselineCondition
GO