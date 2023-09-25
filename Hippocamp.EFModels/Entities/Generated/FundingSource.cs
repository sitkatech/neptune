using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("FundingSource")]
    [Index("OrganizationID", "FundingSourceName", Name = "AK_FundingSource_OrganizationID_FundingSourceName", IsUnique = true)]
    public partial class FundingSource
    {
        public FundingSource()
        {
            FundingEventFundingSources = new HashSet<FundingEventFundingSource>();
        }

        [Key]
        public int FundingSourceID { get; set; }
        public int OrganizationID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string FundingSourceName { get; set; }
        public bool IsActive { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string FundingSourceDescription { get; set; }

        [ForeignKey("OrganizationID")]
        [InverseProperty("FundingSources")]
        public virtual Organization Organization { get; set; }
        [InverseProperty("FundingSource")]
        public virtual ICollection<FundingEventFundingSource> FundingEventFundingSources { get; set; }
    }
}
