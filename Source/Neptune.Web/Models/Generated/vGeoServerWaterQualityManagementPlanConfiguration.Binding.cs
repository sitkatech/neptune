//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vGeoServerWaterQualityManagementPlan]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vGeoServerWaterQualityManagementPlanConfiguration : EntityTypeConfiguration<vGeoServerWaterQualityManagementPlan>
    {
        public vGeoServerWaterQualityManagementPlanConfiguration() : this("dbo"){}

        public vGeoServerWaterQualityManagementPlanConfiguration(string schema)
        {
            ToTable("vGeoServerWaterQualityManagementPlan", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
        }
    }
}