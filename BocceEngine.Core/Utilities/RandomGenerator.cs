using System;
using BocceEngine.Core.EngineComponents;
using Microsoft.Xna.Framework;

namespace BocceEngine.Core.Utilities
{
    public class RandomGenerator : GameComponent, IEngineObject
    {
        private readonly Random _generator;

		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }

        public RandomGenerator(Game game) : base(game)
        {
			IsDebugMode = false;
			IsUpdateable = false;
			//UpdateOrder = base.UpdateOrder;
			IsDrawable = false;
			DrawOrder = base.UpdateOrder;
            _generator = new Random(DateTime.Now.Millisecond);
        }

        /*public T GetRandom<T>(T min, T max)
        {
        }*/

        public int RandomInt
        {
            get { return _generator != null ? _generator.Next(int.MaxValue) : 0; }
        }
    }
}
