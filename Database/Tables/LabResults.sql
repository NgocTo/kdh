CREATE TABLE [dbo].[Results]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ReportId] UNIQUEIDENTIFIER NOT NULL, 
    [TestId] UNIQUEIDENTIFIER NOT NULL, 
    [Flag] VARCHAR(6) NOT NULL, 
    [Result] FLOAT NOT NULL, 
    [Note] VARCHAR(300) NULL, 
    CONSTRAINT [FK_Results_LabReports] FOREIGN KEY ([ReportId]) REFERENCES [LabReports]([Id]), 
    CONSTRAINT [FK_Results_TestTypes] FOREIGN KEY ([TestId]) REFERENCES [TestTypes]([Id]), 
    CONSTRAINT [CK_Results_Flag] CHECK (Flag='High' OR Flag='Normal' OR Flag='Low'),
)
