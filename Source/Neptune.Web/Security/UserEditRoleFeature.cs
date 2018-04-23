﻿using System;
using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Edit a User's Role")]
    public class UserEditRoleFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<Person>
    {
        private readonly NeptuneFeatureWithContextImpl<Person> _neptuneFeatureWithContextImpl;

        public UserEditRoleFeature()
            : base(new List<Role> { Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin })
        {
            _neptuneFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<Person>(this);
            ActionFilter = _neptuneFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, Person contextModelObject)
        {
            _neptuneFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }


        public PermissionCheckResult HasPermission(Person person, Person contextModelObject)
        {
            if (contextModelObject == null)
            {
                return new PermissionCheckResult("The Person who you are requesting to edit doesn't exist.");
            }
            
            //Only SitkaAdmin users should be able to see other SitkaAdmin users
            var currentPersonIsAdmin = person.Role == Role.SitkaAdmin || person.Role == Role.Admin;
            var personBeingEditedIsAdmin = contextModelObject.Role == Role.SitkaAdmin || contextModelObject.Role == Role.Admin;

            if (!currentPersonIsAdmin && personBeingEditedIsAdmin)
            {
                return new PermissionCheckResult("You don\'t have permission to edit this user's role.");
            }

            return new PermissionCheckResult();
        }
      
    }
}