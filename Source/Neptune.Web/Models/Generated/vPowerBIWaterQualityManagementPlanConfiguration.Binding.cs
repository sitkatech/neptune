//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vPowerBIWaterQualityManagementPlan]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vPowerBIWaterQualityManagementPlanConfiguration : EntityTypeConfiguration<vPowerBIWaterQualityManagementPlan>
    {
        public vPowerBIWaterQualityManagementPlanConfiguration() : this("dbo"){}

        public vPowerBIWaterQualityManagementPlanConfiguration(string schema)
        {
            ToTable("vPowerBIWaterQualityManagementPlan", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
        }
    }
}