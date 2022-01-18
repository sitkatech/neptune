using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("FieldVisit")]
    [Index(nameof(FieldVisitID), nameof(TreatmentBMPID), Name = "AK_FieldVisit_FieldVisitID_TreatmentBMPID", IsUnique = true)]
    public partial class FieldVisit
    {
        public FieldVisit()
        {
            MaintenanceRecordFieldVisitNavigations = new HashSet<MaintenanceRecord>();
            TreatmentBMPAssessmentFieldVisitNavigations = new HashSet<TreatmentBMPAssessment>();
            TreatmentBMPAssessmentFieldVisits = new HashSet<TreatmentBMPAssessment>();
        }

        [Key]
        public int FieldVisitID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int FieldVisitStatusID { get; set; }
        public int PerformedByPersonID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime VisitDate { get; set; }
        public bool InventoryUpdated { get; set; }
        public int FieldVisitTypeID { get; set; }
        public bool IsFieldVisitVerified { get; set; }

        [ForeignKey(nameof(FieldVisitStatusID))]
        [InverseProperty("FieldVisits")]
        public virtual FieldVisitStatus FieldVisitStatus { get; set; }
        [ForeignKey(nameof(FieldVisitTypeID))]
        [InverseProperty("FieldVisits")]
        public virtual FieldVisitType FieldVisitType { get; set; }
        [ForeignKey(nameof(PerformedByPersonID))]
        [InverseProperty(nameof(Person.FieldVisits))]
        public virtual Person PerformedByPerson { get; set; }
        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("FieldVisit")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        [InverseProperty(nameof(MaintenanceRecord.FieldVisit))]
        public virtual MaintenanceRecord MaintenanceRecordFieldVisit { get; set; }
        public virtual ICollection<MaintenanceRecord> MaintenanceRecordFieldVisitNavigations { get; set; }
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessmentFieldVisitNavigations { get; set; }
        [InverseProperty(nameof(TreatmentBMPAssessment.FieldVisit))]
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessmentFieldVisits { get; set; }
    }
}
