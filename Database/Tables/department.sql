CREATE TABLE [dbo].[departments]
(
	departmentid int IDENTITY,
    department_name varchar(20) not null,
    department_location varchar(10) not null,
    department_description varchar(100) not null,
    constraint pk_departmentid primary key(departmentid)
)
