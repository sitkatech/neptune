//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistory]
using System;


namespace Neptune.Models.DataTransferObjects
{
    public partial class ProjectNetworkSolveHistoryDto
    {
        public int ProjectNetworkSolveHistoryID { get; set; }
        public ProjectDto Project { get; set; }
        public PersonDto RequestedByPerson { get; set; }
        public ProjectNetworkSolveHistoryStatusTypeDto ProjectNetworkSolveHistoryStatusType { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ErrorMessage { get; set; }
    }

    public partial class ProjectNetworkSolveHistorySimpleDto
    {
        public int ProjectNetworkSolveHistoryID { get; set; }
        public System.Int32 ProjectID { get; set; }
        public System.Int32 RequestedByPersonID { get; set; }
        public System.Int32 ProjectNetworkSolveHistoryStatusTypeID { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ErrorMessage { get; set; }
    }

}