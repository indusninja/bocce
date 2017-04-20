using Microsoft.Xna.Framework;

namespace BocceEngine.Utilities
{
    public enum InputType
    {
        Keyboard,
        Mouse,
        GamePad
    }

    public enum ButtonPressType
    {
        ButtonUp,
        ButtonDown,
        ButtonPress,
    }

    public enum MouseButtonType
    {
        Left,
        Middle,
        Right
    }

    public enum AxisType
    {
        X,
        Y
    }

    public static class GraphicsDeviceManagerExtentions
    {
        public static void ChangeResolution(this GraphicsDeviceManager graphics, int width, int height, bool bFullscreen)
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.IsFullScreen = bFullscreen;
            graphics.ApplyChanges();
        }
    }

    public static class MathValues
    {
        public static float EPSILON
        {
            get { return 0.00001f; }
        }
    }

    public static class  ReflectionHelpers
    {
        public static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}
