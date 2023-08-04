CREATE TABLE [dbo].[Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Person_PersonID] PRIMARY KEY,
	[PersonGuid] [uniqueidentifier] NOT NULL,
	[FirstName] [varchar](100),
	[LastName] [varchar](100),
	[Email] [varchar](255),
	[Phone] [varchar](30) NULL,
	[RoleID] [int] NOT NULL CONSTRAINT [FK_Person_Role_RoleID] FOREIGN KEY([RoleID]) REFERENCES [dbo].[Role] ([RoleID]),
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[LastActivityDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[OrganizationID] [int] NOT NULL CONSTRAINT [FK_Person_Organization_OrganizationID] FOREIGN KEY REFERENCES [dbo].[Organization] ([OrganizationID]),
	[ReceiveSupportEmails] [bit] NOT NULL,
	[LoginName] [varchar](128),
	[ReceiveRSBRevisionRequestEmails] [bit] NOT NULL,
	[WebServiceAccessToken] [uniqueidentifier] NOT NULL,
	[IsOCTAGrantReviewer] [bit] NOT NULL,
)
