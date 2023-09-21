//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ModelBasin]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class ModelBasinDto
    {
        public int ModelBasinID { get; set; }
        public int ModelBasinKey { get; set; }
        public DateTime LastUpdate { get; set; }
        public string ModelBasinState { get; set; }
        public string ModelBasinRegion { get; set; }
    }

    public partial class ModelBasinSimpleDto
    {
        public int ModelBasinID { get; set; }
        public int ModelBasinKey { get; set; }
        public DateTime LastUpdate { get; set; }
        public string ModelBasinState { get; set; }
        public string ModelBasinRegion { get; set; }
    }

}