//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPModelingAttribute]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPModelingAttributeConfiguration : EntityTypeConfiguration<TreatmentBMPModelingAttribute>
    {
        public TreatmentBMPModelingAttributeConfiguration() : this("dbo"){}

        public TreatmentBMPModelingAttributeConfiguration(string schema)
        {
            ToTable("TreatmentBMPModelingAttribute", schema);
            HasKey(x => x.TreatmentBMPModelingAttributeID);
            Property(x => x.TreatmentBMPModelingAttributeID).HasColumnName(@"TreatmentBMPModelingAttributeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.UpstreamTreatmentBMPID).HasColumnName(@"UpstreamTreatmentBMPID").HasColumnType("int").IsOptional();
            Property(x => x.AverageDivertedFlowrate).HasColumnName(@"AverageDivertedFlowrate").HasColumnType("float").IsOptional();
            Property(x => x.AverageTreatmentFlowrate).HasColumnName(@"AverageTreatmentFlowrate").HasColumnType("float").IsOptional();
            Property(x => x.DesignDryWeatherTreatmentCapacity).HasColumnName(@"DesignDryWeatherTreatmentCapacity").HasColumnType("float").IsOptional();
            Property(x => x.DesignLowFlowDiversionCapacity).HasColumnName(@"DesignLowFlowDiversionCapacity").HasColumnType("float").IsOptional();
            Property(x => x.DesignMediaFiltrationRate).HasColumnName(@"DesignMediaFiltrationRate").HasColumnType("float").IsOptional();
            Property(x => x.DiversionRate).HasColumnName(@"DiversionRate").HasColumnType("float").IsOptional();
            Property(x => x.DrawdownTimeforWQDetentionVolume).HasColumnName(@"DrawdownTimeforWQDetentionVolume").HasColumnType("float").IsOptional();
            Property(x => x.EffectiveFootprint).HasColumnName(@"EffectiveFootprint").HasColumnType("float").IsOptional();
            Property(x => x.EffectiveRetentionDepth).HasColumnName(@"EffectiveRetentionDepth").HasColumnType("float").IsOptional();
            Property(x => x.InfiltrationDischargeRate).HasColumnName(@"InfiltrationDischargeRate").HasColumnType("float").IsOptional();
            Property(x => x.InfiltrationSurfaceArea).HasColumnName(@"InfiltrationSurfaceArea").HasColumnType("float").IsOptional();
            Property(x => x.MediaBedFootprint).HasColumnName(@"MediaBedFootprint").HasColumnType("float").IsOptional();
            Property(x => x.PermanentPoolorWetlandVolume).HasColumnName(@"PermanentPoolorWetlandVolume").HasColumnType("float").IsOptional();
            Property(x => x.RoutingConfigurationID).HasColumnName(@"RoutingConfigurationID").HasColumnType("int").IsOptional();
            Property(x => x.StorageVolumeBelowLowestOutletElevation).HasColumnName(@"StorageVolumeBelowLowestOutletElevation").HasColumnType("float").IsOptional();
            Property(x => x.SummerHarvestedWaterDemand).HasColumnName(@"SummerHarvestedWaterDemand").HasColumnType("float").IsOptional();
            Property(x => x.TimeOfConcentrationID).HasColumnName(@"TimeOfConcentrationID").HasColumnType("int").IsOptional();
            Property(x => x.DrawdownTimeForDetentionVolume).HasColumnName(@"DrawdownTimeForDetentionVolume").HasColumnType("float").IsOptional();
            Property(x => x.TotalEffectiveBMPVolume).HasColumnName(@"TotalEffectiveBMPVolume").HasColumnType("float").IsOptional();
            Property(x => x.TotalEffectiveDrywellBMPVolume).HasColumnName(@"TotalEffectiveDrywellBMPVolume").HasColumnType("float").IsOptional();
            Property(x => x.TreatmentRate).HasColumnName(@"TreatmentRate").HasColumnType("float").IsOptional();
            Property(x => x.UnderlyingHydrologicSoilGroupID).HasColumnName(@"UnderlyingHydrologicSoilGroupID").HasColumnType("int").IsOptional();
            Property(x => x.UnderlyingInfiltrationRate).HasColumnName(@"UnderlyingInfiltrationRate").HasColumnType("float").IsOptional();
            Property(x => x.WaterQualityDetentionVolume).HasColumnName(@"WaterQualityDetentionVolume").HasColumnType("float").IsOptional();
            Property(x => x.WettedFootprint).HasColumnName(@"WettedFootprint").HasColumnType("float").IsOptional();
            Property(x => x.WinterHarvestedWaterDemand).HasColumnName(@"WinterHarvestedWaterDemand").HasColumnType("float").IsOptional();
            Property(x => x.MonthsOfOperationID).HasColumnName(@"MonthsOfOperationID").HasColumnType("int").IsOptional();
            Property(x => x.DryWeatherFlowOverrideID).HasColumnName(@"DryWeatherFlowOverrideID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.TreatmentBMPModelingAttributes).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_TreatmentBMPModelingAttribute_TreatmentBMP_TreatmentBMPID
            HasOptional(a => a.UpstreamTreatmentBMP).WithMany(b => b.TreatmentBMPModelingAttributesWhereYouAreTheUpstreamTreatmentBMP).HasForeignKey(c => c.UpstreamTreatmentBMPID).WillCascadeOnDelete(false); // FK_TreatmentBMPModelingAttribute_TreatmentBMP_UpstreamTreatmentBMPID_TreatmentBMPID
        }
    }
}