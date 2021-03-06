﻿using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows editing a Treatment BMP Assessment Photo if you are assigned to manage that BMP's jurisdiction")]
    public class TreatmentBMPAssessmentPhotoManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<TreatmentBMPAssessmentPhoto>
    {
        private readonly NeptuneFeatureWithContextImpl<TreatmentBMPAssessmentPhoto> _lakeTahoeInfoFeatureWithContextImpl;

        public TreatmentBMPAssessmentPhotoManageFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.JurisdictionEditor, Role.JurisdictionManager })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<TreatmentBMPAssessmentPhoto>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, TreatmentBMPAssessmentPhoto contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMPAssessmentPhoto contextModelObject)
        {
            return new TreatmentBMPManageFeature().HasPermission(person, contextModelObject.TreatmentBMPAssessment.TreatmentBMP);
        }
    }
}
