drop procedure if exists dbo.pProjectGrantScoreUpdate
GO

Create Procedure dbo.pProjectGrantScoreUpdate
(
	@projectID int
)
As

update p
set p.OCTAWatersheds  = pgs.Watersheds,
    p.PollutantVolume  = pgs.PollutantVolume,
    p.PollutantMetals = pgs.PollutantMetals,
    p.PollutantBacteria = pgs.PollutantBacteria,
    p.PollutantNutrients = pgs.PollutantNutrients,
    p.PollutantTSS = pgs.PollutantTSS,
    p.TPI = pgs.TPI,
    p.SEA = pgs.SEA,
    p.DryWeatherWQLRI = pgs.DryWeatherWQLRI,
    p.WetWeatherWQLRI = pgs.WetWeatherWQLRI,
    p.AreaTreatedAcres = pgs.ProjectArea,
    p.ImperviousAreaTreatedAcres = pgs.ImperviousAreaTreatedAcres
from dbo.Project p
join dbo.fProjectGrantScore(@projectID) pgs on p.ProjectID = pgs.ProjectID

GO