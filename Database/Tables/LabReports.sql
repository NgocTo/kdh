CREATE TABLE [dbo].[LabReports]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [PatientId] UNIQUEIDENTIFIER NOT NULL, 
    [CollectionDate] DATETIME2 NOT NULL, 
    [IssueDate] DATETIME2 NULL, 
    [Status] VARCHAR(10) NOT NULL, 
    [OrderedBy] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_LabReports_Patients] FOREIGN KEY ([PatientId]) REFERENCES [Patients]([Id]), 
    CONSTRAINT [CK_LabReports_Status] CHECK (Status='Complete' OR Status='Incomplete' OR Status='Processing'),
)
