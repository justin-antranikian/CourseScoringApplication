using Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
	/// <summary>
	/// Dave follows Derek.
	/// Dave is the AthleteFromId.
	/// Derek is the AthleteToId.
	/// This does NOT mean the Derek follows Dave.
	/// </summary>
	[Table("AthleteRelationshipEntries")]
	public class AthleteRelationshipEntry
	{
		[Key]
		public int Id { get; set; }

		/// <summary>
		/// Returns Dave's id in Dave follows Derek scenerio.
		/// </summary>
		public int AthleteFromId { get; set; }

		/// <summary>
		/// Returns Derek's id in Dave follows Derek scenerio.
		/// </summary>
		public int AthleteToId { get; set; }

		public AthleteRelationshipType AthleteRelationshipType { get; set; }

		public AthleteRelationshipEntry(int athleteToId, AthleteRelationshipType athleteRelationshipType)
		{
			AthleteToId = athleteToId;
			AthleteRelationshipType = athleteRelationshipType;
		}
	}
}
