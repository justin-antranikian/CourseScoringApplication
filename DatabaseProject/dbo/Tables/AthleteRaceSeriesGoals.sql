CREATE TABLE [dbo].[AthleteRaceSeriesGoals]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[AthleteId] INT NOT NULL,
	[RaceSeriesType] INT NOT NULL,
    [TotalEvents] INT NOT NULL, 
    CONSTRAINT [FK_AthleteRaceSeriesGoals_Athletes] FOREIGN KEY ([AthleteId]) REFERENCES [Athletes]([Id]),
)
