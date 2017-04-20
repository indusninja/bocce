using BocceEngine.EngineComponents;
using BocceEngine.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BocceEngine.InputSamples
{
    public class DragInputCheckForMouse : IInputCheck, IEngineObject
    {
        private int _start = 0, _current = 0;
        public int CurrentDragLength
        {
            get { return _current - _start; }
        }

        public int DragStartX { get; set; }
        public int DragStartY { get; set; }

        public MouseButtonType MouseButton { get; set; }
        public AxisType MouseAxis { get; set; }

        public DragInputCheckForMouse(MouseButtonType mouseButtonType, AxisType axis)
        {
			IsDebugMode = false;
			IsDrawable = false;
			DrawOrder = 0;
			IsUpdateable = true;
			//UpdateOrder = 0;
            MouseButton = mouseButtonType;
            MouseAxis = axis;
            CommandName = "drag_" + MouseButton.ToString().ToLower() + "_" + axis.ToString().ToLower();
            DeviceType = InputType.Mouse;
        }

        public InputType DeviceType { get; set; }

        public string CommandName { get; set; }

        public float DidInputHappen<T>(T previous, T current)
        {
            if (typeof(T) == typeof(MouseState))
            {
                var buttonPreviousState =
                    (ButtonState) ReflectionHelpers.GetPropertyValue(previous, MouseButton + "Button");
                var buttonCurrentState =
                    (ButtonState) ReflectionHelpers.GetPropertyValue(current, MouseButton + "Button");
                
                if(buttonPreviousState.Equals(ButtonState.Released) && buttonCurrentState.Equals(ButtonState.Pressed))
                {
                    _start = (int) ReflectionHelpers.GetPropertyValue(current, MouseAxis.ToString());
                    //DragStartPosition = ReflectionHelpers.GetPropertyValue(current, MouseAxis.ToString());
                    DragStartX = (int) ReflectionHelpers.GetPropertyValue(current, "X");
                    DragStartY = (int) ReflectionHelpers.GetPropertyValue(current, "Y");
                }
                else if (buttonPreviousState.Equals(ButtonState.Pressed) && buttonCurrentState.Equals(ButtonState.Released))
                {
                    int temp = _start;
                    _start = _current = 0;
                    return (int) ReflectionHelpers.GetPropertyValue(current, MouseAxis.ToString()) - temp;
                }
                else
                {
                    _current = (int) ReflectionHelpers.GetPropertyValue(current, MouseAxis.ToString());
                }
            }
            return 0f;
        }

		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }
    }
}
