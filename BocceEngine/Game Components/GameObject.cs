using System;
using BocceEngine.EngineComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BocceEngine.GameComponents
{
    public class GameObject : GameComponent, IGameObject, IEngineObject
    {
        protected Game ParentGame;

        public string ObjectId { get; protected set; }
		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }
        public float Scale { get; set; }
        public Color Tint { get; set; }
        public Texture2D Texture { get; set; }
        private readonly Texture2D _crossTexture;
        private readonly Texture2D _boxTexture;
        bool _bIsColliding;
        public float Speed { get; set; }
        private Vector2 _direction;

        public Vector2 Center { get; set; }

        public Vector2 Origin
        {
            get { return new Vector2(Texture.Width * Scale * 0.5f, Texture.Height * Scale * 0.5f); }
        }

        public float Radius
        {
            get { return Texture.Width * 0.5f * Scale; }
        }

		public GameObject(Game game)
			: base(game)
		{
			_bIsColliding = false;
			ObjectId = "gameobject";
			IsDebugMode = false;
			IsUpdateable = true;
			//UpdateOrder = base.UpdateOrder;
			IsDrawable = true;
			DrawOrder = base.UpdateOrder;
			ParentGame = game;
			Scale = 1f;

			Texture = null;
			Speed = 0.1f;
			_direction = Vector2.Zero;

			Center = Vector2.Zero;
			Tint = Color.White;
		}

        public GameObject(Game game, string textureName)
            : base(game)
        {
            _bIsColliding = false;
            ObjectId = "gameobject";
			IsDebugMode = false;
			IsUpdateable = true;
			//UpdateOrder = base.UpdateOrder;
			IsDrawable = true;
			DrawOrder = base.UpdateOrder;
            ParentGame = game;
            Scale = 1f;

            Texture = game.Content.Load<Texture2D>(textureName);
            Speed = 0.1f;
            _direction = Vector2.Zero;

            Center = Vector2.Zero;
            Tint = Color.White;
        }

        public GameObject(Game game, string textureName, string debugTextureCross, string debugTextureBox)
            : base(game)
        {
            _bIsColliding = false;
            ObjectId = "gameobject";
			IsDebugMode = false;
			IsUpdateable = true;
			//UpdateOrder = base.UpdateOrder;
			IsDrawable = true;
			DrawOrder = base.UpdateOrder;
            ParentGame = game;
            Scale = 1f;

            Texture = game.Content.Load<Texture2D>(textureName);
            _crossTexture = game.Content.Load<Texture2D>(debugTextureCross);
            _boxTexture = game.Content.Load<Texture2D>(debugTextureBox);
			Speed = 0.1f;
			_direction = Vector2.Zero;

            Center = Vector2.Zero;
            Tint = Color.White;
        }

		public override void Update(GameTime gameTime)
		{
			if (!_direction.Equals(Vector2.Zero))
			{
				_direction.Normalize();
				Center += Speed * _direction * gameTime.ElapsedGameTime.Milliseconds * 0.1f;

				Vector2 LowerBounds = Vector2.Zero;
				Vector2 UpperBounds = new Vector2(ParentGame.GraphicsDevice.Viewport.Width, ParentGame.GraphicsDevice.Viewport.Height);

				if (Center.X < LowerBounds.X)
					Center = new Vector2(UpperBounds.X, Center.Y);
				if (Center.X > UpperBounds.X)
					Center = new Vector2(LowerBounds.X, Center.Y);
				if (Center.Y < LowerBounds.Y)
					Center = new Vector2(Center.X, UpperBounds.Y);
				if (Center.Y > UpperBounds.Y)
					Center = new Vector2(Center.X, LowerBounds.Y);
			}

			base.Update(gameTime);
		}

		public void AddImpulse(float value, string direction)
		{
			if (value != 0)
			{
				switch (direction)
				{
					case "x":
						_direction.X = value;
						break;
					case "y":
						_direction.Y = value;
						break;
				}
			}
		}

        public virtual void RegisterCollision(IGameObject collider)
        {
            _bIsColliding = true;
        }

        public virtual void UnregisterCollision()
        {
            _bIsColliding = false;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            var source = new Rectangle(0, 0, Texture.Width, Texture.Height);
            var dest = new Rectangle((int)(Center.X - Radius), (int)(Center.Y - Radius), (int)(Radius * 2f), (int)(Radius * 2f));
            spriteBatch.Draw(Texture, dest, source, Tint);

            if (!IsDebugMode || _crossTexture == null || _boxTexture == null) 
                return;

            // Top-Left corner
            var des = new Rectangle((int)(Center.X - Radius - (_crossTexture.Width * 0.25f)), (int)(Center.Y - Radius - (_crossTexture.Height * 0.25f)), (int)(_crossTexture.Width * 0.5f), (int)(_crossTexture.Height * 0.5f));
            spriteBatch.Draw(_crossTexture, des, null, Color.White);

            // Center
            des = new Rectangle((int)(Center.X - (_crossTexture.Width * 0.25f)), (int)(Center.Y - (_crossTexture.Height * 0.25f)), (int)(_crossTexture.Width * 0.5f), (int)(_crossTexture.Height * 0.5f));
            spriteBatch.Draw(_crossTexture, des, null, Color.White);

            // Bottom-Right corner
            des = new Rectangle((int)(Center.X + Radius - (_crossTexture.Width * 0.25f)), (int)(Center.Y + Radius - (_crossTexture.Height * 0.25f)), (int)(_crossTexture.Width * 0.5f), (int)(_crossTexture.Height * 0.5f));
            spriteBatch.Draw(_crossTexture, des, null, Color.White);

            // Collision Box
            des = new Rectangle((int)(Center.X - Radius), (int)(Center.Y - Radius), (int)(Radius * 2f), (int)(Radius * 2f));
            spriteBatch.Draw(_boxTexture, des, null, _bIsColliding ? Color.Red : Color.Green);
        }

        public bool CheckCollision(Vector2 point)
        {
            return GetDistanceSquare(Center, point) <= (Radius * Radius);
        }

        public bool CheckCollision(IGameObject obj)
        {
            if (obj is GameObject)
            {
                var dSquare = (float)Math.Pow((Radius + (obj as GameObject).Radius), 2);
                var centerDistance = GetDistanceSquare(Center, (obj as GameObject).Center);

                return centerDistance <= dSquare;
            }
            return false;
        }

        internal float GetDistanceSquare(Vector2 point1, Vector2 point2)
        {
            var d1 = (float)Math.Pow(point2.X - point1.X, 2.0);
            var d2 = (float)Math.Pow(point2.Y - point1.Y, 2.0);
            return d1 + d2;
        }
    }
}