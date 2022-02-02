CREATE TABLE [dbo].[AthleteCourses]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AthleteId] INT NOT NULL, 
    [CourseId] INT NOT NULL, 
    [Bib] NVARCHAR(20) NOT NULL, 
    [CourseGoalDescription] NVARCHAR(500) NULL, 
    [PersonalGoalDescription] NVARCHAR(500) NULL, 
    CONSTRAINT [FK_AthleteCourses_Athletes] FOREIGN KEY ([AthleteId]) REFERENCES [Athletes]([Id]), 
    CONSTRAINT [FK_AthleteCourses_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id])
)

GO

CREATE INDEX [IX_AthleteCourses_AthleteId] ON [dbo].[AthleteCourses] ([AthleteId])
