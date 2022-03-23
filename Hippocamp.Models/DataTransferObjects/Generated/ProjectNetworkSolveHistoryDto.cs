//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectNetworkSolveHistory]
using System;


namespace Hippocamp.Models.DataTransferObjects
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
        public int ProjectID { get; set; }
        public int RequestedByPersonID { get; set; }
        public int ProjectNetworkSolveHistoryStatusTypeID { get; set; }
        public DateTime LastUpdated { get; set; }
        public string ErrorMessage { get; set; }
    }

}