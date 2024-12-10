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

using EllipticCurve.Utils;
using Hangfire.Common;
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
using System.Data;
using System.IO.Compression;
using Neptune.WebMvc.Views.DelineationDownload;

namespace Neptune.WebMvc.Controllers
{
    public class DelineationDownloadController : NeptuneBaseController<DelineationDownloadController>
    {
        private readonly AzureBlobStorageService _azureBlobStorageService;
        private readonly GDALAPIService _gdalApiService;

        public DelineationDownloadController(NeptuneDbContext dbContext, ILogger<DelineationDownloadController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, AzureBlobStorageService azureBlobStorageService, GDALAPIService gdalApiService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _azureBlobStorageService = azureBlobStorageService;
            _gdalApiService = gdalApiService;
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
            var featureCollection = new FeatureCollection();
            var delineations = _dbContext.Delineations.Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.StormwaterJurisdiction)
                .ThenInclude(x => x.Organization)
                .Include(x => x.TreatmentBMP)
                .ThenInclude(x => x.TreatmentBMPType)
                .Where(x =>
                x.TreatmentBMP.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID &&
                x.DelineationTypeID == (int)DelineationTypeEnum.Distributed).ToList();

            foreach (var delineation in delineations)
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
                var feature = new Feature(delineation.DelineationGeometry, attributesTable);
                featureCollection.Add(feature);
            }
            await using var stream = new MemoryStream();
            await GeoJsonSerializer.SerializeAsGeoJsonToStream(featureCollection,
                GeoJsonSerializer.DefaultSerializerOptions, stream);

            var jurisdictionName = stormwaterJurisdiction.Replace(' ', '-');

            var gdbInput = new GdbInput()
            {
                FileContents = stream.ToArray(),
                LayerName = "distributed-delineations",
                CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                GeometryTypeName = "POLYGON",
            };
            var bytes = await _gdalApiService.Ogr2OgrInputToGdbAsZip(new GdbInputsToGdbRequestDto()
            {
                GdbInputs = new List<GdbInput> { gdbInput },
                GdbName = $"{jurisdictionName}-delineation-export"
            });

            return File(bytes, "application/zip", $"{jurisdictionName}-delineation-export.gdb.zip");
        }

        private ViewResult ViewDownloadDelineationGeometry(DownloadDelineationGeometryViewModel viewModel)
        {
            var newGisDownloadUrl = SitkaRoute<DelineationDownloadController>.BuildUrlFromExpression(_linkGenerator, x => x.DownloadDelineationGeometry());
            var gisDownloadUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(_linkGenerator, x => x.UpdateDelineationGeometry());

            var viewData = new DownloadDelineationGeometryViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson,
                newGisDownloadUrl, StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson), gisDownloadUrl);
            return RazorView<DownloadDelineationGeometry, DownloadDelineationGeometryViewData, DownloadDelineationGeometryViewModel>(viewData, viewModel);
        }
    }
}