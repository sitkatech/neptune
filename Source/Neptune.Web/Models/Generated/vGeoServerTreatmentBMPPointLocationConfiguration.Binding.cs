//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vGeoServerTreatmentBMPPointLocation]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vGeoServerTreatmentBMPPointLocationConfiguration : EntityTypeConfiguration<vGeoServerTreatmentBMPPointLocation>
    {
        public vGeoServerTreatmentBMPPointLocationConfiguration() : this("dbo"){}

        public vGeoServerTreatmentBMPPointLocationConfiguration(string schema)
        {
            ToTable("vGeoServerTreatmentBMPPointLocation", schema);
            HasKey(x => x.PrimaryKey);
            
            
        }
    }
}