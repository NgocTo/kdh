CREATE TABLE [dbo].[TestTypes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Category] VARCHAR(20) NOT NULL, 
    [TestItem] VARCHAR(20) NOT NULL, 
    [MaxReference] FLOAT NOT NULL, 
    [MinReference] FLOAT NOT NULL, 
    [Unit] VARCHAR(10) NOT NULL, 
    CONSTRAINT [AK_TestTypes_TestItem] UNIQUE ([TestItem])
)
