//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vNereidTreatmentBMPRegionalSubbasin]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vNereidTreatmentBMPRegionalSubbasinConfiguration : EntityTypeConfiguration<vNereidTreatmentBMPRegionalSubbasin>
    {
        public vNereidTreatmentBMPRegionalSubbasinConfiguration() : this("dbo"){}

        public vNereidTreatmentBMPRegionalSubbasinConfiguration(string schema)
        {
            ToTable("vNereidTreatmentBMPRegionalSubbasin", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
        }
    }
}