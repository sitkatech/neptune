alter table dbo.TreatmentBMP
add UpstreamBMPID int null

alter table dbo.TreatmentBMP
add constraint FK_TreatmentBMP_TreatmentBMP_UpstreamBMPID_TreatmentBMPID foreign key (UpstreamBMPID) references dbo.TreatmentBMP (TreatmentBMPID)