CREATE TABLE [dbo].[Applicants]
(
	ApplicantId INT IDENTITY(1,1) CONSTRAINT applicants_applicantid_pk PRIMARY KEY,
FirstName VARCHAR(50) CONSTRAINT applicants_firstname_nn NOT NULL
CONSTRAINT applicants_firstname_ck CHECK (FirstName NOT LIKE '%[0-9]%'),
LastName VARCHAR(50) CONSTRAINT applicants_lastname_nn NOT NULL
CONSTRAINT applicants_lastname_ck CHECK (LastName NOT LIKE '%[0-9]%'),
DateOfBirth DATE CONSTRAINT applicants_dob_nn NOT NULL
CONSTRAINT applicants_dob_ck CHECK ((DATEDIFF(YEAR, DateOfBirth, GetDate()) >= 18) AND DATEDIFF(YEAR, DateOfBirth, GetDate()) < 60),
Email VARCHAR(50) CONSTRAINT applicants_email_nn NOT NULL
CONSTRAINT applicants_email_ck CHECK (Email LIKE '%_@__%.__%' AND Email NOT LIKE '@%'),
Phone VARCHAR(15) CONSTRAINT applicants_phone_nn NOT NULL
CONSTRAINT applicants_phone_ck CHECK ((Phone NOT LIKE '%[^0-9]%') AND (DATALENGTH([Phone]) > 9)),
JobId VARCHAR(50) CONSTRAINT applicants_jobid_fk REFERENCES Jobs(JobId)
CONSTRAINT applicants_jobid_nn NOT NULL
)
