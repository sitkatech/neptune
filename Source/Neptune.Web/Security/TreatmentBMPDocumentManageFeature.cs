using System.Collections.Generic;
using Neptune.Web.Models;

namespace Neptune.Web.Security
{
    [SecurityFeatureDescription("Allows editing a Treatment BMP Assessment if you are assigned to manage that BMP's jurisdiction")]
    public class TreatmentBMPDocumentManageFeature : NeptuneFeatureWithContext, INeptuneBaseFeatureWithContext<TreatmentBMPDocument>
    {
        private readonly NeptuneFeatureWithContextImpl<TreatmentBMPDocument> _lakeTahoeInfoFeatureWithContextImpl;

        public TreatmentBMPDocumentManageFeature()
            : base(new List<Role> { Role.SitkaAdmin, Role.Admin, Role.Normal })
        {
            _lakeTahoeInfoFeatureWithContextImpl = new NeptuneFeatureWithContextImpl<TreatmentBMPDocument>(this);
            ActionFilter = _lakeTahoeInfoFeatureWithContextImpl;
        }

        public void DemandPermission(Person person, TreatmentBMPDocument contextModelObject)
        {
            _lakeTahoeInfoFeatureWithContextImpl.DemandPermission(person, contextModelObject);
        }

        public PermissionCheckResult HasPermission(Person person, TreatmentBMPDocument contextModelObject)
        {
            return new TreatmentBMPManageFeature().HasPermission(person, contextModelObject.TreatmentBMP);
        }
    }
}