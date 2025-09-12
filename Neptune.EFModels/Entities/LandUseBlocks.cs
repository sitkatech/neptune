﻿using Microsoft.EntityFrameworkCore;
using Neptune.Common.DesignByContract;
using Neptune.Models.DataTransferObjects;
using System.ComponentModel.DataAnnotations;

namespace Neptune.EFModels.Entities;

public static class LandUseBlocks
{
    public static LandUseBlock GetByIDWithChangeTracking(NeptuneDbContext dbContext, int landUseBlockID)
    {
        var landUseBlock = dbContext.LandUseBlocks
            .SingleOrDefault(x => x.LandUseBlockID == landUseBlockID);
        Check.RequireNotNull(landUseBlock, $"Land Use Block with ID {landUseBlockID} not found!");
        return landUseBlock;
    }
    public static List<LandUseBlockGridDto> List(NeptuneDbContext dbContext)
    {
        var landUseBlocks = dbContext.LandUseBlocks.AsNoTracking()
            .Include(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
            .Include(x => x.TrashGeneratingUnits)
            .Select(x => x.AsGridDto()).ToList();
        return landUseBlocks;
    }

    public static async Task Update(NeptuneDbContext dbContext, LandUseBlock landUseBlock, LandUseBlockUpsertDto landUseBlockUpsertDto, int personID)
    {
        landUseBlock.PriorityLandUseTypeID = landUseBlockUpsertDto.PriorityLandUseTypeID;
        landUseBlock.TrashGenerationRate = landUseBlockUpsertDto.TrashGenerationRate;
        landUseBlock.LandUseDescription = landUseBlockUpsertDto.LandUseDescription;
        landUseBlock.MedianHouseholdIncomeResidential = landUseBlockUpsertDto.MedianHouseholdIncomeResidential;
        landUseBlock.MedianHouseholdIncomeRetail = landUseBlockUpsertDto.MedianHouseholdIncomeRetail;
        landUseBlock.PermitTypeID = landUseBlockUpsertDto.PermitTypeID;
        landUseBlock.UpdatePersonID = personID;
        landUseBlock.DateUpdated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
    }
}