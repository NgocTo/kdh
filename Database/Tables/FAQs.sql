CREATE TABLE FAQs
(
	QueId INT CONSTRAINT FAQs_QueId_pk PRIMARY KEY IDENTITY(101,1),
	Question VARCHAR(900) CONSTRAINT FAQs_Question_nn NOT NULL
						 CONSTRAINT FAQs_Question_uk UNIQUE,
	Answer VARCHAR(900),
	DateCreated DateTime CONSTRAINT FAQs_DateCreated_df DEFAULT (getdate())
						CONSTRAINT FAQs_DateCreated_nn NOT NULL,
	AuthorFirstName VARCHAR(50) CONSTRAINT FAQs_AuthorFirstName_nn NOT NULL,
	AuthorityFirstName VARCHAR(50) CONSTRAINT FAQs_AuthorityFirstName_nn NOT NULL,
	PurposeId INT CONSTRAINT FAQs_PurposeId_nn NOT NULL,
	
	CONSTRAINT [FAQs_AuthorFirstName_ck] CHECK (NOT [AuthorFirstName] like '%[^A-Za-z]%'),
    CONSTRAINT [FAQAs_AuthorityFirstName_ck] CHECK (NOT [AuthorityFirstName] like '%[^A-Za-z]%'),
	CONSTRAINT FAQs_PurposeId_fk FOREIGN KEY (PurposeId) REFERENCES Purposes(PurposeId)
);