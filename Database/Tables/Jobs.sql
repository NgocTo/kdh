CREATE TABLE [dbo].[Jobs]
(
	JobId VARCHAR(50) CONSTRAINT jobs_id_pk PRIMARY KEY,
	JobTitle VARCHAR(50) CONSTRAINT jobs_jobtitle_nn NOT NULL,
	JobStatus VARCHAR(50) CONSTRAINT jobs_status_nn NOT NULL,
JobDescription VARCHAR(max),
DepartmentId INT CONSTRAINT jobs_departmentid_fk REFERENCES Departments(DepartmentId)
CONSTRAINT jobs_departmentid_nn NOT NULL,
DatePosted DATE CONSTRAINT jobs_dateposted_ck CHECK(DatePosted >= GetDate()),
DateClosed DATETIME CONSTRAINT jobs_dateclosed_ck CHECK(DateClosed > GetDate()),
JobShift VARCHAR(50),
Salary VARCHAR(50) CONSTRAINT jobs_salary_nn NOT NULL,
Requirement VARCHAR(max) CONSTRAINT jobs_requirement_nn NOT NULL,
UserId UNIQUEIDENTIFIER CONSTRAINT jobs_userid_fk REFERENCES Users(Id)
CONSTRAINT jobs_userid_nn NOT NULL
)
