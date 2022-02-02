CREATE TABLE [dbo].[Courses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [RaceId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [SortOrder] INT NOT NULL, 
    [PaceType] INT NOT NULL, 
    [PreferedMetric] INT NOT NULL, 
    [Distance] FLOAT NOT NULL, 
    [StartDate] DateTime NOT NULL, 
    [CourseType] INT NOT NULL, 
    CONSTRAINT [FK_Courses_Races] FOREIGN KEY (RaceId) REFERENCES [Races]([Id])
)
