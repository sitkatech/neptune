//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vWaterQualityManagementPlanLGUAudit]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vWaterQualityManagementPlanLGUAuditConfiguration : EntityTypeConfiguration<vWaterQualityManagementPlanLGUAudit>
    {
        public vWaterQualityManagementPlanLGUAuditConfiguration() : this("dbo"){}

        public vWaterQualityManagementPlanLGUAuditConfiguration(string schema)
        {
            ToTable("vWaterQualityManagementPlanLGUAudit", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
        }
    }
}