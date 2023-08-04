//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[County]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class CountyDto
    {
        public int CountyID { get; set; }
        public string CountyName { get; set; }
        public StateProvinceDto StateProvince { get; set; }
    }

    public partial class CountySimpleDto
    {
        public int CountyID { get; set; }
        public string CountyName { get; set; }
        public System.Int32 StateProvinceID { get; set; }
    }

}