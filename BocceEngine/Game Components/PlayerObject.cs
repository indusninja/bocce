using System;
using BocceEngine.EngineComponents;
using BocceEngine.Utilities;
using Microsoft.Xna.Framework;

namespace BocceEngine.GameComponents
{
    public class PlayerObject : GameObject, IInputAction
    {
        public PlayerObject(Game game, string textureName)
            : base(game, textureName)
        {
            ObjectId = "player";
            Tint = Color.LightPink;
        }

        public PlayerObject(Game game, string textureName, string debugTextureCross, string debugTextureBox)
            : base(game, textureName, debugTextureCross, debugTextureBox)
        {
            ObjectId = "player";
            Tint = Color.LightPink;
        }

		public void ActionToExecute(float inputValue, IInputCheck inputCheck)
		{
			if (Math.Abs(inputValue - 0) > MathValues.EPSILON)
			{
				Console.WriteLine(
					string.Format("Player received {0} input command {1} with value {2}",
					inputCheck.DeviceType.ToString().ToLower(),
					inputCheck.CommandName,
					inputValue));

				if (inputCheck is BocceEngine.InputSamples.GenericKeyboardValueInputCheck)
				{
					switch (inputCheck.CommandName)
					{
						case "move_left":
						case "move_right":
							AddImpulse(inputValue, "x");
							break;
						case "move_up":
						case "move_down":
							AddImpulse(inputValue, "y");
							break;
					}
				}

				if (inputCheck is BocceEngine.InputSamples.DragInputCheckForMouse)
				{
					int x = (inputCheck as BocceEngine.InputSamples.DragInputCheckForMouse).DragStartX;
					int y = (inputCheck as BocceEngine.InputSamples.DragInputCheckForMouse).DragStartY;
					Console.WriteLine(string.Format("Drag started at ({0}, {1})", x, y));
				}
			}
		}
    }
}
