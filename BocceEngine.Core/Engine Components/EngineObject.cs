using Microsoft.Xna.Framework;

namespace BocceEngine.Core.EngineComponents
{
    public class EngineObject : GameComponent
    {
        public bool IsDrawable { get; set; }

        public EngineObject(Game game)
            : base(game)
        {
            IsDrawable = false;
        }
    }
}
