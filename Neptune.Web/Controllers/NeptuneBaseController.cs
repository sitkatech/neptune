/*-----------------------------------------------------------------------
<copyright file="NeptuneBaseController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;

namespace Neptune.Web.Controllers
{
//    [ApiController]
    [Route("[controller]/[action]", Name = "[controller]_[action]")]
    public abstract class NeptuneBaseController<T> : SitkaController
    {
        protected Person CurrentPerson => UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
        protected readonly NeptuneDbContext _dbContext;
        protected readonly ILogger<T> _logger;

        protected readonly LinkGenerator _linkGenerator;

        //        protected readonly WebConfiguration _qanatConfiguration;

        protected NeptuneBaseController(NeptuneDbContext dbContext, ILogger<T> logger, LinkGenerator linkGenerator
        //, IOptions<WebConfiguration> configuration
        )
        {
            _dbContext = dbContext;
            _logger = logger;
            _linkGenerator = linkGenerator;
            //_qanatConfiguration = qanatConfiguration.Value;
        }
    }
}
