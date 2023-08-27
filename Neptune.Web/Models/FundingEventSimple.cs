using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Models
{
    public class FundingEventSimple
    {
        [Required]
        public int TreatmentBMPID { get; set; }

        [Required]
        [Range(Constants.MinimumYear, 2050, ErrorMessage = "Please enter a valid year range")]
        public int Year { get; set; }

        [Required(ErrorMessage="Funding Event Type is required")]
        [DisplayName("Funding Event Type")]
        public int FundingEventTypeID { get; set; }

        [StringLength(FundingEvent.FieldLengths.Description)]
        public string Description { get; set; }

        public int? FundingEventID { get; set; }

        public List<FundingEventFundingSourceSimple> FundingEventFundingSources { get; set; }


        public FundingEventSimple()
        {
        }

        public FundingEventSimple(FundingEvent fundingEvent)
        {
            TreatmentBMPID = fundingEvent.TreatmentBMPID;
            Year = fundingEvent.Year;
            FundingEventTypeID = fundingEvent.FundingEventTypeID;
            Description = fundingEvent.Description;
            FundingEventFundingSources = fundingEvent.FundingEventFundingSources
                .Select(x => new FundingEventFundingSourceSimple(x)).ToList();
            FundingEventID = fundingEvent.FundingEventID;
        }

        public FundingEventSimple(TreatmentBMP treatmentBMP)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            FundingEventFundingSources = new List<FundingEventFundingSourceSimple>();
            Year = DateTime.Now.Year;
        }

        public FundingEvent ToFundingEvent()
        {
            return new FundingEvent()
            {
                FundingEventID = FundingEventID ?? ModelObjectHelpers.NotYetAssignedID,
                TreatmentBMPID = TreatmentBMPID,
                FundingEventTypeID = FundingEventTypeID, Year = Year, Description = Description,
                FundingEventFundingSources = FundingEventFundingSources.Select(x => x.ToFundingEventFundingSource()).ToList()
            };
        }
    }
}
