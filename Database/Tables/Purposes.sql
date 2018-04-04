CREATE TABLE Purposes
(
	PurposeId INT CONSTRAINT Purposes_PurposeId_pk PRIMARY KEY IDENTITY (201, 1) NOT NULL ,
	PurposeToCreate VARCHAR(900) CONSTRAINT Purposes_PurposeToCreate_nn NOT NULL
	
);