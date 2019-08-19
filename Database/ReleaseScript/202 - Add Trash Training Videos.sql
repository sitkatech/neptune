select * from dbo.TrainingVideo


Update dbo.TrainingVideo 
Set VideoName = 'Inventory Module - ' + VideoName


INSERT INTO dbo.TrainingVideo
           (VideoName
           ,VideoDescription
           ,VideoURL)
     VALUES
           ('Trash Module - Delineations', null, 'https://www.youtube.com/embed/Z1KYD-0KzXM'),
		   ('Trash Module - Performing OVTAs', null, 'https://www.youtube.com/embed/JURia1YkE0c'),
		   ('Trash Module - Results Maps and Interpretations', null, 'https://www.youtube.com/embed/p4-_sbJBNys'),
		   ('Trash Module - Data Export Features', null, 'https://www.youtube.com/embed/YN1SuIqBAEo'),
		   ('Trash Module - Onboarding Options', null, 'https://www.youtube.com/embed/u0W4In9ItJk')
           		   