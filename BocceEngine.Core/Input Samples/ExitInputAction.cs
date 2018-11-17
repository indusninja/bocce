using BocceEngine.Core.EngineComponents;
using Microsoft.Xna.Framework;

namespace BocceEngine.Core.InputSamples
{
    public class ExitInputAction : IInputAction, IEngineObject
    {
        readonly Game _parentGame;

		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }

        public ExitInputAction(Game game)
        {
			IsDebugMode = false;
			IsDrawable = false;
			DrawOrder = 0;
			IsUpdateable = true;
            _parentGame = game;
        }

        public void ActionToExecute(float inputValue, IInputCheck inputCheck)
        {
            if(inputValue > 0f)
                _parentGame.Exit();
        }
    }
}
