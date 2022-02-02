CREATE TABLE [dbo].[Brackets]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CourseId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [BracketType] INT NOT NULL, 
    CONSTRAINT [FK_Brackets_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id])
)
