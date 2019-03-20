
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class DatabaseEntities : DbContext, LtInfo.Common.EntityModelBinding.ILtInfoEntityTypeLoader
    {
        static DatabaseEntities()
        {
            System.Data.Entity.Database.SetInitializer<DatabaseEntities>(null);
        }


        public DatabaseEntities()
            : base("name=DatabaseEntities")
        {

        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new AuditLogConfiguration());
            modelBuilder.Configurations.Add(new CountyConfiguration());
            modelBuilder.Configurations.Add(new CustomAttributeConfiguration());
            modelBuilder.Configurations.Add(new CustomAttributeTypeConfiguration());
            modelBuilder.Configurations.Add(new CustomAttributeValueConfiguration());
            modelBuilder.Configurations.Add(new DelineationConfiguration());
            modelBuilder.Configurations.Add(new FieldDefinitionDataConfiguration());
            modelBuilder.Configurations.Add(new FieldDefinitionDataImageConfiguration());
            modelBuilder.Configurations.Add(new FieldVisitConfiguration());
            modelBuilder.Configurations.Add(new FileResourceConfiguration());
            modelBuilder.Configurations.Add(new FundingEventConfiguration());
            modelBuilder.Configurations.Add(new FundingEventFundingSourceConfiguration());
            modelBuilder.Configurations.Add(new FundingSourceConfiguration());
            modelBuilder.Configurations.Add(new HydrologicSubareaConfiguration());
            modelBuilder.Configurations.Add(new LandUseBlockConfiguration());
            modelBuilder.Configurations.Add(new MaintenanceRecordConfiguration());
            modelBuilder.Configurations.Add(new MaintenanceRecordObservationConfiguration());
            modelBuilder.Configurations.Add(new MaintenanceRecordObservationValueConfiguration());
            modelBuilder.Configurations.Add(new ModeledCatchmentConfiguration());
            modelBuilder.Configurations.Add(new ModeledCatchmentGeometryStagingConfiguration());
            modelBuilder.Configurations.Add(new NeptuneHomePageImageConfiguration());
            modelBuilder.Configurations.Add(new NeptunePageConfiguration());
            modelBuilder.Configurations.Add(new NeptunePageImageConfiguration());
            modelBuilder.Configurations.Add(new NetworkCatchmentConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new OnlandVisualTrashAssessmentConfiguration());
            modelBuilder.Configurations.Add(new OnlandVisualTrashAssessmentAreaConfiguration());
            modelBuilder.Configurations.Add(new OnlandVisualTrashAssessmentObservationConfiguration());
            modelBuilder.Configurations.Add(new OnlandVisualTrashAssessmentObservationPhotoConfiguration());
            modelBuilder.Configurations.Add(new OnlandVisualTrashAssessmentObservationPhotoStagingConfiguration());
            modelBuilder.Configurations.Add(new OrganizationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationTypeConfiguration());
            modelBuilder.Configurations.Add(new ParcelConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new QuickBMPConfiguration());
            modelBuilder.Configurations.Add(new SourceControlBMPConfiguration());
            modelBuilder.Configurations.Add(new SourceControlBMPAttributeConfiguration());
            modelBuilder.Configurations.Add(new SourceControlBMPAttributeCategoryConfiguration());
            modelBuilder.Configurations.Add(new StateProvinceConfiguration());
            modelBuilder.Configurations.Add(new StormwaterJurisdictionConfiguration());
            modelBuilder.Configurations.Add(new StormwaterJurisdictionPersonConfiguration());
            modelBuilder.Configurations.Add(new SupportRequestLogConfiguration());
            modelBuilder.Configurations.Add(new TrainingVideoConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPAssessmentConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPAssessmentObservationTypeConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPAssessmentPhotoConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPBenchmarkAndThresholdConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPDocumentConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPImageConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPObservationConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPTypeConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPTypeAssessmentObservationTypeConfiguration());
            modelBuilder.Configurations.Add(new TreatmentBMPTypeCustomAttributeTypeConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanDocumentConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanParcelConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanPhotoConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanVerifyConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanVerifyPhotoConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanVerifyQuickBMPConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanVerifySourceControlBMPConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanVerifyStatusConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanVerifyTreatmentBMPConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanVerifyTypeConfiguration());
            modelBuilder.Configurations.Add(new WaterQualityManagementPlanVisitStatusConfiguration());
        }
        public virtual DbSet<AuditLog> AuditLogs { get; set; }
        public virtual DbSet<County> Counties { get; set; }
        public virtual DbSet<CustomAttribute> CustomAttributes { get; set; }
        public virtual DbSet<CustomAttributeType> CustomAttributeTypes { get; set; }
        public virtual DbSet<CustomAttributeValue> CustomAttributeValues { get; set; }
        public virtual DbSet<Delineation> Delineations { get; set; }
        public virtual DbSet<FieldDefinitionDataImage> FieldDefinitionDataImages { get; set; }
        public virtual DbSet<FieldDefinitionData> FieldDefinitionDatas { get; set; }
        public virtual DbSet<FieldVisit> FieldVisits { get; set; }
        public virtual DbSet<FileResource> FileResources { get; set; }
        public virtual DbSet<FundingEventFundingSource> FundingEventFundingSources { get; set; }
        public virtual DbSet<FundingEvent> FundingEvents { get; set; }
        public virtual DbSet<FundingSource> FundingSources { get; set; }
        public virtual DbSet<HydrologicSubarea> HydrologicSubareas { get; set; }
        public virtual DbSet<LandUseBlock> LandUseBlocks { get; set; }
        public virtual DbSet<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        public virtual DbSet<MaintenanceRecordObservationValue> MaintenanceRecordObservationValues { get; set; }
        public virtual DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public virtual DbSet<ModeledCatchmentGeometryStaging> ModeledCatchmentGeometryStagings { get; set; }
        public virtual DbSet<ModeledCatchment> ModeledCatchments { get; set; }
        public virtual DbSet<NeptuneHomePageImage> NeptuneHomePageImages { get; set; }
        public virtual DbSet<NeptunePageImage> NeptunePageImages { get; set; }
        public virtual DbSet<NeptunePage> NeptunePages { get; set; }
        public virtual DbSet<NetworkCatchment> NetworkCatchments { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreas { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentObservationPhoto> OnlandVisualTrashAssessmentObservationPhotos { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentObservationPhotoStaging> OnlandVisualTrashAssessmentObservationPhotoStagings { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentObservation> OnlandVisualTrashAssessmentObservations { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes { get; set; }
        public virtual DbSet<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<OrganizationType> OrganizationTypes { get; set; }
        public virtual DbSet<Parcel> Parcels { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<QuickBMP> QuickBMPs { get; set; }
        public virtual DbSet<SourceControlBMPAttributeCategory> SourceControlBMPAttributeCategories { get; set; }
        public virtual DbSet<SourceControlBMPAttribute> SourceControlBMPAttributes { get; set; }
        public virtual DbSet<SourceControlBMP> SourceControlBMPs { get; set; }
        public virtual DbSet<StateProvince> StateProvinces { get; set; }
        public virtual DbSet<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get; set; }
        public virtual DbSet<StormwaterJurisdiction> StormwaterJurisdictions { get; set; }
        public virtual DbSet<SupportRequestLog> SupportRequestLogs { get; set; }
        public virtual DbSet<TrainingVideo> TrainingVideos { get; set; }
        public virtual DbSet<TreatmentBMPAssessmentObservationType> TreatmentBMPAssessmentObservationTypes { get; set; }
        public virtual DbSet<TreatmentBMPAssessmentPhoto> TreatmentBMPAssessmentPhotos { get; set; }
        public virtual DbSet<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
        public virtual DbSet<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual DbSet<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; }
        public virtual DbSet<TreatmentBMPImage> TreatmentBMPImages { get; set; }
        public virtual DbSet<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        public virtual DbSet<TreatmentBMP> TreatmentBMPs { get; set; }
        public virtual DbSet<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get; set; }
        public virtual DbSet<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; }
        public virtual DbSet<TreatmentBMPType> TreatmentBMPTypes { get; set; }
        public virtual DbSet<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
        public virtual DbSet<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get; set; }
        public virtual DbSet<WaterQualityManagementPlanPhoto> WaterQualityManagementPlanPhotos { get; set; }
        public virtual DbSet<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyPhoto> WaterQualityManagementPlanVerifyPhotos { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyQuickBMP> WaterQualityManagementPlanVerifyQuickBMPs { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifySourceControlBMP> WaterQualityManagementPlanVerifySourceControlBMPs { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyStatus> WaterQualityManagementPlanVerifyStatuses { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyTreatmentBMP> WaterQualityManagementPlanVerifyTreatmentBMPs { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVerifyType> WaterQualityManagementPlanVerifyTypes { get; set; }
        public virtual DbSet<WaterQualityManagementPlanVisitStatus> WaterQualityManagementPlanVisitStatuses { get; set; }

        public object LoadType(Type type, int primaryKey)
        {
            switch (type.Name)
            {
                case "AuditLogEventType":
                    var auditLogEventType = AuditLogEventType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(auditLogEventType, "AuditLogEventType", primaryKey);
                    return auditLogEventType;

                case "AuditLog":
                    return AuditLogs.GetAuditLog(primaryKey);

                case "County":
                    return Counties.GetCounty(primaryKey);

                case "CustomAttributeDataType":
                    var customAttributeDataType = CustomAttributeDataType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(customAttributeDataType, "CustomAttributeDataType", primaryKey);
                    return customAttributeDataType;

                case "CustomAttribute":
                    return CustomAttributes.GetCustomAttribute(primaryKey);

                case "CustomAttributeTypePurpose":
                    var customAttributeTypePurpose = CustomAttributeTypePurpose.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(customAttributeTypePurpose, "CustomAttributeTypePurpose", primaryKey);
                    return customAttributeTypePurpose;

                case "CustomAttributeType":
                    return CustomAttributeTypes.GetCustomAttributeType(primaryKey);

                case "CustomAttributeValue":
                    return CustomAttributeValues.GetCustomAttributeValue(primaryKey);

                case "Delineation":
                    return Delineations.GetDelineation(primaryKey);

                case "DelineationType":
                    var delineationType = DelineationType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(delineationType, "DelineationType", primaryKey);
                    return delineationType;

                case "FieldDefinitionDataImage":
                    return FieldDefinitionDataImages.GetFieldDefinitionDataImage(primaryKey);

                case "FieldDefinitionData":
                    return FieldDefinitionDatas.GetFieldDefinitionData(primaryKey);

                case "FieldDefinition":
                    var fieldDefinition = FieldDefinition.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(fieldDefinition, "FieldDefinition", primaryKey);
                    return fieldDefinition;

                case "FieldVisit":
                    return FieldVisits.GetFieldVisit(primaryKey);

                case "FieldVisitSection":
                    var fieldVisitSection = FieldVisitSection.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(fieldVisitSection, "FieldVisitSection", primaryKey);
                    return fieldVisitSection;

                case "FieldVisitStatus":
                    var fieldVisitStatus = FieldVisitStatus.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(fieldVisitStatus, "FieldVisitStatus", primaryKey);
                    return fieldVisitStatus;

                case "FieldVisitType":
                    var fieldVisitType = FieldVisitType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(fieldVisitType, "FieldVisitType", primaryKey);
                    return fieldVisitType;

                case "FileResourceMimeType":
                    var fileResourceMimeType = FileResourceMimeType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(fileResourceMimeType, "FileResourceMimeType", primaryKey);
                    return fileResourceMimeType;

                case "FileResource":
                    return FileResources.GetFileResource(primaryKey);

                case "FundingEventFundingSource":
                    return FundingEventFundingSources.GetFundingEventFundingSource(primaryKey);

                case "FundingEvent":
                    return FundingEvents.GetFundingEvent(primaryKey);

                case "FundingEventType":
                    var fundingEventType = FundingEventType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(fundingEventType, "FundingEventType", primaryKey);
                    return fundingEventType;

                case "FundingSource":
                    return FundingSources.GetFundingSource(primaryKey);

                case "GoogleChartType":
                    var googleChartType = GoogleChartType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(googleChartType, "GoogleChartType", primaryKey);
                    return googleChartType;

                case "HydrologicSubarea":
                    return HydrologicSubareas.GetHydrologicSubarea(primaryKey);

                case "HydromodificationApplies":
                    var hydromodificationApplies = HydromodificationApplies.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(hydromodificationApplies, "HydromodificationApplies", primaryKey);
                    return hydromodificationApplies;

                case "LandUseBlock":
                    return LandUseBlocks.GetLandUseBlock(primaryKey);

                case "MaintenanceRecordObservation":
                    return MaintenanceRecordObservations.GetMaintenanceRecordObservation(primaryKey);

                case "MaintenanceRecordObservationValue":
                    return MaintenanceRecordObservationValues.GetMaintenanceRecordObservationValue(primaryKey);

                case "MaintenanceRecord":
                    return MaintenanceRecords.GetMaintenanceRecord(primaryKey);

                case "MaintenanceRecordType":
                    var maintenanceRecordType = MaintenanceRecordType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(maintenanceRecordType, "MaintenanceRecordType", primaryKey);
                    return maintenanceRecordType;

                case "MeasurementUnitType":
                    var measurementUnitType = MeasurementUnitType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(measurementUnitType, "MeasurementUnitType", primaryKey);
                    return measurementUnitType;

                case "ModeledCatchmentGeometryStaging":
                    return ModeledCatchmentGeometryStagings.GetModeledCatchmentGeometryStaging(primaryKey);

                case "ModeledCatchment":
                    return ModeledCatchments.GetModeledCatchment(primaryKey);

                case "NeptuneArea":
                    var neptuneArea = NeptuneArea.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(neptuneArea, "NeptuneArea", primaryKey);
                    return neptuneArea;

                case "NeptuneHomePageImage":
                    return NeptuneHomePageImages.GetNeptuneHomePageImage(primaryKey);

                case "NeptunePageImage":
                    return NeptunePageImages.GetNeptunePageImage(primaryKey);

                case "NeptunePageRenderType":
                    var neptunePageRenderType = NeptunePageRenderType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(neptunePageRenderType, "NeptunePageRenderType", primaryKey);
                    return neptunePageRenderType;

                case "NeptunePage":
                    return NeptunePages.GetNeptunePage(primaryKey);

                case "NeptunePageType":
                    var neptunePageType = NeptunePageType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(neptunePageType, "NeptunePageType", primaryKey);
                    return neptunePageType;

                case "NetworkCatchment":
                    return NetworkCatchments.GetNetworkCatchment(primaryKey);

                case "Notification":
                    return Notifications.GetNotification(primaryKey);

                case "NotificationType":
                    var notificationType = NotificationType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(notificationType, "NotificationType", primaryKey);
                    return notificationType;

                case "ObservationTargetType":
                    var observationTargetType = ObservationTargetType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(observationTargetType, "ObservationTargetType", primaryKey);
                    return observationTargetType;

                case "ObservationThresholdType":
                    var observationThresholdType = ObservationThresholdType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(observationThresholdType, "ObservationThresholdType", primaryKey);
                    return observationThresholdType;

                case "ObservationTypeCollectionMethod":
                    var observationTypeCollectionMethod = ObservationTypeCollectionMethod.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(observationTypeCollectionMethod, "ObservationTypeCollectionMethod", primaryKey);
                    return observationTypeCollectionMethod;

                case "ObservationTypeSpecification":
                    var observationTypeSpecification = ObservationTypeSpecification.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(observationTypeSpecification, "ObservationTypeSpecification", primaryKey);
                    return observationTypeSpecification;

                case "OnlandVisualTrashAssessmentArea":
                    return OnlandVisualTrashAssessmentAreas.GetOnlandVisualTrashAssessmentArea(primaryKey);

                case "OnlandVisualTrashAssessmentObservationPhoto":
                    return OnlandVisualTrashAssessmentObservationPhotos.GetOnlandVisualTrashAssessmentObservationPhoto(primaryKey);

                case "OnlandVisualTrashAssessmentObservationPhotoStaging":
                    return OnlandVisualTrashAssessmentObservationPhotoStagings.GetOnlandVisualTrashAssessmentObservationPhotoStaging(primaryKey);

                case "OnlandVisualTrashAssessmentObservation":
                    return OnlandVisualTrashAssessmentObservations.GetOnlandVisualTrashAssessmentObservation(primaryKey);

                case "OnlandVisualTrashAssessmentPreliminarySourceIdentificationType":
                    return OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes.GetOnlandVisualTrashAssessmentPreliminarySourceIdentificationType(primaryKey);

                case "OnlandVisualTrashAssessment":
                    return OnlandVisualTrashAssessments.GetOnlandVisualTrashAssessment(primaryKey);

                case "OnlandVisualTrashAssessmentScore":
                    var onlandVisualTrashAssessmentScore = OnlandVisualTrashAssessmentScore.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(onlandVisualTrashAssessmentScore, "OnlandVisualTrashAssessmentScore", primaryKey);
                    return onlandVisualTrashAssessmentScore;

                case "OnlandVisualTrashAssessmentStatus":
                    var onlandVisualTrashAssessmentStatus = OnlandVisualTrashAssessmentStatus.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(onlandVisualTrashAssessmentStatus, "OnlandVisualTrashAssessmentStatus", primaryKey);
                    return onlandVisualTrashAssessmentStatus;

                case "Organization":
                    return Organizations.GetOrganization(primaryKey);

                case "OrganizationType":
                    return OrganizationTypes.GetOrganizationType(primaryKey);

                case "OVTASection":
                    var oVTASection = OVTASection.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(oVTASection, "OVTASection", primaryKey);
                    return oVTASection;

                case "Parcel":
                    return Parcels.GetParcel(primaryKey);

                case "Person":
                    return People.GetPerson(primaryKey);

                case "PreliminarySourceIdentificationCategory":
                    var preliminarySourceIdentificationCategory = PreliminarySourceIdentificationCategory.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(preliminarySourceIdentificationCategory, "PreliminarySourceIdentificationCategory", primaryKey);
                    return preliminarySourceIdentificationCategory;

                case "PreliminarySourceIdentificationType":
                    var preliminarySourceIdentificationType = PreliminarySourceIdentificationType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(preliminarySourceIdentificationType, "PreliminarySourceIdentificationType", primaryKey);
                    return preliminarySourceIdentificationType;

                case "PriorityLandUseType":
                    var priorityLandUseType = PriorityLandUseType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(priorityLandUseType, "PriorityLandUseType", primaryKey);
                    return priorityLandUseType;

                case "QuickBMP":
                    return QuickBMPs.GetQuickBMP(primaryKey);

                case "Role":
                    var role = Role.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(role, "Role", primaryKey);
                    return role;

                case "SizingBasisType":
                    var sizingBasisType = SizingBasisType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(sizingBasisType, "SizingBasisType", primaryKey);
                    return sizingBasisType;

                case "SourceControlBMPAttributeCategory":
                    return SourceControlBMPAttributeCategories.GetSourceControlBMPAttributeCategory(primaryKey);

                case "SourceControlBMPAttribute":
                    return SourceControlBMPAttributes.GetSourceControlBMPAttribute(primaryKey);

                case "SourceControlBMP":
                    return SourceControlBMPs.GetSourceControlBMP(primaryKey);

                case "StateProvince":
                    return StateProvinces.GetStateProvince(primaryKey);

                case "StormwaterBreadCrumbEntity":
                    var stormwaterBreadCrumbEntity = StormwaterBreadCrumbEntity.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(stormwaterBreadCrumbEntity, "StormwaterBreadCrumbEntity", primaryKey);
                    return stormwaterBreadCrumbEntity;

                case "StormwaterJurisdictionPerson":
                    return StormwaterJurisdictionPeople.GetStormwaterJurisdictionPerson(primaryKey);

                case "StormwaterJurisdiction":
                    return StormwaterJurisdictions.GetStormwaterJurisdiction(primaryKey);

                case "SupportRequestLog":
                    return SupportRequestLogs.GetSupportRequestLog(primaryKey);

                case "SupportRequestType":
                    var supportRequestType = SupportRequestType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(supportRequestType, "SupportRequestType", primaryKey);
                    return supportRequestType;

                case "TrainingVideo":
                    return TrainingVideos.GetTrainingVideo(primaryKey);

                case "TrashCaptureStatusType":
                    var trashCaptureStatusType = TrashCaptureStatusType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(trashCaptureStatusType, "TrashCaptureStatusType", primaryKey);
                    return trashCaptureStatusType;

                case "TreatmentBMPAssessmentObservationType":
                    return TreatmentBMPAssessmentObservationTypes.GetTreatmentBMPAssessmentObservationType(primaryKey);

                case "TreatmentBMPAssessmentPhoto":
                    return TreatmentBMPAssessmentPhotos.GetTreatmentBMPAssessmentPhoto(primaryKey);

                case "TreatmentBMPAssessment":
                    return TreatmentBMPAssessments.GetTreatmentBMPAssessment(primaryKey);

                case "TreatmentBMPAssessmentType":
                    var treatmentBMPAssessmentType = TreatmentBMPAssessmentType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(treatmentBMPAssessmentType, "TreatmentBMPAssessmentType", primaryKey);
                    return treatmentBMPAssessmentType;

                case "TreatmentBMPBenchmarkAndThreshold":
                    return TreatmentBMPBenchmarkAndThresholds.GetTreatmentBMPBenchmarkAndThreshold(primaryKey);

                case "TreatmentBMPDocument":
                    return TreatmentBMPDocuments.GetTreatmentBMPDocument(primaryKey);

                case "TreatmentBMPImage":
                    return TreatmentBMPImages.GetTreatmentBMPImage(primaryKey);

                case "TreatmentBMPLifespanType":
                    var treatmentBMPLifespanType = TreatmentBMPLifespanType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(treatmentBMPLifespanType, "TreatmentBMPLifespanType", primaryKey);
                    return treatmentBMPLifespanType;

                case "TreatmentBMPObservation":
                    return TreatmentBMPObservations.GetTreatmentBMPObservation(primaryKey);

                case "TreatmentBMP":
                    return TreatmentBMPs.GetTreatmentBMP(primaryKey);

                case "TreatmentBMPTypeAssessmentObservationType":
                    return TreatmentBMPTypeAssessmentObservationTypes.GetTreatmentBMPTypeAssessmentObservationType(primaryKey);

                case "TreatmentBMPTypeCustomAttributeType":
                    return TreatmentBMPTypeCustomAttributeTypes.GetTreatmentBMPTypeCustomAttributeType(primaryKey);

                case "TreatmentBMPType":
                    return TreatmentBMPTypes.GetTreatmentBMPType(primaryKey);

                case "WaterQualityManagementPlanDevelopmentType":
                    var waterQualityManagementPlanDevelopmentType = WaterQualityManagementPlanDevelopmentType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(waterQualityManagementPlanDevelopmentType, "WaterQualityManagementPlanDevelopmentType", primaryKey);
                    return waterQualityManagementPlanDevelopmentType;

                case "WaterQualityManagementPlanDocument":
                    return WaterQualityManagementPlanDocuments.GetWaterQualityManagementPlanDocument(primaryKey);

                case "WaterQualityManagementPlanDocumentType":
                    var waterQualityManagementPlanDocumentType = WaterQualityManagementPlanDocumentType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(waterQualityManagementPlanDocumentType, "WaterQualityManagementPlanDocumentType", primaryKey);
                    return waterQualityManagementPlanDocumentType;

                case "WaterQualityManagementPlanLandUse":
                    var waterQualityManagementPlanLandUse = WaterQualityManagementPlanLandUse.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(waterQualityManagementPlanLandUse, "WaterQualityManagementPlanLandUse", primaryKey);
                    return waterQualityManagementPlanLandUse;

                case "WaterQualityManagementPlanParcel":
                    return WaterQualityManagementPlanParcels.GetWaterQualityManagementPlanParcel(primaryKey);

                case "WaterQualityManagementPlanPermitTerm":
                    var waterQualityManagementPlanPermitTerm = WaterQualityManagementPlanPermitTerm.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(waterQualityManagementPlanPermitTerm, "WaterQualityManagementPlanPermitTerm", primaryKey);
                    return waterQualityManagementPlanPermitTerm;

                case "WaterQualityManagementPlanPhoto":
                    return WaterQualityManagementPlanPhotos.GetWaterQualityManagementPlanPhoto(primaryKey);

                case "WaterQualityManagementPlanPriority":
                    var waterQualityManagementPlanPriority = WaterQualityManagementPlanPriority.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(waterQualityManagementPlanPriority, "WaterQualityManagementPlanPriority", primaryKey);
                    return waterQualityManagementPlanPriority;

                case "WaterQualityManagementPlan":
                    return WaterQualityManagementPlans.GetWaterQualityManagementPlan(primaryKey);

                case "WaterQualityManagementPlanStatus":
                    var waterQualityManagementPlanStatus = WaterQualityManagementPlanStatus.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(waterQualityManagementPlanStatus, "WaterQualityManagementPlanStatus", primaryKey);
                    return waterQualityManagementPlanStatus;

                case "WaterQualityManagementPlanVerify":
                    return WaterQualityManagementPlanVerifies.GetWaterQualityManagementPlanVerify(primaryKey);

                case "WaterQualityManagementPlanVerifyPhoto":
                    return WaterQualityManagementPlanVerifyPhotos.GetWaterQualityManagementPlanVerifyPhoto(primaryKey);

                case "WaterQualityManagementPlanVerifyQuickBMP":
                    return WaterQualityManagementPlanVerifyQuickBMPs.GetWaterQualityManagementPlanVerifyQuickBMP(primaryKey);

                case "WaterQualityManagementPlanVerifySourceControlBMP":
                    return WaterQualityManagementPlanVerifySourceControlBMPs.GetWaterQualityManagementPlanVerifySourceControlBMP(primaryKey);

                case "WaterQualityManagementPlanVerifyStatus":
                    return WaterQualityManagementPlanVerifyStatuses.GetWaterQualityManagementPlanVerifyStatus(primaryKey);

                case "WaterQualityManagementPlanVerifyTreatmentBMP":
                    return WaterQualityManagementPlanVerifyTreatmentBMPs.GetWaterQualityManagementPlanVerifyTreatmentBMP(primaryKey);

                case "WaterQualityManagementPlanVerifyType":
                    return WaterQualityManagementPlanVerifyTypes.GetWaterQualityManagementPlanVerifyType(primaryKey);

                case "WaterQualityManagementPlanVisitStatus":
                    return WaterQualityManagementPlanVisitStatuses.GetWaterQualityManagementPlanVisitStatus(primaryKey);
                default:
                    throw new NotImplementedException(string.Format("No loader for type \"{0}\"", type.FullName));
            }
        }
    }
}