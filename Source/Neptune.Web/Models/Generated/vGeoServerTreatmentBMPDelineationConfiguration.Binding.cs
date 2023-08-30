//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vGeoServerTreatmentBMPDelineation]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vGeoServerTreatmentBMPDelineationConfiguration : EntityTypeConfiguration<vGeoServerTreatmentBMPDelineation>
    {
        public vGeoServerTreatmentBMPDelineationConfiguration() : this("dbo"){}

        public vGeoServerTreatmentBMPDelineationConfiguration(string schema)
        {
            ToTable("vGeoServerTreatmentBMPDelineation", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
        }
    }
}