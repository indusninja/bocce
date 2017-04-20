using BocceEngine.Utilities;

namespace BocceEngine.EngineComponents
{
    public interface IInputCheck
    {
        InputType DeviceType { get; set; }
        string CommandName { get; set; }
        float DidInputHappen<T>(T previous, T current);
    }
}
