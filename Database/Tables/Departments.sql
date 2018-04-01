CREATE TABLE [dbo].[Departments]
(
	Departmentid int IDENTITY,
    Department_name varchar(20) not null,
    Department_location varchar(10) not null,
    Department_description varchar(100) not null,
    constraint pk_departmentid primary key(Departmentid)
)
