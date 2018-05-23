/*-----------------------------------------------------------------------
<copyright file="FieldVisitController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.FieldVisit;

namespace Neptune.Web.Controllers
{
    public class FieldVisitController : NeptuneBaseController
    {
        [HttpGet]
        [FieldVisitCreateFeature]
        public PartialViewResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new NewFieldVisitViewModel(treatmentBMP.InProgressFieldVisit != null);
            return ViewNew(treatmentBMP, viewModel);
        }

        private PartialViewResult ViewNew(TreatmentBMP treatmentBMP, NewFieldVisitViewModel viewModel)
        {
            var viewData = new NewFieldVisitViewData(treatmentBMP);
            return RazorPartialView<NewFieldVisit, NewFieldVisitViewData, NewFieldVisitViewModel>(viewData, viewModel);
        }

        [HttpPost]
        [FieldVisitCreateFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, NewFieldVisitViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewNew(treatmentBMP, viewModel);
            }

            FieldVisit fieldVisit;
            if (viewModel.Continue == null)
            {
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now);
                HttpRequestStorage.DatabaseEntities.AllFieldVisits.Add(fieldVisit);
            } else if (viewModel.Continue == false)
            {
                var oldFieldVisit = treatmentBMP.InProgressFieldVisit;
                oldFieldVisit.FieldVisitStatusID = FieldVisitStatus.Unresolved.FieldVisitStatusID;
                fieldVisit = new FieldVisit(treatmentBMP, FieldVisitStatus.InProgress, CurrentPerson, DateTime.Now);
            }
            else // if Continue == true
            {
                fieldVisit = treatmentBMP.InProgressFieldVisit;
            }

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return new ModalDialogFormJsonResult(SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x=>x.Inventory(fieldVisit)));
        }

        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Inventory(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            var fieldVisit = fieldVisitPrimaryKey.EntityObject;
            var viewData = new InventoryViewData(CurrentPerson, fieldVisit);
            return RazorView<Inventory, InventoryViewData>(viewData);
        }
        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Location(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Photos(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Attributes(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Assess(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult Maintain(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult PostMaintenanceAssessment(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult WrapUpVisit(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [FieldVisitEditFeature]
        public ViewResult ManageVisit(FieldVisitPrimaryKey fieldVisitPrimaryKey)
        {
            throw new NotImplementedException();
        }
    }
}
