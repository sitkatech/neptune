using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Neptune.EFModels.Entities;
using System.Net;

namespace Neptune.Web.Services.Filters
{

    public class ValidateEntityExistsAndPopulateParameterFilter : TypeFilterAttribute
    {
        public ValidateEntityExistsAndPopulateParameterFilter(params string[] primaryKeyStringInRoute) : base(typeof(ValidateStructureExistsAndPopulateParameterImpl))
        {
            Arguments = new object[] { primaryKeyStringInRoute };
        }

        private class ValidateStructureExistsAndPopulateParameterImpl : ActionFilterAttribute, IAsyncActionFilter
        {
            private readonly NeptuneDbContext _dbContext;
            private readonly string _primaryKeyStringInRoute;

            public ValidateStructureExistsAndPopulateParameterImpl(NeptuneDbContext dbContext, string[] primaryKeyStringInRoute)
            {
                _dbContext = dbContext;
                _primaryKeyStringInRoute = primaryKeyStringInRoute[0];
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var idParameter = context.RouteData.Values[_primaryKeyStringInRoute].ToString();
                var parameterIsInt = int.TryParse(idParameter, out var entityID);

                var primaryKeyType = context.ActionArguments.First().Value.GetType();
                
                var baseType = primaryKeyType.BaseType;
                var entityType = baseType.GenericTypeArguments.First();
                // TODO: Error handling!
                if (!parameterIsInt)
                {
                    // context.Result = new SitkaRecordNotFoundException(entityType.Name, idParameter);
                    context.Result = new NotFoundObjectResult($"Could not find a {entityType.Name} with the ID {idParameter}.");
                    return;
                }

                dynamic entity;
                try
                {
                    entity = await _dbContext.FindAsync(entityType, entityID);
                }
                catch
                {
                    context.Result = new NotFoundObjectResult($"Could not find a {entityType.Name} with the ID {idParameter}.");
                    return;
                }
                var param = context.ActionDescriptor.Parameters.FirstOrDefault(x => x is ControllerParameterDescriptor descriptor && descriptor.ParameterType == primaryKeyType);
                if (param != null)
                {
                    dynamic primaryKeyObject = Activator.CreateInstance(primaryKeyType);
                    primaryKeyType.GetProperty("EntityObject").SetValue(primaryKeyObject, entity);
                    primaryKeyType.GetProperty("PrimaryKeyValue").SetValue(primaryKeyObject, entityID);
                    
                    context.ActionArguments[param.Name] = primaryKeyObject;
                }
                await next();
            }
        }
    }
}
