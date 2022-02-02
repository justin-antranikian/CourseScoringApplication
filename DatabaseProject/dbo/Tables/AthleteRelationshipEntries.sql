CREATE TABLE [dbo].[AthleteRelationshipEntries]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AthleteFromId] INT NOT NULL, 
	[AthleteToId] INT NOT NULL, 
    [AthleteRelationshipType] INT NOT NULL, 
    CONSTRAINT [FK_AthleteRelationshipEntries_Athletes] FOREIGN KEY ([AthleteFromId]) REFERENCES [Athletes]([Id]),
)
