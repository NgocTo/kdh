CREATE TABLE DonateCreditCard
(
	CreditId int CONSTRAINT DonateCreditCard_CreditId_pk PRIMARY KEY IDENTITY(300,1),
	CardNumber CHAR(16) CONSTRAINT DonateCreditCard_CardNumber_nn NOT NULL
						CONSTRAINT DonateCreditCard_CardNumber_ck CHECK(not CardNumber LIKE '%[^0-9]%'),
	CardHolderName VARCHAR(50) CONSTRAINT CreditCardInfo_CardHolderName_nn NOT NULL
						CONSTRAINT DonateCreditCard_CardHolderName_ck CHECK(NOT CardHolderName LIKE '%[^A-Za-z ]%'),
	ExpiryDate VARCHAR(15) CONSTRAINT DonateCreditCard_ExpiryDate_nn NOT NULL,
	SecurityCode CHAR(3) CONSTRAINT DonateCreditCard_SecurityCode_nn NOT NULL
						CONSTRAINT DonateCreditCard_SecurityCode_ck CHECK(not SecurityCode LIKE '%[^0-9]%')
	
);