//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vNereidProjectRegionalSubbasinCentralizedBMP]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vNereidProjectRegionalSubbasinCentralizedBMPConfiguration : EntityTypeConfiguration<vNereidProjectRegionalSubbasinCentralizedBMP>
    {
        public vNereidProjectRegionalSubbasinCentralizedBMPConfiguration() : this("dbo"){}

        public vNereidProjectRegionalSubbasinCentralizedBMPConfiguration(string schema)
        {
            ToTable("vNereidProjectRegionalSubbasinCentralizedBMP", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
            
        }
    }
}