CREATE TABLE [dbo].[AthleteCourseBrackets]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AthleteCourseId] INT NOT NULL, 
    [CourseId] INT NOT NULL, 
    [BracketId] INT NOT NULL, 
    CONSTRAINT [FK_AtheleteCourseBrackets_AtleteCourses] FOREIGN KEY ([AthleteCourseId]) REFERENCES [AthleteCourses]([Id]), 
    CONSTRAINT [FK_AtheleteCourseBrackets_Brackets] FOREIGN KEY ([BracketId]) REFERENCES [Brackets]([Id])
)
