using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neptune.API.Controllers;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Tests.Helpers;

namespace Neptune.Tests
{
    /// <summary>
    /// NPT-1129: the Field Visit, Maintenance Record, Assessment, Assessment Photo and WQMP O&amp;M
    /// Verification write endpoints checked role only ([JurisdictionEditFeature] /
    /// [JurisdictionManageFeature]), so an Editor or Manager could change another jurisdiction's
    /// records by calling the API directly. Pin the jurisdiction-scoped gates on all 23 endpoints
    /// so this regression class can't silently return — same rationale as WqmpEndpointAuthorizationTests.
    /// </summary>
    [TestClass]
    public class FieldVisitEndpointAuthorizationTests
    {
        private static readonly (Type Controller, string Method, Type Attribute)[] ScopedEndpoints =
        [
            (typeof(FieldVisitController), nameof(FieldVisitController.Create), typeof(TreatmentBMPEditFeature)),
            (typeof(FieldVisitController), nameof(FieldVisitController.UpdateDateAndType), typeof(FieldVisitEditFeature)),
            (typeof(FieldVisitController), nameof(FieldVisitController.UpdateInventoryUpdated), typeof(FieldVisitEditFeature)),
            (typeof(FieldVisitController), nameof(FieldVisitController.Finalize), typeof(FieldVisitEditFeature)),
            (typeof(FieldVisitController), nameof(FieldVisitController.Verify), typeof(FieldVisitManageFeature)),
            (typeof(FieldVisitController), nameof(FieldVisitController.MarkProvisional), typeof(FieldVisitManageFeature)),
            (typeof(FieldVisitController), nameof(FieldVisitController.ReturnToEdit), typeof(FieldVisitManageFeature)),
            (typeof(FieldVisitController), nameof(FieldVisitController.Delete), typeof(FieldVisitManageFeature)),

            (typeof(MaintenanceRecordController), nameof(MaintenanceRecordController.CreateForFieldVisit), typeof(FieldVisitEditFeature)),
            (typeof(MaintenanceRecordController), nameof(MaintenanceRecordController.Update), typeof(MaintenanceRecordEditFeature)),
            (typeof(MaintenanceRecordController), nameof(MaintenanceRecordController.Delete), typeof(MaintenanceRecordManageFeature)),

            (typeof(TreatmentBMPAssessmentByFieldVisitController), nameof(TreatmentBMPAssessmentByFieldVisitController.Create), typeof(FieldVisitEditFeature)),

            (typeof(TreatmentBMPAssessmentController), nameof(TreatmentBMPAssessmentController.UpsertObservations), typeof(TreatmentBMPAssessmentEditFeature)),
            (typeof(TreatmentBMPAssessmentController), nameof(TreatmentBMPAssessmentController.CopyFromInitial), typeof(TreatmentBMPAssessmentEditFeature)),
            (typeof(TreatmentBMPAssessmentController), nameof(TreatmentBMPAssessmentController.Delete), typeof(TreatmentBMPAssessmentEditFeature)),

            (typeof(TreatmentBMPAssessmentPhotoController), nameof(TreatmentBMPAssessmentPhotoController.Create), typeof(TreatmentBMPAssessmentEditFeature)),
            (typeof(TreatmentBMPAssessmentPhotoController), nameof(TreatmentBMPAssessmentPhotoController.UpdateCaption), typeof(TreatmentBMPAssessmentEditFeature)),
            (typeof(TreatmentBMPAssessmentPhotoController), nameof(TreatmentBMPAssessmentPhotoController.Delete), typeof(TreatmentBMPAssessmentEditFeature)),

            (typeof(WaterQualityManagementPlanController), nameof(WaterQualityManagementPlanController.CreateVerification), typeof(WaterQualityManagementPlanEditFeature)),
            (typeof(WaterQualityManagementPlanController), nameof(WaterQualityManagementPlanController.UpdateVerification), typeof(WaterQualityManagementPlanEditFeature)),
            (typeof(WaterQualityManagementPlanController), nameof(WaterQualityManagementPlanController.DeleteVerification), typeof(WaterQualityManagementPlanManageFeature)),
            (typeof(WaterQualityManagementPlanController), nameof(WaterQualityManagementPlanController.UploadVerificationSupportingDocumentation), typeof(WaterQualityManagementPlanEditFeature)),
            (typeof(WaterQualityManagementPlanController), nameof(WaterQualityManagementPlanController.DeleteVerificationSupportingDocumentation), typeof(WaterQualityManagementPlanEditFeature)),
        ];

        private static MethodInfo GetControllerMethod(Type controller, string methodName)
        {
            var methods = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => x.Name == methodName)
                .ToList();
            Assert.AreEqual(1, methods.Count, $"Expected exactly one {methodName}() on {controller.Name}.");
            return methods[0];
        }

