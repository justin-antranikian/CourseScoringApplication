using Api.DataModels;
using Api.DataModels.Enums;
using Api.Orchestration.ScoreCourses;

namespace ApiTests.Orchestration;

internal static class TestDataGenerator
{
    internal static List<RaceSeries> GetRaceSeries()
    {
        return new()
        {
            new()
            {
                Name = "Houston Triathalons",
                Description = "All Houston Triathalons",
                RaceSeriesType = RaceSeriesType.Triathalon,
                State = "Colorado",
                Area = "Area 1",
                City = "City 1",
                OverallRank = 4,
                StateRank = 3,
                AreaRank = 2,
                CityRank = 1
            }
        };
    }

    internal static List<Race> GetRaces()
    {
        var baseRace = new Race { RaceSeriesId = 1, Name = "2010 Houston Triathalon", TimeZoneId = "Pacific Standard Time", KickOffDate = new DateTime(2010, 1, 1) };

        return new List<Race>
        {
            baseRace,
            baseRace with { Name = "2011 Houston Triathalon", KickOffDate = new DateTime(2011, 1, 1), Courses = new() },
        };
    }

    internal static List<Course> GetCourses()
    {
        return new List<Course>
        {
            new()
            {
                PaceType = PaceType.None,
                PreferedMetric = PreferredMetric.Imperial,
                Distance = 3000,
                RaceId = 1,
                Name = "Course 1",
                StartDate = new DateTime(2010, 1, 1),
                CourseType = CourseType.HalfIronmanTriathalon,
                SortOrder = 2,
                Brackets = new List<Bracket>
                {
                    new ("Overall", BracketType.Overall), // Id : 1
                    new ("Female", BracketType.Gender),
                    new ("Male", BracketType.Gender),
                    new ("M20-30", BracketType.PrimaryDivision),
                    new ("F20-30", BracketType.PrimaryDivision),
                    new ("Florida 20-30", BracketType.NonPrimaryDivision),
                    new ("Elite", BracketType.NonPrimaryDivision)
                },
                Intervals = new()
                {
                    new ("Swim", 1000, 1000, 1, false, PaceType.MinutePer100Meters, IntervalType.Swim, "Swim D"), // Id : 1
                    new ("Transition 1", 0, 1000, 2, false, PaceType.None, IntervalType.Transition, "Transition 1 D"),
                    new ("Bike", 1000, 2000, 3, false, PaceType.MilesOrKilometersPerHour, IntervalType.Bike, "Bike D"),
                    new ("Transition 2", 0, 2000, 4, false, PaceType.None, IntervalType.Transition, "Transition 2 D"),
                    new ("Run", 1000, 3000, 5, false, PaceType.MinuteMileOrKilometer, IntervalType.Run, "Run D"),
                    new ("Full Course", 3000, 3000, 6, true, PaceType.None, IntervalType.FullCourse, "Full Course D"),
                }
            },
            new()
            {
                PaceType = PaceType.MilesOrKilometersPerHour,
                PreferedMetric = PreferredMetric.Imperial,
                Distance = 3000,
                RaceId = 1,
                Name = "Course 2",
                StartDate = new DateTime(2009, 1, 1),
                CourseType = CourseType.HalfIronmanTriathalon,
                SortOrder = 1,
                Brackets = new()
                {
                    new ("Overall", BracketType.Overall), // Id : 8
                    new ("Gender", BracketType.Gender),
                    new ("M20-30", BracketType.PrimaryDivision),
                    new ("Florida 20-30", BracketType.NonPrimaryDivision),
                    new ("Elite", BracketType.NonPrimaryDivision)
                },
                Intervals = new()
                {
                    new ("Swim", 1000, 1000, 1, false, PaceType.MinutePer100Meters, IntervalType.Swim, null), // Id : 7
                    new ("Transition 1", 0, 1000, 2, false, PaceType.None, IntervalType.Transition, null),
                    new ("Bike", 1000, 2000, 3, false, PaceType.MilesOrKilometersPerHour, IntervalType.Bike, null),
                    new ("Transition 2", 0, 2000, 4, false, PaceType.None, IntervalType.Transition, null),
                    new ("Run", 1000, 3000, 5, false, PaceType.MinuteMileOrKilometer, IntervalType.Run, null),
                    new ("Full Course", 3000, 3000, 6, true, PaceType.None, IntervalType.FullCourse, null),
                }
            },
            new()
            {
                PaceType = PaceType.MilesOrKilometersPerHour,
                PreferedMetric = PreferredMetric.Imperial,
                Distance = 2000,
                RaceId = 2,
                Name = "Course 3",
                StartDate = new DateTime(2011, 1, 1),
                CourseType = CourseType.HalfIronmanTriathalon,
                SortOrder = 1,
                Brackets = new List<Bracket>
                {
                    new("Overall", BracketType.Overall), // Id : 13
                    new("Gender", BracketType.Gender),
                    new("Primary", BracketType.PrimaryDivision),
                },
                Intervals = new List<Interval>
                {
                    new("Swim", 1000, 1000, 1, false, PaceType.MinutePer100Meters, IntervalType.Swim, null), // Id : 13
                    new("Transition 1", 0, 1000, 2, false, PaceType.None, IntervalType.Transition, null),
                    new("Run", 1000, 2000, 3, false, PaceType.MinuteMileOrKilometer, IntervalType.Run, null),
                    new("Full Course", 2000, 2000, 4, true, PaceType.None, IntervalType.FullCourse, null),
                },
            }
        };
    }

