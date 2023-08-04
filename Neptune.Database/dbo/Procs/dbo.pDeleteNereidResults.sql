IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'dbo.pDeleteNereidResults')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROCEDURE dbo.pDeleteNereidResults
GO
Create Procedure dbo.pDeleteNereidResults (
@isBaselineCondition bit
)

As
Delete from dbo.NereidResult where IsBaselineCondition = @isBaselineCondition
GO