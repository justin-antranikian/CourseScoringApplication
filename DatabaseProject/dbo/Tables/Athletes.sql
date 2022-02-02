CREATE TABLE [dbo].[Athletes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [DateOfBirth] DATE NOT NULL, 
    [State] NVARCHAR(50) NOT NULL,
    [Area] NVARCHAR(50) NOT NULL,
    [City] NVARCHAR(50) NOT NULL, 
    [Gender] INT NOT NULL,
    [OverallRank] INT NOT NULL,
    [StateRank] INT NOT NULL,
    [AreaRank] INT NOT NULL,
    [CityRank] INT NOT NULL,
    [FullName] NVARCHAR(100) NOT NULL,
)
