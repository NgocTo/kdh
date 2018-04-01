CREATE TABLE Faqs
(
	QueId INT CONSTRAINT Faqs_QueId_pk PRIMARY KEY IDENTITY(101,1),
	Question VARCHAR(900) CONSTRAINT Faqs_Question_nn NOT NULL
						 CONSTRAINT Faqs_Question_uk UNIQUE,
	Answer VARCHAR(900),
	DateCreated DateTime CONSTRAINT Faqs_DateCreated_df DEFAULT (getdate())
						CONSTRAINT Faqs_DateCreated_nn NOT NULL,
	AuthorFirstName VARCHAR(50) CONSTRAINT Faqs_AuthorFirstName_nn NOT NULL,
	AuthorityFirstName VARCHAR(50) CONSTRAINT Faqs_AuthorityFirstName_nn NOT NULL,
	PurposeId VARCHAR(10) CONSTRAINT Faqs_PurposeId_nn NOT NULL,
	CONSTRAINT [Faqs_AuthorFirstName_ck] CHECK (NOT [AuthorFirstName] like '%[^A-Za-z]%'),
    CONSTRAINT [Faqs_AuthorityFirstName_ck] CHECK (NOT [AuthorityFirstName] like '%[^A-Za-z]%'),
	CONSTRAINT Faqs_PurposeId_fk FOREIGN KEY (PurposeId) REFERENCES Purposes(PurposeId)
);