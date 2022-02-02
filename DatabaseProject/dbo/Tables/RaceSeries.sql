CREATE TABLE [dbo].[RaceSeries]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [RaceSeriesType] INT NOT NULL, 
    [State] NVARCHAR(50) NOT NULL  , 
    [Area] NVARCHAR(50) NOT NULL , 
    [City] NVARCHAR(50) NOT NULL , 
    [OverallRank] INT NOT NULL,
    [StateRank] INT NOT NULL,
    [AreaRank] INT NOT NULL,
    [CityRank] INT NOT NULL, 
    [IsUpcoming] BIT NOT NULL, 
    [Rating] INT NOT NULL,
)
