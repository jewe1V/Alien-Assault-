using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Game
{
    public class WinState : State
    {
        private readonly Texture2D _backgroundTexture;
        private float timer = 0;

        public WinState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("GameContent\\ScreenAndButton\\Win");
        }
        public override void LoadContent()
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            spriteBatch.End();
        }

        private void BackToMenu_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= 2) 
            {
                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
                }
            }
            Console.WriteLine(timer);
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
