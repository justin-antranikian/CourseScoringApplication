CREATE TABLE [dbo].[CourseStatistics]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	CourseId INT NOT NULL,
	BracketId INT NULL,
	IntervalId INT NULL,
	AverageTotalTimeInMilleseconds INT NOT NULL,
	FastestTimeInMilleseconds INT NOT NULL,
	SlowestTimeInMilleseconds INT NOT NULL,
	CONSTRAINT [FK_CourseStatistics_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id]),
)
