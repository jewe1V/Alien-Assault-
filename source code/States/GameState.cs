using Game;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace Game
{
    public class GameState : State
    {
        private Player player;
        private Weapon weapon;
        private Texture2D[] bgLayers;
        private List<Enemy> enemies;
        private List<Item> items;
        private readonly PlayerStates currentPlayerState = new();
        private SpriteFont font;
        private SpriteBatch spriteBatch;
        private Texture2D gameScreen;
        private State _menuState;
        private SoundEffectInstance music;
        private bool isPaused = false;
        private KeyboardState previousKeyboardState;
        private Texture2D pauseScreen; 
        private List<Enemy> deadEnemies = new List<Enemy>();

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
           : base(game, graphicsDevice, content)
        {
        }

        public override void LoadContent()
        {
            music = _content.Load<SoundEffect>("GameContent\\Sounds\\Music").CreateInstance();
            font = _content.Load<SpriteFont>("GameContent\\ScreenAndButton\\Font");
            _menuState = new MenuState(_game, _graphicsDevice, _content);
            spriteBatch = new SpriteBatch(_graphicsDevice);
            bgLayers = ContentLoad.BgLayersLoad(_content);
            gameScreen = ContentLoad.GameScreenLoad(_content);
            pauseScreen = _content.Load<Texture2D>("GameContent\\ScreenAndButton\\PauseScreen");

            enemies = new List<Enemy>
            {
                new Enemy(ContentLoad.Enemy1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(1100, 444), 120, 25, 2f, false, false),

                new Enemy(ContentLoad.Enemy1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(1800, 444), 120, 25, 1.5f, false, false),
                new Enemy(ContentLoad.Enemy2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(1700, 444), 150, 35, 3.1f, false, false),
                new Enemy(ContentLoad.Enemy1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(1700, 500), 120, 25, 4.5f, false, false),          
                new Enemy(ContentLoad.Enemy2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(1800, 500), 150, 35, 3.8f, false, false),

                new Enemy(ContentLoad.Enemy2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(3408, 538), 150, 35, 3.3f, false, false),
                new Enemy(ContentLoad.Enemy1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(5000, 438), 120, 25, 1.8f, false, false),

                new Enemy(ContentLoad.Enemy1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(7162, 438), 120, 25, 2.1f, false, false),
                new Enemy(ContentLoad.Enemy2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(6208, 438), 150, 35, 3.3f, false, false),
                new Enemy(ContentLoad.Enemy2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(7000, 644), 150, 35, 4.8f, false, false),
                new Enemy(ContentLoad.Enemy1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(7340, 744), 150, 35, 2.7f, false, false),
                new Enemy(ContentLoad.Enemy2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(6500, 604), 150, 35, 3.1f, false, false),

                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(10100, 480), 500, 50, 4.2f, false, true),
                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(10100, 644), 500, 50, 4f, false, true),

                new Enemy(ContentLoad.Alien2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(11420, 544), 400, 45, 3f, true, true),

                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(13600, 438), 500, 50, 4.2f, false, true),
                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(13600, 638), 500, 50, 4f, false, true),
                new Enemy(ContentLoad.Alien3SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(14000, 538), 400, 45, 3f, true, true),

                new Enemy(ContentLoad.Alien2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(15000, 544), 400, 45, 3.3f, true, true),
                new Enemy(ContentLoad.Alien3SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(14800, 544), 400, 45, 3f, true, true),
                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(15600, 438), 500, 50, 4.5f, false, true),
                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(15600, 638), 500, 50, 4f, false, true),

                new Enemy(ContentLoad.Alien2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(18500, 544), 400, 45, 3.2f, true, true),
                new Enemy(ContentLoad.Alien3SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(18600, 544), 400, 45, 3f, true, true),
                new Enemy(ContentLoad.Alien2SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(18700, 544), 400, 45, 2.8f, true, true),
                new Enemy(ContentLoad.Alien3SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(18800, 544), 400, 45, 4.5f, true, true),

                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(18400, 438), 500, 50, 4.5f, false, true),
                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(18400, 488), 500, 50, 4.3f, false, true),
                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(18400, 538), 500, 50, 4.7f, false, true),
                new Enemy(ContentLoad.Alien1SpritesLoad(_content), ContentLoad.EnemySound(_content), new Vector2(18400, 638), 500, 50, 4.2f, false, true),

            };

            player = new Player(ContentLoad.PlayerSpritesLoad(_content), ContentLoad.PlayerSound(_content), currentPlayerState);
            weapon = new Weapon(30, ContentLoad.BulletTextureLoad(_content), ContentLoad.NoAmmoSound(_content), player, currentPlayerState);
            items = new List<Item>
            {
                new Item(ContentLoad.AidTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(3450, 644), ItemType.Aid, player, weapon),
                new Item(ContentLoad.StoreUpgradeTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(1830, 644), ItemType.AmmoUpgrade, player, weapon),

                new Item(ContentLoad.AidTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(9698, 644), ItemType.Aid, player, weapon),
                new Item(ContentLoad.StoreUpgradeTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(9698, 744), ItemType.AmmoUpgrade, player, weapon),
                new Item(ContentLoad.ArmorTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(9798, 644), ItemType.Armor, player, weapon),
                new Item(ContentLoad.GunUpgradeTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(9798, 744), ItemType.WeaponUpgrade, player, weapon),

                new Item(ContentLoad.AidTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(12350, 644), ItemType.Aid, player, weapon),

                new Item(ContentLoad.ArmorTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(16000, 644), ItemType.Armor, player, weapon),
                new Item(ContentLoad.ArmorTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(17500, 644), ItemType.Armor, player, weapon),
                new Item(ContentLoad.GunUpgradeTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(16598, 644), ItemType.WeaponUpgrade, player, weapon),
                new Item(ContentLoad.GunUpgradeTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(17598, 644), ItemType.WeaponUpgrade, player, weapon),
                new Item(ContentLoad.GunUpgradeTextureLoad(_content), ContentLoad.CollectSound(_content), new Vector2(17598, 744), ItemType.WeaponUpgrade, player, weapon),
            };
        }

        public override void Update(GameTime gameTime)
        {
            Enemy lastKilledEnemy = enemies.LastOrDefault(enemy => !enemy.IsAlive);
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
            {
                isPaused = !isPaused;
            }
            previousKeyboardState = keyboardState;

            if (!isPaused) 
            {
                music.IsLooped = true;
                music.Play();
                if (player.isDead || enemies is null)
                    music.Stop();

                foreach (var enemy in enemies)
                {
                    enemy.Update(gameTime, player);
                    if (!enemy.IsAlive)
                    {
                        deadEnemies.Add(enemy);
                    }
                }

                foreach (var deadEnemy in deadEnemies)
                {
                    enemies.Remove(deadEnemy);
                }

                foreach (var item in items)
                {
                    item.Update();
                }
                weapon.Update(gameTime, enemies);
                player.Update(gameTime);
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            if (player.animationTimer >= 5f)
            {
                _game.ChangeState(_menuState);
            }

            if (enemies.Count == 0)
            {
                _game.ChangeState(new WinState(_game, _graphicsDevice, _content));
                music.Stop();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isPaused)
            {
                var fixedImagePosition = ParallaxBackground.CalculateImageScreenPosition(new Vector2(0, 0), player.position);
                var fixedHealthPosition = ParallaxBackground.CalculateImageScreenPosition(new Vector2(1410, 15), player.position);
                var fixedAmmoPosition = ParallaxBackground.CalculateImageScreenPosition(new Vector2(1420, 75), player.position);

                ParallaxBackground.Draw(player, this.spriteBatch, bgLayers);

                this.spriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(600 - MathHelper.Clamp(player.position.X, 600, 19200), 0, 0));

                foreach (var item in items)
                {
                    item.Draw(this.spriteBatch);
                }

                foreach (var enemy in enemies)
                {
                    enemy.Draw(this.spriteBatch, gameTime, player);
                }

                weapon.Draw(this.spriteBatch);
                player.Draw(this.spriteBatch, gameTime);
                this.spriteBatch.DrawString(font, $"{player.health}" + "", fixedHealthPosition, Color.Red);
                this.spriteBatch.DrawString(font, weapon.currentAmmo + "/" + weapon.maxAmmo, fixedAmmoPosition, Color.White);
                this.spriteBatch.Draw(gameScreen, fixedImagePosition, Color.White);
                this.spriteBatch.End();
            }
            else
            {
                this.spriteBatch.Begin();
                this.spriteBatch.Draw(pauseScreen, new Vector2(0, 0), Color.White);
                this.spriteBatch.End();
            }
        }
    }
}
