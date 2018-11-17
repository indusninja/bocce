using System.Collections.Generic;
using BocceEngine.Core.EngineComponents;
using BocceEngine.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BocceEngine.Core.GameComponents
{
    public class InputManager : GameComponent, IEngineObject
    {
		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }

        public KeyboardState PreviousKeyboardState { get; set; }

        public KeyboardState CurrentKeyboardState { get; set; }

        public MouseState PreviousMouseState { get; set; }

        public MouseState CurrentMouseState { get; set; }

        public GamePadState PreviousGamepadState { get; set; }

        public GamePadState CurrentGamepadState { get; set; }

        public GameTime PreviousUpdateTimeStamp { get; set; }

        private readonly Dictionary<IInputCheck, IInputAction> _keyBindings = new Dictionary<IInputCheck, IInputAction>();

        public IInputAction GetKeyBinding(IInputCheck inputKey)
        {
            return _keyBindings.ContainsKey(inputKey) ? _keyBindings[inputKey] : null;
        }

        public void SetKeyBinding(IInputCheck inputKey, IInputAction executeAction)
        {
            if (_keyBindings.ContainsKey(inputKey))
                _keyBindings[inputKey] = executeAction;
            else
                _keyBindings.Add(inputKey, executeAction);
        }

        public InputManager(Game game)
            : base(game)
		{
			IsDebugMode = false;
			IsUpdateable = true;
			//UpdateOrder = base.UpdateOrder;
			IsDrawable = false;
			DrawOrder = base.UpdateOrder;

            PreviousUpdateTimeStamp = new GameTime();

            PreviousKeyboardState = new KeyboardState();
            PreviousMouseState = new MouseState();
            PreviousGamepadState = new GamePadState();

            CurrentKeyboardState = new KeyboardState();
            CurrentMouseState = new MouseState();
            CurrentGamepadState = new GamePadState();
        }

        public override void Update(GameTime gameTime)
        {
            if (!PreviousUpdateTimeStamp.Equals(gameTime))
            {

                PreviousKeyboardState = CurrentKeyboardState;
                CurrentKeyboardState = Keyboard.GetState();

                PreviousMouseState = CurrentMouseState;
                CurrentMouseState = Mouse.GetState();

                PreviousGamepadState = CurrentGamepadState;
                CurrentGamepadState = GamePad.GetState(PlayerIndex.One);

                foreach (var pair in _keyBindings)
                {
                    switch (pair.Key.DeviceType)
                    {
                        case InputType.GamePad:
                            pair.Value.ActionToExecute(
                                pair.Key.DidInputHappen(PreviousGamepadState, CurrentGamepadState), pair.Key);
                            break;
                        case InputType.Keyboard:
                            pair.Value.ActionToExecute(
                                pair.Key.DidInputHappen(PreviousKeyboardState, CurrentKeyboardState), pair.Key);
                            break;
                        case InputType.Mouse:
                            pair.Value.ActionToExecute(
                                pair.Key.DidInputHappen(PreviousMouseState, CurrentMouseState), pair.Key);
                            break;
                    }
                }
            }

            base.Update(gameTime);
        }
    }
}
