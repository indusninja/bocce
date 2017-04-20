using BocceEngine.EngineComponents;
using BocceEngine.Utilities;
using Microsoft.Xna.Framework.Input;

namespace BocceEngine.InputSamples
{
    public class ExitInputCheckForKeyboard : IInputCheck, IEngineObject
    {
        public InputType DeviceType { get; set; }
        public string CommandName { get; set; }
		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }

        public ExitInputCheckForKeyboard()
        {
			IsDebugMode = false;
			IsDrawable = false;
			DrawOrder = 0;
			IsUpdateable = true;
            DeviceType = InputType.Keyboard;
        }

        public float DidInputHappen<T>(T previous, T current)
        {
            if (typeof(T) == typeof(KeyboardState))
            {
                var keyboardStateType = typeof(T);
                
                var keyDownMethodInfo = keyboardStateType.GetMethod("IsKeyDown");
                var previousState = keyDownMethodInfo.Invoke(previous, new object[] { Keys.Escape });
                
                var keyUpMethodInfo = keyboardStateType.GetMethod("IsKeyUp");
                var currentState = keyUpMethodInfo.Invoke(current, new object[] { Keys.Escape });
                
                if(currentState!=null && previousState!=null)
                {
                    if ((bool)currentState && (bool)previousState)
                    {
                        return 1f;
                    }
                }
            }
            return 0f;
        }
    }
}
