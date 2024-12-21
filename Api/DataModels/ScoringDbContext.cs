using Api.DataModels.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Api.DataModels;

public class ScoringDbContext(DbContextOptions<ScoringDbContext> options) : DbContext(options)
{
    public DbSet<Athlete> Athletes { get; set; }
    public DbSet<AthleteCourseBracket> AtheleteCourseBrackets { get; set; }
    public DbSet<AthleteCourse> AthleteCourses { get; set; }
    public DbSet<AthleteCourseTraining> AthleteCourseTrainings { get; set; }
    public DbSet<AthleteRaceSeriesGoal> AthleteRaceSeriesGoals { get; set; }
    public DbSet<AthleteWellnessEntry> AthleteWellnessEntries { get; set; }
    public DbSet<Bracket> Brackets { get; set; }
    public DbSet<BracketMetadata> BracketMetadataEntries { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseInformationEntry> CourseInformationEntries { get; set; }
    public DbSet<Interval> Intervals { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<RaceSeries> RaceSeries { get; set; }
    public DbSet<Result> Results { get; set; }
    public DbSet<TagRead> TagReads { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        AthleteConfiguration.Configure(modelBuilder.Entity<Athlete>());
        AthleteCourseConfiguration.Configure(modelBuilder.Entity<AthleteCourse>());
        AthleteCourseBracketConfiguration.Configure(modelBuilder.Entity<AthleteCourseBracket>());
        AthleteCourseTrainingConfiguration.Configure(modelBuilder.Entity<AthleteCourseTraining>());
        AthleteRaceSeriesGoalConfiguration.Configure(modelBuilder.Entity<AthleteRaceSeriesGoal>());
        AthleteWellnessEntryConfiguration.Configure(modelBuilder.Entity<AthleteWellnessEntry>());
        BracketConfiguration.Configure(modelBuilder.Entity<Bracket>());
        BracketMetadataConfiguration.Configure(modelBuilder.Entity<BracketMetadata>());
        CourseConfiguration.Configure(modelBuilder.Entity<Course>());
        CourseInformationEntryConfiguration.Configure(modelBuilder.Entity<CourseInformationEntry>());
        IntervalConfiguration.Configure(modelBuilder.Entity<Interval>());
        RaceConfiguration.Configure(modelBuilder.Entity<Race>());
        RaceSeriesConfiguration.Configure(modelBuilder.Entity<RaceSeries>());
        ResultConfiguration.Configure(modelBuilder.Entity<Result>());
        TagReadConfiguration.Configure(modelBuilder.Entity<TagRead>());
    }
}
