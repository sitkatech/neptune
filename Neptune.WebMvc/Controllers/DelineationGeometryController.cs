/*-----------------------------------------------------------------------
<copyright file="DelineationController.cs" company="Tahoe Regional Planning Agency">
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Services;
using NetTopologySuite.Features;
using Neptune.WebMvc.Views.DelineationGeometry;

namespace Neptune.WebMvc.Controllers
{
    public class DelineationGeometryController : NeptuneBaseController<DelineationGeometryController>
    {
        private readonly AzureBlobStorageService _azureBlobStorageService;
        private readonly GDALAPIService _gdalApiService;

        public DelineationGeometryController(NeptuneDbContext dbContext, ILogger<DelineationGeometryController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, AzureBlobStorageService azureBlobStorageService, GDALAPIService gdalApiService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _azureBlobStorageService = azureBlobStorageService;
            _gdalApiService = gdalApiService;
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult UpdateDelineationGeometry()
        {
            var viewModel = new UpdateDelineationGeometryViewModel(){ TreatmentBMPNameField = "TreatmentBMPName" };
            return ViewUpdateDelineationGeometry(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        public async Task<ActionResult> UpdateDelineationGeometry(UpdateDelineationGeometryViewModel viewModel)
        {
            var file = viewModel.FileResourceData;
            var blobName = Guid.NewGuid().ToString();
            await _azureBlobStorageService.UploadToBlobStorage(await FileStreamHelpers.StreamToBytes(file), blobName, ".gdb");
            var featureClassNames = await _gdalApiService.OgrInfoGdbToFeatureClassInfo(file);

            if (featureClassNames.Count == 0)
            {
                ModelState.AddModelError("FileResourceData",
                    "The file geodatabase contained no feature class. Please upload a file geodatabase containing exactly one feature class.");
            }
            else if (featureClassNames.Count > 1)
            {
                ModelState.AddModelError("FileResourceData",
                    "The file geodatabase contained more than one feature class. Please upload a file geodatabase containing exactly one feature class.");
            }

            var featureClassName = featureClassNames.Single().LayerName;
            //if (!OgrInfoCommandLineRunner.ConfirmAttributeExistsOnFeatureClass(
            //        new FileInfo(NeptuneWebConfiguration.OgrInfoExecutable),
            //        gdbFile,
            //        Ogr2OgrCommandLineRunner.DefaultTimeOut, featureClassName, TreatmentBMPNameField))
            //{
            //    errors.Add(new ValidationResult($"The feature class in the file geodatabase does not have an attribute named {TreatmentBMPNameField}. Please double-check the attribute name you entered and try again."));
            //    return errors;
            //}

            if (!ModelState.IsValid)
            {
                return ViewUpdateDelineationGeometryErrors(viewModel);
            }

            try
            {
                var columns = new List<string>
                {
                    $"{CurrentPerson.PersonID} as UploadedByPersonID",
                    $"{viewModel.StormwaterJurisdictionID} as StormwaterJurisdictionID",
                    $"{viewModel.TreatmentBMPNameField} as TreatmentBMPName",
                    $"DelineationStatus as DelineationStatus"
                };

                var apiRequest = new GdbToGeoJsonRequestDto()
                {
                    BlobContainer = AzureBlobStorageService.BlobContainerName,
                    CanonicalName = blobName,
                    GdbLayerOutputs = new()
                    {
                        new()
                        {
                            Columns = columns,
                            FeatureLayerName = featureClassName,
                            NumberOfSignificantDigits = 4,
                            Filter = "",
                            CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                        }
                    }
                };
                var geoJson = await _gdalApiService.Ogr2OgrGdbToGeoJson(apiRequest);
                var delineationStagings = await GeoJsonSerializer.DeserializeFromFeatureCollectionWithCCWCheck<DelineationStaging>(geoJson,
                    GeoJsonSerializer.DefaultSerializerOptions, Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID);
                // todo: Run MakeValid "update dbo.DelineationStaging set Geometry = Geometry.MakeValid() where Geometry.STIsValid() = 0";

                var validDelineationStagings = delineationStagings.Where(x => x.Geometry is { IsValid: true, Area: > 0 }).ToList();
                if (validDelineationStagings.Any())
                {
                    await _dbContext.DelineationStagings.Where(x => x.UploadedByPersonID == CurrentPerson.PersonID).ExecuteDeleteAsync();
                    _dbContext.DelineationStagings.AddRange(validDelineationStagings);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Unrecognized field name",
                        StringComparison.InvariantCultureIgnoreCase))
                {
                    ModelState.AddModelError("",
                        "The columns in the uploaded file did not match the Delineation schema. The file is invalid and cannot be uploaded.");
                }
                else
                {
                    ModelState.AddModelError("",
                        $"There was a problem processing the Feature Class \"{featureClassName}\". The file may be corrupted or invalid.");
                }
                return ViewUpdateDelineationGeometryErrors(viewModel);
            }

            var stormwaterJurisdictionID = viewModel.StormwaterJurisdictionID.GetValueOrDefault();
            var treatmentBMPAKs = _dbContext.DelineationStagings.Where(x => x.UploadedByPersonID == CurrentPerson.PersonID).Select(x => x.TreatmentBMPName).ToList();

            var treatmentBMPsWithUpstreamBMPAlreadySet = _dbContext.TreatmentBMPs.Where(x => x.StormwaterJurisdictionID == stormwaterJurisdictionID &&
                treatmentBMPAKs.Contains(x.TreatmentBMPName) && x.UpstreamBMPID != null).ToList();

            if (treatmentBMPsWithUpstreamBMPAlreadySet.Any())
            {
                var namesOfInvalidBMPs = treatmentBMPsWithUpstreamBMPAlreadySet.Select(x=> x.TreatmentBMPName);

                namesOfInvalidBMPs.ToList().ForEach(x =>
                {
                    ModelState.AddModelError("Upstream BMP", $"Treatment BMP:{x} has an Upstream BMP and cannot accept delineations. Please either remove this Treatment BMP from your file or remove the Upstream BMP from the Treatment BMP and try again.");
                });
                return ViewUpdateDelineationGeometryErrors(viewModel);
            }

            return RedirectToAction(new SitkaRoute<DelineationGeometryController>(_linkGenerator, x => x.ApproveDelineationGisUpload()));
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult DownloadDelineationGeometry()
        {
            var viewModel = new DownloadDelineationGeometryViewModel();
            return ViewDownloadDelineationGeometry(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [Produces(@"application/zip")]
        public async Task<FileContentResult> DownloadDelineationGeometry(DownloadDelineationGeometryViewModel viewModel)
        {
            var stormwaterJurisdiction = _dbContext.StormwaterJurisdictions.Include(x => x.Organization)
                .Single(x => x.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID)
                .GetOrganizationDisplayName();
            var distributedFeatureCollection = new FeatureCollection();
            var centralizedFeatureCollection = new FeatureCollection();
            var distributedDelineations = _dbContext.Delineations.Include(x => x.TreatmentBMP)
                    .ThenInclude(x => x.StormwaterJurisdiction)
                        .ThenInclude(x => x.Organization)
                .Include(x => x.TreatmentBMP)
                    .ThenInclude(x => x.TreatmentBMPType)
                .Where(x =>
                x.TreatmentBMP.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID &&
                x.DelineationTypeID == (int)DelineationTypeEnum.Distributed).ToList();
            var centralizedDelineations = _dbContext.Delineations.Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.TreatmentBMPType)
                .Where(x =>
                    x.TreatmentBMP.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID &&
                    x.DelineationTypeID == (int)DelineationTypeEnum.Centralized).ToList();

            foreach (var delineation in distributedDelineations)
            {
                var attributesTable = new AttributesTable
                {
                    { "DelineationID", delineation.DelineationID },
                    { "TreatmentBMPName", delineation.TreatmentBMP.TreatmentBMPName },
                    { "Jurisdiction", delineation.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName() },
                    { "BMPType", delineation.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName },
                    { "DelineationStatus", delineation.GetDelineationStatus() },
                    { "DelineationArea", delineation.GetDelineationArea() },
                    { "DateOfLastDelineationModification", delineation.DateLastModified },
                    { "DateOfLastDelineationVerification", delineation.DateLastVerified },
                };
                distributedFeatureCollection.Add(new Feature(delineation.DelineationGeometry, attributesTable));
            }

            foreach (var delineation in centralizedDelineations)
            {
                var attributesTable = new AttributesTable
                {
                    { "DelineationID", delineation.DelineationID },
                    { "TreatmentBMPName", delineation.TreatmentBMP.TreatmentBMPName },
                    { "Jurisdiction", delineation.TreatmentBMP.StormwaterJurisdiction.GetOrganizationDisplayName() },
                    { "BMPType", delineation.TreatmentBMP.TreatmentBMPType.TreatmentBMPTypeName },
                    { "DelineationStatus", delineation.GetDelineationStatus() },
                    { "DelineationArea", delineation.GetDelineationArea() },
                    { "DateOfLastDelineationModification", delineation.DateLastModified },
                    { "DateOfLastDelineationVerification", delineation.DateLastVerified },
                };
                centralizedFeatureCollection.Add(new Feature(delineation.DelineationGeometry, attributesTable));
            }


            var jurisdictionName = stormwaterJurisdiction.Replace(' ', '-');

            var gdbInput = new GdbInput()
            {
                FileContents = GeoJsonSerializer.SerializeToByteArray(distributedFeatureCollection, GeoJsonSerializer.DefaultSerializerOptions),
                LayerName = "distributed-delineations",
                CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                GeometryTypeName = "POLYGON",
            };

            var gdbInput2 = new GdbInput()
            {
                FileContents = GeoJsonSerializer.SerializeToByteArray(centralizedFeatureCollection, GeoJsonSerializer.DefaultSerializerOptions),
                LayerName = "centralized-delineations",
                CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                GeometryTypeName = "POLYGON",
            };
            var bytes = await _gdalApiService.Ogr2OgrInputToGdbAsZip(new GdbInputsToGdbRequestDto()
            {
                GdbInputs = new List<GdbInput> { gdbInput, gdbInput2 },
                GdbName = $"{jurisdictionName}-delineation-export"
            });

            return File(bytes, "application/zip", $"{jurisdictionName}-delineation-export.gdb.zip");
        }


        private PartialViewResult ViewUpdateDelineationGeometryErrors(UpdateDelineationGeometryViewModel viewModel)
        {
            var viewData = new UpdateDelineationGeometryViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson,
                null, null, StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson), null);
            return RazorPartialView<UpdateDelineationGeometryErrors, UpdateDelineationGeometryViewData, UpdateDelineationGeometryViewModel>(viewData, viewModel);
        }

        private ViewResult ViewUpdateDelineationGeometry(UpdateDelineationGeometryViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(_linkGenerator, x => x.UpdateDelineationGeometry());
            var approveGisUploadUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(_linkGenerator, x => x.ApproveDelineationGisUpload());
            var downloadGisUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(_linkGenerator, x => x.DownloadDelineationGeometry());

            var viewData = new UpdateDelineationGeometryViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson,
                newGisUploadUrl, approveGisUploadUrl, StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson), downloadGisUrl);
            return RazorView<UpdateDelineationGeometry, UpdateDelineationGeometryViewData, UpdateDelineationGeometryViewModel>(viewData, viewModel);
        }

        private ViewResult ViewDownloadDelineationGeometry(DownloadDelineationGeometryViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(_linkGenerator, x => x.DownloadDelineationGeometry());
            var gisUploadUrl = SitkaRoute<DelineationGeometryController>.BuildUrlFromExpression(_linkGenerator, x => x.UpdateDelineationGeometry());

            var viewData = new DownloadDelineationGeometryViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson,
                newGisUploadUrl, StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson), gisUploadUrl);
            return RazorView<DownloadDelineationGeometry, DownloadDelineationGeometryViewData, DownloadDelineationGeometryViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ActionResult ApproveDelineationGisUpload()
        {
            var viewModel = new ApproveDelineationGisUploadViewModel();
            return ViewApproveDelineationGisUpload(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        public async Task<ActionResult> ApproveDelineationGisUpload(ApproveDelineationGisUploadViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewUpdateDelineationGeometry(new UpdateDelineationGeometryViewModel());
            }

            var delineationStagings = _dbContext.DelineationStagings.Where(x => x.UploadedByPersonID == CurrentPerson.PersonID).ToList();

            // Will break if there are multiple batches of staged uploads, which is precisely what we want to happen. 
            var stormwaterJurisdictionID = delineationStagings.Select(x => x.StormwaterJurisdictionID).Distinct().Single();
            var stormwaterJurisdiction = StormwaterJurisdictions.GetByID(_dbContext, stormwaterJurisdictionID);
            var stormwaterJurisdictionName = stormwaterJurisdiction.GetOrganizationDisplayName();
                                                        
            // Starting from the treatment BMP is kind of backwards, conceptually, but it's easier to read and write
            var treatmentBMPNames = delineationStagings.Select(x => x.TreatmentBMPName).ToList();
            var treatmentBMPsToUpdate = 
                stormwaterJurisdiction.TreatmentBMPs.Where(x => treatmentBMPNames.Contains(x.TreatmentBMPName)).ToList();

            foreach (var treatmentBMP in treatmentBMPsToUpdate)
            {
                var delineationStaging = delineationStagings.Single(z => treatmentBMP.TreatmentBMPName == z.TreatmentBMPName);

                treatmentBMP.Delineation?.DeleteFull(_dbContext);

                treatmentBMP.Delineation = new Delineation
                {
                    HasDiscrepancies = false,
                    IsVerified = delineationStaging.DelineationStatus?.Trim().ToLower() == "verified",
                    DelineationTypeID = (int) DelineationTypeEnum.Distributed,
                    TreatmentBMPID = treatmentBMP.TreatmentBMPID,
                    DateLastModified = DateTime.UtcNow,
                    VerifiedByPersonID = CurrentPerson.PersonID,
                    DateLastVerified = DateTime.UtcNow,
                    DelineationGeometry4326 = delineationStaging.Geometry.ProjectTo4326(),
                    DelineationGeometry = delineationStaging.Geometry
                };
            }

            var successfulUploadCount = (int?)treatmentBMPsToUpdate.Count;

            SetMessageForDisplay($"{successfulUploadCount} Delineations were successfully uploaded for Jurisdiction {stormwaterJurisdictionName}");

            await _dbContext.SaveChangesAsync();

            await _dbContext.DelineationStagings.Where(x => x.UploadedByPersonID == CurrentPerson.PersonID).ExecuteDeleteAsync();

            return RedirectToAction(new SitkaRoute<ManagerDashboardController>(_linkGenerator, x => x.Index()));
        }

        private PartialViewResult ViewApproveDelineationGisUpload(ApproveDelineationGisUploadViewModel viewModel)
        {
            var delineationStagings = _dbContext.DelineationStagings.Include(x => x.StormwaterJurisdiction)
                .Where(x => x.UploadedByPersonID == CurrentPerson.PersonID).ToList();

            var delineationUpoadGisReportFromStaging = DelineationUploadGisReportJsonResult.GetDelineationUploadGisReportFromStaging(_dbContext, CurrentPerson, delineationStagings);

            var viewData = new ApproveDelineationGisUploadViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, delineationUpoadGisReportFromStaging);
            return RazorPartialView<ApproveDelineationGisUpload, ApproveDelineationGisUploadViewData, ApproveDelineationGisUploadViewModel>(viewData, viewModel);

        }
    }
}