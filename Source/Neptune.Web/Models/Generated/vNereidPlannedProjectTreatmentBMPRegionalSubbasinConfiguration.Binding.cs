//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vNereidPlannedProjectTreatmentBMPRegionalSubbasin]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vNereidPlannedProjectTreatmentBMPRegionalSubbasinConfiguration : EntityTypeConfiguration<vNereidPlannedProjectTreatmentBMPRegionalSubbasin>
    {
        public vNereidPlannedProjectTreatmentBMPRegionalSubbasinConfiguration() : this("dbo"){}

        public vNereidPlannedProjectTreatmentBMPRegionalSubbasinConfiguration(string schema)
        {
            ToTable("vNereidPlannedProjectTreatmentBMPRegionalSubbasin", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
        }
    }
}