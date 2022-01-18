//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LSPCBasin]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class LSPCBasinDto
    {
        public int LSPCBasinID { get; set; }
        public int LSPCBasinKey { get; set; }
        public string LSPCBasinName { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    public partial class LSPCBasinSimpleDto
    {
        public int LSPCBasinID { get; set; }
        public int LSPCBasinKey { get; set; }
        public string LSPCBasinName { get; set; }
        public DateTime LastUpdate { get; set; }
    }

}