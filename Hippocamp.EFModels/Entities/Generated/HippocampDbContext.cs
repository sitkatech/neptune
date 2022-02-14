using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    public partial class HippocampDbContext : DbContext
    {
        public HippocampDbContext()
        {
        }

        public HippocampDbContext(DbContextOptions<HippocampDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<AuditLogEventType> AuditLogEventTypes { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<CustomAttribute> CustomAttributes { get; set; }
        public virtual DbSet<CustomAttributeDataType> CustomAttributeDataTypes { get; set; }
        public virtual DbSet<CustomAttributeType> CustomAttributeTypes { get; set; }
        public virtual DbSet<CustomAttributeTypePurpose> CustomAttributeTypePurposes { get; set; }
        public virtual DbSet<CustomAttributeValue> CustomAttributeValues { get; set; }
        public virtual DbSet<DatabaseMigration> DatabaseMigrations { get; set; }
        public virtual DbSet<Delineation> Delineations { get; set; }
        public virtual DbSet<DelineationOverlap> DelineationOverlaps { get; set; }
        public virtual DbSet<DelineationStaging> DelineationStagings { get; set; }
        public virtual DbSet<DelineationType> DelineationTypes { get; set; }
        public virtual DbSet<Deployment> Deployments { get; set; }
        public virtual DbSet<DirtyModelNode> DirtyModelNodes { get; set; }
        public virtual DbSet<DryWeatherFlowOverride> DryWeatherFlowOverrides { get; set; }
        public virtual DbSet<FieldDefinition> FieldDefinitions { get; set; }
        public virtual DbSet<FieldDefinitionType> FieldDefinitionTypes { get; set; }
        public virtual DbSet<FieldVisit> FieldVisits { get; set; }
        public virtual DbSet<FieldVisitSection> FieldVisitSections { get; set; }
        public virtual DbSet<FieldVisitStatus> FieldVisitStatuses { get; set; }
        public virtual DbSet<FieldVisitType> FieldVisitTypes { get; set; }
        public virtual DbSet<FileResource> FileResources { get; set; }
        public virtual DbSet<FileResourceMimeType> FileResourceMimeTypes { get; set; }
        public virtual DbSet<FundingEvent> FundingEvents { get; set; }
        public virtual DbSet<FundingEventFundingSource> FundingEventFundingSources { get; set; }
        public virtual DbSet<FundingEventType> FundingEventTypes { get; set; }
        public virtual DbSet<FundingSource> FundingSources { get; set; }
        public virtual DbSet<GoogleChartType> GoogleChartTypes { get; set; }
        public virtual DbSet<HRUCharacteristic> HRUCharacteristics { get; set; }
        public virtual DbSet<HRUCharacteristicLandUseCode> HRUCharacteristicLandUseCodes { get; set; }
        public virtual DbSet<HydrologicSubarea> HydrologicSubareas { get; set; }
        public virtual DbSet<HydromodificationAppliesType> HydromodificationAppliesTypes { get; set; }
        public virtual DbSet<LEGAL_LOTS_ATTRIBUTES_4326> LEGAL_LOTS_ATTRIBUTES_4326s { get; set; }
        public virtual DbSet<LSPCBasin> LSPCBasins { get; set; }
        public virtual DbSet<LSPCBasinStaging> LSPCBasinStagings { get; set; }
        public virtual DbSet<LandUseBlock> LandUseBlocks { get; set; }
        public virtual DbSet<LandUseBlockStaging> LandUseBlockStagings { get; set; }
        public virtual DbSet<LoadGeneratingUnit> LoadGeneratingUnits { get; set; }
        public virtual DbSet<LoadGeneratingUnitRefreshArea> LoadGeneratingUnitRefreshAreas { get; set; }
        public virtual DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public virtual DbSet<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        public virtual DbSet<MaintenanceRecordObservationValue> MaintenanceRecordObservationValues { get; set; }
        public virtual DbSet<MaintenanceRecordType> MaintenanceRecordTypes { get; set; }
        public virtual DbSet<MeasurementUnitType> MeasurementUnitTypes { get; set; }
        public virtual DbSet<MonthsOfOperation> MonthsOfOperations { get; set; }
        public virtual DbSet<NeptuneArea> NeptuneAreas { get; set; }
        public virtual DbSet<NeptuneHomePageImage> NeptuneHomePageImages { get; set; }
        public virtual DbSet<NeptunePage> NeptunePages { get; set; }
        public virtual DbSet<NeptunePageImage> NeptunePageImages { get; set; }
        public virtual DbSet<NeptunePageType> NeptunePageTypes { get; set; }
        public virtual DbSet<NereidResult> NereidResults { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }
        public virtual DbSet<OVTASection> OVTASections { get; set; }
        public virtual DbSet<ObservationTargetType> ObservationTargetTypes { get; set; }
        public virtual DbSet<ObservationThresholdType> ObservationThresholdTypes { get; set; }
        public virtual DbSet<ObservationTypeCollectionMethod> ObservationTypeCollectionMethods { get; set; }
        public virtual DbSet<ObservationTypeSpecification> ObservationTypeSpecifications { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreas { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentObservation> OnlandVisualTrashAssessmentObservations { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentObservationPhoto> OnlandVisualTrashAssessmentObservationPhotos { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentObservationPhotoStaging> OnlandVisualTrashAssessmentObservationPhotoStagings { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentScore> OnlandVisualTrashAssessmentScores { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentStatus> OnlandVisualTrashAssessmentStatuses { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationType> OrganizationTypes { get; set; }
        public virtual DbSet<Parcel> Parcels { get; set; }
        public virtual DbSet<PermitType> PermitTypes { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<PrecipitationZone> PrecipitationZones { get; set; }
        public virtual DbSet<PrecipitationZoneStaging> PrecipitationZoneStagings { get; set; }
        public virtual DbSet<PreliminarySourceIdentificationCategory> PreliminarySourceIdentificationCategories { get; set; }
        public virtual DbSet<PreliminarySourceIdentificationType> PreliminarySourceIdentificationTypes { get; set; }
        public virtual DbSet<PriorityLandUseType> PriorityLandUseTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectDocument> ProjectDocuments { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public virtual DbSet<QuickBMP> QuickBMPs { get; set; }
        public virtual DbSet<RegionalSubbasin> RegionalSubbasins { get; set; }
        public virtual DbSet<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequests { get; set; }
        public virtual DbSet<RegionalSubbasinRevisionRequestStatus> RegionalSubbasinRevisionRequestStatuses { get; set; }
        public virtual DbSet<RegionalSubbasinStaging> RegionalSubbasinStagings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoutingConfiguration> RoutingConfigurations { get; set; }
        public virtual DbSet<SizingBasisType> SizingBasisTypes { get; set; }
        public virtual DbSet<SourceControlBMP> SourceControlBMPs { get; set; }
        public virtual DbSet<SourceControlBMPAttribute> SourceControlBMPAttributes { get; set; }
        public virtual DbSet<SourceControlBMPAttributeCategory> SourceControlBMPAttributeCategories { get; set; }
        public virtual DbSet<StateProvince> StateProvinces { get; set; }
        public virtual DbSet<StormwaterBreadCrumbEntity> StormwaterBreadCrumbEntities { get; set; }
        public virtual DbSet<StormwaterJurisdiction> StormwaterJurisdictions { get; set; }
        public virtual DbSet<StormwaterJurisdictionGeometry> StormwaterJurisdictionGeometries { get; set; }
        public virtual DbSet<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        public virtual DbSet<StormwaterJurisdictionPublicBMPVisibilityType> StormwaterJurisdictionPublicBMPVisibilityTypes { get; set; }
        public virtual DbSet<StormwaterJurisdictionPublicWQMPVisibilityType> StormwaterJurisdictionPublicWQMPVisibilityTypes { get; set; }
        public virtual DbSet<SupportRequestLog> SupportRequestLogs { get; set; }
        public virtual DbSet<SupportRequestType> SupportRequestTypes { get; set; }
        public virtual DbSet<TimeOfConcentration> TimeOfConcentrations { get; set; }
        public virtual DbSet<TrainingVideo> TrainingVideos { get; set; }
        public virtual DbSet<TrashCaptureStatusType> TrashCaptureStatusTypes { get; set; }
        public virtual DbSet<TrashGeneratingUnit> TrashGeneratingUnits { get; set; }
        public virtual DbSet<TrashGeneratingUnit4326> TrashGeneratingUnit4326s { get; set; }
        public virtual DbSet<TrashGeneratingUnitAdjustment> TrashGeneratingUnitAdjustments { get; set; }
        public virtual DbSet<TreatmentBMP> TreatmentBMPs { get; set; }
        public virtual DbSet<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
        public virtual DbSet<TreatmentBMPAssessmentObservationType> TreatmentBMPAssessmentObservationTypes { get; set; }
        public virtual DbSet<TreatmentBMPAssessmentPhoto> TreatmentBMPAssessmentPhotos { get; set; }
        public virtual DbSet<TreatmentBMPAssessmentType> TreatmentBMPAssessmentTypes { get; set; }
        public virtual DbSet<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual DbSet<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; }
        public virtual DbSet<TreatmentBMPImage> TreatmentBMPImages { get; set; }
        public virtual DbSet<TreatmentBMPLifespanType> TreatmentBMPLifespanTypes { get; set; }
        public virtual DbSet<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
        public virtual DbSet<TreatmentBMPModelingType> TreatmentBMPModelingTypes { get; set; }
        public virtual DbSet<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public virtual DbSet<TreatmentBMPType> TreatmentBMPTypes { get; set; }
        public virtual DbSet<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get; set; }
        public virtual DbSet<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; }
        public virtual DbSet<UnderlyingHydrologicSoilGroup> UnderlyingHydrologicSoilGroups { get; set; }
        public virtual DbSet<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
        public virtual DbSet<WaterQualityManagementPlanDevelopmentType> WaterQualityManagementPlanDevelopmentTypes { get; set; }
        public virtual DbSet<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
        public virtual DbSet<WaterQualityManagementPlanDocumentType> WaterQualityManagementPlanDocumentTypes { get; set; }
        public virtual DbSet<WaterQualityManagementPlanLandUse> WaterQualityManagementPlanLandUses { get; set; }
        public virtual DbSet<WaterQualityManagementPlanModelingApproach> WaterQualityManagementPlanModelingApproaches { get; set; }
        public virtual DbSet<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get; set; }
        public virtual DbSet<WaterQualityManagementPlanPermitTerm> WaterQualityManagementPlanPermitTerms { get; set; }
        public virtual DbSet<WaterQualityManagementPlanPhoto> WaterQualityManagementPlanPhotos { get; set; }
        public virtual DbSet<WaterQualityManagementPlanPriority> WaterQualityManagementPlanPriorities { get; set; }
        public virtual DbSet<WaterQualityManagementPlanStatus> WaterQualityManagementPlanStatuses { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyPhoto> WaterQualityManagementPlanVerifyPhotos { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifySourceControlBMP> WaterQualityManagementPlanVerifySourceControlBMPs { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyStatus> WaterQualityManagementPlanVerifyStatuses { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyType> WaterQualityManagementPlanVerifyTypes { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVisitStatus> WaterQualityManagementPlanVisitStatuses { get; set; }
        public virtual DbSet<Watershed> Watersheds { get; set; }
        public virtual DbSet<geometry_column> geometry_columns { get; set; }
        public virtual DbSet<gt_pk_metadatum> gt_pk_metadata { get; set; }
        public virtual DbSet<spatial_ref_sy> spatial_ref_sys { get; set; }
        public virtual DbSet<vDelineationLGUInput> vDelineationLGUInputs { get; set; }
        public virtual DbSet<vDelineationTGUInput> vDelineationTGUInputs { get; set; }
        public virtual DbSet<vFieldVisitDetailed> vFieldVisitDetaileds { get; set; }
        public virtual DbSet<vGeoServerAssessmentAreaExport> vGeoServerAssessmentAreaExports { get; set; }
        public virtual DbSet<vGeoServerDelineation> vGeoServerDelineations { get; set; }
        public virtual DbSet<vGeoServerJurisdiction> vGeoServerJurisdictions { get; set; }
        public virtual DbSet<vGeoServerLandUseBlock> vGeoServerLandUseBlocks { get; set; }
        public virtual DbSet<vGeoServerMaskLayer> vGeoServerMaskLayers { get; set; }
        public virtual DbSet<vGeoServerObservationPointExport> vGeoServerObservationPointExports { get; set; }
        public virtual DbSet<vGeoServerOnlandVisualTrashAssessmentArea> vGeoServerOnlandVisualTrashAssessmentAreas { get; set; }
        public virtual DbSet<vGeoServerParcel> vGeoServerParcels { get; set; }
        public virtual DbSet<vGeoServerRegionalSubbasin> vGeoServerRegionalSubbasins { get; set; }
        public virtual DbSet<vGeoServerTransectLineExport> vGeoServerTransectLineExports { get; set; }
        public virtual DbSet<vGeoServerTrashGeneratingUnit> vGeoServerTrashGeneratingUnits { get; set; }
        public virtual DbSet<vGeoServerTrashGeneratingUnitLoad> vGeoServerTrashGeneratingUnitLoads { get; set; }
        public virtual DbSet<vGeoServerWaterQualityManagementPlan> vGeoServerWaterQualityManagementPlans { get; set; }
        public virtual DbSet<vGeoServerWatershed> vGeoServerWatersheds { get; set; }
        public virtual DbSet<vLSPCBasinLGUInput> vLSPCBasinLGUInputs { get; set; }
        public virtual DbSet<vLandUseBlockTGUInput> vLandUseBlockTGUInputs { get; set; }
        public virtual DbSet<vMostRecentTreatmentBMPAssessment> vMostRecentTreatmentBMPAssessments { get; set; }
        public virtual DbSet<vNereidBMPColocation> vNereidBMPColocations { get; set; }
        public virtual DbSet<vNereidLoadingInput> vNereidLoadingInputs { get; set; }
        public virtual DbSet<vNereidRegionalSubbasinCentralizedBMP> vNereidRegionalSubbasinCentralizedBMPs { get; set; }
        public virtual DbSet<vNereidTreatmentBMPRegionalSubbasin> vNereidTreatmentBMPRegionalSubbasins { get; set; }
        public virtual DbSet<vOnlandVisualTrashAssessmentAreaDated> vOnlandVisualTrashAssessmentAreaDateds { get; set; }
        public virtual DbSet<vOnlandVisualTrashAssessmentAreaProgress> vOnlandVisualTrashAssessmentAreaProgresses { get; set; }
        public virtual DbSet<vPowerBICentralizedBMPLoadGeneratingUnit> vPowerBICentralizedBMPLoadGeneratingUnits { get; set; }
        public virtual DbSet<vPowerBILandUseStatistic> vPowerBILandUseStatistics { get; set; }
        public virtual DbSet<vPowerBITreatmentBMP> vPowerBITreatmentBMPs { get; set; }
        public virtual DbSet<vPowerBIWaterQualityManagementPlan> vPowerBIWaterQualityManagementPlans { get; set; }
        public virtual DbSet<vPowerBIWaterQualityManagementPlanOAndMVerification> vPowerBIWaterQualityManagementPlanOAndMVerifications { get; set; }
        public virtual DbSet<vRegionalSubbasinLGUInput> vRegionalSubbasinLGUInputs { get; set; }
        public virtual DbSet<vRegionalSubbasinUpstreamCatchmentGeometry4326> vRegionalSubbasinUpstreamCatchmentGeometry4326s { get; set; }
        public virtual DbSet<vStormwaterJurisdictionOrganizationMapping> vStormwaterJurisdictionOrganizationMappings { get; set; }
        public virtual DbSet<vTrashGeneratingUnitLoadStatistic> vTrashGeneratingUnitLoadStatistics { get; set; }
        public virtual DbSet<vTreatmentBMPDetailed> vTreatmentBMPDetaileds { get; set; }
        public virtual DbSet<vViewTreatmentBMPModelingAttribute> vViewTreatmentBMPModelingAttributes { get; set; }
        public virtual DbSet<vWaterQualityManagementPlanLGUAudit> vWaterQualityManagementPlanLGUAudits { get; set; }
        public virtual DbSet<vWaterQualityManagementPlanLGUInput> vWaterQualityManagementPlanLGUInputs { get; set; }
        public virtual DbSet<vWaterQualityManagementPlanTGUInput> vWaterQualityManagementPlanTGUInputs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.Property(e => e.AuditDescription).IsUnicode(false);

                entity.Property(e => e.ColumnName).IsUnicode(false);

                entity.Property(e => e.NewValue).IsUnicode(false);

                entity.Property(e => e.OriginalValue).IsUnicode(false);

                entity.Property(e => e.TableName).IsUnicode(false);

                entity.HasOne(d => d.AuditLogEventType)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.AuditLogEventTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.AuditLogs)
                    .HasForeignKey(d => d.PersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AuditLogEventType>(entity =>
            {
                entity.Property(e => e.AuditLogEventTypeID).ValueGeneratedNever();

                entity.Property(e => e.AuditLogEventTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.AuditLogEventTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<County>(entity =>
            {
                entity.Property(e => e.CountyID).ValueGeneratedNever();

                entity.Property(e => e.CountyName).IsUnicode(false);

                entity.HasOne(d => d.StateProvince)
                    .WithMany(p => p.Counties)
                    .HasForeignKey(d => d.StateProvinceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CustomAttribute>(entity =>
            {
                entity.HasOne(d => d.CustomAttributeType)
                    .WithMany(p => p.CustomAttributes)
                    .HasForeignKey(d => d.CustomAttributeTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.CustomAttributeTreatmentBMPs)
                    .HasForeignKey(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPTypeCustomAttributeType)
                    .WithMany(p => p.CustomAttributeTreatmentBMPTypeCustomAttributeTypes)
                    .HasForeignKey(d => d.TreatmentBMPTypeCustomAttributeTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.CustomAttributes)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPNavigation)
                    .WithMany(p => p.CustomAttributeTreatmentBMPNavigations)
                    .HasPrincipalKey(p => new { p.TreatmentBMPID, p.TreatmentBMPTypeID })
                    .HasForeignKey(d => new { d.TreatmentBMPID, d.TreatmentBMPTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPTypeCustomAttributeTypeNavigation)
                    .WithMany(p => p.CustomAttributeTreatmentBMPTypeCustomAttributeTypeNavigations)
                    .HasPrincipalKey(p => new { p.TreatmentBMPTypeID, p.CustomAttributeTypeID })
                    .HasForeignKey(d => new { d.TreatmentBMPTypeID, d.CustomAttributeTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CustomAttributeDataType>(entity =>
            {
                entity.Property(e => e.CustomAttributeDataTypeID).ValueGeneratedNever();

                entity.Property(e => e.CustomAttributeDataTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.CustomAttributeDataTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<CustomAttributeType>(entity =>
            {
                entity.Property(e => e.CustomAttributeTypeDescription).IsUnicode(false);

                entity.Property(e => e.CustomAttributeTypeName).IsUnicode(false);

                entity.Property(e => e.CustomAttributeTypeOptionsSchema).IsUnicode(false);

                entity.HasOne(d => d.CustomAttributeDataType)
                    .WithMany(p => p.CustomAttributeTypes)
                    .HasForeignKey(d => d.CustomAttributeDataTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.CustomAttributeTypePurpose)
                    .WithMany(p => p.CustomAttributeTypes)
                    .HasForeignKey(d => d.CustomAttributeTypePurposeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<CustomAttributeTypePurpose>(entity =>
            {
                entity.Property(e => e.CustomAttributeTypePurposeID).ValueGeneratedNever();

                entity.Property(e => e.CustomAttributeTypePurposeDisplayName).IsUnicode(false);

                entity.Property(e => e.CustomAttributeTypePurposeName).IsUnicode(false);
            });

            modelBuilder.Entity<CustomAttributeValue>(entity =>
            {
                entity.Property(e => e.AttributeValue).IsUnicode(false);

                entity.HasOne(d => d.CustomAttribute)
                    .WithMany(p => p.CustomAttributeValues)
                    .HasForeignKey(d => d.CustomAttributeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<DatabaseMigration>(entity =>
            {
                entity.HasKey(e => e.DatabaseMigrationNumber)
                    .HasName("PK_DatabaseMigration_DatabaseMigrationNumber");

                entity.Property(e => e.DatabaseMigrationNumber).ValueGeneratedNever();
            });

            modelBuilder.Entity<Delineation>(entity =>
            {
                entity.HasOne(d => d.DelineationType)
                    .WithMany(p => p.Delineations)
                    .HasForeignKey(d => d.DelineationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithOne(p => p.Delineation)
                    .HasForeignKey<Delineation>(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.VerifiedByPerson)
                    .WithMany(p => p.Delineations)
                    .HasForeignKey(d => d.VerifiedByPersonID)
                    .HasConstraintName("FK_Delineation_Person_VerifiedByPersonID_PersonID");
            });

            modelBuilder.Entity<DelineationOverlap>(entity =>
            {
                entity.Property(e => e.DelineationOverlapID).ValueGeneratedNever();

                entity.HasOne(d => d.Delineation)
                    .WithMany(p => p.DelineationOverlapDelineations)
                    .HasForeignKey(d => d.DelineationID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.OverlappingDelineation)
                    .WithMany(p => p.DelineationOverlapOverlappingDelineations)
                    .HasForeignKey(d => d.OverlappingDelineationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DelineationOverlap_Delineation_OverlappingDelineationID_DelineationID");
            });

            modelBuilder.Entity<DelineationStaging>(entity =>
            {
                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.DelineationStagings)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UploadedByPerson)
                    .WithMany(p => p.DelineationStagings)
                    .HasForeignKey(d => d.UploadedByPersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DelineationStaging_Person_UploadedByPersonID_PersonID");
            });

            modelBuilder.Entity<DelineationType>(entity =>
            {
                entity.Property(e => e.DelineationTypeID).ValueGeneratedNever();

                entity.Property(e => e.DelineationTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.DelineationTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Deployment>(entity =>
            {
                entity.Property(e => e.DeployedBy).IsUnicode(false);

                entity.Property(e => e.DeployedFrom).IsUnicode(false);

                entity.Property(e => e.Script).IsUnicode(false);

                entity.Property(e => e.Source).IsUnicode(false);

                entity.Property(e => e.Version).IsUnicode(false);
            });

            modelBuilder.Entity<DryWeatherFlowOverride>(entity =>
            {
                entity.Property(e => e.DryWeatherFlowOverrideID).ValueGeneratedNever();

                entity.Property(e => e.DryWeatherFlowOverrideDisplayName).IsUnicode(false);

                entity.Property(e => e.DryWeatherFlowOverrideName).IsUnicode(false);
            });

            modelBuilder.Entity<FieldDefinition>(entity =>
            {
                entity.Property(e => e.FieldDefinitionValue).IsUnicode(false);

                entity.HasOne(d => d.FieldDefinitionType)
                    .WithMany(p => p.FieldDefinitions)
                    .HasForeignKey(d => d.FieldDefinitionTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FieldDefinitionType>(entity =>
            {
                entity.Property(e => e.FieldDefinitionTypeID).ValueGeneratedNever();

                entity.Property(e => e.FieldDefinitionTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.FieldDefinitionTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<FieldVisit>(entity =>
            {
                entity.HasIndex(e => e.TreatmentBMPID, "CK_AtMostOneFieldVisitMayBeInProgressAtAnyTimePerBMP")
                    .IsUnique()
                    .HasFilter("([FieldVisitStatusID]=(1))");

                entity.HasOne(d => d.FieldVisitStatus)
                    .WithMany(p => p.FieldVisits)
                    .HasForeignKey(d => d.FieldVisitStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.FieldVisitType)
                    .WithMany(p => p.FieldVisits)
                    .HasForeignKey(d => d.FieldVisitTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PerformedByPerson)
                    .WithMany(p => p.FieldVisits)
                    .HasForeignKey(d => d.PerformedByPersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldVisit_Person_PerformedByPersonID_PersonID");

                entity.HasOne(d => d.TreatmentBMP)
                    .WithOne(p => p.FieldVisit)
                    .HasForeignKey<FieldVisit>(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FieldVisitSection>(entity =>
            {
                entity.Property(e => e.FieldVisitSectionID).ValueGeneratedNever();

                entity.Property(e => e.FieldVisitSectionDisplayName).IsUnicode(false);

                entity.Property(e => e.FieldVisitSectionName).IsUnicode(false);

                entity.Property(e => e.SectionHeader).IsUnicode(false);
            });

            modelBuilder.Entity<FieldVisitStatus>(entity =>
            {
                entity.Property(e => e.FieldVisitStatusID).ValueGeneratedNever();

                entity.Property(e => e.FieldVisitStatusDisplayName).IsUnicode(false);

                entity.Property(e => e.FieldVisitStatusName).IsUnicode(false);
            });

            modelBuilder.Entity<FieldVisitType>(entity =>
            {
                entity.Property(e => e.FieldVisitTypeID).ValueGeneratedNever();

                entity.Property(e => e.FieldVisitTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.FieldVisitTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<FileResource>(entity =>
            {
                entity.Property(e => e.OriginalBaseFilename).IsUnicode(false);

                entity.Property(e => e.OriginalFileExtension).IsUnicode(false);

                entity.HasOne(d => d.CreatePerson)
                    .WithMany(p => p.FileResources)
                    .HasForeignKey(d => d.CreatePersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FileResource_Person_CreatePersonID_PersonID");

                entity.HasOne(d => d.FileResourceMimeType)
                    .WithMany(p => p.FileResources)
                    .HasForeignKey(d => d.FileResourceMimeTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FileResourceMimeType>(entity =>
            {
                entity.Property(e => e.FileResourceMimeTypeID).ValueGeneratedNever();

                entity.Property(e => e.FileResourceMimeTypeContentTypeName).IsUnicode(false);

                entity.Property(e => e.FileResourceMimeTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.FileResourceMimeTypeIconNormalFilename).IsUnicode(false);

                entity.Property(e => e.FileResourceMimeTypeIconSmallFilename).IsUnicode(false);

                entity.Property(e => e.FileResourceMimeTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<FundingEvent>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasOne(d => d.FundingEventType)
                    .WithMany(p => p.FundingEvents)
                    .HasForeignKey(d => d.FundingEventTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.FundingEvents)
                    .HasForeignKey(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FundingEventFundingSource>(entity =>
            {
                entity.HasOne(d => d.FundingEvent)
                    .WithMany(p => p.FundingEventFundingSources)
                    .HasForeignKey(d => d.FundingEventID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.FundingSource)
                    .WithMany(p => p.FundingEventFundingSources)
                    .HasForeignKey(d => d.FundingSourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<FundingEventType>(entity =>
            {
                entity.Property(e => e.FundingEventTypeID).ValueGeneratedNever();

                entity.Property(e => e.FundingEventTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.FundingEventTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<FundingSource>(entity =>
            {
                entity.Property(e => e.FundingSourceDescription).IsUnicode(false);

                entity.Property(e => e.FundingSourceName).IsUnicode(false);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.FundingSources)
                    .HasForeignKey(d => d.OrganizationID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<GoogleChartType>(entity =>
            {
                entity.Property(e => e.GoogleChartTypeID).ValueGeneratedNever();

                entity.Property(e => e.GoogleChartTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.GoogleChartTypeName).IsUnicode(false);

                entity.Property(e => e.SeriesDataDisplayType).IsUnicode(false);
            });

            modelBuilder.Entity<HRUCharacteristic>(entity =>
            {
                entity.Property(e => e.HydrologicSoilGroup).IsUnicode(false);

                entity.HasOne(d => d.BaselineHRUCharacteristicLandUseCode)
                    .WithMany(p => p.HRUCharacteristicBaselineHRUCharacteristicLandUseCodes)
                    .HasForeignKey(d => d.BaselineHRUCharacteristicLandUseCodeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HRUCharacteristic_HRUCharacteristicLandUseCodeID");

                entity.HasOne(d => d.HRUCharacteristicLandUseCode)
                    .WithMany(p => p.HRUCharacteristicHRUCharacteristicLandUseCodes)
                    .HasForeignKey(d => d.HRUCharacteristicLandUseCodeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.LoadGeneratingUnit)
                    .WithMany(p => p.HRUCharacteristics)
                    .HasForeignKey(d => d.LoadGeneratingUnitID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<HRUCharacteristicLandUseCode>(entity =>
            {
                entity.Property(e => e.HRUCharacteristicLandUseCodeID).ValueGeneratedNever();

                entity.Property(e => e.HRUCharacteristicLandUseCodeDisplayName).IsUnicode(false);

                entity.Property(e => e.HRUCharacteristicLandUseCodeName).IsUnicode(false);
            });

            modelBuilder.Entity<HydrologicSubarea>(entity =>
            {
                entity.Property(e => e.HydrologicSubareaName).IsUnicode(false);
            });

            modelBuilder.Entity<HydromodificationAppliesType>(entity =>
            {
                entity.Property(e => e.HydromodificationAppliesTypeID).ValueGeneratedNever();

                entity.Property(e => e.HydromodificationAppliesTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.HydromodificationAppliesTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<LEGAL_LOTS_ATTRIBUTES_4326>(entity =>
            {
                entity.Property(e => e.ASSESSMENT_NO).IsUnicode(false);

                entity.Property(e => e.CATEGORY).IsUnicode(false);

                entity.Property(e => e.COLOR).IsUnicode(false);

                entity.Property(e => e.DESCRIPTIO).IsUnicode(false);

                entity.Property(e => e.DOC_BUYER_NAME).IsUnicode(false);

                entity.Property(e => e.DOC_DEED_TYPE).IsUnicode(false);

                entity.Property(e => e.DOC_REF_DATE).IsUnicode(false);

                entity.Property(e => e.DOC_REF_NO).IsUnicode(false);

                entity.Property(e => e.DOC_SELLER_NAME).IsUnicode(false);

                entity.Property(e => e.Dist_Regs).IsUnicode(false);

                entity.Property(e => e.LEGAL_DESCR).IsUnicode(false);

                entity.Property(e => e.LOT_SIZE).IsUnicode(false);

                entity.Property(e => e.Link).IsUnicode(false);

                entity.Property(e => e.MAIL_ADDRESS).IsUnicode(false);

                entity.Property(e => e.MAIL_ADDR_NO).IsUnicode(false);

                entity.Property(e => e.MAIL_CITY_STATE).IsUnicode(false);

                entity.Property(e => e.MAIL_PREFIX).IsUnicode(false);

                entity.Property(e => e.MAIL_STREET).IsUnicode(false);

                entity.Property(e => e.MAIL_SUFFIX).IsUnicode(false);

                entity.Property(e => e.MAIL_UNIT_NO).IsUnicode(false);

                entity.Property(e => e.MAIL_ZIP5).IsUnicode(false);

                entity.Property(e => e.OWNER_NAMES).IsUnicode(false);

                entity.Property(e => e.RECORDER_NO).IsUnicode(false);

                entity.Property(e => e.SALE_TYPE).IsUnicode(false);

                entity.Property(e => e.SITE_ADDRESS).IsUnicode(false);

                entity.Property(e => e.SITE_ADDR_NO).IsUnicode(false);

                entity.Property(e => e.SITE_CITY_STATE).IsUnicode(false);

                entity.Property(e => e.SITE_STREET_NAME).IsUnicode(false);

                entity.Property(e => e.SITE_STREET_PREFIX).IsUnicode(false);

                entity.Property(e => e.SITE_STREET_SUFFIX).IsUnicode(false);

                entity.Property(e => e.SITE_ZIP5).IsUnicode(false);

                entity.Property(e => e.USE_DQ_LANDUSE).IsUnicode(false);

                entity.Property(e => e.ZONECLASS).IsUnicode(false);

                entity.Property(e => e.ZONE_AND_Housing).IsUnicode(false);
            });

            modelBuilder.Entity<LSPCBasin>(entity =>
            {
                entity.Property(e => e.LSPCBasinName).IsUnicode(false);
            });

            modelBuilder.Entity<LSPCBasinStaging>(entity =>
            {
                entity.Property(e => e.LSPCBasinName).IsUnicode(false);
            });

            modelBuilder.Entity<LandUseBlock>(entity =>
            {
                entity.Property(e => e.LandUseDescription).IsUnicode(false);

                entity.Property(e => e.LandUseForTGR).IsUnicode(false);

                entity.HasOne(d => d.PermitType)
                    .WithMany(p => p.LandUseBlocks)
                    .HasForeignKey(d => d.PermitTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.LandUseBlocks)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<LandUseBlockStaging>(entity =>
            {
                entity.Property(e => e.LandUseDescription).IsUnicode(false);

                entity.Property(e => e.LandUseForTGR).IsUnicode(false);

                entity.Property(e => e.PermitType).IsUnicode(false);

                entity.Property(e => e.PriorityLandUseType).IsUnicode(false);

                entity.Property(e => e.StormwaterJurisdiction).IsUnicode(false);

                entity.HasOne(d => d.UploadedByPerson)
                    .WithMany(p => p.LandUseBlockStagings)
                    .HasForeignKey(d => d.UploadedByPersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LandUseBlockStaging_Person_UploadedByPersonID_PersonID");
            });

            modelBuilder.Entity<MaintenanceRecord>(entity =>
            {
                entity.Property(e => e.MaintenanceRecordDescription).IsUnicode(false);

                entity.HasOne(d => d.FieldVisit)
                    .WithOne(p => p.MaintenanceRecordFieldVisit)
                    .HasForeignKey<MaintenanceRecord>(d => d.FieldVisitID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.MaintenanceRecordTreatmentBMPs)
                    .HasForeignKey(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.MaintenanceRecords)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.FieldVisitNavigation)
                    .WithMany(p => p.MaintenanceRecordFieldVisitNavigations)
                    .HasPrincipalKey(p => new { p.FieldVisitID, p.TreatmentBMPID })
                    .HasForeignKey(d => new { d.FieldVisitID, d.TreatmentBMPID })
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPNavigation)
                    .WithMany(p => p.MaintenanceRecordTreatmentBMPNavigations)
                    .HasPrincipalKey(p => new { p.TreatmentBMPID, p.TreatmentBMPTypeID })
                    .HasForeignKey(d => new { d.TreatmentBMPID, d.TreatmentBMPTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MaintenanceRecordObservation>(entity =>
            {
                entity.HasOne(d => d.CustomAttributeType)
                    .WithMany(p => p.MaintenanceRecordObservations)
                    .HasForeignKey(d => d.CustomAttributeTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MaintenanceRecord)
                    .WithMany(p => p.MaintenanceRecordObservationMaintenanceRecords)
                    .HasForeignKey(d => d.MaintenanceRecordID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPTypeCustomAttributeType)
                    .WithMany(p => p.MaintenanceRecordObservationTreatmentBMPTypeCustomAttributeTypes)
                    .HasForeignKey(d => d.TreatmentBMPTypeCustomAttributeTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.MaintenanceRecordObservations)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.MaintenanceRecordNavigation)
                    .WithMany(p => p.MaintenanceRecordObservationMaintenanceRecordNavigations)
                    .HasPrincipalKey(p => new { p.MaintenanceRecordID, p.TreatmentBMPTypeID })
                    .HasForeignKey(d => new { d.MaintenanceRecordID, d.TreatmentBMPTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPTypeCustomAttributeTypeNavigation)
                    .WithMany(p => p.MaintenanceRecordObservationTreatmentBMPTypeCustomAttributeTypeNavigations)
                    .HasPrincipalKey(p => new { p.TreatmentBMPTypeID, p.CustomAttributeTypeID })
                    .HasForeignKey(d => new { d.TreatmentBMPTypeID, d.CustomAttributeTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MaintenanceRecordObservationValue>(entity =>
            {
                entity.Property(e => e.ObservationValue).IsUnicode(false);

                entity.HasOne(d => d.MaintenanceRecordObservation)
                    .WithMany(p => p.MaintenanceRecordObservationValues)
                    .HasForeignKey(d => d.MaintenanceRecordObservationID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MaintenanceRecordType>(entity =>
            {
                entity.Property(e => e.MaintenanceRecordTypeID).ValueGeneratedNever();

                entity.Property(e => e.MaintenanceRecordTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.MaintenanceRecordTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<MeasurementUnitType>(entity =>
            {
                entity.Property(e => e.MeasurementUnitTypeID).ValueGeneratedNever();

                entity.Property(e => e.LegendDisplayName).IsUnicode(false);

                entity.Property(e => e.MeasurementUnitTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.MeasurementUnitTypeName).IsUnicode(false);

                entity.Property(e => e.SingularDisplayName).IsUnicode(false);
            });

            modelBuilder.Entity<MonthsOfOperation>(entity =>
            {
                entity.Property(e => e.MonthsOfOperationID).ValueGeneratedNever();

                entity.Property(e => e.MonthsOfOperationDisplayName).IsUnicode(false);

                entity.Property(e => e.MonthsOfOperationName).IsUnicode(false);

                entity.Property(e => e.MonthsOfOperationNereidAlias).IsUnicode(false);
            });

            modelBuilder.Entity<NeptuneArea>(entity =>
            {
                entity.Property(e => e.NeptuneAreaID).ValueGeneratedNever();

                entity.Property(e => e.NeptuneAreaDisplayName).IsUnicode(false);

                entity.Property(e => e.NeptuneAreaName).IsUnicode(false);
            });

            modelBuilder.Entity<NeptuneHomePageImage>(entity =>
            {
                entity.Property(e => e.Caption).IsUnicode(false);

                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.NeptuneHomePageImages)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<NeptunePage>(entity =>
            {
                entity.Property(e => e.NeptunePageContent).IsUnicode(false);

                entity.HasOne(d => d.NeptunePageType)
                    .WithMany(p => p.NeptunePages)
                    .HasForeignKey(d => d.NeptunePageTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<NeptunePageImage>(entity =>
            {
                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.NeptunePageImages)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.NeptunePage)
                    .WithMany(p => p.NeptunePageImages)
                    .HasForeignKey(d => d.NeptunePageID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<NeptunePageType>(entity =>
            {
                entity.Property(e => e.NeptunePageTypeID).ValueGeneratedNever();

                entity.Property(e => e.NeptunePageTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.NeptunePageTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<NereidResult>(entity =>
            {
                entity.Property(e => e.FullResponse).IsUnicode(false);

                entity.Property(e => e.NodeID).IsUnicode(false);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.NotificationType)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.NotificationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.PersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<NotificationType>(entity =>
            {
                entity.Property(e => e.NotificationTypeID).ValueGeneratedNever();

                entity.Property(e => e.NotificationTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.NotificationTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<OVTASection>(entity =>
            {
                entity.Property(e => e.OVTASectionID).ValueGeneratedNever();

                entity.Property(e => e.OVTASectionDisplayName).IsUnicode(false);

                entity.Property(e => e.OVTASectionName).IsUnicode(false);

                entity.Property(e => e.SectionHeader).IsUnicode(false);
            });

            modelBuilder.Entity<ObservationTargetType>(entity =>
            {
                entity.Property(e => e.ObservationTargetTypeID).ValueGeneratedNever();

                entity.Property(e => e.ObservationTargetTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.ObservationTargetTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<ObservationThresholdType>(entity =>
            {
                entity.Property(e => e.ObservationThresholdTypeID).ValueGeneratedNever();

                entity.Property(e => e.ObservationThresholdTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.ObservationThresholdTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<ObservationTypeCollectionMethod>(entity =>
            {
                entity.Property(e => e.ObservationTypeCollectionMethodID).ValueGeneratedNever();

                entity.Property(e => e.ObservationTypeCollectionMethodDescription).IsUnicode(false);

                entity.Property(e => e.ObservationTypeCollectionMethodDisplayName).IsUnicode(false);

                entity.Property(e => e.ObservationTypeCollectionMethodName).IsUnicode(false);
            });

            modelBuilder.Entity<ObservationTypeSpecification>(entity =>
            {
                entity.Property(e => e.ObservationTypeSpecificationID).ValueGeneratedNever();

                entity.Property(e => e.ObservationTypeSpecificationDisplayName).IsUnicode(false);

                entity.Property(e => e.ObservationTypeSpecificationName).IsUnicode(false);

                entity.HasOne(d => d.ObservationTargetType)
                    .WithMany(p => p.ObservationTypeSpecifications)
                    .HasForeignKey(d => d.ObservationTargetTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ObservationThresholdType)
                    .WithMany(p => p.ObservationTypeSpecifications)
                    .HasForeignKey(d => d.ObservationThresholdTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.ObservationTypeCollectionMethod)
                    .WithMany(p => p.ObservationTypeSpecifications)
                    .HasForeignKey(d => d.ObservationTypeCollectionMethodID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OnlandVisualTrashAssessment>(entity =>
            {
                entity.HasIndex(e => e.OnlandVisualTrashAssessmentAreaID, "CK_OnlandVisualTrashAssessment_AtMostOneTransectBackingAssessmentPerArea")
                    .IsUnique()
                    .HasFilter("([IsTransectBackingAssessment]=(1))");

                entity.Property(e => e.DraftAreaDescription).IsUnicode(false);

                entity.Property(e => e.DraftAreaName).IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.HasOne(d => d.CreatedByPerson)
                    .WithMany(p => p.OnlandVisualTrashAssessments)
                    .HasForeignKey(d => d.CreatedByPersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID");

                entity.HasOne(d => d.OnlandVisualTrashAssessmentStatus)
                    .WithMany(p => p.OnlandVisualTrashAssessments)
                    .HasForeignKey(d => d.OnlandVisualTrashAssessmentStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.OnlandVisualTrashAssessments)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.OnlandVisualTrashAssessmentAreaNavigation)
                    .WithMany(p => p.OnlandVisualTrashAssessmentOnlandVisualTrashAssessmentAreaNavigations)
                    .HasPrincipalKey(p => new { p.OnlandVisualTrashAssessmentAreaID, p.StormwaterJurisdictionID })
                    .HasForeignKey(d => new { d.OnlandVisualTrashAssessmentAreaID, d.StormwaterJurisdictionID });
            });

            modelBuilder.Entity<OnlandVisualTrashAssessmentArea>(entity =>
            {
                entity.Property(e => e.AssessmentAreaDescription).IsUnicode(false);

                entity.Property(e => e.OnlandVisualTrashAssessmentAreaName).IsUnicode(false);

                entity.HasOne(d => d.OnlandVisualTrashAssessmentBaselineScore)
                    .WithMany(p => p.OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentBaselineScores)
                    .HasForeignKey(d => d.OnlandVisualTrashAssessmentBaselineScoreID)
                    .HasConstraintName("FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentBaselineScoreID_OnlandVisualTrash");

                entity.HasOne(d => d.OnlandVisualTrashAssessmentProgressScore)
                    .WithMany(p => p.OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentProgressScores)
                    .HasForeignKey(d => d.OnlandVisualTrashAssessmentProgressScoreID)
                    .HasConstraintName("FK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentProgressScoreID_OnlandVisualTrash");

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.OnlandVisualTrashAssessmentAreas)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OnlandVisualTrashAssessmentObservation>(entity =>
            {
                entity.Property(e => e.Note).IsUnicode(false);

                entity.HasOne(d => d.OnlandVisualTrashAssessment)
                    .WithMany(p => p.OnlandVisualTrashAssessmentObservations)
                    .HasForeignKey(d => d.OnlandVisualTrashAssessmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OnlandVisualTrashAssessmentObservationPhoto>(entity =>
            {
                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.OnlandVisualTrashAssessmentObservationPhotos)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.OnlandVisualTrashAssessmentObservation)
                    .WithMany(p => p.OnlandVisualTrashAssessmentObservationPhotos)
                    .HasForeignKey(d => d.OnlandVisualTrashAssessmentObservationID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OnlandVisualTrashAssessmentObservationPhotoStaging>(entity =>
            {
                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.OnlandVisualTrashAssessmentObservationPhotoStagings)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.OnlandVisualTrashAssessment)
                    .WithMany(p => p.OnlandVisualTrashAssessmentObservationPhotoStagings)
                    .HasForeignKey(d => d.OnlandVisualTrashAssessmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType>(entity =>
            {
                entity.Property(e => e.ExplanationIfTypeIsOther).IsUnicode(false);

                entity.HasOne(d => d.OnlandVisualTrashAssessment)
                    .WithMany(p => p.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes)
                    .HasForeignKey(d => d.OnlandVisualTrashAssessmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PreliminarySourceIdentificationType)
                    .WithMany(p => p.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes)
                    .HasForeignKey(d => d.PreliminarySourceIdentificationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_PreliminarySourceIdentificationType_PreliminarySourceIdentific");
            });

            modelBuilder.Entity<OnlandVisualTrashAssessmentScore>(entity =>
            {
                entity.Property(e => e.OnlandVisualTrashAssessmentScoreID).ValueGeneratedNever();

                entity.Property(e => e.OnlandVisualTrashAssessmentScoreDisplayName).IsUnicode(false);

                entity.Property(e => e.OnlandVisualTrashAssessmentScoreName).IsUnicode(false);
            });

            modelBuilder.Entity<OnlandVisualTrashAssessmentStatus>(entity =>
            {
                entity.Property(e => e.OnlandVisualTrashAssessmentStatusID).ValueGeneratedNever();

                entity.Property(e => e.OnlandVisualTrashAssessmentStatusDisplayName).IsUnicode(false);

                entity.Property(e => e.OnlandVisualTrashAssessmentStatusName).IsUnicode(false);
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.OrganizationShortName).IsUnicode(false);

                entity.Property(e => e.OrganizationUrl).IsUnicode(false);

                entity.HasOne(d => d.LogoFileResource)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.LogoFileResourceID)
                    .HasConstraintName("FK_Organization_FileResource_LogoFileResourceID_FileResourceID");

                entity.HasOne(d => d.OrganizationType)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.OrganizationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PrimaryContactPerson)
                    .WithMany(p => p.Organizations)
                    .HasForeignKey(d => d.PrimaryContactPersonID)
                    .HasConstraintName("FK_Organization_Person_PrimaryContactPersonID_PersonID");
            });

            modelBuilder.Entity<OrganizationType>(entity =>
            {
                entity.Property(e => e.LegendColor).IsUnicode(false);

                entity.Property(e => e.OrganizationTypeAbbreviation).IsUnicode(false);

                entity.Property(e => e.OrganizationTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Parcel>(entity =>
            {
                entity.Property(e => e.LandUse).IsUnicode(false);

                entity.Property(e => e.OwnerName).IsUnicode(false);

                entity.Property(e => e.ParcelAddress).IsUnicode(false);

                entity.Property(e => e.ParcelNumber).IsUnicode(false);

                entity.Property(e => e.ParcelStreetNumber).IsUnicode(false);

                entity.Property(e => e.ParcelZipCode).IsUnicode(false);
            });

            modelBuilder.Entity<PermitType>(entity =>
            {
                entity.Property(e => e.PermitTypeID).ValueGeneratedNever();

                entity.Property(e => e.PermitTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.PermitTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.LoginName).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.OrganizationID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.People)
                    .HasForeignKey(d => d.RoleID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PreliminarySourceIdentificationCategory>(entity =>
            {
                entity.Property(e => e.PreliminarySourceIdentificationCategoryID).ValueGeneratedNever();

                entity.Property(e => e.PreliminarySourceIdentificationCategoryDisplayName).IsUnicode(false);

                entity.Property(e => e.PreliminarySourceIdentificationCategoryName).IsUnicode(false);
            });

            modelBuilder.Entity<PreliminarySourceIdentificationType>(entity =>
            {
                entity.Property(e => e.PreliminarySourceIdentificationTypeID).ValueGeneratedNever();

                entity.Property(e => e.PreliminarySourceIdentificationTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.PreliminarySourceIdentificationTypeName).IsUnicode(false);

                entity.HasOne(d => d.PreliminarySourceIdentificationCategory)
                    .WithMany(p => p.PreliminarySourceIdentificationTypes)
                    .HasForeignKey(d => d.PreliminarySourceIdentificationCategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<PriorityLandUseType>(entity =>
            {
                entity.Property(e => e.PriorityLandUseTypeID).ValueGeneratedNever();

                entity.Property(e => e.MapColorHexCode).IsUnicode(false);

                entity.Property(e => e.PriorityLandUseTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.PriorityLandUseTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.AdditionalContactInformation).IsUnicode(false);

                entity.Property(e => e.ProjectDescription).IsUnicode(false);

                entity.Property(e => e.ProjectName).IsUnicode(false);

                entity.HasOne(d => d.CreatePerson)
                    .WithMany(p => p.ProjectCreatePeople)
                    .HasForeignKey(d => d.CreatePersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Person_CreatePersonID_PersonID");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.OrganizationID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.PrimaryContactPerson)
                    .WithMany(p => p.ProjectPrimaryContactPeople)
                    .HasForeignKey(d => d.PrimaryContactPersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Person_PrimaryContactPersonID_PersonID");

                entity.HasOne(d => d.ProjectStatus)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProjectDocument>(entity =>
            {
                entity.Property(e => e.DisplayName).IsUnicode(false);

                entity.Property(e => e.DocumentDescription).IsUnicode(false);

                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.ProjectDocuments)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectDocuments)
                    .HasForeignKey(d => d.ProjectID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ProjectStatus>(entity =>
            {
                entity.Property(e => e.ProjectStatusID).ValueGeneratedNever();

                entity.Property(e => e.ProjectStatusDisplayName).IsUnicode(false);

                entity.Property(e => e.ProjectStatusName).IsUnicode(false);
            });

            modelBuilder.Entity<QuickBMP>(entity =>
            {
                entity.Property(e => e.QuickBMPName).IsUnicode(false);

                entity.Property(e => e.QuickBMPNote).IsUnicode(false);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.QuickBMPs)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlan)
                    .WithMany(p => p.QuickBMPs)
                    .HasForeignKey(d => d.WaterQualityManagementPlanID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<RegionalSubbasin>(entity =>
            {
                entity.Property(e => e.DrainID).IsUnicode(false);

                entity.Property(e => e.Watershed).IsUnicode(false);

                entity.HasOne(d => d.OCSurveyDownstreamCatchment)
                    .WithMany(p => p.InverseOCSurveyDownstreamCatchment)
                    .HasPrincipalKey(p => p.OCSurveyCatchmentID)
                    .HasForeignKey(d => d.OCSurveyDownstreamCatchmentID)
                    .HasConstraintName("FK_RegionalSubbasin_RegionalSubbasin_OCSurveyDownstreamCatchmentID_OCSurveyCatchmentID");
            });

            modelBuilder.Entity<RegionalSubbasinRevisionRequest>(entity =>
            {
                entity.Property(e => e.CloseNotes).IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.HasOne(d => d.ClosedByPerson)
                    .WithMany(p => p.RegionalSubbasinRevisionRequestClosedByPeople)
                    .HasForeignKey(d => d.ClosedByPersonID)
                    .HasConstraintName("FK_RegionalSubbasinRevisionRequest_Person_ClosedByPersonID_PersonID");

                entity.HasOne(d => d.RegionalSubbasinRevisionRequestStatus)
                    .WithMany(p => p.RegionalSubbasinRevisionRequests)
                    .HasForeignKey(d => d.RegionalSubbasinRevisionRequestStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.RequestPerson)
                    .WithMany(p => p.RegionalSubbasinRevisionRequestRequestPeople)
                    .HasForeignKey(d => d.RequestPersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegionalSubbasinRevisionRequest_Person_RequestPersonID_PersonID");

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.RegionalSubbasinRevisionRequests)
                    .HasForeignKey(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_RegionalSubbasinRevisionRequest_TreatmentBMP_TreatmentBMPID");
            });

            modelBuilder.Entity<RegionalSubbasinRevisionRequestStatus>(entity =>
            {
                entity.Property(e => e.RegionalSubbasinRevisionRequestStatusID).ValueGeneratedNever();

                entity.Property(e => e.RegionalSubbasinRevisionRequestStatusDisplayName).IsUnicode(false);

                entity.Property(e => e.RegionalSubbasinRevisionRequestStatusName).IsUnicode(false);
            });

            modelBuilder.Entity<RegionalSubbasinStaging>(entity =>
            {
                entity.Property(e => e.DrainID).IsUnicode(false);

                entity.Property(e => e.Watershed).IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleID).ValueGeneratedNever();

                entity.Property(e => e.RoleDescription).IsUnicode(false);

                entity.Property(e => e.RoleDisplayName).IsUnicode(false);

                entity.Property(e => e.RoleName).IsUnicode(false);
            });

            modelBuilder.Entity<RoutingConfiguration>(entity =>
            {
                entity.Property(e => e.RoutingConfigurationID).ValueGeneratedNever();

                entity.Property(e => e.RoutingConfigurationDisplayName).IsUnicode(false);

                entity.Property(e => e.RoutingConfigurationName).IsUnicode(false);
            });

            modelBuilder.Entity<SizingBasisType>(entity =>
            {
                entity.Property(e => e.SizingBasisTypeID).ValueGeneratedNever();

                entity.Property(e => e.SizingBasisTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.SizingBasisTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<SourceControlBMP>(entity =>
            {
                entity.Property(e => e.SourceControlBMPNote).IsUnicode(false);

                entity.HasOne(d => d.SourceControlBMPAttribute)
                    .WithMany(p => p.SourceControlBMPs)
                    .HasForeignKey(d => d.SourceControlBMPAttributeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlan)
                    .WithMany(p => p.SourceControlBMPs)
                    .HasForeignKey(d => d.WaterQualityManagementPlanID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SourceControlBMPAttribute>(entity =>
            {
                entity.Property(e => e.SourceControlBMPAttributeID).ValueGeneratedNever();

                entity.Property(e => e.SourceControlBMPAttributeName).IsUnicode(false);

                entity.HasOne(d => d.SourceControlBMPAttributeCategory)
                    .WithMany(p => p.SourceControlBMPAttributes)
                    .HasForeignKey(d => d.SourceControlBMPAttributeCategoryID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SourceControlBMPAttributeCategory>(entity =>
            {
                entity.Property(e => e.SourceControlBMPAttributeCategoryID).ValueGeneratedNever();

                entity.Property(e => e.SourceControlBMPAttributeCategoryName).IsUnicode(false);

                entity.Property(e => e.SourceControlBMPAttributeCategoryShortName).IsUnicode(false);
            });

            modelBuilder.Entity<StateProvince>(entity =>
            {
                entity.Property(e => e.StateProvinceID).ValueGeneratedNever();

                entity.Property(e => e.StateProvinceAbbreviation)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.StateProvinceName).IsUnicode(false);
            });

            modelBuilder.Entity<StormwaterBreadCrumbEntity>(entity =>
            {
                entity.Property(e => e.StormwaterBreadCrumbEntityID).ValueGeneratedNever();

                entity.Property(e => e.ColorClass).IsUnicode(false);

                entity.Property(e => e.GlyphIconClass).IsUnicode(false);

                entity.Property(e => e.StormwaterBreadCrumbEntityDisplayName).IsUnicode(false);

                entity.Property(e => e.StormwaterBreadCrumbEntityName).IsUnicode(false);
            });

            modelBuilder.Entity<StormwaterJurisdiction>(entity =>
            {
                entity.HasOne(d => d.Organization)
                    .WithOne(p => p.StormwaterJurisdiction)
                    .HasForeignKey<StormwaterJurisdiction>(d => d.OrganizationID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.StateProvince)
                    .WithMany(p => p.StormwaterJurisdictions)
                    .HasForeignKey(d => d.StateProvinceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.StormwaterJurisdictionPublicBMPVisibilityType)
                    .WithMany(p => p.StormwaterJurisdictions)
                    .HasForeignKey(d => d.StormwaterJurisdictionPublicBMPVisibilityTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.StormwaterJurisdictionPublicWQMPVisibilityType)
                    .WithMany(p => p.StormwaterJurisdictions)
                    .HasForeignKey(d => d.StormwaterJurisdictionPublicWQMPVisibilityTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<StormwaterJurisdictionGeometry>(entity =>
            {
                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithOne(p => p.StormwaterJurisdictionGeometry)
                    .HasForeignKey<StormwaterJurisdictionGeometry>(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<StormwaterJurisdictionPerson>(entity =>
            {
                entity.HasOne(d => d.Person)
                    .WithMany(p => p.StormwaterJurisdictionPeople)
                    .HasForeignKey(d => d.PersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.StormwaterJurisdictionPeople)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<StormwaterJurisdictionPublicBMPVisibilityType>(entity =>
            {
                entity.Property(e => e.StormwaterJurisdictionPublicBMPVisibilityTypeID).ValueGeneratedNever();

                entity.Property(e => e.StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.StormwaterJurisdictionPublicBMPVisibilityTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<StormwaterJurisdictionPublicWQMPVisibilityType>(entity =>
            {
                entity.Property(e => e.StormwaterJurisdictionPublicWQMPVisibilityTypeID).ValueGeneratedNever();

                entity.Property(e => e.StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.StormwaterJurisdictionPublicWQMPVisibilityTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<SupportRequestLog>(entity =>
            {
                entity.Property(e => e.RequestDescription).IsUnicode(false);

                entity.Property(e => e.RequestPersonEmail).IsUnicode(false);

                entity.Property(e => e.RequestPersonName).IsUnicode(false);

                entity.Property(e => e.RequestPersonOrganization).IsUnicode(false);

                entity.Property(e => e.RequestPersonPhone).IsUnicode(false);

                entity.HasOne(d => d.RequestPerson)
                    .WithMany(p => p.SupportRequestLogs)
                    .HasForeignKey(d => d.RequestPersonID)
                    .HasConstraintName("FK_SupportRequestLog_Person_RequestPersonID_PersonID");

                entity.HasOne(d => d.SupportRequestType)
                    .WithMany(p => p.SupportRequestLogs)
                    .HasForeignKey(d => d.SupportRequestTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<SupportRequestType>(entity =>
            {
                entity.Property(e => e.SupportRequestTypeID).ValueGeneratedNever();

                entity.Property(e => e.SupportRequestTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.SupportRequestTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<TimeOfConcentration>(entity =>
            {
                entity.Property(e => e.TimeOfConcentrationID).ValueGeneratedNever();

                entity.Property(e => e.TimeOfConcentrationDisplayName).IsUnicode(false);

                entity.Property(e => e.TimeOfConcentrationName).IsUnicode(false);
            });

            modelBuilder.Entity<TrainingVideo>(entity =>
            {
                entity.Property(e => e.VideoDescription).IsUnicode(false);

                entity.Property(e => e.VideoName).IsUnicode(false);

                entity.Property(e => e.VideoURL).IsUnicode(false);
            });

            modelBuilder.Entity<TrashCaptureStatusType>(entity =>
            {
                entity.Property(e => e.TrashCaptureStatusTypeID).ValueGeneratedNever();

                entity.Property(e => e.TrashCaptureStatusTypeColorCode).IsUnicode(false);

                entity.Property(e => e.TrashCaptureStatusTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.TrashCaptureStatusTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<TrashGeneratingUnit>(entity =>
            {
                entity.Property(e => e.LastUpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.TrashGeneratingUnits)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TrashGeneratingUnit4326>(entity =>
            {
                entity.Property(e => e.LastUpdateDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.TrashGeneratingUnit4326s)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TrashGeneratingUnitAdjustment>(entity =>
            {
                entity.HasOne(d => d.AdjustedByPerson)
                    .WithMany(p => p.TrashGeneratingUnitAdjustments)
                    .HasForeignKey(d => d.AdjustedByPersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrashGeneratingUnitAdjustment_Person_AdjustedByPersonID_PersonID");
            });

            modelBuilder.Entity<TreatmentBMP>(entity =>
            {
                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.SystemOfRecordID).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);

                entity.HasOne(d => d.InventoryVerifiedByPerson)
                    .WithMany(p => p.TreatmentBMPs)
                    .HasForeignKey(d => d.InventoryVerifiedByPersonID)
                    .HasConstraintName("FK_TreatmentBMP_Person_InventoryVerifiedByPersonID_PersonID");

                entity.HasOne(d => d.OwnerOrganization)
                    .WithMany(p => p.TreatmentBMPs)
                    .HasForeignKey(d => d.OwnerOrganizationID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TreatmentBMP_Organization_OwnerOrganizationID_OrganizationID");

                entity.HasOne(d => d.SizingBasisType)
                    .WithMany(p => p.TreatmentBMPs)
                    .HasForeignKey(d => d.SizingBasisTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.TreatmentBMPs)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TrashCaptureStatusType)
                    .WithMany(p => p.TreatmentBMPs)
                    .HasForeignKey(d => d.TrashCaptureStatusTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.TreatmentBMPs)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UpstreamBMP)
                    .WithMany(p => p.InverseUpstreamBMP)
                    .HasForeignKey(d => d.UpstreamBMPID)
                    .HasConstraintName("FK_TreatmentBMP_TreatmentBMP_UpstreamBMPID_TreatmentBMPID");
            });

            modelBuilder.Entity<TreatmentBMPAssessment>(entity =>
            {
                entity.Property(e => e.Notes).IsUnicode(false);

                entity.HasOne(d => d.FieldVisit)
                    .WithMany(p => p.TreatmentBMPAssessmentFieldVisits)
                    .HasForeignKey(d => d.FieldVisitID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPAssessmentType)
                    .WithMany(p => p.TreatmentBMPAssessments)
                    .HasForeignKey(d => d.TreatmentBMPAssessmentTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.TreatmentBMPAssessmentTreatmentBMPs)
                    .HasForeignKey(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.TreatmentBMPAssessments)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.FieldVisitNavigation)
                    .WithMany(p => p.TreatmentBMPAssessmentFieldVisitNavigations)
                    .HasPrincipalKey(p => new { p.FieldVisitID, p.TreatmentBMPID })
                    .HasForeignKey(d => new { d.FieldVisitID, d.TreatmentBMPID })
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPNavigation)
                    .WithMany(p => p.TreatmentBMPAssessmentTreatmentBMPNavigations)
                    .HasPrincipalKey(p => new { p.TreatmentBMPID, p.TreatmentBMPTypeID })
                    .HasForeignKey(d => new { d.TreatmentBMPID, d.TreatmentBMPTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TreatmentBMPAssessmentObservationType>(entity =>
            {
                entity.Property(e => e.TreatmentBMPAssessmentObservationTypeName).IsUnicode(false);

                entity.HasOne(d => d.ObservationTypeSpecification)
                    .WithMany(p => p.TreatmentBMPAssessmentObservationTypes)
                    .HasForeignKey(d => d.ObservationTypeSpecificationID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TreatmentBMPAssessmentPhoto>(entity =>
            {
                entity.Property(e => e.Caption).IsUnicode(false);

                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.TreatmentBMPAssessmentPhotos)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPAssessment)
                    .WithMany(p => p.TreatmentBMPAssessmentPhotos)
                    .HasForeignKey(d => d.TreatmentBMPAssessmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TreatmentBMPAssessmentType>(entity =>
            {
                entity.Property(e => e.TreatmentBMPAssessmentTypeID).ValueGeneratedNever();

                entity.Property(e => e.TreatmentBMPAssessmentTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPAssessmentTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<TreatmentBMPBenchmarkAndThreshold>(entity =>
            {
                entity.HasOne(d => d.TreatmentBMPAssessmentObservationType)
                    .WithMany(p => p.TreatmentBMPBenchmarkAndThresholds)
                    .HasForeignKey(d => d.TreatmentBMPAssessmentObservationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.TreatmentBMPBenchmarkAndThresholdTreatmentBMPs)
                    .HasForeignKey(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPTypeAssessmentObservationType)
                    .WithMany(p => p.TreatmentBMPBenchmarkAndThresholdTreatmentBMPTypeAssessmentObservationTypes)
                    .HasForeignKey(d => d.TreatmentBMPTypeAssessmentObservationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.TreatmentBMPBenchmarkAndThresholds)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPNavigation)
                    .WithMany(p => p.TreatmentBMPBenchmarkAndThresholdTreatmentBMPNavigations)
                    .HasPrincipalKey(p => new { p.TreatmentBMPID, p.TreatmentBMPTypeID })
                    .HasForeignKey(d => new { d.TreatmentBMPID, d.TreatmentBMPTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP1)
                    .WithMany(p => p.TreatmentBMPBenchmarkAndThresholdTreatmentBMP1s)
                    .HasPrincipalKey(p => new { p.TreatmentBMPTypeAssessmentObservationTypeID, p.TreatmentBMPTypeID, p.TreatmentBMPAssessmentObservationTypeID })
                    .HasForeignKey(d => new { d.TreatmentBMPTypeAssessmentObservationTypeID, d.TreatmentBMPTypeID, d.TreatmentBMPAssessmentObservationTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_Treat");
            });

            modelBuilder.Entity<TreatmentBMPDocument>(entity =>
            {
                entity.Property(e => e.DisplayName).IsUnicode(false);

                entity.Property(e => e.DocumentDescription).IsUnicode(false);

                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.TreatmentBMPDocuments)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.TreatmentBMPDocuments)
                    .HasForeignKey(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TreatmentBMPImage>(entity =>
            {
                entity.Property(e => e.Caption).IsUnicode(false);

                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.TreatmentBMPImages)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.TreatmentBMPImages)
                    .HasForeignKey(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TreatmentBMPLifespanType>(entity =>
            {
                entity.Property(e => e.TreatmentBMPLifespanTypeID).ValueGeneratedNever();

                entity.Property(e => e.TreatmentBMPLifespanTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPLifespanTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<TreatmentBMPModelingAttribute>(entity =>
            {
                entity.HasOne(d => d.MonthsOfOperation)
                    .WithMany(p => p.TreatmentBMPModelingAttributes)
                    .HasForeignKey(d => d.MonthsOfOperationID)
                    .HasConstraintName("FK__TreatmentBMPModelingAttribute_MonthsOfOperation_MonthsOfOperationID");

                entity.HasOne(d => d.TreatmentBMP)
                    .WithOne(p => p.TreatmentBMPModelingAttributeTreatmentBMP)
                    .HasForeignKey<TreatmentBMPModelingAttribute>(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.UpstreamTreatmentBMP)
                    .WithMany(p => p.TreatmentBMPModelingAttributeUpstreamTreatmentBMPs)
                    .HasForeignKey(d => d.UpstreamTreatmentBMPID)
                    .HasConstraintName("FK_TreatmentBMPModelingAttribute_TreatmentBMP_UpstreamTreatmentBMPID_TreatmentBMPID");
            });

            modelBuilder.Entity<TreatmentBMPModelingType>(entity =>
            {
                entity.Property(e => e.TreatmentBMPModelingTypeID).ValueGeneratedNever();

                entity.Property(e => e.TreatmentBMPModelingTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPModelingTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<TreatmentBMPObservation>(entity =>
            {
                entity.HasOne(d => d.TreatmentBMPAssessment)
                    .WithMany(p => p.TreatmentBMPObservationTreatmentBMPAssessments)
                    .HasForeignKey(d => d.TreatmentBMPAssessmentID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPAssessmentObservationType)
                    .WithMany(p => p.TreatmentBMPObservations)
                    .HasForeignKey(d => d.TreatmentBMPAssessmentObservationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPTypeAssessmentObservationType)
                    .WithMany(p => p.TreatmentBMPObservationTreatmentBMPTypeAssessmentObservationTypes)
                    .HasForeignKey(d => d.TreatmentBMPTypeAssessmentObservationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.TreatmentBMPObservations)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.TreatmentBMPObservationTreatmentBMPs)
                    .HasPrincipalKey(p => new { p.TreatmentBMPAssessmentID, p.TreatmentBMPTypeID })
                    .HasForeignKey(d => new { d.TreatmentBMPAssessmentID, d.TreatmentBMPTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPNavigation)
                    .WithMany(p => p.TreatmentBMPObservationTreatmentBMPNavigations)
                    .HasPrincipalKey(p => new { p.TreatmentBMPTypeAssessmentObservationTypeID, p.TreatmentBMPTypeID, p.TreatmentBMPAssessmentObservationTypeID })
                    .HasForeignKey(d => new { d.TreatmentBMPTypeAssessmentObservationTypeID, d.TreatmentBMPTypeID, d.TreatmentBMPAssessmentObservationTypeID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TreatmentBMPObservation_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTyp");
            });

            modelBuilder.Entity<TreatmentBMPType>(entity =>
            {
                entity.Property(e => e.TreatmentBMPTypeDescription).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<TreatmentBMPTypeAssessmentObservationType>(entity =>
            {
                entity.HasOne(d => d.TreatmentBMPAssessmentObservationType)
                    .WithMany(p => p.TreatmentBMPTypeAssessmentObservationTypes)
                    .HasForeignKey(d => d.TreatmentBMPAssessmentObservationTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.TreatmentBMPTypeAssessmentObservationTypes)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TreatmentBMPTypeCustomAttributeType>(entity =>
            {
                entity.HasOne(d => d.CustomAttributeType)
                    .WithMany(p => p.TreatmentBMPTypeCustomAttributeTypes)
                    .HasForeignKey(d => d.CustomAttributeTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TreatmentBMPType)
                    .WithMany(p => p.TreatmentBMPTypeCustomAttributeTypes)
                    .HasForeignKey(d => d.TreatmentBMPTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<UnderlyingHydrologicSoilGroup>(entity =>
            {
                entity.Property(e => e.UnderlyingHydrologicSoilGroupID).ValueGeneratedNever();

                entity.Property(e => e.UnderlyingHydrologicSoilGroupDisplayName).IsUnicode(false);

                entity.Property(e => e.UnderlyingHydrologicSoilGroupName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlan>(entity =>
            {
                entity.Property(e => e.MaintenanceContactAddress1).IsUnicode(false);

                entity.Property(e => e.MaintenanceContactAddress2).IsUnicode(false);

                entity.Property(e => e.MaintenanceContactCity).IsUnicode(false);

                entity.Property(e => e.MaintenanceContactName).IsUnicode(false);

                entity.Property(e => e.MaintenanceContactOrganization).IsUnicode(false);

                entity.Property(e => e.MaintenanceContactPhone).IsUnicode(false);

                entity.Property(e => e.MaintenanceContactState).IsUnicode(false);

                entity.Property(e => e.MaintenanceContactZip).IsUnicode(false);

                entity.Property(e => e.RecordNumber).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanName).IsUnicode(false);

                entity.HasOne(d => d.StormwaterJurisdiction)
                    .WithMany(p => p.WaterQualityManagementPlans)
                    .HasForeignKey(d => d.StormwaterJurisdictionID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.TrashCaptureStatusType)
                    .WithMany(p => p.WaterQualityManagementPlans)
                    .HasForeignKey(d => d.TrashCaptureStatusTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlanModelingApproach)
                    .WithMany(p => p.WaterQualityManagementPlans)
                    .HasForeignKey(d => d.WaterQualityManagementPlanModelingApproachID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WaterQualityManagementPlanDevelopmentType>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanDevelopmentTypeID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanDevelopmentTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanDevelopmentTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlanDocument>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayName).IsUnicode(false);

                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.WaterQualityManagementPlanDocuments)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlanDocumentType)
                    .WithMany(p => p.WaterQualityManagementPlanDocuments)
                    .HasForeignKey(d => d.WaterQualityManagementPlanDocumentTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlan)
                    .WithMany(p => p.WaterQualityManagementPlanDocuments)
                    .HasForeignKey(d => d.WaterQualityManagementPlanID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WaterQualityManagementPlanDocumentType>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanDocumentTypeID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanDocumentTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanDocumentTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlanLandUse>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanLandUseID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanLandUseDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanLandUseName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlanModelingApproach>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanModelingApproachID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanModelingApproachDescription).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanModelingApproachDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanModelingApproachName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlanParcel>(entity =>
            {
                entity.HasOne(d => d.Parcel)
                    .WithMany(p => p.WaterQualityManagementPlanParcels)
                    .HasForeignKey(d => d.ParcelID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlan)
                    .WithMany(p => p.WaterQualityManagementPlanParcels)
                    .HasForeignKey(d => d.WaterQualityManagementPlanID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WaterQualityManagementPlanPermitTerm>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanPermitTermID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanPermitTermDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanPermitTermName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlanPhoto>(entity =>
            {
                entity.Property(e => e.Caption).IsUnicode(false);

                entity.HasOne(d => d.FileResource)
                    .WithMany(p => p.WaterQualityManagementPlanPhotos)
                    .HasForeignKey(d => d.FileResourceID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WaterQualityManagementPlanPriority>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanPriorityID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanPriorityDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanPriorityName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlanStatus>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanStatusID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanStatusDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanStatusName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlanVerify>(entity =>
            {
                entity.Property(e => e.EnforcementOrFollowupActions).IsUnicode(false);

                entity.Property(e => e.SourceControlCondition).IsUnicode(false);

                entity.HasOne(d => d.LastEditedByPerson)
                    .WithMany(p => p.WaterQualityManagementPlanVerifies)
                    .HasForeignKey(d => d.LastEditedByPersonID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WaterQualityManagementPlanVerify_Person_LastEditedByPersonID_PersonID");

                entity.HasOne(d => d.WaterQualityManagementPlan)
                    .WithMany(p => p.WaterQualityManagementPlanVerifies)
                    .HasForeignKey(d => d.WaterQualityManagementPlanID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlanVerifyType)
                    .WithMany(p => p.WaterQualityManagementPlanVerifies)
                    .HasForeignKey(d => d.WaterQualityManagementPlanVerifyTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlanVisitStatus)
                    .WithMany(p => p.WaterQualityManagementPlanVerifies)
                    .HasForeignKey(d => d.WaterQualityManagementPlanVisitStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WaterQualityManagementPlanVerifyPhoto>(entity =>
            {
                entity.HasOne(d => d.WaterQualityManagementPlanPhoto)
                    .WithMany(p => p.WaterQualityManagementPlanVerifyPhotos)
                    .HasForeignKey(d => d.WaterQualityManagementPlanPhotoID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlanVerify)
                    .WithMany(p => p.WaterQualityManagementPlanVerifyPhotos)
                    .HasForeignKey(d => d.WaterQualityManagementPlanVerifyID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WaterQualityManagementPlanVerifyQuickBMP>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanVerifyQuickBMPNote).IsUnicode(false);

                entity.HasOne(d => d.QuickBMP)
                    .WithMany(p => p.WaterQualityManagementPlanVerifyQuickBMPs)
                    .HasForeignKey(d => d.QuickBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlanVerify)
                    .WithMany(p => p.WaterQualityManagementPlanVerifyQuickBMPs)
                    .HasForeignKey(d => d.WaterQualityManagementPlanVerifyID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WaterQualityManagementPlanVerifySourceControlBMP>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanSourceControlCondition).IsUnicode(false);

                entity.HasOne(d => d.SourceControlBMP)
                    .WithMany(p => p.WaterQualityManagementPlanVerifySourceControlBMPs)
                    .HasForeignKey(d => d.SourceControlBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlanVerify)
                    .WithMany(p => p.WaterQualityManagementPlanVerifySourceControlBMPs)
                    .HasForeignKey(d => d.WaterQualityManagementPlanVerifyID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WaterQualityManagementPlanVerifyStatus>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanVerifyStatusID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanVerifyStatusName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlanVerifyTreatmentBMP>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanVerifyTreatmentBMPNote).IsUnicode(false);

                entity.HasOne(d => d.TreatmentBMP)
                    .WithMany(p => p.WaterQualityManagementPlanVerifyTreatmentBMPs)
                    .HasForeignKey(d => d.TreatmentBMPID)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.WaterQualityManagementPlanVerify)
                    .WithMany(p => p.WaterQualityManagementPlanVerifyTreatmentBMPs)
                    .HasForeignKey(d => d.WaterQualityManagementPlanVerifyID)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<WaterQualityManagementPlanVerifyType>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanVerifyTypeID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanVerifyTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<WaterQualityManagementPlanVisitStatus>(entity =>
            {
                entity.Property(e => e.WaterQualityManagementPlanVisitStatusID).ValueGeneratedNever();

                entity.Property(e => e.WaterQualityManagementPlanVisitStatusName).IsUnicode(false);
            });

            modelBuilder.Entity<Watershed>(entity =>
            {
                entity.Property(e => e.WatershedName).IsUnicode(false);
            });

            modelBuilder.Entity<geometry_column>(entity =>
            {
                entity.HasKey(e => new { e.f_table_catalog, e.f_table_schema, e.f_table_name, e.f_geometry_column })
                    .HasName("PK_geometry_columns_f_table_catalog_f_table_schema_f_table_name_f_geometry_column");

                entity.Property(e => e.f_table_catalog).IsUnicode(false);

                entity.Property(e => e.f_table_schema).IsUnicode(false);

                entity.Property(e => e.f_table_name).IsUnicode(false);

                entity.Property(e => e.f_geometry_column).IsUnicode(false);

                entity.Property(e => e.geometry_type).IsUnicode(false);
            });

            modelBuilder.Entity<gt_pk_metadatum>(entity =>
            {
                entity.Property(e => e.pk_column).IsUnicode(false);

                entity.Property(e => e.pk_policy).IsUnicode(false);

                entity.Property(e => e.pk_sequence).IsUnicode(false);

                entity.Property(e => e.table_name).IsUnicode(false);

                entity.Property(e => e.table_schema).IsUnicode(false);
            });

            modelBuilder.Entity<spatial_ref_sy>(entity =>
            {
                entity.HasKey(e => e.srid)
                    .HasName("PK_spatial_ref_sys_srid");

                entity.Property(e => e.srid).ValueGeneratedNever();

                entity.Property(e => e.auth_name).IsUnicode(false);

                entity.Property(e => e.proj4text).IsUnicode(false);

                entity.Property(e => e.srtext).IsUnicode(false);
            });

            modelBuilder.Entity<vDelineationLGUInput>(entity =>
            {
                entity.ToView("vDelineationLGUInput");
            });

            modelBuilder.Entity<vDelineationTGUInput>(entity =>
            {
                entity.ToView("vDelineationTGUInput");

                entity.Property(e => e.TrashCaptureStatusTypeDisplayName).IsUnicode(false);
            });

            modelBuilder.Entity<vFieldVisitDetailed>(entity =>
            {
                entity.ToView("vFieldVisitDetailed");

                entity.Property(e => e.FieldVisitStatusDisplayName).IsUnicode(false);

                entity.Property(e => e.FieldVisitTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.PerformedByPersonName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerAssessmentAreaExport>(entity =>
            {
                entity.ToView("vGeoServerAssessmentAreaExport");

                entity.Property(e => e.CompletedDate).IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.JurisName).IsUnicode(false);

                entity.Property(e => e.OVTAAreaName).IsUnicode(false);

                entity.Property(e => e.Score).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerDelineation>(entity =>
            {
                entity.ToView("vGeoServerDelineation");

                entity.Property(e => e.DelineationStatus).IsUnicode(false);

                entity.Property(e => e.DelineationType).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerJurisdiction>(entity =>
            {
                entity.ToView("vGeoServerJurisdiction");
            });

            modelBuilder.Entity<vGeoServerLandUseBlock>(entity =>
            {
                entity.ToView("vGeoServerLandUseBlock");

                entity.Property(e => e.LandUseBlockID).ValueGeneratedOnAdd();

                entity.Property(e => e.LandUseDescription).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerMaskLayer>(entity =>
            {
                entity.ToView("vGeoServerMaskLayer");
            });

            modelBuilder.Entity<vGeoServerObservationPointExport>(entity =>
            {
                entity.ToView("vGeoServerObservationPointExport");

                entity.Property(e => e.CompletedDate).IsUnicode(false);

                entity.Property(e => e.JurisName).IsUnicode(false);

                entity.Property(e => e.Note).IsUnicode(false);

                entity.Property(e => e.OVTAAreaName).IsUnicode(false);

                entity.Property(e => e.PhotoUrl).IsUnicode(false);

                entity.Property(e => e.Score).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerOnlandVisualTrashAssessmentArea>(entity =>
            {
                entity.ToView("vGeoServerOnlandVisualTrashAssessmentArea");

                entity.Property(e => e.OnlandVisualTrashAssessmentAreaName).IsUnicode(false);

                entity.Property(e => e.Score).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerParcel>(entity =>
            {
                entity.ToView("vGeoServerParcel");

                entity.Property(e => e.LandUse).IsUnicode(false);

                entity.Property(e => e.OwnerName).IsUnicode(false);

                entity.Property(e => e.ParcelAddress).IsUnicode(false);

                entity.Property(e => e.ParcelNumber).IsUnicode(false);

                entity.Property(e => e.ParcelStreetNumber).IsUnicode(false);

                entity.Property(e => e.ParcelZipCode).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerRegionalSubbasin>(entity =>
            {
                entity.ToView("vGeoServerRegionalSubbasin");

                entity.Property(e => e.DrainID).IsUnicode(false);

                entity.Property(e => e.RegionalSubbasinID).ValueGeneratedOnAdd();

                entity.Property(e => e.Watershed).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerTransectLineExport>(entity =>
            {
                entity.ToView("vGeoServerTransectLineExport");

                entity.Property(e => e.OVTAAreaName).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerTrashGeneratingUnit>(entity =>
            {
                entity.ToView("vGeoServerTrashGeneratingUnit");

                entity.Property(e => e.AssessmentScore).IsUnicode(false);

                entity.Property(e => e.LandUseType).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.TrashCaptureStatus).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanName).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerTrashGeneratingUnitLoad>(entity =>
            {
                entity.ToView("vGeoServerTrashGeneratingUnitLoad");

                entity.Property(e => e.LandUseType).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.PriorityLandUseTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanName).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerWaterQualityManagementPlan>(entity =>
            {
                entity.ToView("vGeoServerWaterQualityManagementPlan");

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.TrashCaptureStatusTypeDisplayName).IsUnicode(false);
            });

            modelBuilder.Entity<vGeoServerWatershed>(entity =>
            {
                entity.ToView("vGeoServerWatershed");

                entity.Property(e => e.WatershedID).ValueGeneratedOnAdd();

                entity.Property(e => e.WatershedName).IsUnicode(false);
            });

            modelBuilder.Entity<vLSPCBasinLGUInput>(entity =>
            {
                entity.ToView("vLSPCBasinLGUInput");

                entity.Property(e => e.LSPCID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<vLandUseBlockTGUInput>(entity =>
            {
                entity.ToView("vLandUseBlockTGUInput");

                entity.Property(e => e.LandUseBlockID).ValueGeneratedOnAdd();

                entity.Property(e => e.LandUseDescription).IsUnicode(false);

                entity.Property(e => e.LandUseForTGR).IsUnicode(false);
            });

            modelBuilder.Entity<vMostRecentTreatmentBMPAssessment>(entity =>
            {
                entity.ToView("vMostRecentTreatmentBMPAssessment");

                entity.Property(e => e.FieldVisitType).IsUnicode(false);

                entity.Property(e => e.OwnerOrganizationName).IsUnicode(false);

                entity.Property(e => e.StormwaterJurisdictionName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);
            });

            modelBuilder.Entity<vNereidBMPColocation>(entity =>
            {
                entity.ToView("vNereidBMPColocation");
            });

            modelBuilder.Entity<vNereidLoadingInput>(entity =>
            {
                entity.ToView("vNereidLoadingInput");

                entity.Property(e => e.BaselineLandUseCode).IsUnicode(false);

                entity.Property(e => e.HydrologicSoilGroup).IsUnicode(false);

                entity.Property(e => e.LandUseCode).IsUnicode(false);
            });

            modelBuilder.Entity<vNereidRegionalSubbasinCentralizedBMP>(entity =>
            {
                entity.ToView("vNereidRegionalSubbasinCentralizedBMP");
            });

            modelBuilder.Entity<vNereidTreatmentBMPRegionalSubbasin>(entity =>
            {
                entity.ToView("vNereidTreatmentBMPRegionalSubbasin");
            });

            modelBuilder.Entity<vOnlandVisualTrashAssessmentAreaDated>(entity =>
            {
                entity.ToView("vOnlandVisualTrashAssessmentAreaDated");

                entity.Property(e => e.MostRecentAssessmentScore).IsUnicode(false);
            });

            modelBuilder.Entity<vOnlandVisualTrashAssessmentAreaProgress>(entity =>
            {
                entity.ToView("vOnlandVisualTrashAssessmentAreaProgress");

                entity.Property(e => e.OnlandVisualTrashAssessmentScoreDisplayName).IsUnicode(false);
            });

            modelBuilder.Entity<vPowerBICentralizedBMPLoadGeneratingUnit>(entity =>
            {
                entity.ToView("vPowerBICentralizedBMPLoadGeneratingUnit");
            });

            modelBuilder.Entity<vPowerBILandUseStatistic>(entity =>
            {
                entity.ToView("vPowerBILandUseStatistic");

                entity.Property(e => e.HRUCharacteristicLandUseCodeDisplayName).IsUnicode(false);

                entity.Property(e => e.HydrologicSoilGroup).IsUnicode(false);

                entity.Property(e => e.LSPCBasinName).IsUnicode(false);

                entity.Property(e => e.LandUse).IsUnicode(false);

                entity.Property(e => e.SurfaceKey).IsUnicode(false);

                entity.Property(e => e.WatershedName).IsUnicode(false);
            });

            modelBuilder.Entity<vPowerBITreatmentBMP>(entity =>
            {
                entity.ToView("vPowerBITreatmentBMP");

                entity.Property(e => e.DelineationType).IsUnicode(false);

                entity.Property(e => e.Jurisdiction).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPTypeName).IsUnicode(false);

                entity.Property(e => e.Watershed).IsUnicode(false);
            });

            modelBuilder.Entity<vPowerBIWaterQualityManagementPlan>(entity =>
            {
                entity.ToView("vPowerBIWaterQualityManagementPlan");

                entity.Property(e => e.HydrologicSubareaName).IsUnicode(false);

                entity.Property(e => e.HydromodificationAppliesDisplayName).IsUnicode(false);

                entity.Property(e => e.ModelingApproach).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.TrashCaptureStatusTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanDevelopmentTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanLandUseDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanPermitTermDisplayName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanStatusDisplayName).IsUnicode(false);
            });

            modelBuilder.Entity<vPowerBIWaterQualityManagementPlanOAndMVerification>(entity =>
            {
                entity.ToView("vPowerBIWaterQualityManagementPlanOAndMVerification");

                entity.Property(e => e.DraftOrFinalized).IsUnicode(false);

                entity.Property(e => e.EnforcementOrFollowupActions).IsUnicode(false);

                entity.Property(e => e.Jurisdiction).IsUnicode(false);

                entity.Property(e => e.LastEditedBy).IsUnicode(false);

                entity.Property(e => e.SourceControlCondition).IsUnicode(false);

                entity.Property(e => e.TypeOfVerification).IsUnicode(false);

                entity.Property(e => e.VerificationStatus).IsUnicode(false);

                entity.Property(e => e.VisitStatus).IsUnicode(false);

                entity.Property(e => e.WQMPName).IsUnicode(false);
            });

            modelBuilder.Entity<vRegionalSubbasinLGUInput>(entity =>
            {
                entity.ToView("vRegionalSubbasinLGUInput");

                entity.Property(e => e.RSBID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<vRegionalSubbasinUpstreamCatchmentGeometry4326>(entity =>
            {
                entity.ToView("vRegionalSubbasinUpstreamCatchmentGeometry4326");
            });

            modelBuilder.Entity<vStormwaterJurisdictionOrganizationMapping>(entity =>
            {
                entity.ToView("vStormwaterJurisdictionOrganizationMapping");

                entity.Property(e => e.OrganizationName).IsUnicode(false);
            });

            modelBuilder.Entity<vTrashGeneratingUnitLoadStatistic>(entity =>
            {
                entity.ToView("vTrashGeneratingUnitLoadStatistic");

                entity.Property(e => e.LandUseType).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.PriorityLandUseTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);

                entity.Property(e => e.WaterQualityManagementPlanName).IsUnicode(false);
            });

            modelBuilder.Entity<vTreatmentBMPDetailed>(entity =>
            {
                entity.ToView("vTreatmentBMPDetailed");

                entity.Property(e => e.DelineationTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.OwnerOrganizationName).IsUnicode(false);

                entity.Property(e => e.SizingBasisTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.TrashCaptureStatusTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPLifespanTypeDisplayName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPTypeName).IsUnicode(false);
            });

            modelBuilder.Entity<vViewTreatmentBMPModelingAttribute>(entity =>
            {
                entity.ToView("vViewTreatmentBMPModelingAttributes");

                entity.Property(e => e.DelineationStatus).IsUnicode(false);

                entity.Property(e => e.DelineationType).IsUnicode(false);

                entity.Property(e => e.OperationMonths).IsUnicode(false);

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPName).IsUnicode(false);

                entity.Property(e => e.TreatmentBMPTypeName).IsUnicode(false);

                entity.Property(e => e.UpstreamBMPName).IsUnicode(false);

                entity.Property(e => e.WatershedName).IsUnicode(false);
            });

            modelBuilder.Entity<vWaterQualityManagementPlanLGUAudit>(entity =>
            {
                entity.ToView("vWaterQualityManagementPlanLGUAudit");

                entity.Property(e => e.WaterQualityManagementPlanName).IsUnicode(false);
            });

            modelBuilder.Entity<vWaterQualityManagementPlanLGUInput>(entity =>
            {
                entity.ToView("vWaterQualityManagementPlanLGUInput");

                entity.Property(e => e.WQMPID).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<vWaterQualityManagementPlanTGUInput>(entity =>
            {
                entity.ToView("vWaterQualityManagementPlanTGUInput");

                entity.Property(e => e.OrganizationName).IsUnicode(false);

                entity.Property(e => e.TrashCaptureStatusTypeDisplayName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
