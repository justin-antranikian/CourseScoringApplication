CREATE TABLE [dbo].[Results]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AthleteCourseId] INT NOT NULL, 
    [CourseId] INT NOT NULL, 
    [IntervalId] INT NOT NULL, 
    [BracketId] INT NOT NULL, 
    [TimeOnInterval] INT NOT NULL, 
    [TimeOnCourse] INT NOT NULL, 
    [Rank] INT NOT NULL, 
    [OverallRank] INT NOT NULL, 
    [GenderRank] INT NOT NULL, 
    [DivisionRank] INT NOT NULL, 
    [IsHighestIntervalCompleted] BIT NOT NULL
)

GO

CREATE INDEX [IX_Results_Rank] ON [dbo].[Results] ([Rank])

GO
