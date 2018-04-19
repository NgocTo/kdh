CREATE TABLE [dbo].[Testimonials]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] NCHAR(50) NOT NULL, 
    [Role] NCHAR(50) NOT NULL, 
    [Subject] NCHAR(100) NOT NULL, 
    [Content] NCHAR(500) NOT NULL, 
    [Rate] INT NULL, 
    [Timestamp] DATETIME  CONSTRAINT [DF_Testimonials_Timestamp] DEFAULT (getdate())  NOT NULL, 
    [Reviewed] NCHAR(7) CONSTRAINT [DF_Testimonials_Reviewed] DEFAULT ('NO') NOT NULL, 
    [DepartmentId] INT NOT NULL, 
    CONSTRAINT [CK_Testimonials_Rate] CHECK ((0 < Rate) AND (Rate < 6)), 
    CONSTRAINT [CK_Testimonials_Reviewed] CHECK (Reviewed LIKE 'YES' OR Reviewed LIKE 'NO' OR Reviewed LIKE 'PENDING'), 
    CONSTRAINT [FK_Testimonials_DepartmentId] FOREIGN KEY (DepartmentId) REFERENCES Departments(Departmentid),
)
