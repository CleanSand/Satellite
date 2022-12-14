using Satellite.Models;

namespace Satellite.ViewModels;

public class ObjectInformationVM : ViewModelBase
{
    public Solar Solar
    {
        get;
    }
    public ObjectInformationVM(Solar solar)
    {
        Solar = solar;
    }
}