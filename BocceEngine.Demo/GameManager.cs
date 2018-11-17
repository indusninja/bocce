using System.Linq;
using BocceEngine.Core.EngineComponents;
using BocceEngine.Core.GameComponents;
using BocceEngine.Core.InputSamples;
using BocceEngine.Core.Physics;
using BocceEngine.Core.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BocceEngine.Demo
{
    public class GameManager : Game
    {
        readonly GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        public GameManager()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            IsMouseVisible = true;

			#region Adding Component: Random number generator
			RandomGenerator randomGenerator = new RandomGenerator(this);
			Components.Add(randomGenerator);
			#endregion

			#region Adding Component: InputManager
			InputManager inputMngr = new InputManager(this);

			ExitInputAction exitAction = new ExitInputAction(this);
			ExitInputCheckForKeyboard exitInput = new ExitInputCheckForKeyboard();
			inputMngr.SetKeyBinding(exitInput, exitAction);

			FullscreenInputAction fullscreenAction = new FullscreenInputAction(_graphics);
			FullscreenInputCheckForKeyboard fullscreenInput = new FullscreenInputCheckForKeyboard();
			inputMngr.SetKeyBinding(fullscreenInput, fullscreenAction);

			ToggleDebugModeInputCheckForKeyboard switchDebugModeInput = new ToggleDebugModeInputCheckForKeyboard();
			ToggleDebugModeInputAction switchDebugModeAction = new ToggleDebugModeInputAction(this);
            inputMngr.SetKeyBinding(switchDebugModeInput, switchDebugModeAction);

            Components.Add(inputMngr);
            #endregion

            #region Adding Component: GameObject 1
			GameObject testObject1 = new GameObject(this, "Textures/circle", "Textures/cross", "Textures/OutlineBox");
            testObject1.Center = new Vector2(_graphics.PreferredBackBufferWidth * 0.5f, _graphics.PreferredBackBufferHeight * 0.5f);
            testObject1.Scale = 0.3f;
            Components.Add(testObject1);
            #endregion

            #region Disabled - Adding Component: GameObject 2
            /*GameObject TestObject2 = new GameObject(this, "circle", "cross", "OutlineBox");
            TestObject2.Center = new Vector2(graphics.PreferredBackBufferWidth * 0.75f, graphics.PreferredBackBufferHeight * 0.75f);
            TestObject2.Scale = 0.3f;
            Components.Add(TestObject2);*/
            #endregion

            #region Adding Component: Player
			PlayerObject gamePlayer = new PlayerObject(this, "Textures/circle", "Textures/cross", "Textures/OutlineBox");
            gamePlayer.Center = new Vector2(_graphics.PreferredBackBufferWidth * 0.75f, _graphics.PreferredBackBufferHeight * 0.75f);
            gamePlayer.Scale = 0.3f;
			//gamePlayer.Speed = 0.01f;
            Components.Add(gamePlayer);

			GenericKeyboardValueInputCheck playerInput1 = new GenericKeyboardValueInputCheck(Microsoft.Xna.Framework.Input.Keys.A, -1, ButtonPressType.ButtonPress, "move_left");
			inputMngr.SetKeyBinding(playerInput1, gamePlayer);

			GenericKeyboardValueInputCheck playerInput2 = new GenericKeyboardValueInputCheck(Microsoft.Xna.Framework.Input.Keys.W, -1, ButtonPressType.ButtonPress, "move_up");
			inputMngr.SetKeyBinding(playerInput2, gamePlayer);

			GenericKeyboardValueInputCheck playerInput3 = new GenericKeyboardValueInputCheck(Microsoft.Xna.Framework.Input.Keys.S, 1, ButtonPressType.ButtonPress, "move_down");
			inputMngr.SetKeyBinding(playerInput3, gamePlayer);

			GenericKeyboardValueInputCheck playerInput4 = new GenericKeyboardValueInputCheck(Microsoft.Xna.Framework.Input.Keys.D, 1, ButtonPressType.ButtonPress, "move_right");
			inputMngr.SetKeyBinding(playerInput4, gamePlayer);

			DragInputCheckForMouse playerMouseInput1 = new DragInputCheckForMouse(MouseButtonType.Left, AxisType.X);
			inputMngr.SetKeyBinding(playerMouseInput1, gamePlayer);

			DragInputCheckForMouse playerMouseInput2 = new DragInputCheckForMouse(MouseButtonType.Left, AxisType.Y);
            inputMngr.SetKeyBinding(playerMouseInput2, gamePlayer);

			/*DragInputCheckForMouse playerMouseInput3 = new DragInputCheckForMouse(MouseButtonType.Right, AxisType.X);
			DragInputCheckForMouse playerMouseInput4 = new DragInputCheckForMouse(MouseButtonType.Right, AxisType.Y);

            inputMngr.SetKeyBinding(playerMouseInput3, gamePlayer);
            inputMngr.SetKeyBinding(playerMouseInput4, gamePlayer);*/
            #endregion

            #region Adding Component: CollisionSolver
			CollisionSolver solver = new CollisionSolver(this);
            Components.Add(solver);
            #endregion

            #region Disabled - Adding Component: Event logger
            //EventLogger Logger = new EventLogger(this);
            //Components.Add(Logger);
            #endregion

        }

		protected override void Update(GameTime gameTime)
		{
			#region Gods Must be crazy old update code
			/*if ((previousState.LeftButton != ButtonState.Pressed) && (currentState.LeftButton == ButtonState.Pressed))
            {
                lastAsteroidSpawn = new Vector2((float)currentState.X, (float)currentState.Y);
                // Get sorted list so UpdateOrder also defines render order
                IEnumerable<GameComponent> sortedComponents = Components.Cast<GameComponent>().OrderBy(gameComponent => gameComponent.UpdateOrder);
                foreach (CelestialObj gameObject in sortedComponents)
                {
                    if (gameObject != null && gameObject is MeteorField && !canLaunch)
                    {
                        canLaunch = gameObject.CheckCollision(lastAsteroidSpawn);
                    }
                }

                if (canLaunch)
                    drawOrbit = true;
                else
                {
                    lastAsteroidSpawn = Vector2.Zero;
                    drawOrbit = false;
                }
            }

            if ((previousState.LeftButton == ButtonState.Pressed) && (currentState.LeftButton != ButtonState.Pressed) && canLaunch)
            {
                canLaunch = false;
                Asteroid a = new Asteroid(this, "Meteor/Meteor", 1f);
                a.Position = lastAsteroidSpawn - a.Origin;
                Vector2 temp = new Vector2(-lastAsteroidSpawn.X + (float)currentState.X,
                    -lastAsteroidSpawn.Y + (float)currentState.Y);
                temp /= 50f;
                float length = temp.Length();
                bool doAdd = true;
                if (hud.IsAddEnergyPossible(-20f - length))
                {
                    a.Velocity = temp;
                    hud.Energy -= (20f + length);
                }
                else if (hud.IsAddEnergyPossible(-20f))
                {
                    hud.Energy -= 20f;
                    temp.Normalize();
                    temp *= hud.Energy;
                    hud.Energy = 0f;
                }
                else
                {
                    doAdd = false;
                }
                if (doAdd)
                    Components.Add(a);
                drawOrbit = false;
            }

            if ((previousState.RightButton == ButtonState.Pressed) && (currentState.RightButton != ButtonState.Pressed))
            {
                if (hud.IsAddEnergyPossible(-10f))
                {
                    hud.Energy -= 10f;

                    Vector2 click = new Vector2(currentState.X, currentState.Y);
                    Vector2 bestDiff = new Vector2(10000, 10000);
                    Vector2 origin = new Vector2();
                    foreach (var item in Components)
                    {
                        if (item is Star)
                        {
                            Vector2 diff = (click - ((Star)item).Position);
                            if (diff.LengthSquared() < bestDiff.LengthSquared())
                            {
                                bestDiff = diff;
                                origin = ((Star)item).Position;
                            }
                        }
                    }
                    if (bestDiff.X != 10000)
                    {
                        SolarFlare sf = new SolarFlare(this, "SolarFlare/Shockwave", origin, bestDiff);
                        Components.Add(sf);
                    }
                }

            }*/
			#endregion

			// Get sorted list so as to find update order
			var sortedComponents = Components.
				Cast<IEngineObject>().
				Where(x => x != null && x.IsUpdateable).
				Cast<GameComponent>().
				Where(x => x != null).
				OrderBy(x => x.UpdateOrder);
			foreach (GameComponent gameObject in sortedComponents)
			{
				gameObject.Update(gameTime);
			}

			base.Update(gameTime);
		}

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);

            // Get sorted list so as to find render order
			var sortedComponents = Components.
				Cast<IEngineObject>().
				Where(x => x != null && x.IsDrawable).
				OrderBy(x => x.DrawOrder).
				Cast<GameComponent>().
				Where(x => x != null);
			foreach (GameObject gameObject in sortedComponents)
            {
                gameObject.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
