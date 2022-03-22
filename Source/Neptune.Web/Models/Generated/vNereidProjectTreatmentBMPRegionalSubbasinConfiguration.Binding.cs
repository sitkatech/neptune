//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vNereidProjectTreatmentBMPRegionalSubbasin]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vNereidProjectTreatmentBMPRegionalSubbasinConfiguration : EntityTypeConfiguration<vNereidProjectTreatmentBMPRegionalSubbasin>
    {
        public vNereidProjectTreatmentBMPRegionalSubbasinConfiguration() : this("dbo"){}

        public vNereidProjectTreatmentBMPRegionalSubbasinConfiguration(string schema)
        {
            ToTable("vNereidProjectTreatmentBMPRegionalSubbasin", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
        }
    }
}