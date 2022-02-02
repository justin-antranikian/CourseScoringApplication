CREATE TABLE [dbo].[AthleteWellnessEntries]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AthleteId] INT NOT NULL, 
    [AthleteWellnessType] INT NOT NULL, 
    [Description] NVARCHAR(250) NOT NULL,
    CONSTRAINT [FK_AthleteWellnessEntries_Athletes] FOREIGN KEY ([AthleteId]) REFERENCES [Athletes]([Id]),
)
