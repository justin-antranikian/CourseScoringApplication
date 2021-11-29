using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
	[Table("Courses")]
	public record Course
	{
		[Key]
		public int Id { get; init; }

		public int RaceId { get; init; }

		public string Name { get; init; }

		public int SortOrder { get; init; }

		public PaceType PaceType { get; init; }

		public PreferedMetric PreferedMetric { get; init; }

		public double Distance { get; set; }

		public DateTime StartDate { get; init; }

		public CourseType CourseType { get; init; }

		public Race Race { get; init; }

		[ForeignKey("CourseId")]
		public List<Interval> Intervals { get; init; }

		[ForeignKey("CourseId")]
		public List<Bracket> Brackets { get; init; }

		[ForeignKey("CourseId")]
		public List<CourseInformationEntry> CourseInformationEntries { get; init; }
	}
}
