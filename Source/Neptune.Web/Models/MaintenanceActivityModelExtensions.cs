﻿using LtInfo.Common;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Models
{
    public static class MaintenanceRecordModelExtensions
    {
        public static readonly UrlTemplate<int> EditUrlTemplate = new UrlTemplate<int>(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(t => t.Edit(UrlTemplate.Parameter1Int)));
        public static string GetEditUrl(this MaintenanceRecord maintenanceRecord)
        {
            return EditUrlTemplate.ParameterReplace(maintenanceRecord.MaintenanceRecordID);
        }

        public static readonly UrlTemplate<int> DeleteUrlTemplate = new UrlTemplate<int>(SitkaRoute<MaintenanceRecordController>.BuildUrlFromExpression(t => t.Delete(UrlTemplate.Parameter1Int)));
        public static string GetDeleteUrl(this MaintenanceRecord maintenanceRecord)
        {
            return DeleteUrlTemplate.ParameterReplace(maintenanceRecord.MaintenanceRecordID);
        }
    }
}