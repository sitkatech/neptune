delete from dbo.FieldDefinition
go

INSERT [dbo].[FieldDefinition] ([FieldDefinitionID], [FieldDefinitionName], [FieldDefinitionDisplayName], [DefaultDefinition], CanCustomizeLabel) 
VALUES 
(12, N'IsPrimaryContactOrganization', N'Is Primary Contact Organization', N'<p>The entity with primary responsibility for organizing, planning, and executing implementation activities for a project or program. This is usually the lead implementer.</p>', 1),
(14, N'Organization', N'Organization', N'<p>A partner entity that is directly involved with implementation or funding a project.&nbsp;</p>', 1),
(17, N'Password', N'Password', N'<p>Password required to log into the ProjectNeptune tool in order to access and edit project and program information.</p>', 0),
(21, N'MeasurementUnit', N'Measurement Unit', N'<p>The unit of measure used by an Indicator (aka&nbsp;Performance Measure) to track the extent of implementation.</p>', 1),
(22, N'PhotoCaption', N'Photo Caption', N'<p>A concise yet descriptive explanation of an uploaded photo. Photo captions are displayed in the lower right-hand corner of the image as it appears on the webpage.</p>', 1),
(23, N'PhotoCredit', N'Photo Credit', N'<p>If needed, credit is given to the photographer or owner of an image on the website. Photo credits are displayed in the lower right-hand corner of the image as it appears on the webpage.</p>', 1),
(24, N'PhotoTiming', N'Photo Timing', N'<p>The phase in a project timeline during which the photograph was taken. Photo timing can be before, during or after project implementation.&nbsp;</p>', 1),
(25, N'PrimaryContact', N'Primary Contact', N'<p>An individual at the listed organization responsible for reporting accomplishments and expenditures achieved by the project or program, and who should be contacted when there are questions related to any project associated to the organization.</p>', 1),
(34, N'OrganizationType', N'Organization Type', N'<p>A categorization of an organization, e.g. Local, State, Federal or Private.</p>', 1),
(42, N'Username', N'User name', N'<p>Password required to log into the system&nbsp;order to access and edit project and program information that is not allowed by public users.</p>', 1),
(88, N'ExternalLinks', N'External Links', N'<p>Links to external web pages where you might find additional information.</p>', 1),
(182, N'RoleName', N'Role Name', N'<p>The name or title describing&nbsp;function or set of permissions that can be assigned to a user.</p>', 1),
(247, 'Chart Last Updated Date', 'ChartLastUpdatedDate','<p>The date this chart was last updated with current information.</p>', 3)