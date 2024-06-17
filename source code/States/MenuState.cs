using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Game
{
    public class MenuState : State
    {
        private readonly List<Component> _components;
        private readonly Texture2D MenuBackgroundTexture; 

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            MenuBackgroundTexture = _content.Load<Texture2D>("GameContent\\ScreenAndButton\\MenuBackground");

            var backButtonTexture = _content.Load<Texture2D>("GameContent\\ScreenAndButton\\BackButton");
            var howToPlayButtonTexture = _content.Load<Texture2D>("GameContent\\ScreenAndButton\\HowToPlayButton");
            var playButtonTexture = _content.Load<Texture2D>("GameContent\\ScreenAndButton\\PlayButton");
            var quitButtonTexture = _content.Load<Texture2D>("GameContent\\ScreenAndButton\\QuitButton");
            var click = _content.Load<SoundEffect>("GameContent\\Sounds\\Click");

            var newGameButton = new Button(playButtonTexture, click)
            {
                Position = new Vector2(1235, 380)
            };

            newGameButton.Click += NewGameButton_Click;

            var howToPlayButton = new Button(howToPlayButtonTexture, click)
            {
                Position = new Vector2(1235, 510)
            };

            howToPlayButton.Click += HowToPlayButton_Click;

            var quitGameButton = new Button(quitButtonTexture, click)
            {
                Position = new Vector2(1235, 750)
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
            {
                newGameButton,
                howToPlayButton,
                quitGameButton,
            };

        }
        public override void LoadContent()
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(MenuBackgroundTexture, new Vector2(0, 0), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void HowToPlayButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new TutorialState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
