using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game
{
    public class TutorialState : State
    {
        private readonly List<Component> _components;
        private readonly Texture2D _backgroundTexture;

        public TutorialState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            _backgroundTexture = _content.Load<Texture2D>("GameContent\\ScreenAndButton\\HowToPlayScreen");
            var font = _content.Load<SpriteFont>("GameContent\\ScreenAndButton\\Font");
            var backButtonTexture = _content.Load<Texture2D>("GameContent\\ScreenAndButton\\BackButton");
            var click = _content.Load<SoundEffect>("GameContent\\Sounds\\Click");

            var newGameButton = new Button(backButtonTexture, click)
            {
                Position = new Vector2(1235, 750)
            };

            newGameButton.Click += BackToMenu_Click;

            _components = new List<Component>()
            {
                newGameButton,
            };

        }
        public override void LoadContent()
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, new Vector2(0, 0), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void BackToMenu_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }
    }
}
