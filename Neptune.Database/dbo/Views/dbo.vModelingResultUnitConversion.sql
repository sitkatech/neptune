if exists (select * from dbo.sysobjects where id = object_id('dbo.vModelingResultUnitConversion'))
    drop view dbo.vModelingResultUnitConversion
go

create view dbo.vModelingResultUnitConversion
as

select 1 as PrimaryKey, 0.453592 as PoundsToKilogramsFactor, 453.59 as PoundsToGramsFactor, 1e9 as BillionsFactor

GO
/*
select * from dbo.vModelingResultUnitConversion
*/