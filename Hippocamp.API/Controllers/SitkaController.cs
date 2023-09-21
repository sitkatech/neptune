using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Hippocamp.API.Services;
using Hippocamp.EFModels.Entities;

namespace Hippocamp.API.Controllers
{
    public abstract class SitkaController<T> : ControllerBase
    {
        protected readonly HippocampDbContext _dbContext;
        protected readonly ILogger<T> _logger;
        protected readonly KeystoneService _keystoneService;
        protected readonly HippocampConfiguration _hippocampConfiguration;

        protected SitkaController(HippocampDbContext dbContext, ILogger<T> logger, KeystoneService keystoneService, IOptions<HippocampConfiguration> hippocampConfiguration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _keystoneService = keystoneService;
            _hippocampConfiguration = hippocampConfiguration.Value;
        }

        protected ActionResult RequireNotNullThrowNotFound(object theObject, string objectType, object objectID)
        {
            return ThrowNotFound(theObject, objectType, objectID, out var actionResult) ? actionResult : Ok(theObject);
        }

        protected bool ThrowNotFound(object theObject, string objectType, object objectID, out ActionResult actionResult)
        {
            if (theObject == null)
            {
                var notFoundMessage = $"{objectType} with ID {objectID} does not exist!";
                _logger.LogError(notFoundMessage);
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