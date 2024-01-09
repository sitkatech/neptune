using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Neptune.Common;
using Neptune.EFModels.Entities;

namespace Neptune.WebMvc.Services.Filters
{

    public class ValidateEntityExistsAndPopulateParameterFilter : TypeFilterAttribute
    {
        public ValidateEntityExistsAndPopulateParameterFilter(params string[] primaryKeyStringInRoute) : base(typeof(ValidateStructureExistsAndPopulateParameterImpl))
        {
            Arguments = new object[] { primaryKeyStringInRoute };
            Order = -1; // this ensures that this filter will execute first, since the feature with context ones need the primarykeyobject set
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
                if (!parameterIsInt)
                {
                    throw new SitkaRecordNotFoundException($"Could not find a {entityType.Name} with the ID {idParameter}.");
                }

                dynamic entity;
                try
                {
                    entity = await _dbContext.FindAsync(entityType, entityID);
                }
                catch
                {
                    throw new SitkaRecordNotFoundException($"Could not find a {entityType.Name} with the ID {idParameter}.");
                }

                if (entity == null)
                {
                    throw new SitkaRecordNotFoundException($"Could not find a {entityType.Name} with the ID {idParameter}.");
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
