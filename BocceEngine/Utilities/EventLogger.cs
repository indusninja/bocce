using BocceEngine.EngineComponents;
using Microsoft.Xna.Framework;

namespace BocceEngine.Utilities
{
    public class EventLogger : GameComponent, IEngineObject
    {
		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }

        public EventLogger(Game game)
            : base(game)
        {
			IsDebugMode = false;
			IsUpdateable = true;
			//UpdateOrder = base.UpdateOrder;
			IsDrawable = false;
			DrawOrder = base.UpdateOrder;
            /*var writer = new StreamWriter(@"C:\documents\logfiles\file.txt");
            writer.WriteLine("Line 1");
            writer.Dispose();*/
        }
    }
}
