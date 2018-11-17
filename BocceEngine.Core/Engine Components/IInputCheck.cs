using BocceEngine.Core.Utilities;

namespace BocceEngine.Core.EngineComponents
{
    public interface IInputCheck
    {
        InputType DeviceType { get; set; }
        string CommandName { get; set; }
        float DidInputHappen<T>(T previous, T current);
    }
}
