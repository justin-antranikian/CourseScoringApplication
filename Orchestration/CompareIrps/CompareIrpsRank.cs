
using System;

namespace Orchestration.CompareIrps
{
	public enum CompareIrpsRank
	{
		First,
		Second,
		Third,
		Fourth
	}

	public static class CompareIrpsRankExtensions
	{
		public static CompareIrpsRank MapToCompareIrpsRank(this int index)
		{
			return index switch
			{
				0 => CompareIrpsRank.First,
				1 => CompareIrpsRank.Second,
				2 => CompareIrpsRank.Third,
				3 => CompareIrpsRank.Fourth,
				_ => throw new NotImplementedException()
			};
		}
	}
}
