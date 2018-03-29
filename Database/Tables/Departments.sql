CREATE TABLE [dbo].[Departments]
(
	DepartmentId INT IDENTITY(1,1) CONSTRAINT departments_id_pk PRIMARY KEY,
	DepartmentName VARCHAR(50) CONSTRAINT departments_departmentname_nn NOT NULL
							   CONSTRAINT departments_departmentname_uk UNIQUE,
	DepartmentDescription VARCHAR(max)
)
