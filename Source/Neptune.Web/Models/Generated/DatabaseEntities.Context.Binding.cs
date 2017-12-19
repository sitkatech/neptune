
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
        public DatabaseEntities()
            : base("name=DatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
        public virtual DbSet<AuditLog> AllAuditLogs { get; set; }
        public virtual IQueryable<AuditLog> AuditLogs { get { return AllAuditLogs.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<County> AllCounties { get; set; }
        public virtual IQueryable<County> Counties { get { return AllCounties.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FieldDefinitionDataImage> AllFieldDefinitionDataImages { get; set; }
        public virtual IQueryable<FieldDefinitionDataImage> FieldDefinitionDataImages { get { return AllFieldDefinitionDataImages.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FieldDefinitionData> AllFieldDefinitionDatas { get; set; }
        public virtual IQueryable<FieldDefinitionData> FieldDefinitionDatas { get { return AllFieldDefinitionDatas.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<FileResource> AllFileResources { get; set; }
        public virtual IQueryable<FileResource> FileResources { get { return AllFileResources.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
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
        public virtual DbSet<TreatmentBMPAssessment> AllTreatmentBMPAssessments { get; set; }
        public virtual IQueryable<TreatmentBMPAssessment> TreatmentBMPAssessments { get { return AllTreatmentBMPAssessments.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPBenchmarkAndThreshold> AllTreatmentBMPBenchmarkAndThresholds { get; set; }
        public virtual IQueryable<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get { return AllTreatmentBMPBenchmarkAndThresholds.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPInfiltrationReading> AllTreatmentBMPInfiltrationReadings { get; set; }
        public virtual IQueryable<TreatmentBMPInfiltrationReading> TreatmentBMPInfiltrationReadings { get { return AllTreatmentBMPInfiltrationReadings.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPObservationDetail> AllTreatmentBMPObservationDetails { get; set; }
        public virtual IQueryable<TreatmentBMPObservationDetail> TreatmentBMPObservationDetails { get { return AllTreatmentBMPObservationDetails.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPObservation> AllTreatmentBMPObservations { get; set; }
        public virtual IQueryable<TreatmentBMPObservation> TreatmentBMPObservations { get { return AllTreatmentBMPObservations.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMP> AllTreatmentBMPs { get; set; }
        public virtual IQueryable<TreatmentBMP> TreatmentBMPs { get { return AllTreatmentBMPs.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }
        public virtual DbSet<TreatmentBMPTypeObservationType> AllTreatmentBMPTypeObservationTypes { get; set; }
        public virtual IQueryable<TreatmentBMPTypeObservationType> TreatmentBMPTypeObservationTypes { get { return AllTreatmentBMPTypeObservationTypes.Where(x => x.TenantID == HttpRequestStorage.Tenant.TenantID); } }

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

                case "FieldDefinitionDataImage":
                    return FieldDefinitionDataImages.GetFieldDefinitionDataImage(primaryKey);

                case "FieldDefinitionData":
                    return FieldDefinitionDatas.GetFieldDefinitionData(primaryKey);

                case "FieldDefinition":
                    var fieldDefinition = FieldDefinition.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(fieldDefinition, "FieldDefinition", primaryKey);
                    return fieldDefinition;

                case "FileResourceMimeType":
                    var fileResourceMimeType = FileResourceMimeType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(fileResourceMimeType, "FileResourceMimeType", primaryKey);
                    return fileResourceMimeType;

                case "FileResource":
                    return FileResources.GetFileResource(primaryKey);

                case "GoogleChartType":
                    var googleChartType = GoogleChartType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(googleChartType, "GoogleChartType", primaryKey);
                    return googleChartType;

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

                case "ObservationType":
                    var observationType = ObservationType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(observationType, "ObservationType", primaryKey);
                    return observationType;

                case "ObservationValueType":
                    var observationValueType = ObservationValueType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(observationValueType, "ObservationValueType", primaryKey);
                    return observationValueType;

                case "Organization":
                    return Organizations.GetOrganization(primaryKey);

                case "OrganizationType":
                    return OrganizationTypes.GetOrganizationType(primaryKey);

                case "Person":
                    return People.GetPerson(primaryKey);

                case "Role":
                    var role = Role.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(role, "Role", primaryKey);
                    return role;

                case "StateProvince":
                    return StateProvinces.GetStateProvince(primaryKey);

                case "StormwaterAssessmentType":
                    var stormwaterAssessmentType = StormwaterAssessmentType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(stormwaterAssessmentType, "StormwaterAssessmentType", primaryKey);
                    return stormwaterAssessmentType;

                case "StormwaterBreadCrumbEntity":
                    var stormwaterBreadCrumbEntity = StormwaterBreadCrumbEntity.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(stormwaterBreadCrumbEntity, "StormwaterBreadCrumbEntity", primaryKey);
                    return stormwaterBreadCrumbEntity;

                case "StormwaterJurisdictionPerson":
                    return StormwaterJurisdictionPeople.GetStormwaterJurisdictionPerson(primaryKey);

                case "StormwaterJurisdiction":
                    return StormwaterJurisdictions.GetStormwaterJurisdiction(primaryKey);

                case "StormwaterRole":
                    var stormwaterRole = StormwaterRole.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(stormwaterRole, "StormwaterRole", primaryKey);
                    return stormwaterRole;

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

                case "TreatmentBMPAssessment":
                    return TreatmentBMPAssessments.GetTreatmentBMPAssessment(primaryKey);

                case "TreatmentBMPBenchmarkAndThreshold":
                    return TreatmentBMPBenchmarkAndThresholds.GetTreatmentBMPBenchmarkAndThreshold(primaryKey);

                case "TreatmentBMPInfiltrationReading":
                    return TreatmentBMPInfiltrationReadings.GetTreatmentBMPInfiltrationReading(primaryKey);

                case "TreatmentBMPObservationDetail":
                    return TreatmentBMPObservationDetails.GetTreatmentBMPObservationDetail(primaryKey);

                case "TreatmentBMPObservationDetailType":
                    var treatmentBMPObservationDetailType = TreatmentBMPObservationDetailType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(treatmentBMPObservationDetailType, "TreatmentBMPObservationDetailType", primaryKey);
                    return treatmentBMPObservationDetailType;

                case "TreatmentBMPObservation":
                    return TreatmentBMPObservations.GetTreatmentBMPObservation(primaryKey);

                case "TreatmentBMP":
                    return TreatmentBMPs.GetTreatmentBMP(primaryKey);

                case "TreatmentBMPTypeObservationType":
                    return TreatmentBMPTypeObservationTypes.GetTreatmentBMPTypeObservationType(primaryKey);

                case "TreatmentBMPType":
                    var treatmentBMPType = TreatmentBMPType.All.SingleOrDefault(x => x.PrimaryKey == primaryKey);
                    Check.RequireNotNullThrowNotFound(treatmentBMPType, "TreatmentBMPType", primaryKey);
                    return treatmentBMPType;
                default:
                    throw new NotImplementedException(string.Format("No loader for type \"{0}\"", type.FullName));
            }
        }
    }
}