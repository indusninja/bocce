using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BocceEngine.Core.EngineComponents
{
    public interface IGameObject
    {
        string ObjectId { get; }
        float Scale { get; set; }
        Color Tint { get; set; }
        Texture2D Texture { get; set; }
        float Speed { get; set; }
        Vector2 Center { get; set; }
        Vector2 Origin { get; }
        float Radius { get; }
        void Draw(SpriteBatch spriteBatch);
        bool CheckCollision(Vector2 obj);
        bool CheckCollision(IGameObject obj);
        void RegisterCollision(IGameObject obj);
        void UnregisterCollision();
    }
}
