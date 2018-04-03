CREATE TABLE [dbo].[Survey]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[UserName] VARCHAR(20),
	[DateOfSurvey] DATETIME NOT NULL,
    [QualityService] VARCHAR(20) NOT NULL
					 CONSTRAINT Survey_QualityService_ck CHECK(QualityService IN ('Very good', 'Good', 'Average', 'Poor', 'Very poor')),
    [AverageVisit] VARCHAR(20) NOT NULL
					 CONSTRAINT Survey_AverageVisit_ck CHECK(AverageVisit IN ('Less than 1 visit', '1-2 visits', '3-5 visits', 'More than 5 visits')),
    [AppointmentIssue] VARCHAR(5) NOT NULL
					 CONSTRAINT Survey_AppointmentIssue_ck CHECK(AppointmentIssue IN ('Yes', 'No')),
    [StaffRate] VARCHAR(50) NOT NULL
					 CONSTRAINT Survey_StaffRate_ck CHECK(StaffRate IN ('Very professional', 'Somewhat professional', 'Neutral', 'Somewhat unprofessional', 'Unprofessional')),
    [Comment] VARCHAR(500) NULL
);
