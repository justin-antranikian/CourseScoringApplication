﻿// <auto-generated />
using System;
using DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace DataModels.Migrations
{
    [DbContext(typeof(ScoringDbContext))]
    partial class ScoringDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataModels.Athlete", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("AreaRank")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("CityRank")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("OverallRank")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("StateRank")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Athletes", (string)null);
                });

            modelBuilder.Entity("DataModels.AthleteCourse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<string>("Bib")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("CourseGoalDescription")
                        .HasColumnType("VARCHAR(500)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("PersonalGoalDescription")
                        .HasColumnType("VARCHAR(500)");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId");

                    b.HasIndex("CourseId");

                    b.ToTable("AthleteCourses", (string)null);
                });

            modelBuilder.Entity("DataModels.AthleteCourseBracket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteCourseId")
                        .HasColumnType("int");

                    b.Property<int>("BracketId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AthleteCourseId");

                    b.HasIndex("BracketId");

                    b.HasIndex("CourseId");

                    b.ToTable("AthleteCourseBrackets", (string)null);
                });

            modelBuilder.Entity("DataModels.AthleteCourseTraining", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteCourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("AthleteCourseId");

                    b.ToTable("AthleteCourseTrainings", (string)null);
                });

            modelBuilder.Entity("DataModels.AthleteRaceSeriesGoal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<string>("RaceSeriesType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("TotalEvents")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId");

                    b.ToTable("AthleteRaceSeriesGoals", (string)null);
                });

            modelBuilder.Entity("DataModels.AthleteWellnessEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<string>("AthleteWellnessType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(200)");

                    b.HasKey("Id");

                    b.HasIndex("AthleteId");

                    b.ToTable("AthleteWellnessEntries", (string)null);
                });

            modelBuilder.Entity("DataModels.Bracket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BracketType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Brackets", (string)null);
                });

            modelBuilder.Entity("DataModels.BracketMetadata", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BracketId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int?>("IntervalId")
                        .HasColumnType("int");

                    b.Property<int>("TotalRacers")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BracketId");

                    b.HasIndex("CourseId");

                    b.HasIndex("IntervalId");

                    b.ToTable("BracketMetadatas", (string)null);
                });

            modelBuilder.Entity("DataModels.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<Geometry>("Location")
                        .HasColumnType("geometry");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<string>("PaceType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("PreferedMetric")
                        .IsRequired()
                        .HasColumnType("VARCHAR(25)");

                    b.Property<int>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RaceId");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("DataModels.CourseInformationEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CourseInformationType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseInformationEntries", (string)null);
                });

            modelBuilder.Entity("DataModels.Interval", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(250)");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<double>("DistanceFromStart")
                        .HasColumnType("float");

                    b.Property<string>("IntervalType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<bool>("IsFullCourse")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<string>("PaceType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Intervals", (string)null);
                });

            modelBuilder.Entity("DataModels.Race", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("KickOffDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("RaceSeriesId")
                        .HasColumnType("int");

                    b.Property<string>("TimeZoneId")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.HasKey("Id");

                    b.HasIndex("RaceSeriesId");

                    b.ToTable("Races", (string)null);
                });

            modelBuilder.Entity("DataModels.RaceSeries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Area")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("AreaRank")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("CityRank")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)");

                    b.Property<bool>("IsUpcoming")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)");

                    b.Property<int>("OverallRank")
                        .HasColumnType("int");

                    b.Property<string>("RaceSeriesType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)");

                    b.Property<int>("StateRank")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RaceSeries", (string)null);
                });

            modelBuilder.Entity("DataModels.Result", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteCourseId")
                        .HasColumnType("int");

                    b.Property<int>("BracketId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("DivisionRank")
                        .HasColumnType("int");

                    b.Property<int>("GenderRank")
                        .HasColumnType("int");

                    b.Property<int>("IntervalId")
                        .HasColumnType("int");

                    b.Property<bool>("IsHighestIntervalCompleted")
                        .HasColumnType("bit");

                    b.Property<int>("OverallRank")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<int>("TimeOnCourse")
                        .HasColumnType("int");

                    b.Property<int>("TimeOnInterval")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AthleteCourseId");

                    b.HasIndex("BracketId");

                    b.HasIndex("CourseId");

                    b.HasIndex("IntervalId");

                    b.ToTable("Results", (string)null);
                });

            modelBuilder.Entity("DataModels.TagRead", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AthleteCourseId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("IntervalId")
                        .HasColumnType("int");

                    b.Property<int>("TimeOnCourse")
                        .HasColumnType("int");

                    b.Property<int>("TimeOnInterval")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AthleteCourseId");

                    b.HasIndex("CourseId");

                    b.HasIndex("IntervalId");

                    b.ToTable("TagReads", (string)null);
                });

            modelBuilder.Entity("DataModels.AthleteCourse", b =>
                {
                    b.HasOne("DataModels.Athlete", "Athlete")
                        .WithMany("AthleteCourses")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Course", "Course")
                        .WithMany("AthleteCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DataModels.AthleteCourseBracket", b =>
                {
                    b.HasOne("DataModels.AthleteCourse", "AthleteCourse")
                        .WithMany("AthleteCourseBrackets")
                        .HasForeignKey("AthleteCourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Bracket", "Bracket")
                        .WithMany("AthleteCourseBrackets")
                        .HasForeignKey("BracketId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Course", "Course")
                        .WithMany("AthleteCourseBrackets")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AthleteCourse");

                    b.Navigation("Bracket");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DataModels.AthleteCourseTraining", b =>
                {
                    b.HasOne("DataModels.AthleteCourse", "AthleteCourse")
                        .WithMany("AthleteCourseTrainings")
                        .HasForeignKey("AthleteCourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AthleteCourse");
                });

            modelBuilder.Entity("DataModels.AthleteRaceSeriesGoal", b =>
                {
                    b.HasOne("DataModels.Athlete", "Athlete")
                        .WithMany("AthleteRaceSeriesGoals")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Athlete");
                });

            modelBuilder.Entity("DataModels.AthleteWellnessEntry", b =>
                {
                    b.HasOne("DataModels.Athlete", "Athlete")
                        .WithMany("AthleteWellnessEntries")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Athlete");
                });

            modelBuilder.Entity("DataModels.Bracket", b =>
                {
                    b.HasOne("DataModels.Course", "Course")
                        .WithMany("Brackets")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DataModels.BracketMetadata", b =>
                {
                    b.HasOne("DataModels.Bracket", "Bracket")
                        .WithMany("BracketMetadatas")
                        .HasForeignKey("BracketId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Course", "Course")
                        .WithMany("BracketMetadatas")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Interval", "Interval")
                        .WithMany("BracketMetadatas")
                        .HasForeignKey("IntervalId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Bracket");

                    b.Navigation("Course");

                    b.Navigation("Interval");
                });

            modelBuilder.Entity("DataModels.Course", b =>
                {
                    b.HasOne("DataModels.Race", "Race")
                        .WithMany("Courses")
                        .HasForeignKey("RaceId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Race");
                });

            modelBuilder.Entity("DataModels.CourseInformationEntry", b =>
                {
                    b.HasOne("DataModels.Course", "Course")
                        .WithMany("CourseInformationEntries")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DataModels.Interval", b =>
                {
                    b.HasOne("DataModels.Course", "Course")
                        .WithMany("Intervals")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DataModels.Race", b =>
                {
                    b.HasOne("DataModels.RaceSeries", "RaceSeries")
                        .WithMany("Races")
                        .HasForeignKey("RaceSeriesId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RaceSeries");
                });

            modelBuilder.Entity("DataModels.Result", b =>
                {
                    b.HasOne("DataModels.AthleteCourse", "AthleteCourse")
                        .WithMany("Results")
                        .HasForeignKey("AthleteCourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Bracket", "Bracket")
                        .WithMany("Results")
                        .HasForeignKey("BracketId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Course", "Course")
                        .WithMany("Results")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Interval", "Interval")
                        .WithMany("Results")
                        .HasForeignKey("IntervalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AthleteCourse");

                    b.Navigation("Bracket");

                    b.Navigation("Course");

                    b.Navigation("Interval");
                });

            modelBuilder.Entity("DataModels.TagRead", b =>
                {
                    b.HasOne("DataModels.AthleteCourse", "AthleteCourse")
                        .WithMany("TagReads")
                        .HasForeignKey("AthleteCourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Course", "Course")
                        .WithMany("TagReads")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataModels.Interval", "Interval")
                        .WithMany("TagReads")
                        .HasForeignKey("IntervalId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("AthleteCourse");

                    b.Navigation("Course");

                    b.Navigation("Interval");
                });

            modelBuilder.Entity("DataModels.Athlete", b =>
                {
                    b.Navigation("AthleteCourses");

                    b.Navigation("AthleteRaceSeriesGoals");

                    b.Navigation("AthleteWellnessEntries");
                });

            modelBuilder.Entity("DataModels.AthleteCourse", b =>
                {
                    b.Navigation("AthleteCourseBrackets");

                    b.Navigation("AthleteCourseTrainings");

                    b.Navigation("Results");

                    b.Navigation("TagReads");
                });

            modelBuilder.Entity("DataModels.Bracket", b =>
                {
                    b.Navigation("AthleteCourseBrackets");

                    b.Navigation("BracketMetadatas");

                    b.Navigation("Results");
                });

            modelBuilder.Entity("DataModels.Course", b =>
                {
                    b.Navigation("AthleteCourseBrackets");

                    b.Navigation("AthleteCourses");

                    b.Navigation("BracketMetadatas");

                    b.Navigation("Brackets");

                    b.Navigation("CourseInformationEntries");

                    b.Navigation("Intervals");

                    b.Navigation("Results");

                    b.Navigation("TagReads");
                });

            modelBuilder.Entity("DataModels.Interval", b =>
                {
                    b.Navigation("BracketMetadatas");

                    b.Navigation("Results");

                    b.Navigation("TagReads");
                });

            modelBuilder.Entity("DataModels.Race", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("DataModels.RaceSeries", b =>
                {
                    b.Navigation("Races");
                });
#pragma warning restore 612, 618
        }
    }
}
