CREATE TABLE [dbo].[BracketMetaDatas]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CourseId] INT NOT NULL,
    [BracketId] INT NOT NULL, 
    [TotalRacers] INT NOT NULL, 
    [IntervalId] INT NULL, 
    CONSTRAINT [FK_BracketMetaDatas_Brackets] FOREIGN KEY ([BracketId]) REFERENCES [Brackets]([Id]) 
)
