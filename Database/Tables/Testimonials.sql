CREATE TABLE [dbo].[Testimonials]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Role] VARCHAR(50) NOT NULL, 
    [Subject] VARCHAR(100) NOT NULL, 
    [Content] NVARCHAR(500) NOT NULL, 
    [Rate] INT NOT NULL, 
    [Timestamp] DATETIME2  CONSTRAINT [DF_Testimonials_Timestamp] DEFAULT (getdate()) NULL, 
    [Reviewed] VARCHAR(7) CONSTRAINT [DF_Testimonials_Reviewed] DEFAULT ('NO') NULL, 
    [DepartmentId] INT NOT NULL, 
    CONSTRAINT [CK_Testimonials_Rate] CHECK ((0 < Rate) AND (Rate < 6)), 
    CONSTRAINT [FK_Testimonials_DepartmentId] FOREIGN KEY (DepartmentId) REFERENCES Departments(Departmentid),
)
