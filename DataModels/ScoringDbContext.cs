using Microsoft.EntityFrameworkCore;

namespace DataModels;

public class ScoringDbContext : DbContext
{
    public ScoringDbContext(DbContextOptions<ScoringDbContext> options) : base(options) { }

    public DbSet<Athlete> Athletes { get; set; }

    public DbSet<AthleteCourse> AthleteCourses { get; set; }

    public DbSet<AthleteCourseBracket> AtheleteCourseBrackets { get; set; }

    public DbSet<BracketMetadata> BracketMetadataEntries { get; set; }

    public DbSet<Bracket> Brackets { get; set; }

    public DbSet<Course> Courses { get; set; }

    public DbSet<CourseStatistic> CourseStatistics { get; set; }

    public DbSet<CourseTypeStatistic> CourseTypeStatistics { get; set; }

    public DbSet<Interval> Intervals { get; set; }

    public DbSet<Race> Races { get; set; }

    public DbSet<RaceSeries> RaceSeries { get; set; }

    public DbSet<Result> Results { get; set; }

    public DbSet<TagRead> TagReads { get; set; }
}
