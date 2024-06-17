using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Microsoft.Xna.Framework.Audio;

namespace Game
{
    public class Weapon
    {
        public int Damage;
        private static Texture2D BulletTexture;
        public int maxAmmo = 6;
        public int currentAmmo;
        public float ReloadTime = 1.47f;
        private float reloadTimer;
        private float timeSinceLastShot;
        private bool canShoot = true;
        private readonly float shotInterval = 0.65f;
        public List<Bullet> bullets;
        public bool IsActive;
        private readonly Player player;
        private readonly PlayerStates currentPlayerState;
        private MouseState previousMouseState;
        private readonly SoundEffect sound;

        public Weapon(int damage, Texture2D bulletTexture, SoundEffect sound, Player player, PlayerStates currentPlayerState)
        {
            Damage = damage;
            BulletTexture = bulletTexture;
            currentAmmo = maxAmmo;
            bullets = new List<Bullet>();
            IsActive = true;
            timeSinceLastShot = shotInterval;
            this.player = player;
            this.currentPlayerState = currentPlayerState;
            this.sound = sound;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeSinceLastShot >= shotInterval)
            {
                canShoot = true;
                currentPlayerState.IsShooting = false;
                player.ignoreKeyboardInput = false;
            }
            UpdateBullets(gameTime, enemies);
            Use(gameTime);
            Recharge(gameTime);
        }

        public void Use(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (CanFire() && mouseState.LeftButton == ButtonState.Pressed
                && previousMouseState.LeftButton == ButtonState.Released
                && !player.isDead)
            {
                Shoot(player);
            }
            if (NeedsReloading() && keyboard.IsKeyDown(Keys.R) && !player.isDead)
            {
                StartReloading(gameTime);
            }
            if(currentAmmo == 0 && mouseState.LeftButton == ButtonState.Pressed
                && previousMouseState.LeftButton == ButtonState.Released)
            {
                sound.Play();
            }
            previousMouseState = mouseState;
        }

        private bool CanFire() => currentAmmo > 0 && canShoot && !currentPlayerState.IsReloading;

        private void Shoot(Player player)
        {
            Vector2 bulletStartPosition = GetBulletStartPosition();
            Vector2 shootDirection = GetShootDirection(player);
            player.ignoreKeyboardInput = true;
            bullets.Add(new Bullet(bulletStartPosition, shootDirection));
            canShoot = false;
            currentPlayerState.SetShooting();
            currentAmmo--;
            timeSinceLastShot = 0f;
            Console.WriteLine(currentAmmo);
        }

        private Vector2 GetBulletStartPosition() =>
            new(player.position.X + 40, player.position.Y + 146);

        private static Vector2 GetShootDirection(Player player) =>
            player.isRight ? new Vector2(1, 0) : new Vector2(-1, 0);

        private bool NeedsReloading() => currentAmmo < maxAmmo && !currentPlayerState.IsReloading;

        private void StartReloading(GameTime gameTime)
        {
            currentPlayerState.IsReloading = true;
            reloadTimer = 0f;
            Recharge(gameTime);
        }

        public void Recharge(GameTime gameTime)
        {
            if (currentPlayerState.IsReloading)
            {
                reloadTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (reloadTimer >= ReloadTime)
                {
                    currentAmmo = maxAmmo;
                    currentPlayerState.IsReloading = false;
                    player.ignoreKeyboardInput = false;
                }
                player.ignoreKeyboardInput = true;
            }
        }

        private void UpdateBullets(GameTime gameTime, List<Enemy> enemies)
        {
            foreach (var bullet in bullets)
            {
                bullet.Update(gameTime);
                if (Math.Abs(bullet.Position.X - player.position.X) > 950)
                    bullet.IsVisible = false;
            }
            HandleCollisions(enemies);
            bullets.RemoveAll(bullet => !bullet.IsVisible);
        }

        private void HandleCollisions(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (CheckCollisionWithEnemy(enemy))
                {
                    bullets.RemoveAll(bullet => enemy.collisionRectangle.Contains(bullet.Position));
                    enemy.currentHealth -= Damage;
                    enemy.isHurt = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) =>
            bullets.ForEach(bullet => bullet.Draw(spriteBatch, BulletTexture));

        public bool CheckCollisionWithEnemy(Enemy enemy) =>
            bullets.Any(bullet => enemy.collisionRectangle.Contains(bullet.Position));
    }
}