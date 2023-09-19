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

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.DhtmlWrappers.DhtmlxExport;

namespace Neptune.Web.Controllers
{
//    [ApiController]
    [Route("[controller]/[action]", Name = "[controller]_[action]")]
    public abstract class NeptuneBaseController<T> : SitkaController
    {
        protected IOptions<WebConfiguration> _webConfigurationOptions { get; }
        protected Person CurrentPerson => UserContext.GetUserFromHttpContext(_dbContext, HttpContext);
        protected readonly NeptuneDbContext _dbContext;
        protected readonly ILogger<T> _logger;

        protected readonly LinkGenerator _linkGenerator;

        protected readonly WebConfiguration _webConfiguration;

        protected NeptuneBaseController(NeptuneDbContext dbContext, ILogger<T> logger, LinkGenerator linkGenerator,
            IOptions<WebConfiguration> webConfiguration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _linkGenerator = linkGenerator;
            _webConfigurationOptions = webConfiguration;
            _webConfiguration = webConfiguration.Value;
        }

        protected FileResult ExportGridToExcelImpl(string gridName, bool printFooter)
        {
            var generator = new ExcelWriter { PrintFooter = false };
            var xml = Request.Form["grid_xml"].ToString();
            xml = WebUtility.UrlDecode(xml);
            xml = xml.Replace("<![CDATA[$", "<![CDATA["); // RL 7/11/2015 Poor man's hack to remove currency and allow for total rows
            var stream = generator.Generate(xml);
            return File(stream.ToArray(), generator.ContentType, $"{gridName}.xlsx");
        }
    }
}
