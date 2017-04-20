using BocceEngine.EngineComponents;
using BocceEngine.Utilities;
using Microsoft.Xna.Framework;
using System;

namespace BocceEngine.InputSamples
{
	public class FullscreenInputAction : IInputAction, IEngineObject
	{
		readonly GraphicsDeviceManager _parentGraphics;

		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }

		private int _width;
		private int _height;

		// https://pacoup.com/2011/06/12/list-of-true-169-resolutions/
		public FullscreenInputAction(GraphicsDeviceManager graphics, int width = 1024, int height = 576)
		{
			IsDebugMode = false;
			IsDrawable = false;
			DrawOrder = 0;
			IsUpdateable = true;
			_parentGraphics = graphics;
			_width = width;
			_height = height;
			_parentGraphics.ChangeResolution(_width, _height, false);
		}

		public void ActionToExecute(float inputValue, IInputCheck inputCheck)
		{
			if (inputValue > 0f)
				if (_parentGraphics.IsFullScreen)
				{
					_parentGraphics.ChangeResolution(_width, _height, false);
					Console.WriteLine(string.Format("Switching to {0} x {1}, in windowed mode {2} {3} {4} {5}", _width, _height, inputValue, _parentGraphics.IsFullScreen, _parentGraphics.PreferredBackBufferWidth, _parentGraphics.PreferredBackBufferHeight));
				}
				else
				{
					_parentGraphics.ChangeResolution(_parentGraphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width, _parentGraphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height, true);
					Console.WriteLine(string.Format("Switching to {0} x {1}, in fullscreen mode {2} {3} {4} {5}", _width, _height, inputValue, _parentGraphics.IsFullScreen, _parentGraphics.PreferredBackBufferWidth, _parentGraphics.PreferredBackBufferHeight));
				}
		}
	}
}