        [TestMethod]
        public void AllScopedEndpoints_AreCovered()
        {
            Assert.AreEqual(23, ScopedEndpoints.Length, "NPT-1129 scopes 23 write endpoints.");
            Assert.AreEqual(23, ScopedEndpoints.Select(x => (x.Controller, x.Method)).Distinct().Count(), "Each endpoint should appear once.");
        }

        [TestMethod]
        public void WriteEndpoints_AreJurisdictionScoped()
        {
            foreach (var (controller, methodName, attributeType) in ScopedEndpoints)
            {
                var method = GetControllerMethod(controller, methodName);
                var label = $"{controller.Name}.{methodName}";

                Assert.IsTrue(method.GetCustomAttributes(attributeType, true).Any(),
                    $"{label} must be [{attributeType.Name}].");
                Assert.IsTrue(typeof(JurisdictionScopedFeature).IsAssignableFrom(attributeType),
                    $"{label}'s gate must be jurisdiction-scoped.");
                Assert.IsFalse(method.GetCustomAttributes(typeof(JurisdictionEditFeature), true).Any(),
                    $"{label} must not be the role-only [JurisdictionEditFeature] — it lets any Editor change another jurisdiction's records.");
                Assert.IsFalse(method.GetCustomAttributes(typeof(JurisdictionManageFeature), true).Any(),
                    $"{label} must not be the role-only [JurisdictionManageFeature] — it lets any Manager change another jurisdiction's records.");
            }
        }

        [TestMethod]
        public void DeleteVerification_IsManagerOnly()
        {
            // PO decision on NPT-1129: deleting a verification becomes Manager-only.
            var method = GetControllerMethod(typeof(WaterQualityManagementPlanController), nameof(WaterQualityManagementPlanController.DeleteVerification));
            Assert.IsTrue(method.GetCustomAttributes(typeof(WaterQualityManagementPlanManageFeature), true).Any());
            Assert.IsFalse(method.GetCustomAttributes(typeof(WaterQualityManagementPlanEditFeature), true).Any(),
                "DeleteVerification must not also carry the Edit-level gate, which grants JurisdictionEditors.");
        }

        [TestMethod]
        public void DeleteMaintenanceRecord_StaysManagerOnly()
        {
            // PO decision on NPT-1129: deleting a maintenance record stays Manager-only (NPT-984).
            var method = GetControllerMethod(typeof(MaintenanceRecordController), nameof(MaintenanceRecordController.Delete));
            Assert.IsTrue(method.GetCustomAttributes(typeof(MaintenanceRecordManageFeature), true).Any());
        }

        [TestMethod]
        public void BulkUploadTrashScreen_StaysRoleGated()
        {
            // No route id to scope on; the importer checks each row's jurisdiction against the caller's.
            var method = GetControllerMethod(typeof(FieldVisitController), nameof(FieldVisitController.BulkUploadTrashScreen));
            Assert.IsTrue(method.GetCustomAttributes(typeof(JurisdictionEditFeature), true).Any());
        }

        [TestMethod]
        public void ManageFeatures_ExcludeJurisdictionEditor()
        {
            var expected = new List<RoleEnum> { RoleEnum.JurisdictionManager, RoleEnum.Admin, RoleEnum.SitkaAdmin };
            BaseAuthorizationAttribute[] manageFeatures =
            [
                new FieldVisitManageFeature(),
                new MaintenanceRecordManageFeature(),
                new WaterQualityManagementPlanManageFeature(),
            ];

            foreach (var attribute in manageFeatures)
            {
                var grantedRoles = AuthorizationAttributeHelpers.GetGrantedRoles(attribute);
                CollectionAssert.AreEquivalent(expected, grantedRoles,
                    $"{attribute.GetType().Name} must grant exactly Manager-and-up — an Editor-level set would undo NPT-984's Manager-only rules.");
                CollectionAssert.DoesNotContain(grantedRoles, RoleEnum.JurisdictionEditor);
            }
        }

        [TestMethod]
        public void EditFeatures_GrantJurisdictionEditor()
        {
            var expected = new List<RoleEnum> { RoleEnum.SitkaAdmin, RoleEnum.Admin, RoleEnum.JurisdictionManager, RoleEnum.JurisdictionEditor };
            BaseAuthorizationAttribute[] editFeatures =
            [
                new TreatmentBMPEditFeature(),
                new WaterQualityManagementPlanEditFeature(),
                new FieldVisitEditFeature(),
                new MaintenanceRecordEditFeature(),
                new TreatmentBMPAssessmentEditFeature(),
            ];

            foreach (var attribute in editFeatures)
            {
                CollectionAssert.AreEquivalent(expected, AuthorizationAttributeHelpers.GetGrantedRoles(attribute),
                    $"{attribute.GetType().Name} must grant exactly Editor-and-up.");
            }
        }
    }
}
