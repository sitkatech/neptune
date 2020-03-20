//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vPowerBICentralizedBMPLoadGeneratingUnit]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vPowerBICentralizedBMPLoadGeneratingUnitConfiguration : EntityTypeConfiguration<vPowerBICentralizedBMPLoadGeneratingUnit>
    {
        public vPowerBICentralizedBMPLoadGeneratingUnitConfiguration() : this("dbo"){}

        public vPowerBICentralizedBMPLoadGeneratingUnitConfiguration(string schema)
        {
            ToTable("vPowerBICentralizedBMPLoadGeneratingUnit", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
        }
    }
}