    internal static List<TagRead> GetTagReads()
    {
        return new()
        {
            new (1, 1, 1, 700, 700),
            new (1, 1, 2, 700, 1400),
            new (1, 4, 1, 701, 701),
            new (1, 4, 2, 701, 1402),
            new (1, 7, 1, 702, 702),
            new (1, 7, 2, 702, 1404),
            new (1, 10, 1, 703, 703),
            new (1, 10, 2, 703, 1406),
            new (1, 10, 3, 703, 2109),
            new (2, 2, 7, 700, 700),
            new (2, 5, 7, 701, 701),
            new (2, 8, 7, 702, 702),
            new (2, 11, 7, 703, 703),
            new (3, 3, 13, 700, 700),
            new (3, 3, 14, 700, 1400),
            new (3, 6, 13, 699, 699),
            new (3, 6, 14, 703, 1402),
            new (3, 6, 15, 701, 2103),
            new (3, 6, 16, 701, 2804),
        };
    }

    internal static List<AthleteCourseBracket> GetAthleteCourseBrackets()
    {
        return new ()
        {
            new (1, 1, 1),
            new (1, 1, 2),
            new (1, 1, 4),
            new (1, 1, 6),
            new (4, 1, 1),
            new (4, 1, 2),
            new (4, 1, 4),
            new (4, 1, 7),
            new (7, 1, 1),
            new (7, 1, 3),
            new (7, 1, 5),
            new (7, 1, 6),
            new (10, 1, 1),
            new (10, 1, 3),
            new (10, 1, 5),
            new (10, 1, 7),
            new (2, 2, 8),
            new (2, 2, 9),
            new (2, 2, 10),
            new (5, 2, 8),
            new (5, 2, 9),
            new (5, 2, 10),
            new (8, 2, 8),
            new (8, 2, 9),
            new (8, 2, 10),
            new (11, 2, 8),
            new (11, 2, 9),
            new (11, 2, 10),
            new (3, 3, 13),
            new (3, 3, 14),
            new (3, 3, 15),
            new (6, 3, 13),
            new (6, 3, 14),
            new (6, 3, 15),
        };
    }

    internal static (List<BracketMetadata> metadataEntries, List<Result> results) GetScoringResults(ScoringDbContext dbContext, params int[] courseIds)
    {
        var allCourseBrackets = GetAthleteCourseBrackets();
        var allReads = GetTagReads();

        var scoringResults = courseIds.Select(courseId =>
        {
            var reads = allReads.Where(oo => oo.CourseId == courseId).ToList(); ;
            var athleteCourseBrackets = allCourseBrackets.Where(oo => oo.CourseId == courseId).ToList();
            var course = dbContext.Courses.Single(oo => oo.Id == courseId);
            var scorer = new CourseScorer(course, course.Brackets.ToList(), reads, athleteCourseBrackets, course.Intervals.ToList());
            var scoringResult = scorer.GetScoringResult();
            return scoringResult;
        });

        var metadataForAllCourses = scoringResults.SelectMany(oo => oo.MetadataResults).ToList();
        var resultsForAllCourses = scoringResults.SelectMany(oo => oo.Results).ToList();

        return (metadataForAllCourses, resultsForAllCourses);
    }

    internal static List<Athlete> GetAthletes()
    {
        var athletes = (new[] { "A", "B", "C", "D" }).Select(oo =>
        {
            var bib = $"B{oo}";
            return new Athlete
            {
                FirstName = $"F{oo}",
                LastName = $"L{oo}",
                FullName = $"F{oo} L{oo}",
                Gender = Gender.Femail,
                State = $"S{oo}",
                Area = $"A{oo}",
                City = $"C{oo}",
                OverallRank = 4,
                StateRank = 3,
                AreaRank = 2,
                CityRank = 1,
                DateOfBirth = new DateTime(2000, 1, 1),
                AthleteCourses = new()
                {
                    new() { CourseId = 1, Bib = bib },
                    new() { CourseId = 2, Bib = bib },
                    new() { CourseId = 3, Bib = bib },
                },
                AthleteRaceSeriesGoals = new()
                {
                    new (RaceSeriesType.Triathalon, 15),
                    new (RaceSeriesType.Running, 1)
                },
                AthleteWellnessEntries = new()
                {
                    new (AthleteWellnessType.Gear, "G1"),
                    new (AthleteWellnessType.Gear, "G2"),
                    new (AthleteWellnessType.Diet, "D1"),
                    new (AthleteWellnessType.Goal, "G1"),
                    new (AthleteWellnessType.Motivational, "M1"),
                    new (AthleteWellnessType.Motivational, "M2"),
                    new (AthleteWellnessType.Training, "T1"),
                    new (AthleteWellnessType.Training, "T2"),
                    new (AthleteWellnessType.Training, "T3"),
                }
            };
        });

        return athletes.ToList();
    }
}
