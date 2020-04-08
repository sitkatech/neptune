/*-----------------------------------------------------------------------
<copyright file="IWebServices.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Collections.Generic;
using System.ServiceModel;
using Neptune.Web.Common;
using LtInfo.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Service
{
    [ServiceContract]
    [WebServicesErrorHandler]
    public interface IWebServices
    {
        [OperationContract]
        [WebServiceDocumentationAttribute("BMP Attributes")]
        void TreatmentBMPAttributeSummary(string webServiceToken);

        [OperationContract]
        [WebServiceDocumentationAttribute("WQMP Summary")]
        void WaterQualityManagementPlanAttributeSummary(string webServiceToken);

        [OperationContract]
        [WebServiceDocumentationAttribute("Land Surface Attributes")]
        void LandUseStatistics(string webServiceToken);

        [OperationContract]
        [WebServiceDocumentationAttribute("BMP Modeling Parameterization Summary")]
        void TreatmentBMPParameterizationSummary(string webServiceToken);

        [OperationContract]
        [WebServiceDocumentationAttribute("Centralized BMP mapping to Land Use")]
        void CentralizedBMPLoadGeneratingUnitMapping(string webServiceToken);

        [OperationContract]
        [WebServiceDocumentationAttribute("Land Use Statistics")]
        void GetHRUCharacteristicsForPowerBI(string webServiceToken);
    }
}
