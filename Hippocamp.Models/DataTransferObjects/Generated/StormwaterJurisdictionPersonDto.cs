//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionPerson]
using System;


namespace Hippocamp.Models.DataTransferObjects
{
    public partial class StormwaterJurisdictionPersonDto
    {
        public int StormwaterJurisdictionPersonID { get; set; }
        public StormwaterJurisdictionDto StormwaterJurisdiction { get; set; }
        public PersonDto Person { get; set; }
    }

    public partial class StormwaterJurisdictionPersonSimpleDto
    {
        public int StormwaterJurisdictionPersonID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int PersonID { get; set; }
    }

}