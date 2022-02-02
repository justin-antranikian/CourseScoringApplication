CREATE TABLE [dbo].[AthleteCourseTrainings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AthleteCourseId] INT NOT NULL,
	[Description] NVARCHAR(500) NOT NULL, 
    CONSTRAINT [FK_AtheleteCourseTrainings_AtleteCourses] FOREIGN KEY ([AthleteCourseId]) REFERENCES [AthleteCourses]([Id]),
)
