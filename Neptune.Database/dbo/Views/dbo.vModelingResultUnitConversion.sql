create view dbo.vModelingResultUnitConversion
as

select 1 as PrimaryKey, 0.453592 as PoundsToKilogramsFactor, 453.59 as PoundsToGramsFactor, 1e9 as BillionsFactor

GO
/*
select * from dbo.vModelingResultUnitConversion
*/