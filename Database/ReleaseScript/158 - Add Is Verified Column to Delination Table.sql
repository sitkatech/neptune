Alter Table dbo.Delineation 
Add IsVerified bit Null

Go 

Update dbo.Delineation
Set IsVerified = 0

Go

Alter Table dbo.Delineation 
Alter Column IsVerified Bit Not Null

Go

Alter Table dbo.Delineation
Add DateLastVerified DateTime Null