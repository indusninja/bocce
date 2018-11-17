namespace BocceEngine.Core.EngineComponents
{
    public interface IInputAction
    {
        void ActionToExecute(float inputValue, IInputCheck inputCheck);
    }
}
