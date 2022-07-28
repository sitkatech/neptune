//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vRegionalSubbasinUpstream]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vRegionalSubbasinUpstreamConfiguration : EntityTypeConfiguration<vRegionalSubbasinUpstream>
    {
        public vRegionalSubbasinUpstreamConfiguration() : this("dbo"){}

        public vRegionalSubbasinUpstreamConfiguration(string schema)
        {
            ToTable("vRegionalSubbasinUpstream", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
        }
    }
}