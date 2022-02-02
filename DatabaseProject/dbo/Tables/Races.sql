CREATE TABLE [dbo].[Races]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(100) NOT NULL, 
    [RaceSeriesId] INT NOT NULL, 
    [KickOffDate] DATETIME NOT NULL, 
    [TimeZoneId] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Races_RaceSeries] FOREIGN KEY ([RaceSeriesId]) REFERENCES [RaceSeries]([Id])
)
