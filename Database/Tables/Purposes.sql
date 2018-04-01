CREATE TABLE Purposes
(
	PurposeId VARCHAR(10) CONSTRAINT Purposes_PurposeId_pk PRIMARY KEY ,
	PurposeToCreate VARCHAR(MAX) CONSTRAINT Purposes_PurposeToCreate_nn NOT NULL
	
);