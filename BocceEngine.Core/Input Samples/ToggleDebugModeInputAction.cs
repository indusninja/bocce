using BocceEngine.Core.EngineComponents;
using Microsoft.Xna.Framework;

namespace BocceEngine.Core.InputSamples
{
    public class ToggleDebugModeInputAction : IInputAction, IEngineObject
    {
        readonly Game _parentGame;

        public string CommandName { get; set; }
		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }

        public ToggleDebugModeInputAction(Game game)
        {
			IsDebugMode = false;
			IsDrawable = false;
			DrawOrder = 0;
			IsUpdateable = true;
            CommandName = "TOGGLE_DEBUG_MODE";
            _parentGame = game;
            foreach (IEngineObject component in _parentGame.Components)
            {
                component.IsDebugMode = false;
            }
        }

        public void ActionToExecute(float inputValue, IInputCheck inputCheck)
        {
#if DEBUG
            if (inputValue <= 0f) return;
            IsDebugMode = !IsDebugMode;
            foreach (IEngineObject component in _parentGame.Components)
            {
                component.IsDebugMode = IsDebugMode;
            }
#endif
        }
    }
}
