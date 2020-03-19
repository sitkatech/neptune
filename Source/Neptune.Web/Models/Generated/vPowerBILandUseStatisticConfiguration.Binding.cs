//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source View: [dbo].[vPowerBILandUseStatistic]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class vPowerBILandUseStatisticConfiguration : EntityTypeConfiguration<vPowerBILandUseStatistic>
    {
        public vPowerBILandUseStatisticConfiguration() : this("dbo"){}

        public vPowerBILandUseStatisticConfiguration(string schema)
        {
            ToTable("vPowerBILandUseStatistic", schema);
            HasKey(x => x.PrimaryKey);
            
            
            
            
            
            
            
            
            
            
            
            
        }
    }
}