using Api.DataModels;
using Api.Orchestration.GenerateData;

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
                CityRank = 1,
                Rating = 0
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
        Interval CreateInterval(string name, double distance, double cumulativeDistance, int order, bool isFullCourse, PaceType paceType, IntervalType intervalType)
        {
            return new Interval
            {
                Distance = distance,
                DistanceFromStart = cumulativeDistance,
                IntervalType = intervalType,
                IsFullCourse = isFullCourse,
                Name = name,
                Order = order,
                PaceType = paceType
            };
        }

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
                    Bracket.Create(0, "Overall", BracketType.Overall), // Id : 1
                    Bracket.Create(0, "Female", BracketType.Gender),
                    Bracket.Create(0, "Male", BracketType.Gender),
                    Bracket.Create(0, "M20-30", BracketType.PrimaryDivision),
                    Bracket.Create(0, "F20-30", BracketType.PrimaryDivision),
                    Bracket.Create(0, "Florida 20-30", BracketType.NonPrimaryDivision),
                    Bracket.Create(0, "Elite", BracketType.NonPrimaryDivision)
                },
                Intervals = new()
                {
                    CreateInterval("Swim", 1000, 1000, 1, false, PaceType.MinutePer100Meters, IntervalType.Swim), // Id : 1
                    CreateInterval("Transition 1", 0, 1000, 2, false, PaceType.None, IntervalType.Transition),
                    CreateInterval("Bike", 1000, 2000, 3, false, PaceType.MilesOrKilometersPerHour, IntervalType.Bike),
                    CreateInterval("Transition 2", 0, 2000, 4, false, PaceType.None, IntervalType.Transition),
                    CreateInterval("Run", 1000, 3000, 5, false, PaceType.MinuteMileOrKilometer, IntervalType.Run),
                    CreateInterval("Full Course", 3000, 3000, 6, true, PaceType.None, IntervalType.FullCourse),
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
                    Bracket.Create(0, "Overall", BracketType.Overall), // Id : 8
                    Bracket.Create(0, "Gender", BracketType.Gender),
                    Bracket.Create(0, "M20-30", BracketType.PrimaryDivision),
                    Bracket.Create(0, "Florida 20-30", BracketType.NonPrimaryDivision),
                    Bracket.Create(0, "Elite", BracketType.NonPrimaryDivision)
                },
                Intervals = new()
                {
                    CreateInterval("Swim", 1000, 1000, 1, false, PaceType.MinutePer100Meters, IntervalType.Swim), // Id : 7
                    CreateInterval("Transition 1", 0, 1000, 2, false, PaceType.None, IntervalType.Transition),
                    CreateInterval("Bike", 1000, 2000, 3, false, PaceType.MilesOrKilometersPerHour, IntervalType.Bike),
                    CreateInterval("Transition 2", 0, 2000, 4, false, PaceType.None, IntervalType.Transition),
                    CreateInterval("Run", 1000, 3000, 5, false, PaceType.MinuteMileOrKilometer, IntervalType.Run),
                    CreateInterval("Full Course", 3000, 3000, 6, true, PaceType.None, IntervalType.FullCourse),
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
                    Bracket.Create(0, "Overall", BracketType.Overall), // Id : 13
                    Bracket.Create(0 ,"Gender", BracketType.Gender),
                    Bracket.Create(0, "Primary", BracketType.PrimaryDivision),
                },
                Intervals = new List<Interval>
                {
                    CreateInterval("Swim", 1000, 1000, 1, false, PaceType.MinutePer100Meters, IntervalType.Swim), // Id : 13
                    CreateInterval("Transition 1", 0, 1000, 2, false, PaceType.None, IntervalType.Transition),
                    CreateInterval("Run", 1000, 2000, 3, false, PaceType.MinuteMileOrKilometer, IntervalType.Run),
                    CreateInterval("Full Course", 2000, 2000, 4, true, PaceType.None, IntervalType.FullCourse),
                },
            }
        };
    }

    internal static List<TagRead> GetTagReads()
    {
        return new()
        {
            TagRead.Create(1, 1, 1, 700, 700),
            TagRead.Create(1, 1, 2, 700, 1400),
            TagRead.Create(4, 1, 1, 701, 701),
            TagRead.Create(4, 1, 2, 701, 1402),
            TagRead.Create(7, 1, 1, 702, 702),
            TagRead.Create(7, 1, 2, 702, 1404),
            TagRead.Create(10, 1, 1, 703, 703),
            TagRead.Create(10, 1, 2, 703, 1406),
            TagRead.Create(10, 1, 3, 703, 2109),
            TagRead.Create(2, 2, 7, 700, 700),
            TagRead.Create(5, 2, 7, 701, 701),
            TagRead.Create(8, 2, 7, 702, 702),
            TagRead.Create(11, 2, 7, 703, 703),
            TagRead.Create(3, 3, 13, 700, 700),
            TagRead.Create(3, 3, 14, 700, 1400),
            TagRead.Create(6, 3, 13, 699, 699),
            TagRead.Create(6, 3, 14, 703, 1402),
            TagRead.Create(6, 3, 15, 701, 2103),
            TagRead.Create(6, 3, 16, 701, 2804),
        };
    }

    internal static List<AthleteCourseBracket> GetAthleteCourseBrackets()
    {
        return new ()
        {
            AthleteCourseBracket.Create(1, 1, 1),
            AthleteCourseBracket.Create(1, 2, 1),
            AthleteCourseBracket.Create(1, 4, 1),
            AthleteCourseBracket.Create(1, 6, 1),
            AthleteCourseBracket.Create(4, 1, 1),
            AthleteCourseBracket.Create(4, 2, 1),
            AthleteCourseBracket.Create(4, 4, 1),
            AthleteCourseBracket.Create(4, 7, 1),
            AthleteCourseBracket.Create(7, 1, 1),
            AthleteCourseBracket.Create(7, 3, 1),
            AthleteCourseBracket.Create(7, 5, 1),
            AthleteCourseBracket.Create(7, 6, 1),
            AthleteCourseBracket.Create(10, 1, 1),
            AthleteCourseBracket.Create(10, 3, 1),
            AthleteCourseBracket.Create(10, 5, 1),
            AthleteCourseBracket.Create(10, 7, 1),
            AthleteCourseBracket.Create(2, 8, 2),
            AthleteCourseBracket.Create(2, 9, 2),
            AthleteCourseBracket.Create(2, 10, 2),
            AthleteCourseBracket.Create(5, 8, 2),
            AthleteCourseBracket.Create(5, 9, 2),
            AthleteCourseBracket.Create(5, 10, 2),
            AthleteCourseBracket.Create(8, 8, 2),
            AthleteCourseBracket.Create(8, 9, 2),
            AthleteCourseBracket.Create(8, 10, 2),
            AthleteCourseBracket.Create(11, 8, 2),
            AthleteCourseBracket.Create(11, 9, 2),
            AthleteCourseBracket.Create(11, 10, 2),
            AthleteCourseBracket.Create(3, 13, 3),
            AthleteCourseBracket.Create(3, 14, 3),
            AthleteCourseBracket.Create(3, 15, 3),
            AthleteCourseBracket.Create(6, 13, 3),
            AthleteCourseBracket.Create(6, 14, 3),
            AthleteCourseBracket.Create(6, 15, 3),
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
                    new() { CourseId = 1, Bib = bib, CourseGoalDescription = "", PersonalGoalDescription = "" },
                    new() { CourseId = 2, Bib = bib, CourseGoalDescription = "", PersonalGoalDescription = "" },
                    new() { CourseId = 3, Bib = bib, CourseGoalDescription = "", PersonalGoalDescription = "" },
                },
                AthleteRaceSeriesGoals = new()
                {
                    AthleteRaceSeriesGoal.Create(RaceSeriesType.Triathalon, 15),
                    AthleteRaceSeriesGoal.Create(RaceSeriesType.Running, 1)
                },
                AthleteWellnessEntries = new()
                {
                    AthleteWellnessEntry.Create(AthleteWellnessType.Gear, "G1"),
                    AthleteWellnessEntry.Create(AthleteWellnessType.Gear, "G2"),
                    AthleteWellnessEntry.Create(AthleteWellnessType.Diet, "D1"),
                    AthleteWellnessEntry.Create(AthleteWellnessType.Goal, "G1"),
                    AthleteWellnessEntry.Create(AthleteWellnessType.Motivational, "M1"),
                    AthleteWellnessEntry.Create(AthleteWellnessType.Motivational, "M2"),
                    AthleteWellnessEntry.Create(AthleteWellnessType.Training, "T1"),
                    AthleteWellnessEntry.Create(AthleteWellnessType.Training, "T2"),
                    AthleteWellnessEntry.Create(AthleteWellnessType.Training, "T3"),
                }
            };
        });

        return athletes.ToList();
    }
}
