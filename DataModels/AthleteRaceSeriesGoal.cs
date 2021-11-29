using Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
	[Table("AthleteRaceSeriesGoals")]
	public class AthleteRaceSeriesGoal
	{
		[Key]
		public int Id { get; set; }

		public int AthleteId { get; set; }

		public RaceSeriesType RaceSeriesType { get; set; }

		public int TotalEvents { get; set; }

		public AthleteRaceSeriesGoal(RaceSeriesType raceSeriesType, int totalEvents)
		{
			RaceSeriesType = raceSeriesType;
			TotalEvents = totalEvents;
		}
	}
}
