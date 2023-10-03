/*-----------------------------------------------------------------------
<copyright file="RoleController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Neptune.Web.Security;
using Neptune.Web.Views.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;

namespace Neptune.Web.Controllers
{
    public class RoleController : NeptuneBaseController<RoleController>
    {
        public RoleController(NeptuneDbContext dbContext, ILogger<RoleController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [UserEditFeature]
        [HttpGet]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson);
            return RazorView<Views.Role.Index, IndexViewData>(viewData);
        }

        [UserEditFeature]
        [HttpGet]
        public GridJsonNetJObjectResult<RoleSimpleDto> IndexGridJsonData()
        {
            var gridSpec = new IndexGridSpec(_linkGenerator);
            var roleSummaries = GetRoleSummaryData(_dbContext);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<RoleSimpleDto>(roleSummaries, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [UserEditFeature]
        [HttpGet("{roleID}")]
        public GridJsonNetJObjectResult<Person> PersonWithRoleGridJsonData([FromRoute] int roleID)
        {
            var gridSpec = new PersonWithRoleGridSpec(_linkGenerator);
            var peopleWithRole = People.ListByRoleID(_dbContext, roleID);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Person>(peopleWithRole, gridSpec);
            return gridJsonNetJObjectResult;
        }

        public static List<RoleSimpleDto> GetRoleSummaryData(NeptuneDbContext dbContext)
        {
            //var roles = new List<IRole> {new AnonymousRole()};
            //roles.AddRange(Role.All);
            //return roles.OrderBy(x => x.RoleDisplayName).ToList();
            var peopleDict = People.ListActive(dbContext).GroupBy(x => x.RoleID).ToDictionary(x => x.Key, x => x.Count());
            return Role.All.Select(x =>
            {
                var roleSimpleDto = x.AsSimpleDto();
                roleSimpleDto.PeopleWithRoleCount = peopleDict[x.RoleID];
                return roleSimpleDto;
            }).OrderBy(x => x.RoleDisplayName).ToList();
        }

        //[UserEditFeature]
        //public ViewResult Anonymous()
        //{
        //    return ViewDetail(new AnonymousRole());
        //}

        [UserEditFeature]
        [HttpGet("{roleID}")]
        public ViewResult Detail([FromRoute] int roleID)
        {
            var role = Role.AllLookupDictionary[roleID];
            return ViewDetail(role);
        }

        private ViewResult ViewDetail(IRole role)
        {
            var viewData = new DetailViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, role);
            return RazorView<Detail, DetailViewData>(viewData);
        }
    }
}
