//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vNereidPlannedProjectRegionalSubbasinCentralizedBMP]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vNereidPlannedProjectRegionalSubbasinCentralizedBMPConfiguration : EntityTypeConfiguration<vNereidPlannedProjectRegionalSubbasinCentralizedBMP>
    {
        public vNereidPlannedProjectRegionalSubbasinCentralizedBMPConfiguration() : this("dbo"){}

        public vNereidPlannedProjectRegionalSubbasinCentralizedBMPConfiguration(string schema)
        {
            ToTable("vNereidPlannedProjectRegionalSubbasinCentralizedBMP", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
            
        }
    }
}