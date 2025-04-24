using NetTopologySuite.Geometries;

namespace Api.Orchestration;

public static class GeometryExtensions
{
    public const double MilesToMeters = 1609.34;

    public static Point CreatePoint(double latitude, double longitude, int srid = 4326)
    {
        return new Point(longitude, latitude) { SRID = srid };
    }
}