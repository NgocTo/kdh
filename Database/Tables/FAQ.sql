CREATE TABLE FAQ
(
	QueId INT CONSTRAINT Faq_QueId_pk PRIMARY KEY IDENTITY(101,1),
	Question VARCHAR(MAX) CONSTRAINT Faq_Question_nn NOT NULL
						 CONSTRAINT Faq_Question_uk UNIQUE,
	Answer VARCHAR(MAX),
	DateCreated DateTime CONSTRAINT FAQ_DateCreated_df DEFAULT (getdate())
						CONSTRAINT Faq_DateCreated_nn NOT NULL,
	AuthorFirstName VARCHAR(50) CONSTRAINT Faq_AuthorFirstName_nn NOT NULL,
	AuthorityFirstName VARCHAR(50) CONSTRAINT Faq_AuthorityFirstName_nn NOT NULL,
	PurposeId VARCHAR(10) CONSTRAINT FAQ_PurposeId_nn NOT NULL,
	CONSTRAINT [faq_AuthorFirstName_ck] CHECK (NOT [AuthorFirstName] like '%[^A-Za-z]%'),
    CONSTRAINT [faq_AuthorityFirstName_ck] CHECK (NOT [AuthorityFirstName] like '%[^A-Za-z]%'),
	CONSTRAINT FAQ_PurposeId_fk FOREIGN KEY (PurposeId) REFERENCES Purpose(PurposeId)
);