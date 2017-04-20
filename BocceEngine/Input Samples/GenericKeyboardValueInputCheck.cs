using BocceEngine.EngineComponents;
using Microsoft.Xna.Framework.Input;
using BocceEngine.Utilities;

namespace BocceEngine.InputSamples
{
    public class GenericKeyboardValueInputCheck : IInputCheck, IEngineObject
    {
        private readonly Keys _triggerKey;
        private readonly float _triggerValue;

        public InputType DeviceType { get; set; }
        public string CommandName { get; set; }
		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }
        public ButtonPressType ButtonPressType { get; set; }

        public GenericKeyboardValueInputCheck(Keys key, float value, ButtonPressType pressType, string commandname = "generic_key")
        {
			IsDebugMode = false;
			IsDrawable = false;
			DrawOrder = 0;
			IsUpdateable = true;

            ButtonPressType = pressType;
            CommandName = commandname;
            DeviceType = InputType.Keyboard;
            _triggerKey = key;
            _triggerValue = value;
        }

        public float DidInputHappen<T>(T previous, T current)
        {
            if (typeof(T) == typeof(KeyboardState))
            {
                var keyboardStateType = typeof(T);

                var keyDownMethodInfo = keyboardStateType.GetMethod("IsKeyDown");
                var keyUpMethodInfo = keyboardStateType.GetMethod("IsKeyUp");
                object previousState;
                object currentState;

                switch (ButtonPressType)
                {
                    case ButtonPressType.ButtonDown:
                        previousState = keyUpMethodInfo.Invoke(previous, new object[] { _triggerKey });
                        currentState = keyDownMethodInfo.Invoke(current, new object[] { _triggerKey });
                        if ((bool)previousState && (bool)currentState)
                        {
                            return _triggerValue;
                        }
                        break;
                    case ButtonPressType.ButtonUp:
                        previousState = keyDownMethodInfo.Invoke(previous, new object[] { _triggerKey });
                        currentState = keyUpMethodInfo.Invoke(current, new object[] { _triggerKey });
                        if ((bool)previousState && (bool)currentState)
                        {
                            return _triggerValue;
                        }
                        break;
                    case ButtonPressType.ButtonPress:
                        currentState = keyDownMethodInfo.Invoke(current, new object[] { _triggerKey });
                        if ((bool)currentState)
                        {
                            return _triggerValue;
                        }
                        break;
                }
            }

            return 0f;
        }
    }
}
