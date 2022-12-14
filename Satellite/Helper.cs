using Satellite.FolderContext;
using Satellite.Models;

namespace Satellite;

public class Helper
{
    private static SatelliteContext _satellitecontext;
    
    public static User User { get; set; }
    public static SatelliteContext GetContext()
    {
        return _satellitecontext ??= new();
    }
}