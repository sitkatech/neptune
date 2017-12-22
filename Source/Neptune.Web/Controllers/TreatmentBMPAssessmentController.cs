/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPAssessmentController.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMPAssessment;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPAssessmentController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult Detail(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewData = new DetailViewData(CurrentPerson, treatmentBMPAssessment);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public ViewResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPAssessment = CreatePlaceholderTreatmentBMPAssessment(treatmentBMP);
            var viewModel = new AssessmentInformationViewModel(treatmentBMPAssessment, CurrentPerson);
            return ViewEdit(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, AssessmentInformationViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPAssessment = CreatePlaceholderTreatmentBMPAssessment(treatmentBMP);
            return EditPostImpl(treatmentBMPAssessment, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult Edit(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new AssessmentInformationViewModel(treatmentBMPAssessment, CurrentPerson);
            return ViewEdit(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, AssessmentInformationViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            return EditPostImpl(treatmentBMPAssessment, viewModel);
        }

        private ActionResult EditPostImpl(TreatmentBMPAssessment treatmentBMPAssessment, AssessmentInformationViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(treatmentBMPAssessment, viewModel);
            }

            if (!ModelObjectHelpers.IsRealPrimaryKeyValue(treatmentBMPAssessment.PrimaryKey))
            {

                HttpRequestStorage.DatabaseEntities.AllTreatmentBMPAssessments.Add(treatmentBMPAssessment);
                HttpRequestStorage.DatabaseEntities.SaveChanges();
            }

            viewModel.UpdateModel(treatmentBMPAssessment, CurrentPerson);
            
            SetMessageForDisplay("Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, null)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.Edit(treatmentBMPAssessment.TreatmentBMPAssessmentID)));

        }

        private ViewResult ViewEdit(TreatmentBMPAssessment treatmentBMPAssessment, AssessmentInformationViewModel viewModel)
        {
            var stormwaterJurisdictionPeople = treatmentBMPAssessment.TreatmentBMP.StormwaterJurisdiction.PeopleWhoCanManageStormwaterJurisdictionExceptSitka().ToList();
            stormwaterJurisdictionPeople.AddRange(new[] {CurrentPerson});

            if (!stormwaterJurisdictionPeople.Contains(treatmentBMPAssessment.Person))
            {
                stormwaterJurisdictionPeople.Add(treatmentBMPAssessment.Person);
            }

            stormwaterJurisdictionPeople = stormwaterJurisdictionPeople.Distinct().ToList();

            var peopleSelectList = stormwaterJurisdictionPeople.ToSelectList(p => p.PersonID.ToString(CultureInfo.InvariantCulture), p => p.FullNameFirstLastAndOrgAbbreviation);
            
            var assessmentTypes = StormwaterAssessmentType.All.ToSelectList(x => x.StormwaterAssessmentTypeID.ToString(CultureInfo.InvariantCulture), x => x.StormwaterAssessmentTypeDisplayName);

            var viewData = new AssessmentInformationViewData(CurrentPerson, treatmentBMPAssessment, peopleSelectList, assessmentTypes);
            return RazorView<AssessmentInformation, AssessmentInformationViewData, AssessmentInformationViewModel>(viewData, viewModel);
        }        

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public PartialViewResult Delete(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMPAssessment.TreatmentBMPAssessmentID);
            return ViewDeleteTreatmentBMPAssessment(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var treatmentBMPID = treatmentBMPAssessment.TreatmentBMPID;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMPAssessment(treatmentBMPAssessment, viewModel);
            }


            treatmentBMPAssessment.TreatmentBMPObservations.SelectMany(x => x.TreatmentBMPObservationDetails.SelectMany(y => y.TreatmentBMPInfiltrationReadings)).ToList().DeleteTreatmentBMPInfiltrationReading();
            treatmentBMPAssessment.TreatmentBMPObservations.SelectMany(x => x.TreatmentBMPObservationDetails).ToList().DeleteTreatmentBMPObservationDetail();
            treatmentBMPAssessment.TreatmentBMPObservations.DeleteTreatmentBMPObservation();
            treatmentBMPAssessment.DeleteTreatmentBMPAssessment();

            SetMessageForDisplay("BMP Assessment successfully deleted.");

            return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.Detail(treatmentBMPID)));
        }

        private PartialViewResult ViewDeleteTreatmentBMPAssessment(TreatmentBMPAssessment treatmentBMPAssessment, ConfirmDialogFormViewModel viewModel)
        {
            var waterYear = treatmentBMPAssessment.GetWaterYear();
            var canDelete = treatmentBMPAssessment.CanDelete(CurrentPerson, waterYear);
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete the assessment dated {treatmentBMPAssessment.AssessmentDate.ToShortDateString()}?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Treatment BMP", SitkaRoute<TreatmentBMPAssessmentController>.BuildLinkFromExpression(x => x.Detail(treatmentBMPAssessment), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        private TreatmentBMPAssessment CreatePlaceholderTreatmentBMPAssessment(TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPAssessment(treatmentBMP, StormwaterAssessmentType.Regular, DateTime.Now, CurrentPerson, false);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMPAssessment> AssessmentGridJsonData(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            TreatmentBMPAssessmentGridSpec gridSpec;
            var treatmentBMPs = GetTreatmentBMPsAndGridSpec(out gridSpec, CurrentPerson, treatmentBMP);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMPAssessment>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TreatmentBMPAssessment> GetTreatmentBMPsAndGridSpec(out TreatmentBMPAssessmentGridSpec gridSpec, Person currentPerson, TreatmentBMP treatmentBMP)
        {
            gridSpec = new TreatmentBMPAssessmentGridSpec(currentPerson);
            return HttpRequestStorage.DatabaseEntities.TreatmentBMPAssessments.Where(x => x.TreatmentBMPID == treatmentBMP.TreatmentBMPID).ToList();
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult ConveyanceFunction(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new ConveyanceFunctionViewModel(treatmentBMPAssessment);
            return ViewConveyanceFunction(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ConveyanceFunction(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, ConveyanceFunctionViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewConveyanceFunction(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.ConveyanceFunction.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.ConveyanceFunction, ObservationValueType.booleanType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local);

            SetMessageForDisplay("Conveyance Function Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.ConveyanceFunction)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.ConveyanceFunction(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewConveyanceFunction(TreatmentBMPAssessment treatmentBMPAssessment, ConveyanceFunctionViewModel viewModel)
        {
            var viewData = new ConveyanceFunctionViewData(CurrentPerson, treatmentBMPAssessment);
            return RazorView<ConveyanceFunction, ConveyanceFunctionViewData, ConveyanceFunctionViewModel>(viewData, viewModel);
        }
       
        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult MaterialAccumulation(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new MaterialAccumulationViewModel(treatmentBMPAssessment);
            return ViewMaterialAccumulation(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult MaterialAccumulation(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, MaterialAccumulationViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewMaterialAccumulation(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.MaterialAccumulation.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.MaterialAccumulation, ObservationValueType.floatType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local);

            SetMessageForDisplay("Material Accumulation Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.MaterialAccumulation)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.MaterialAccumulation(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewMaterialAccumulation(TreatmentBMPAssessment treatmentBMPAssessment, MaterialAccumulationViewModel viewModel)
        {
            var observationDetailTypes = ObservationType.MaterialAccumulation.TreatmentBMPObservationDetailTypes.ToList();
            var viewData = new MaterialAccumulationViewData(CurrentPerson, treatmentBMPAssessment, observationDetailTypes);
            return RazorView<MaterialAccumulation, MaterialAccumulationViewData, MaterialAccumulationViewModel>(viewData, viewModel);
        }        

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult SedimentTrapCapacity(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new SedimentTrapCapacityViewModel(treatmentBMPAssessment);
            return ViewSedimentTrapCapacity(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult SedimentTrapCapacity(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, SedimentTrapCapacityViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewSedimentTrapCapacity(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.SedimentTrapCapacity.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.SedimentTrapCapacity, ObservationValueType.floatType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local);

            SetMessageForDisplay("Sediment Trap Capacity Assessment successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.SedimentTrapCapacity)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.SedimentTrapCapacity(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewSedimentTrapCapacity(TreatmentBMPAssessment treatmentBMPAssessment, SedimentTrapCapacityViewModel viewModel)
        {
            var observationDetailTypes = ObservationType.SedimentTrapCapacity.TreatmentBMPObservationDetailTypes.ToList();
            var viewData = new SedimentTrapCapacityViewData(CurrentPerson, treatmentBMPAssessment, observationDetailTypes);
            return RazorView<SedimentTrapCapacity, SedimentTrapCapacityViewData, SedimentTrapCapacityViewModel>(viewData, viewModel);
        }
       
        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult VaultCapacity(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new VaultCapacityViewModel(treatmentBMPAssessment);
            return ViewVaultCapacity(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult VaultCapacity(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, VaultCapacityViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewVaultCapacity(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.VaultCapacity.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.VaultCapacity, ObservationValueType.floatType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local);

            SetMessageForDisplay("Vault Capacity Assessment successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.VaultCapacity)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.VaultCapacity(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewVaultCapacity(TreatmentBMPAssessment treatmentBMPAssessment, VaultCapacityViewModel viewModel)
        {
            var observationDetailTypes = ObservationType.VaultCapacity.TreatmentBMPObservationDetailTypes.ToList();
            var viewData = new VaultCapacityViewData(CurrentPerson, treatmentBMPAssessment, observationDetailTypes);
            return RazorView<VaultCapacity, VaultCapacityViewData, VaultCapacityViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult Runoff(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new RunoffViewModel(treatmentBMPAssessment);
            return ViewRunoff(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Runoff(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, RunoffViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewRunoff(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.Runoff.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.Runoff, ObservationValueType.floatType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local);

            SetMessageForDisplay("Runoff Assessment successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.Runoff)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.Runoff(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewRunoff(TreatmentBMPAssessment treatmentBMPAssessment, RunoffViewModel viewModel)
        {
            var observationDetailTypes = ObservationType.Runoff.TreatmentBMPObservationDetailTypes.ToList();
            var viewData = new RunoffViewData(CurrentPerson, treatmentBMPAssessment, observationDetailTypes);
            return RazorView<Runoff, RunoffViewData, RunoffViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult InfiltrationRate(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new InfiltrationRateViewModel(treatmentBMPAssessment);
            return ViewInfiltrationRate(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult InfiltrationRate(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, InfiltrationRateViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewInfiltrationRate(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.InfiltrationRate.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.InfiltrationRate, ObservationValueType.floatType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();
            HttpRequestStorage.DatabaseEntities.TreatmentBMPInfiltrationReadings.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPInfiltrationReadings.Local);

            SetMessageForDisplay("Infiltration Rate Observations successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.InfiltrationRate)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.InfiltrationRate(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewInfiltrationRate(TreatmentBMPAssessment treatmentBMPAssessment, InfiltrationRateViewModel viewModel)
        {
            var treatmentBMPObservationDetailTypesToExclude = new List<TreatmentBMPObservationDetailType>();

            if (treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType == TreatmentBMPType.PorousPavement)
            {
                treatmentBMPObservationDetailTypesToExclude.Add(TreatmentBMPObservationDetailType.ConstantHeadPermeameter);
            }
            var observationDetailTypes = ObservationType.InfiltrationRate.TreatmentBMPObservationDetailTypes.Except(treatmentBMPObservationDetailTypesToExclude).ToList();
            var viewData = new InfiltrationRateViewData(CurrentPerson, treatmentBMPAssessment, observationDetailTypes);
            return RazorView<InfiltrationRate, InfiltrationRateViewData, InfiltrationRateViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult StandingWater(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new StandingWaterViewModel(treatmentBMPAssessment);
            return ViewStandingWater(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult StandingWater(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, StandingWaterViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewStandingWater(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.StandingWater.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.StandingWater, ObservationValueType.booleanType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local);

            SetMessageForDisplay("Standing Water Assessment successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.StandingWater)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.StandingWater(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewStandingWater(TreatmentBMPAssessment treatmentBMPAssessment, StandingWaterViewModel viewModel)
        {
            var observationDetailTypes = ObservationType.StandingWater.TreatmentBMPObservationDetailTypes.ToList();
            var viewData = new StandingWaterViewData(CurrentPerson, treatmentBMPAssessment, observationDetailTypes);
            return RazorView<StandingWater, StandingWaterViewData, StandingWaterViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult VegetativeCover(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new VegetativeCoverViewModel(treatmentBMPAssessment);
            return ViewVegetativeCover(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult VegetativeCover(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, VegetativeCoverViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewVegetativeCover(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.VegetativeCover.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.VegetativeCover, ObservationValueType.floatType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local);

            SetMessageForDisplay("Vegetative Cover Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.VegetativeCover)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.VegetativeCover(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewVegetativeCover(TreatmentBMPAssessment treatmentBMPAssessment, VegetativeCoverViewModel viewModel)
        {
            var viewData = new VegetativeCoverViewData(CurrentPerson, treatmentBMPAssessment);
            return RazorView<VegetativeCover, VegetativeCoverViewData, VegetativeCoverViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult WetBasinVegetativeCover(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new WetBasinVegetativeCoverViewModel(treatmentBMPAssessment);
            return ViewWetBasinVegetativeCover(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult WetBasinVegetativeCover(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, WetBasinVegetativeCoverViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewWetBasinVegetativeCover(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.WetBasinVegetativeCover.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.WetBasinVegetativeCover, ObservationValueType.floatType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local);

            SetMessageForDisplay("Vegetative Cover Assessment Information successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.WetBasinVegetativeCover)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.WetBasinVegetativeCover(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewWetBasinVegetativeCover(TreatmentBMPAssessment treatmentBMPAssessment, WetBasinVegetativeCoverViewModel viewModel)
        {
            var viewData = new WetBasinVegetativeCoverViewData(CurrentPerson, treatmentBMPAssessment);
            return RazorView<WetBasinVegetativeCover, WetBasinVegetativeCoverViewData, WetBasinVegetativeCoverViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult Installation(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new InstallationViewModel(treatmentBMPAssessment);
            return ViewInstallation(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Installation(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, InstallationViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewInstallation(treatmentBMPAssessment, viewModel);
            }

            var existingObservation = treatmentBMPAssessment.TreatmentBMPObservations.ToList().Find(x => x.ObservationType.ObservationTypeID == ObservationType.Installation.ObservationTypeID);
            var treatmentBMPObservation = existingObservation ?? new TreatmentBMPObservation(treatmentBMPAssessment, ObservationType.Installation, ObservationValueType.booleanType);

            HttpRequestStorage.DatabaseEntities.TreatmentBMPObservationDetails.Load();

            viewModel.UpdateModel(treatmentBMPObservation, HttpRequestStorage.DatabaseEntities.AllTreatmentBMPObservationDetails.Local);

            SetMessageForDisplay("Installation Assessment successfully saved.");

            return viewModel.AutoAdvance
                ? GetNextObservationTypeViewResult(treatmentBMPAssessment, ObservationType.Installation)
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.Installation(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewInstallation(TreatmentBMPAssessment treatmentBMPAssessment, InstallationViewModel viewModel)
        {
            var observationDetailTypes = ObservationType.Installation.TreatmentBMPObservationDetailTypes.ToList();
            var viewData = new InstallationViewData(CurrentPerson, treatmentBMPAssessment, observationDetailTypes);
            return RazorView<Installation, InstallationViewData, InstallationViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPAssessmentManageFeature]
        public ViewResult Score(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            var viewModel = new ScoreViewModel(treatmentBMPAssessment);
            return ViewScore(treatmentBMPAssessment, viewModel);
        }

        [HttpPost]
        [TreatmentBMPAssessmentManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Score(TreatmentBMPAssessmentPrimaryKey treatmentBMPAssessmentPrimaryKey, ScoreViewModel viewModel)
        {
            var treatmentBMPAssessment = treatmentBMPAssessmentPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewScore(treatmentBMPAssessment, viewModel);
            }
            
            viewModel.UpdateModel(treatmentBMPAssessment, CurrentPerson);

            SetMessageForDisplay("Score successfully saved.");

            return viewModel.AutoAdvance
                ? RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.Detail(treatmentBMPAssessment.TreatmentBMPAssessmentID)))
                : RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(c => c.Score(treatmentBMPAssessment.TreatmentBMPAssessmentID)));
        }

        private ViewResult ViewScore(TreatmentBMPAssessment treatmentBMPAssessment, ScoreViewModel viewModel)
        {
            var viewData = new ScoreViewData(CurrentPerson, treatmentBMPAssessment);
            return RazorView<Score, ScoreViewData, ScoreViewModel>(viewData, viewModel);
        }   
 

        private RedirectResult GetNextObservationTypeViewResult(TreatmentBMPAssessment treatmentBMPAssessment, ObservationType observationType)
        {
            //Null observationType means we are on the Assessment Information page, in which case dummy in a sort order which is guaranteed to return the actual lowest sort order as the next page.
            var observationTypeSortOrder = observationType == null ? int.MinValue : observationType.SortOrder;

            var nextObservationType = treatmentBMPAssessment.TreatmentBMP.TreatmentBMPType.GetObservationTypes().OrderBy(x => x.SortOrder).FirstOrDefault(x => x.SortOrder > observationTypeSortOrder);
            var isNextPageScore = nextObservationType == null;

            var nextObservationTypeViewResult = isNextPageScore
                ? RedirectToAction(new SitkaRoute<TreatmentBMPAssessmentController>(x => x.Score(treatmentBMPAssessment.TreatmentBMPAssessmentID)))
                : Redirect(nextObservationType.AssessmentUrl(treatmentBMPAssessment.TreatmentBMPAssessmentID));
            return nextObservationTypeViewResult;
        }

    }

}
