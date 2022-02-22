//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vRegionalSubbasinUpstreamCatchmentGeometry4326]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vRegionalSubbasinUpstreamCatchmentGeometry4326Configuration : EntityTypeConfiguration<vRegionalSubbasinUpstreamCatchmentGeometry4326>
    {
        public vRegionalSubbasinUpstreamCatchmentGeometry4326Configuration() : this("dbo"){}

        public vRegionalSubbasinUpstreamCatchmentGeometry4326Configuration(string schema)
        {
            ToTable("vRegionalSubbasinUpstreamCatchmentGeometry4326", schema);
            HasKey(x => x.PrimaryKey);
            
        }
    }
}