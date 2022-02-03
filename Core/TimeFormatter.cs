using System;

namespace Core;

public static class TimeFormatter
{
	public static string Format(int timeInSeconds)
	{
		var timeSpan = TimeSpan.FromSeconds(timeInSeconds);
		var formattedSeconds = PadLeadingZero(timeSpan.Seconds);

		if (timeInSeconds < 60)
		{
			return $"00:{formattedSeconds}";
		}

		if (timeInSeconds >= 60 && timeInSeconds < 3600)
		{
			return $"{timeSpan.Minutes}:{formattedSeconds}";
		}

		var formattedMinutes = PadLeadingZero(timeSpan.Minutes);
		return $"{timeSpan.Hours}:{formattedMinutes}:{formattedSeconds}";
	}

	private static string PadLeadingZero(int time) => time < 10 ? $"0{time}" : $"{time}";
}
