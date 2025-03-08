using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace Api.Orchestration;

public static class GeometryExtensions
{
    public const double MilesToMeters = 1609.34;

    public const string ColoradoPolygon = "POLYGON((-109.0448 37.0004,-102.0424 36.9949,-102.0534 41.0006,-109.0489 40.9996,-109.0448 37.0004,-109.0448 37.0004))";
    public const string WyomingPolygon = "POLYGON((-104.0556 41.0037,-104.0584 44.9949,-111.0539 44.9998,-111.0457 40.9986,-104.0556 41.0006,-104.0556 41.0037))";

    public static Point CreatePoint(double latitude, double longitude) => new(longitude, latitude) { SRID = 4326 };

    public static WKTReader GetReader()
    {
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory();
        return new WKTReader(geometryFactory);
    }

    public static string coloradoWkt = @"
      POLYGON ((
        -109.05 41.00,
        -102.05 41.00,
        -102.05 36.99,
        -109.05 36.99,
        -109.05 41.00
      ))";

    public static Geometry GetColoradoGeometry()
    {
        var coloradoPolygon = GetReader().Read(coloradoWkt);
        coloradoPolygon.SRID = 4326;
        var x = coloradoPolygon.Reverse();
        return x;
    }

    public static Geometry GetWyomingGeometry() => GetReader().Read(WyomingPolygon);
}