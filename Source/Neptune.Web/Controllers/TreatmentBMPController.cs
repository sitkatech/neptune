/*-----------------------------------------------------------------------
<copyright file="TreatmentBMPController.cs" company="Tahoe Regional Planning Agency">
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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LtInfo.Common.Models;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;
using Neptune.Web.Views.TreatmentBMP;
using Newtonsoft.Json;
using Index = Neptune.Web.Views.TreatmentBMP.Index;
using IndexViewData = Neptune.Web.Views.TreatmentBMP.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class TreatmentBMPController : NeptuneBaseController
    {
        [NeptuneViewFeature]
        public ViewResult Index()
        {
            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList().Where(x => x.CanView(CurrentPerson));
            var mapInitJson = new SearchMapInitJson("StormwaterIndexMap", StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(treatmentBMPs, false, false));
            var jurisdictionLayerGeoJson = mapInitJson.Layers.Single(x => x.LayerName == MapInitJson.CountyCityLayerName);
            jurisdictionLayerGeoJson.LayerOpacity = 0;
            jurisdictionLayerGeoJson.LayerInitialVisibility = LayerInitialVisibility.Show;


            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.TreatmentBMP);
            var viewData = new IndexViewData(CurrentPerson, mapInitJson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneViewFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> TreatmentBMPGridJsonData()
        {
            TreatmentBMPGridSpec gridSpec;
            var treatmentBMPs = GetTreatmentBMPsAndGridSpec(out gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TreatmentBMP> GetTreatmentBMPsAndGridSpec(out TreatmentBMPGridSpec gridSpec, Person currentPerson)
        {
            gridSpec = new TreatmentBMPGridSpec(currentPerson);
            return HttpRequestStorage.DatabaseEntities.TreatmentBMPs.ToList().Where(x => x.CanView(CurrentPerson)).ToList();
        }

        [NeptuneViewFeature]
        public ViewResult Detail(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var mapInitJson = new StormwaterMapInitJson("StormwaterDetailMap");
            mapInitJson.Layers.Add(StormwaterMapInitJson.MakeTreatmentBMPLayerGeoJson(new[] {treatmentBMP}, false, true));

            var viewData = new DetailViewData(CurrentPerson, treatmentBMP, mapInitJson);
            return RazorView<Detail, DetailViewData>(viewData);
        }

        [HttpGet]
        [NeptuneEditFeature]
        public ViewResult New()
        {
            var viewModel = new EditViewModel();
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [NeptuneEditFeature]
        public ActionResult New(EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            
            var treatmentBMP = MakePlaceholderTreatmentBMP(viewModel);
            viewModel.UpdateModel(treatmentBMP, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.AllTreatmentBMPs.Add(treatmentBMP);
            HttpRequestStorage.DatabaseEntities.SaveChanges(CurrentPerson);

            SetMessageForDisplay("Treatment BMP successfully saved.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private static TreatmentBMP MakePlaceholderTreatmentBMP(EditViewModel viewModel)
        {
            return new TreatmentBMP(string.Empty, viewModel.TreatmentBMPTypeID, viewModel.StormwaterJurisdictionID);
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public ViewResult Edit(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var treatmentBMPLocationPoint = treatmentBMP.LocationPoint;
            var viewModel = new EditViewModel(treatmentBMP, treatmentBMPLocationPoint);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            viewModel.UpdateModel(treatmentBMP, CurrentPerson);

            SetMessageForDisplay("Treatment BMP successfully saved.");

            return RedirectToAction(new SitkaRoute<TreatmentBMPController>(c => c.Detail(treatmentBMP.PrimaryKey)));
        }

        private ViewResult ViewEdit(EditViewModel viewModel)
        {
            var stormwaterJurisdictions = CurrentPerson.StormwaterJurisdictionPeople
                .Select(x => x.StormwaterJurisdiction).ToList();

            if (ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.StormwaterJurisdictionID))
            {
                var currentJurisdiction =
                    HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.GetStormwaterJurisdiction(
                        viewModel.StormwaterJurisdictionID);

                if (!stormwaterJurisdictions.Contains(currentJurisdiction))
                {
                    stormwaterJurisdictions.Add(currentJurisdiction);
                }
            }

            var treatmentBMPTypes = HttpRequestStorage.DatabaseEntities.TreatmentBMPTypes.ToList();
            var layerGeoJsons = MapInitJson.GetJurisdictionMapLayers();
            var mapInitJson = new MapInitJson($"BMP_{CurrentPerson.PersonID}_EditBMP", 10, layerGeoJsons, BoundingBox.MakeNewDefaultBoundingBox(), false) { AllowFullScreen = false };
            var mapFormID = "treatmentBMPLocation";

            var treatmentBMP = ModelObjectHelpers.IsRealPrimaryKeyValue(viewModel.TreatmentBMPID) ? HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetTreatmentBMP(viewModel.TreatmentBMPID) : null;
            var viewData = new EditViewData(CurrentPerson, treatmentBMP, stormwaterJurisdictions, treatmentBMPTypes, mapInitJson, mapFormID);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [TreatmentBMPManageFeature]
        public PartialViewResult Delete(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(treatmentBMP.TreatmentBMPID);
            return ViewDeleteTreatmentBMP(treatmentBMP, viewModel);
        }

        [HttpPost]
        [TreatmentBMPManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteTreatmentBMP(treatmentBMP, viewModel);
            }
            if (treatmentBMP.TreatmentBMPBenchmarkAndThresholds.Any())
            {
                treatmentBMP.TreatmentBMPBenchmarkAndThresholds.DeleteTreatmentBMPBenchmarkAndThreshold();
            }
            treatmentBMP.DeleteTreatmentBMP();
            return new ModalDialogFormJsonResult(SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(c => c.Index()));
        }

        private PartialViewResult ViewDeleteTreatmentBMP(TreatmentBMP treatmentBMP, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = !treatmentBMP.HasDependentObjectsBesidesBenchmarksAndThresholds();
            var confirmMessage = canDelete
                ? $"Are you sure you want to delete the '{treatmentBMP.TreatmentBMPName}' treatment BMP?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Treatment BMP", SitkaRoute<TreatmentBMPController>.BuildLinkFromExpression(x => x.Detail(treatmentBMP), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }

        [NeptuneViewFeature]
        public PartialViewResult SummaryForMap(TreatmentBMPPrimaryKey treatmentBMPPrimaryKey)
        {
            var treatmentBMP = treatmentBMPPrimaryKey.EntityObject;
            var viewData = new SummaryForMapViewData(CurrentPerson, treatmentBMP);
            return RazorPartialView<SummaryForMap, SummaryForMapViewData>(viewData);
        }

        [NeptuneViewFeature]
        public JsonResult FindByName(string term)
        {
            var searchString = term.Trim();
            var allTreatmentBMPsMatchingSearchString =
                HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(
                    x => (x.TreatmentBMPName).Contains(searchString)).ToList();

            var listItems = allTreatmentBMPsMatchingSearchString.OrderBy(x => x.TreatmentBMPName).Take(20).Select(bmp =>
            {
                var treatmentBMPMapSummaryData = new Neptune.Web.Views.Shared.SearchMapSummaryData(bmp.GetMapSummaryUrl(), bmp.LocationPoint, bmp.LocationPoint.YCoordinate.Value, bmp.LocationPoint.XCoordinate.Value, bmp.TreatmentBMPID);
                var listItem = new ListItem(bmp.TreatmentBMPName, JsonConvert.SerializeObject(treatmentBMPMapSummaryData));
                return listItem;
            }).ToList();

            return Json(listItems, JsonRequestBehavior.AllowGet);
        }
    }
}
