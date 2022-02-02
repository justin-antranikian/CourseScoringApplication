CREATE TABLE [dbo].[CourseTypeStatistics]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	CourseType INT NOT NULL,
	AthleteId INT NULL,
	AverageTotalTimeInMilleseconds INT NOT NULL,
	FastestTimeInMilleseconds INT NOT NULL,
	SlowestTimeInMilleseconds INT NOT NULL,
	CONSTRAINT [FK_CourseTypeStatistics_Athletes] FOREIGN KEY ([AthleteId]) REFERENCES [Athletes]([Id]),
)
