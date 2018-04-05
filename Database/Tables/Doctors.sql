CREATE TABLE [dbo].[Doctors]
(
    Doctorid int IDENTITY,
    Fname varchar(100) not null,
    Lname varchar(100) not null,
    Email varchar(100) not null,
    Address_line1 varchar(50) not null,
    Address_line2 varchar(50),
    Postal_code varchar(7) not null,
    Mobile_no varchar(15) not null,
    Date_of_join datetime default getDate(),
    Departmentid int not null,
    Speciality varchar(20) not null,
    Province varchar(40) not null,
    City varchar(40) not null,
    constraint pk_doctorid primary key(doctorid),
    constraint chk_fname check (Fname not like '%[^A-Z]%'),
    constraint chk_lname check (Lname not like '%[^A-Z]%'),
    constraint chk_email check (Email LIKE '%___@___%'),
    constraint chk_mobile check (Mobile_no not like '%[^0-9]%'),
    constraint chk_province check (Province not like '%[^A-Z ]%'),
    constraint chk_city check (City not like '%[^A-Z ]%'),
    constraint fk_dept foreign key(Departmentid) references Departments(Departmentid)		
)

