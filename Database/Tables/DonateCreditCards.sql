CREATE TABLE DonateCreditCards
(
	CreditId int CONSTRAINT DonateCreditCards_CreditId_pk PRIMARY KEY IDENTITY(300,1),
	CardNumber CHAR(16) CONSTRAINT DonateCreditCards_CardNumber_nn NOT NULL
						CONSTRAINT DonateCreditCards_CardNumber_ck CHECK(not CardNumber LIKE '%[^0-9]%'),
	CardHolderName VARCHAR(50) CONSTRAINT CreditCardInfo_CardHolderName_nn NOT NULL
						CONSTRAINT DonateCreditCards_CardHolderName_ck CHECK(NOT CardHolderName LIKE '%[^A-Za-z ]%'),
	ExpiryDate VARCHAR(15) CONSTRAINT DonateCreditCards_ExpiryDate_nn NOT NULL,
	SecurityCode CHAR(3) CONSTRAINT DonateCreditCards_SecurityCode_nn NOT NULL
						CONSTRAINT DonateCreditCards_SecurityCode_ck CHECK(not SecurityCode LIKE '%[^0-9]%')
	
);