using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseScoringTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentLocationId = table.Column<int>(type: "int", nullable: true),
                    LocationType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Slug = table.Column<string>(type: "VARCHAR(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentLocationId",
                        column: x => x.ParentLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaLocationId = table.Column<int>(type: "int", nullable: false),
                    CityLocationId = table.Column<int>(type: "int", nullable: false),
                    StateLocationId = table.Column<int>(type: "int", nullable: false),
                    AreaRank = table.Column<int>(type: "int", nullable: false),
                    CityRank = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    FullName = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Gender = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    LastName = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    OverallRank = table.Column<int>(type: "int", nullable: false),
                    StateRank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Athletes_Locations_AreaLocationId",
                        column: x => x.AreaLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Athletes_Locations_CityLocationId",
                        column: x => x.CityLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Athletes_Locations_StateLocationId",
                        column: x => x.StateLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RaceSeries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaLocationId = table.Column<int>(type: "int", nullable: false),
                    CityLocationId = table.Column<int>(type: "int", nullable: false),
                    StateLocationId = table.Column<int>(type: "int", nullable: false),
                    AreaRank = table.Column<int>(type: "int", nullable: false),
                    CityRank = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<Geometry>(type: "geography", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    OverallRank = table.Column<int>(type: "int", nullable: false),
                    RaceSeriesType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    StateRank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceSeries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RaceSeries_Locations_AreaLocationId",
                        column: x => x.AreaLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaceSeries_Locations_CityLocationId",
                        column: x => x.CityLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RaceSeries_Locations_StateLocationId",
                        column: x => x.StateLocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AthleteRaceSeriesGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    RaceSeriesType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    TotalEvents = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteRaceSeriesGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AthleteRaceSeriesGoals_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AthleteWellnessEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    AthleteWellnessType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(200)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteWellnessEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AthleteWellnessEntries_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceSeriesId = table.Column<int>(type: "int", nullable: false),
                    KickOffDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    TimeZoneId = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Races_RaceSeries_RaceSeriesId",
                        column: x => x.RaceSeriesId,
                        principalTable: "RaceSeries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    CourseType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    PaceType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    PreferedMetric = table.Column<string>(type: "VARCHAR(25)", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AthleteCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Bib = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    CourseGoalDescription = table.Column<string>(type: "VARCHAR(500)", nullable: true),
                    PersonalGoalDescription = table.Column<string>(type: "VARCHAR(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AthleteCourses_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AthleteCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Brackets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    BracketType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brackets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Brackets_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Intervals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    DistanceFromStart = table.Column<double>(type: "float", nullable: false),
                    IntervalType = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    IsFullCourse = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    PaceType = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intervals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intervals_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AthleteCourseTrainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteCourseId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteCourseTrainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AthleteCourseTrainings_AthleteCourses_AthleteCourseId",
                        column: x => x.AthleteCourseId,
                        principalTable: "AthleteCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AthleteCourseBrackets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteCourseId = table.Column<int>(type: "int", nullable: false),
                    BracketId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthleteCourseBrackets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AthleteCourseBrackets_AthleteCourses_AthleteCourseId",
                        column: x => x.AthleteCourseId,
                        principalTable: "AthleteCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AthleteCourseBrackets_Brackets_BracketId",
                        column: x => x.BracketId,
                        principalTable: "Brackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AthleteCourseBrackets_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BracketMetadatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BracketId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    IntervalId = table.Column<int>(type: "int", nullable: true),
                    TotalRacers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BracketMetadatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BracketMetadatas_Brackets_BracketId",
                        column: x => x.BracketId,
                        principalTable: "Brackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BracketMetadatas_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BracketMetadatas_Intervals_IntervalId",
                        column: x => x.IntervalId,
                        principalTable: "Intervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteCourseId = table.Column<int>(type: "int", nullable: false),
                    BracketId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    DivisionRank = table.Column<int>(type: "int", nullable: false),
                    GenderRank = table.Column<int>(type: "int", nullable: false),
                    IntervalId = table.Column<int>(type: "int", nullable: false),
                    IsHighestIntervalCompleted = table.Column<bool>(type: "bit", nullable: false),
                    OverallRank = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    TimeOnInterval = table.Column<int>(type: "int", nullable: false),
                    TimeOnCourse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_AthleteCourses_AthleteCourseId",
                        column: x => x.AthleteCourseId,
                        principalTable: "AthleteCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Brackets_BracketId",
                        column: x => x.BracketId,
                        principalTable: "Brackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Results_Intervals_IntervalId",
                        column: x => x.IntervalId,
                        principalTable: "Intervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TagReads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AthleteCourseId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    IntervalId = table.Column<int>(type: "int", nullable: false),
                    TimeOnInterval = table.Column<int>(type: "int", nullable: false),
                    TimeOnCourse = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagReads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagReads_AthleteCourses_AthleteCourseId",
                        column: x => x.AthleteCourseId,
                        principalTable: "AthleteCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagReads_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TagReads_Intervals_IntervalId",
                        column: x => x.IntervalId,
                        principalTable: "Intervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthleteCourseBrackets_AthleteCourseId",
                table: "AthleteCourseBrackets",
                column: "AthleteCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteCourseBrackets_BracketId",
                table: "AthleteCourseBrackets",
                column: "BracketId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteCourseBrackets_CourseId",
                table: "AthleteCourseBrackets",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteCourses_AthleteId",
                table: "AthleteCourses",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteCourses_CourseId",
                table: "AthleteCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteCourseTrainings_AthleteCourseId",
                table: "AthleteCourseTrainings",
                column: "AthleteCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteRaceSeriesGoals_AthleteId",
                table: "AthleteRaceSeriesGoals",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_AreaLocationId",
                table: "Athletes",
                column: "AreaLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_CityLocationId",
                table: "Athletes",
                column: "CityLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Athletes_StateLocationId",
                table: "Athletes",
                column: "StateLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AthleteWellnessEntries_AthleteId",
                table: "AthleteWellnessEntries",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_BracketMetadatas_BracketId",
                table: "BracketMetadatas",
                column: "BracketId");

            migrationBuilder.CreateIndex(
                name: "IX_BracketMetadatas_CourseId",
                table: "BracketMetadatas",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_BracketMetadatas_IntervalId",
                table: "BracketMetadatas",
                column: "IntervalId");

            migrationBuilder.CreateIndex(
                name: "IX_Brackets_CourseId",
                table: "Brackets",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_RaceId",
                table: "Courses",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Intervals_CourseId",
                table: "Intervals",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentLocationId",
                table: "Locations",
                column: "ParentLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_RaceSeriesId",
                table: "Races",
                column: "RaceSeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSeries_AreaLocationId",
                table: "RaceSeries",
                column: "AreaLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSeries_CityLocationId",
                table: "RaceSeries",
                column: "CityLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceSeries_StateLocationId",
                table: "RaceSeries",
                column: "StateLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_AthleteCourseId",
                table: "Results",
                column: "AthleteCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_BracketId",
                table: "Results",
                column: "BracketId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_CourseId",
                table: "Results",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_IntervalId",
                table: "Results",
                column: "IntervalId");

            migrationBuilder.CreateIndex(
                name: "IX_TagReads_AthleteCourseId",
                table: "TagReads",
                column: "AthleteCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TagReads_CourseId",
                table: "TagReads",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_TagReads_IntervalId",
                table: "TagReads",
                column: "IntervalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthleteCourseBrackets");

            migrationBuilder.DropTable(
                name: "AthleteCourseTrainings");

            migrationBuilder.DropTable(
                name: "AthleteRaceSeriesGoals");

            migrationBuilder.DropTable(
                name: "AthleteWellnessEntries");

            migrationBuilder.DropTable(
                name: "BracketMetadatas");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "TagReads");

            migrationBuilder.DropTable(
                name: "Brackets");

            migrationBuilder.DropTable(
                name: "AthleteCourses");

            migrationBuilder.DropTable(
                name: "Intervals");

            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "RaceSeries");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
