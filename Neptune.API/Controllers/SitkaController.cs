using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Controllers
{
    public abstract class SitkaController<T>(
        NeptuneDbContext dbContext,
        ILogger<T> logger,
        KeystoneService keystoneService,
        IOptions<NeptuneConfiguration> neptuneConfiguration)
        : ControllerBase
    {
        protected readonly NeptuneDbContext DbContext = dbContext;
        protected readonly ILogger<T> Logger = logger;
        protected readonly KeystoneService KeystoneService = keystoneService;
        protected readonly NeptuneConfiguration NeptuneConfiguration = neptuneConfiguration.Value;

        protected PersonDto CallingUser => UserContext.GetUserFromHttpContext(dbContext, HttpContext);

        protected ActionResult RequireNotNullThrowNotFound(object theObject, string objectType, object objectID)
        {
            return ThrowNotFound(theObject, objectType, objectID, out var actionResult) ? actionResult : Ok(theObject);
        }

        protected bool ThrowNotFound(object theObject, string objectType, object objectID, out ActionResult actionResult)
        {
            if (theObject == null)
            {
                var notFoundMessage = $"{objectType} with ID {objectID} does not exist!";
                Logger.LogError(notFoundMessage);
                {
                    actionResult = NotFound(notFoundMessage);
                    return true;
                }
            }

            actionResult = null;
            return false;
        }
    }
}