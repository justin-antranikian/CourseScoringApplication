using NetTopologySuite.Geometries;

namespace Api.Orchestration;

public static class GeometryExtensions
{
    public static Point CreatePoint(double latitude, double longitude) => new(longitude, latitude) { SRID = 4326 };
}