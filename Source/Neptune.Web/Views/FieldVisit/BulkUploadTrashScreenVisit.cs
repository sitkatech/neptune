using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.FieldVisit
{
    public abstract class BulkUploadTrashScreenVisit : TypedWebViewPage<BulkUploadTrashScreenVisitViewData, BulkUploadTrashScreenVisitViewModel>
    {
    }

    public class BulkUploadTrashScreenVisitViewModel : FormViewModel
    {
        [Required]
        [SitkaFileExtensions("xlsx")]
        [DisplayName("XLSX File to Import")]
        public HttpPostedFileBase UploadXLSX { get; set; }

        public BulkUploadTrashScreenVisitViewModel()
        {

        }
    }
}