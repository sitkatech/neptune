Create Procedure dbo.pOCTAPrioritizationUpdateFromStaging
As

Merge dbo.OCTAPrioritization t Using dbo.OCTAPrioritizationStaging s
	on (t.OCTAPrioritizationKey = s.OCTAPrioritizationKey)
When Matched
	Then Update set
		t.OCTAPrioritizationKey = s.OCTAPrioritizationKey,  
		t.OCTAPrioritizationGeometry = s.OCTAPrioritizationGeometry,  
		t.Watershed = s.Watershed,  
		t.CatchIDN = s.CatchIDN,  
		t.TPI = s.TPI,  
		t.WQNLU = s.WQNLU,  
		t.WQNMON = s.WQNMON, 
		t.IMPAIR = s.IMPAIR,  
		t.MON = s.MON, 
		t.SEA = s.SEA,  
		t.SEA_PCTL = s.SEA_PCTL,  
		t.PC_VOL_PCT = s.PC_VOL_PCT,  
		t.PC_NUT_PCT = s.PC_NUT_PCT,  
		t.PC_BAC_PCT = s.PC_BAC_PCT,  
		t.PC_MET_PCT = s.PC_MET_PCT,  
		t.PC_TSS_PCT = s.PC_TSS_PCT,
		t.LastUpdate = GetDate()
When not matched by Target
	Then insert (
		OCTAPrioritizationKey, 
		OCTAPrioritizationGeometry, 
		LastUpdate, 
		Watershed, 
		CatchIDN, 
		TPI, 
		WQNLU, 
		WQNMON, 
		IMPAIR, 
		MON, 
		SEA, 
		SEA_PCTL, 
		PC_VOL_PCT, 
		PC_NUT_PCT, 
		PC_BAC_PCT, 
		PC_MET_PCT, 
		PC_TSS_PCT
	)
	values (
		s.OCTAPrioritizationKey,  
		s.OCTAPrioritizationGeometry, 
		GetDate(), 
		s.Watershed, 
		s.CatchIDN, 
		s.TPI, 
		s.WQNLU, 
		s.WQNMON, 
		s.IMPAIR, 
		s.MON, 
		s.SEA, 
		s.SEA_PCTL, 
		s.PC_VOL_PCT, 
		s.PC_NUT_PCT, 
		s.PC_BAC_PCT, 
		s.PC_MET_PCT, 
		s.PC_TSS_PCT
	)
When Not Matched by Source
	Then Delete;

update dbo.OCTAPrioritizationStaging
set OCTAPrioritizationGeometry = OCTAPrioritizationGeometry.MakeValid()
where OCTAPrioritizationGeometry.STIsValid() = 0

GO