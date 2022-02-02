CREATE TABLE [dbo].[TagReads]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CourseId] INT NOT NULL, 
    [AthleteCourseId] INT NOT NULL, 
    [IntervalId] INT NOT NULL, 
    [TimeOnInterval] INT NOT NULL, 
    [TimeOnCourse] INT NOT NULL, 
    CONSTRAINT [FK_TagReads_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id]), 
    CONSTRAINT [FK_TagReads_Intervals] FOREIGN KEY ([IntervalId]) REFERENCES [Intervals]([Id]), 
    CONSTRAINT [FK_TagReads_AthleteCourses] FOREIGN KEY ([AthleteCourseId]) REFERENCES [AthleteCourses]([Id])
)
