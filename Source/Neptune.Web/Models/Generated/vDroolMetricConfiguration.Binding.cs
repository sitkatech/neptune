//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vDroolMetric]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vDroolMetricConfiguration : EntityTypeConfiguration<vDroolMetric>
    {
        public vDroolMetricConfiguration() : this("dbo"){}

        public vDroolMetricConfiguration(string schema)
        {
            ToTable("vDroolMetric", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
        }
    }
}