using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
	[Table("AthleteCourseBrackets")]
	public class AthleteCourseBracket
	{
		[Key]
		public int Id { get; set; }

		public int AthleteCourseId { get; set; }

		public int CourseId { get; set; }

		public int BracketId { get; set; }

		public Bracket Bracket { get; set; }

		public AthleteCourseBracket(int athleteCourseId, int courseId, int bracketId)
		{
			AthleteCourseId = athleteCourseId;
			CourseId = courseId;
			BracketId = bracketId;
		}
	}
}
