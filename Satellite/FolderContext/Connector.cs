namespace Satellite.FolderContext;

public static class Connector
{
    private static SatelliteContext _db;

    public static SatelliteContext GetContext()
    {
        return _db ??= new SatelliteContext();
    }
}

