using System.Linq;
using BocceEngine.Core.EngineComponents;
using Microsoft.Xna.Framework;

namespace BocceEngine.Core.Physics
{
    public class CollisionSolver : GameComponent, IEngineObject
    {
        private readonly Game _parentGame;

		public bool IsDebugMode { get; set; }
		public bool IsDrawable { get; set; }
		public int DrawOrder { get; set; }
		public bool IsUpdateable { get; set; }

        public CollisionSolver(Game game)
            : base(game)
        {
			IsDebugMode = false;
			IsUpdateable = true;
			//UpdateOrder = base.UpdateOrder;
			IsDrawable = false;
			DrawOrder = base.UpdateOrder;
            _parentGame = game;
        }

        public override void Update(GameTime gameTime)
        {
            var filteredObjectList = (from IEngineObject obj in _parentGame.Components where obj.IsDrawable && (obj is IGameObject) select obj as IGameObject).ToList();

            for (var i = 0; i < filteredObjectList.Count; i++)
            {
                var collided = false;
                for (var j = 0; j < filteredObjectList.Count; j++)
                {
                    if (i == j) continue;
                    if (!filteredObjectList[i].CheckCollision(filteredObjectList[j])) continue;
                    filteredObjectList[i].RegisterCollision(filteredObjectList[j]);
                    collided = true;
                }
                if (!collided)
                    filteredObjectList[i].UnregisterCollision();
            }

            base.Update(gameTime);
        }
    }
}
