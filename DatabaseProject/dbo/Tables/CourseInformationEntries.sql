CREATE TABLE [dbo].[CourseInformationEntries]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	CourseId INT NOT NULL,
	[CourseInformationType] INT NOT NULL, 
    [Description] NVARCHAR(250) NOT NULL, 
    CONSTRAINT [FK_CourseInformationEntries_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id]),
)
