//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPModelingAttribute]
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    // Table [dbo].[TreatmentBMPModelingAttribute] is NOT multi-tenant, so is attributed as ICanDeleteFull
    [Table("[dbo].[TreatmentBMPModelingAttribute]")]
    public partial class TreatmentBMPModelingAttribute : IHavePrimaryKey, ICanDeleteFull
    {
        /// <summary>
        /// Default Constructor; only used by EF
        /// </summary>
        protected TreatmentBMPModelingAttribute()
        {

        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPModelingAttribute(int treatmentBMPModelingAttributeID, int treatmentBMPID, int? upstreamTreatmentBMPID, double? averageDivertedFlowrate, double? averageTreatmentFlowrate, double? designDryWeatherTreatmentCapacity, double? designLowFlowDiversionCapacity, double? designMediaFiltrationRate, double? designResidenceTimeforPermanentPool, double? diversionRate, double? drawdownTimeforWQDetentionVolume, double? effectiveFootprint, double? effectiveRetentionDepth, double? infiltrationDischargeRate, double? infiltrationSurfaceArea, double? mediaBedFootprint, double? permanentPoolorWetlandVolume, int? routingConfigurationID, double? storageVolumeBelowLowestOutletElevation, double? summerHarvestedWaterDemand, int? timeOfConcentrationID, double? drawdownTimeForDetentionVolume, double? totalEffectiveBMPVolume, double? totalEffectiveDrywellBMPVolume, double? treatmentRate, int? underlyingHydrologicSoilGroupID, double? underlyingInfiltrationRate, double? waterQualityDetentionVolume, double? wettedFootprint, double? winterHarvestedWaterDemand, int? operationMonthID) : this()
        {
            this.TreatmentBMPModelingAttributeID = treatmentBMPModelingAttributeID;
            this.TreatmentBMPID = treatmentBMPID;
            this.UpstreamTreatmentBMPID = upstreamTreatmentBMPID;
            this.AverageDivertedFlowrate = averageDivertedFlowrate;
            this.AverageTreatmentFlowrate = averageTreatmentFlowrate;
            this.DesignDryWeatherTreatmentCapacity = designDryWeatherTreatmentCapacity;
            this.DesignLowFlowDiversionCapacity = designLowFlowDiversionCapacity;
            this.DesignMediaFiltrationRate = designMediaFiltrationRate;
            this.DesignResidenceTimeforPermanentPool = designResidenceTimeforPermanentPool;
            this.DiversionRate = diversionRate;
            this.DrawdownTimeforWQDetentionVolume = drawdownTimeforWQDetentionVolume;
            this.EffectiveFootprint = effectiveFootprint;
            this.EffectiveRetentionDepth = effectiveRetentionDepth;
            this.InfiltrationDischargeRate = infiltrationDischargeRate;
            this.InfiltrationSurfaceArea = infiltrationSurfaceArea;
            this.MediaBedFootprint = mediaBedFootprint;
            this.PermanentPoolorWetlandVolume = permanentPoolorWetlandVolume;
            this.RoutingConfigurationID = routingConfigurationID;
            this.StorageVolumeBelowLowestOutletElevation = storageVolumeBelowLowestOutletElevation;
            this.SummerHarvestedWaterDemand = summerHarvestedWaterDemand;
            this.TimeOfConcentrationID = timeOfConcentrationID;
            this.DrawdownTimeForDetentionVolume = drawdownTimeForDetentionVolume;
            this.TotalEffectiveBMPVolume = totalEffectiveBMPVolume;
            this.TotalEffectiveDrywellBMPVolume = totalEffectiveDrywellBMPVolume;
            this.TreatmentRate = treatmentRate;
            this.UnderlyingHydrologicSoilGroupID = underlyingHydrologicSoilGroupID;
            this.UnderlyingInfiltrationRate = underlyingInfiltrationRate;
            this.WaterQualityDetentionVolume = waterQualityDetentionVolume;
            this.WettedFootprint = wettedFootprint;
            this.WinterHarvestedWaterDemand = winterHarvestedWaterDemand;
            this.OperationMonthID = operationMonthID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields in preparation for insert into database
        /// </summary>
        public TreatmentBMPModelingAttribute(int treatmentBMPID) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPModelingAttributeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            
            this.TreatmentBMPID = treatmentBMPID;
        }

        /// <summary>
        /// Constructor for building a new object with MinimalConstructor required fields, using objects whenever possible
        /// </summary>
        public TreatmentBMPModelingAttribute(TreatmentBMP treatmentBMP) : this()
        {
            // Mark this as a new object by setting primary key with special value
            this.TreatmentBMPModelingAttributeID = ModelObjectHelpers.MakeNextUnsavedPrimaryKeyValue();
            this.TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            this.TreatmentBMP = treatmentBMP;
        }

        /// <summary>
        /// Creates a "blank" object of this type and populates primitives with defaults
        /// </summary>
        public static TreatmentBMPModelingAttribute CreateNewBlank(TreatmentBMP treatmentBMP)
        {
            return new TreatmentBMPModelingAttribute(treatmentBMP);
        }

        /// <summary>
        /// Does this object have any dependent objects? (If it does have dependent objects, these would need to be deleted before this object could be deleted.)
        /// </summary>
        /// <returns></returns>
        public bool HasDependentObjects()
        {
            return false;
        }

        /// <summary>
        /// Dependent type names of this entity
        /// </summary>
        public static readonly List<string> DependentEntityTypeNames = new List<string> {typeof(TreatmentBMPModelingAttribute).Name};


        /// <summary>
        /// Delete just the entity 
        /// </summary>
        public void Delete(DatabaseEntities dbContext)
        {
            dbContext.TreatmentBMPModelingAttributes.Remove(this);
        }
        
        /// <summary>
        /// Delete entity plus all children
        /// </summary>
        public void DeleteFull(DatabaseEntities dbContext)
        {
            
            Delete(dbContext);
        }

        [Key]
        public int TreatmentBMPModelingAttributeID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int? UpstreamTreatmentBMPID { get; set; }
        public double? AverageDivertedFlowrate { get; set; }
        public double? AverageTreatmentFlowrate { get; set; }
        public double? DesignDryWeatherTreatmentCapacity { get; set; }
        public double? DesignLowFlowDiversionCapacity { get; set; }
        public double? DesignMediaFiltrationRate { get; set; }
        public double? DesignResidenceTimeforPermanentPool { get; set; }
        public double? DiversionRate { get; set; }
        public double? DrawdownTimeforWQDetentionVolume { get; set; }
        public double? EffectiveFootprint { get; set; }
        public double? EffectiveRetentionDepth { get; set; }
        public double? InfiltrationDischargeRate { get; set; }
        public double? InfiltrationSurfaceArea { get; set; }
        public double? MediaBedFootprint { get; set; }
        public double? PermanentPoolorWetlandVolume { get; set; }
        public int? RoutingConfigurationID { get; set; }
        public double? StorageVolumeBelowLowestOutletElevation { get; set; }
        public double? SummerHarvestedWaterDemand { get; set; }
        public int? TimeOfConcentrationID { get; set; }
        public double? DrawdownTimeForDetentionVolume { get; set; }
        public double? TotalEffectiveBMPVolume { get; set; }
        public double? TotalEffectiveDrywellBMPVolume { get; set; }
        public double? TreatmentRate { get; set; }
        public int? UnderlyingHydrologicSoilGroupID { get; set; }
        public double? UnderlyingInfiltrationRate { get; set; }
        public double? WaterQualityDetentionVolume { get; set; }
        public double? WettedFootprint { get; set; }
        public double? WinterHarvestedWaterDemand { get; set; }
        public int? OperationMonthID { get; set; }
        [NotMapped]
        public int PrimaryKey { get { return TreatmentBMPModelingAttributeID; } set { TreatmentBMPModelingAttributeID = value; } }

        public virtual TreatmentBMP TreatmentBMP { get; set; }
        public virtual TreatmentBMP UpstreamTreatmentBMP { get; set; }
        public RoutingConfiguration RoutingConfiguration { get { return RoutingConfigurationID.HasValue ? RoutingConfiguration.AllLookupDictionary[RoutingConfigurationID.Value] : null; } }
        public TimeOfConcentration TimeOfConcentration { get { return TimeOfConcentrationID.HasValue ? TimeOfConcentration.AllLookupDictionary[TimeOfConcentrationID.Value] : null; } }
        public UnderlyingHydrologicSoilGroup UnderlyingHydrologicSoilGroup { get { return UnderlyingHydrologicSoilGroupID.HasValue ? UnderlyingHydrologicSoilGroup.AllLookupDictionary[UnderlyingHydrologicSoilGroupID.Value] : null; } }
        public OperationMonth OperationMonth { get { return OperationMonthID.HasValue ? OperationMonth.AllLookupDictionary[OperationMonthID.Value] : null; } }

        public static class FieldLengths
        {

        }
    }
}