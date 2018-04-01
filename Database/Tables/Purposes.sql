CREATE TABLE Purposes
(
	PurposeId VARCHAR(10) CONSTRAINT Purposes_PurposeId_pk PRIMARY KEY ,
	PurposeToCreate VARCHAR(900) CONSTRAINT Purposes_PurposeToCreate_nn NOT NULL
	
);