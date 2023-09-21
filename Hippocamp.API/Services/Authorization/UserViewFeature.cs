﻿using Hippocamp.EFModels.Entities;

namespace Hippocamp.API.Services.Authorization
{
    public class UserViewFeature : BaseAuthorizationAttribute
    {
        public UserViewFeature() : base(new []{RoleEnum.Admin, RoleEnum.SitkaAdmin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor, RoleEnum.Unassigned})
        {
        }
    }
}