using System;

namespace Core;

public static class TimeZoneExtensions
{
	public static string ToAbbreviation(this string timeZoneId)
	{
		return timeZoneId switch
		{
			"Pacific Standard Time" => "PST",
			"Mountain Standard Time" => "MST",
			"Central Standard Time" => "CST",
			"Eastern Standard Time" => "EST",
			_ => throw new NotImplementedException()
		};
	}
}
