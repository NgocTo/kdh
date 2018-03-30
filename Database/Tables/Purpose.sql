CREATE TABLE Purpose
(
	PurposeId VARCHAR(10) CONSTRAINT Purpose_PurposeId_pk PRIMARY KEY ,
	PurposeToCreate VARCHAR(MAX) CONSTRAINT Purpose_PurposeToCreate_nn NOT NULL
	
);