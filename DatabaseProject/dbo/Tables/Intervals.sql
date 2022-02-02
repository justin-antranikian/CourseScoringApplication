CREATE TABLE [dbo].[Intervals]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CourseId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Distance] FLOAT NOT NULL, 
    [DistanceFromStart] FLOAT NOT NULL, 
    [Order] INT NOT NULL, 
    [IsFullCourse] BIT NOT NULL, 
    [PaceType] INT NOT NULL, 
    [IntervalType] INT NOT NULL, 
    [Description] NVARCHAR(500) NULL, 
    CONSTRAINT [FK_Intervals_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id])
)
