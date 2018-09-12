
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
            modelBuilder.Configurations.Add(new FieldDefinitionDataConfiguration());
            modelBuilder.Configurations.Add(new FieldDefinitionDataImageConfiguration());
            modelBuilder.Configurations.Add(new FieldVisitConfiguration());
            modelBuilder.Configurations.Add(new FileResourceConfiguration());
            modelBuilder.Configurations.Add(new FundingEventConfiguration());
            modelBuilder.Configurations.Add(new FundingEventFundingSourceConfiguration());
            modelBuilder.Configurations.Add(new FundingSourceConfiguration());
            modelBuilder.Configurations.Add(new HydrologicSubareaConfiguration());
            modelBuilder.Configurations.Add(new MaintenanceRecordConfiguration());
            modelBuilder.Configurations.Add(new MaintenanceRecordObservationConfiguration());
            modelBuilder.Configurations.Add(new MaintenanceRecordObservationValueConfiguration());
            modelBuilder.Configurations.Add(new ModeledCatchmentConfiguration());
            modelBuilder.Configurations.Add(new ModeledCatchmentGeometryStagingConfiguration());
            modelBuilder.Configurations.Add(new NeptuneHomePageImageConfiguration());
            modelBuilder.Configurations.Add(new NeptunePageConfiguration());
            modelBuilder.Configurations.Add(new NeptunePageImageConfiguration());
            modelBuilder.Configurations.Add(new NotificationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationConfiguration());
            modelBuilder.Configurations.Add(new OrganizationTypeConfiguration());
            modelBuilder.Configurations.Add(new ParcelConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new StateProvinceConfiguration());
            modelBuilder.Configurations.Add(new StormwaterJurisdictionConfiguration());
            modelBuilder.Configurations.Add(new StormwaterJurisdictionPersonConfiguration());
            modelBuilder.Configurations.Add(new SupportRequestLogConfiguration());
            modelBuilder.Configurations.Add(new TenantAttributeConfiguration());
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
        }
        public virtual DbSet<AuditLog> AllAuditLogs { get; set; }
        public virtual IQueryable<AuditLog> AuditLogs { get { return AllAuditLogs.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<County> AllCounties { get; set; }
        public virtual IQueryable<County> Counties { get { return AllCounties.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<CustomAttribute> AllCustomAttributes { get; set; }
        public virtual IQueryable<CustomAttribute> CustomAttributes { get { return AllCustomAttributes.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<CustomAttributeType> AllCustomAttributeTypes { get; set; }
        public virtual IQueryable<CustomAttributeType> CustomAttributeTypes { get { return AllCustomAttributeTypes.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<CustomAttributeValue> AllCustomAttributeValues { get; set; }
        public virtual IQueryable<CustomAttributeValue> CustomAttributeValues { get { return AllCustomAttributeValues.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FieldDefinitionDataImage> AllFieldDefinitionDataImages { get; set; }
        public virtual IQueryable<FieldDefinitionDataImage> FieldDefinitionDataImages { get { return AllFieldDefinitionDataImages.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FieldDefinitionData> AllFieldDefinitionDatas { get; set; }
        public virtual IQueryable<FieldDefinitionData> FieldDefinitionDatas { get { return AllFieldDefinitionDatas.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FieldVisit> AllFieldVisits { get; set; }
        public virtual IQueryable<FieldVisit> FieldVisits { get { return AllFieldVisits.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FileResource> AllFileResources { get; set; }
        public virtual IQueryable<FileResource> FileResources { get { return AllFileResources.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FundingEventFundingSource> AllFundingEventFundingSources { get; set; }
        public virtual IQueryable<FundingEventFundingSource> FundingEventFundingSources { get { return AllFundingEventFundingSources.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FundingEvent> AllFundingEvents { get; set; }
        public virtual IQueryable<FundingEvent> FundingEvents { get { return AllFundingEvents.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FundingSource> AllFundingSources { get; set; }
        public virtual IQueryable<FundingSource> FundingSources { get { return AllFundingSources.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<HydrologicSubarea> HydrologicSubareas { get; set; }
        public virtual DbSet<MaintenanceRecordObservation> AllMaintenanceRecordObservations { get; set; }
        public virtual IQueryable<MaintenanceRecordObservation> MaintenanceRecordObservations { get { return AllMaintenanceRecordObservations.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<MaintenanceRecordObservationValue> AllMaintenanceRecordObservationValues { get; set; }
        public virtual IQueryable<MaintenanceRecordObservationValue> MaintenanceRecordObservationValues { get { return AllMaintenanceRecordObservationValues.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<MaintenanceRecord> AllMaintenanceRecords { get; set; }
        public virtual IQueryable<MaintenanceRecord> MaintenanceRecords { get { return AllMaintenanceRecords.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<ModeledCatchmentGeometryStaging> AllModeledCatchmentGeometryStagings { get; set; }
        public virtual IQueryable<ModeledCatchmentGeometryStaging> ModeledCatchmentGeometryStagings { get { return AllModeledCatchmentGeometryStagings.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<ModeledCatchment> AllModeledCatchments { get; set; }
        public virtual IQueryable<ModeledCatchment> ModeledCatchments { get { return AllModeledCatchments.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<NeptuneHomePageImage> AllNeptuneHomePageImages { get; set; }
        public virtual IQueryable<NeptuneHomePageImage> NeptuneHomePageImages { get { return AllNeptuneHomePageImages.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<NeptunePageImage> AllNeptunePageImages { get; set; }
        public virtual IQueryable<NeptunePageImage> NeptunePageImages { get { return AllNeptunePageImages.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<NeptunePage> AllNeptunePages { get; set; }
        public virtual IQueryable<NeptunePage> NeptunePages { get { return AllNeptunePages.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<Notification> AllNotifications { get; set; }
        public virtual IQueryable<Notification> Notifications { get { return AllNotifications.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<Organization> AllOrganizations { get; set; }
        public virtual IQueryable<Organization> Organizations { get { return AllOrganizations.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<OrganizationType> AllOrganizationTypes { get; set; }
        public virtual IQueryable<OrganizationType> OrganizationTypes { get { return AllOrganizationTypes.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<Parcel> AllParcels { get; set; }
        public virtual IQueryable<Parcel> Parcels { get { return AllParcels.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<Person> AllPeople { get; set; }
        public virtual IQueryable<Person> People { get { return AllPeople.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<StateProvince> AllStateProvinces { get; set; }
        public virtual IQueryable<StateProvince> StateProvinces { get { return AllStateProvinces.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<StormwaterJurisdictionPerson> AllStormwaterJurisdictionPeople { get; set; }
        public virtual IQueryable<StormwaterJurisdictionPerson> StormwaterJurisdictionPeople { get { return AllStormwaterJurisdictionPeople.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<StormwaterJurisdiction> AllStormwaterJurisdictions { get; set; }
        public virtual IQueryable<StormwaterJurisdiction> StormwaterJurisdictions { get { return AllStormwaterJurisdictions.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<SupportRequestLog> AllSupportRequestLogs { get; set; }
        public virtual IQueryable<SupportRequestLog> SupportRequestLogs { get { return AllSupportRequestLogs.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TenantAttribute> AllTenantAttributes { get; set; }
        public virtual IQueryable<TenantAttribute> TenantAttributes { get { return AllTenantAttributes.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TrainingVideo> AllTrainingVideos { get; set; }
        public virtual IQueryable<TrainingVideo> TrainingVideos { get { return AllTrainingVideos.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPAssessmentObservationType> AllTreatmentBMPAssessmentObservationTypes { get; set; }
        public virtual IQueryable<TreatmentBMPAssessmentObservationType> TreatmentBMPAssessmentObservationTypes { get { return AllTreatmentBMPAssessmentObservationTypes.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPAssessmentPhoto> AllTreatmentBMPAssessmentPhotos { get; set; }
        public virtual IQueryable<TreatmentBMPAssessmentPhoto> TreatmentBMPAssessmentPhotos { get { return AllTreatmentBMPAssessmentPhotos.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPAssessment> AllTreatmentBMPAssessments { get; set; }
        public virtual IQueryable<TreatmentBMPAssessment> TreatmentBMPAssessments { get { return AllTreatmentBMPAssessments.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPBenchmarkAndThreshold> AllTreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual IQueryable<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get { return AllTreatmentBMPBenchmarkAndThresholds.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPDocument> AllTreatmentBMPDocuments { get; set; }
        public virtual IQueryable<TreatmentBMPDocument> TreatmentBMPDocuments { get { return AllTreatmentBMPDocuments.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPImage> AllTreatmentBMPImages { get; set; }
        public virtual IQueryable<TreatmentBMPImage> TreatmentBMPImages { get { return AllTreatmentBMPImages.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPObservation> AllTreatmentBMPObservations { get; set; }
        public virtual IQueryable<TreatmentBMPObservation> TreatmentBMPObservations { get { return AllTreatmentBMPObservations.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMP> AllTreatmentBMPs { get; set; }
        public virtual IQueryable<TreatmentBMP> TreatmentBMPs { get { return AllTreatmentBMPs.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPTypeAssessmentObservationType> AllTreatmentBMPTypeAssessmentObservationTypes { get; set; }
        public virtual IQueryable<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get { return AllTreatmentBMPTypeAssessmentObservationTypes.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPTypeCustomAttributeType> AllTreatmentBMPTypeCustomAttributeTypes { get; set; }
        public virtual IQueryable<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get { return AllTreatmentBMPTypeCustomAttributeTypes.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPType> AllTreatmentBMPTypes { get; set; }
        public virtual IQueryable<TreatmentBMPType> TreatmentBMPTypes { get { return AllTreatmentBMPTypes.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<WaterQualityManagementPlanDocument> AllWaterQualityManagementPlanDocuments { get; set; }
        public virtual IQueryable<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get { return AllWaterQualityManagementPlanDocuments.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<WaterQualityManagementPlanParcel> AllWaterQualityManagementPlanParcels { get; set; }
        public virtual IQueryable<WaterQualityManagementPlanParcel> WaterQualityManagementPlanParcels { get { return AllWaterQualityManagementPlanParcels.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<WaterQualityManagementPlan> AllWaterQualityManagementPlans { get; set; }
        public virtual IQueryable<WaterQualityManagementPlan> WaterQualityManagementPlans { get { return AllWaterQualityManagementPlans.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }

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

                case "Organization":
                    return Organizations.GetOrganization(primaryKey);

                case "OrganizationType":
                    return OrganizationTypes.GetOrganizationType(primaryKey);

                case "Parcel":
                    return Parcels.GetParcel(primaryKey);

                case "Person":
                    return People.GetPerson(primaryKey);

                case "Role":
                    var role = Role.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(role, "Role", primaryKey);
                    return role;

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

                case "TenantAttribute":
                    return TenantAttributes.GetTenantAttribute(primaryKey);

                case "Tenant":
                    var tenant = Tenant.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(tenant, "Tenant", primaryKey);
                    return tenant;

                case "TrainingVideo":
                    return TrainingVideos.GetTrainingVideo(primaryKey);

                case "TreatmentBMPAssessmentObservationType":
                    return TreatmentBMPAssessmentObservationTypes.GetTreatmentBMPAssessmentObservationType(primaryKey);

                case "TreatmentBMPAssessmentPhoto":
                    return TreatmentBMPAssessmentPhotos.GetTreatmentBMPAssessmentPhoto(primaryKey);

                case "TreatmentBMPAssessment":
                    return TreatmentBMPAssessments.GetTreatmentBMPAssessment(primaryKey);

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
                default:
                    throw new NotImplementedException(string.Format("No loader for type \"{0}\"", type.FullName));
            }
        }
    }
}