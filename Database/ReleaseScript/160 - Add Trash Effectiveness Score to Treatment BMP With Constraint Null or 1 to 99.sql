Alter table dbo.TreatmentBMP
Add TrashCaptureEffectiveness Int Null Constraint CK_TreatmentBMP_TrashCaptureEffectivenessMustBeBetween1And99 Check (TrashCaptureEffectiveness is null or (TrashCaptureEffectiveness > 0 And TrashCaptureEffectiveness < 100))

