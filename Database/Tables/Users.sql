CREATE TABLE [dbo].[Users]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(), 
    [Email] VARCHAR(100) NOT NULL, 
    [Password] CHAR(64) NULL, 
    [Role] VARCHAR(7) NOT NULL, 
    CONSTRAINT [AK_Users_Email] UNIQUE ([Email]), 
    CONSTRAINT [CK_Users_Role] CHECK ([Role] IN ('admin', 'patient', 'hr')),
	CONSTRAINT [CK_Users_Email] CHECK (Email like '%_@__%.__%' AND Email not like '@%')
)
