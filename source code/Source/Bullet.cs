using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    public class Bullet
    {
        public Vector2 Position { get; private set; }
        public Vector2 Direction { get; private set; }
        public float Speed { get; private set; }
        public bool IsVisible { get; set; }

        public Bullet(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
            Speed = 1500;
            IsVisible = true;
        }

        public void Update(GameTime gameTime)
        {
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch,Texture2D bulletTexture)
        {
            spriteBatch.Draw(bulletTexture, Position, Color.White);
        }
    }
}