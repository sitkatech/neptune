﻿using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows managing documents for a WQMP if you are assigned to its jurisdiction")]
    public class WaterQualityManagementPlanDocumentManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<WaterQualityManagementPlanDocument>
    {
        private readonly NeptuneFeatureWithContextImpl<WaterQualityManagementPlanDocument> _lakeTahoeInfoFeatureWithContextImpl;

        public WaterQualityManagementPlanDocumentManageFeature()
            : base(new List<Role> {Role.JurisdictionManager, Role.Admin, Role.SitkaAdmin})
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<WaterQualityManagementPlanDocument>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public PermissionCheckResult HasPermission(Person person, WaterQualityManagementPlanDocument waterQualityManagementPlanDocument)
        {
            return new WaterQualityManagementPlanManageFeature().HasPermission(person, waterQualityManagementPlanDocument.WaterQualityManagementPlan);
        }

        public void DemandPermission(Person person, WaterQualityManagementPlanDocument contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }
    }
}
