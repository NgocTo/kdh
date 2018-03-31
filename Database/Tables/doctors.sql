CREATE TABLE [dbo].[doctors]
(
	doctorid int IDENTITY,
    fname varchar(100) not null,
    lname varchar(100) not null,
    email varchar(100) not null,
    address_line1 varchar(50) not null,
    address_line2 varchar(50) not null,
    postal_code varchar(7) not null,
    mobile_no varchar(15) not null,
    date_of_join datetime default getDate(),
    departmentid int not null,
    speciality varchar(20) not null,
    province varchar(40) not null,
    city varchar(40) not null,
    constraint pk_doctorid primary key(doctorid),
    constraint chk_fname check (fname not like '%[^A-Z]%'),
    constraint chk_lname check (fname not like '%[^A-Z]%'),
    constraint chk_email check (email LIKE '%___@___%'),
    constraint chk_mobile check (mobile_no not like '%[^0-9]%'),
    constraint chk_province check (province not like '%[^A-Z ]%'),
    constraint chk_city check (city not like '%[^A-Z ]%')
)
