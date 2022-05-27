using System.Collections.Generic;

namespace Hippocamp.Models.DataTransferObjects
{
    public class ProjectModeledResultsDto : ProjectSimpleDto
    {
        public List<TreatmentBMPHRUCharacteristicsSummarySimpleDto> TreatmentBMPHRUCharacteristicsSummary { get; set; }
        public List<TreatmentBMPModeledResultSimpleDto> TreatmentBMPModeledResults { get; set; }
    }
}