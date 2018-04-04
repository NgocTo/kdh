CREATE TABLE [dbo].[OnlineConsultationForm]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [FirstName] VARCHAR(20) NOT NULL
				CONSTRAINT OnlineConsultationForm_FirstName_ck CHECK(FirstName NOT LIKE '%[0-9]%'), 
    [LastName] VARCHAR(20) NOT NULL
				CONSTRAINT OnlineConsultationForm_LastName_ck CHECK(LastName NOT LIKE '%[0-9]%'), 
    [DateOfBirth] DATE NOT NULL, 
    [Gender] VARCHAR(10) NOT NULL 
			 CONSTRAINT OnlineConsultationForm_Gender_ck CHECK(Gender IN ('Male', 'Female', 'Other')), 
    [PhoneNumber] VARCHAR(15) NOT NULL 
				  CONSTRAINT OnlineConsultationForm_PhoneNumber_ck CHECK((PhoneNumber not like '%[^0-9]%' ) AND (DATALENGTH([PhoneNumber]))>9), 
    [Email] VARCHAR(50) NOT NULL
			CONSTRAINT OnlineConsultationForm_Email_ck UNIQUE ([Email]), 
    [Specialization] VARCHAR(30) NOT NULL
					 CONSTRAINT OnlineConsultationForm_Specialization_ck CHECK(Specialization IN ('Cardiology', 'Ear nose and throat', 'Gynaecology', 'Urology', 'Nutrition and dietetics')), 
    [Comment] VARCHAR(500) NULL, 
    [DateOfConsultation] DATETIME NOT NULL

);
