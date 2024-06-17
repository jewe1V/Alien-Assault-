using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    public class Enemy
    {
        public Vector2 position;
        public Vector2 velocity = new(1, 1);
        public Animation[] enemyAnimation;
        public SoundEffect[] sounds;
        public Rectangle collisionRectangle;
        private readonly float patrolDistance;
        private float traveledDistance = 0;
        public int currentHealth;
        public int damage;
        public bool IsAlive;
        public bool isMoving = true;
        public float Speed;
        private int maxHealth;
        public bool isHurt = false;
        public bool canMove = true;
        private Vector2 shootDirection;
        private readonly bool isShootType;
        private readonly Texture2D bulletTexture;
        private bool haveToShoot;
        private readonly float shotInterval = 0.5f;
        private float timeSinceLastShot;
        private readonly float animationDelay = 1f;
        private float timeSinceLastAnimation = 0.0f;
        private Vector2 direction;
        private bool wasEnemyHurting = false;
        private bool wasAlienHurting = false;
        private readonly bool isAlien;

        public List<Bullet> bullets;
        private bool movingDown = true;
        public float hurt = 0;

        public Enemy(Texture2D[] sprites, SoundEffect[] sounds, Vector2 _position, int health, int damage, float speed, bool isShootType, bool isAlien)
        {
            enemyAnimation = new Animation[14];

            for (int i = 0; i < sprites.Length; i++)
            {
                enemyAnimation[i] = new Animation(sprites[i]);
            }

            position = _position;
            patrolDistance = 220;
            maxHealth = health;
            currentHealth = maxHealth;
            IsAlive = true;
            Speed = speed;
            this.damage = damage;
            this.isShootType = isShootType;
            bulletTexture = sprites[9];
            bullets = new List<Bullet>();
            timeSinceLastShot = shotInterval;
            this.sounds = sounds;
            this.isAlien = isAlien;
        }

        private void PlaySounds()
        {
            if (isHurt && !isShootType && !wasEnemyHurting)
                sounds[0].Play();
            wasEnemyHurting = isHurt;

            if (isHurt && (isShootType || isAlien) && (!wasAlienHurting || !wasEnemyHurting))
                sounds[1].Play();
            wasAlienHurting = isHurt;
        }

        public void Update(GameTime gameTime, Player player)
        {
            IndicateRestrictions();

            if (currentHealth <= 0)
                IsAlive = false;

            if (!IsAlive) { collisionRectangle.Width = 0; collisionRectangle.Height = 0; }

            if ((Vector2.Distance(player.position, position) < 700 || currentHealth < maxHealth) && IsAlive && canMove && !isShootType && !isHurt && !player.isDead)
            {
                MoveTowardsPlayer(player);
                InteractionWithPlayer(player);
            }
            else if (!player.isDead && isShootType && Vector2.Distance(player.position, position) < 1000)
            {
                FollowPlayer(player.position);
                timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceLastShot >= shotInterval)
                {
                    Attack(gameTime, player);
                }
                else haveToShoot = false;
                HandleCollisions(player);
            }
            else
            {
                isMoving = false;
            }

            direction = DetermineDirectionView(player);
            PauseToPlayHurtAnimation(gameTime);
            UpdateBullets(gameTime, player);
            PlaySounds();
        }

        private void InteractionWithPlayer(Player player)
        {
            if (Vector2.Distance(player.position, position) < 5 && IsAlive)
            {
                if (player.canTakeDamage)
                {
                    player.health -= damage;
                    player.canTakeDamage = false;
                    player.timeSinceLastDamage = 0;
                }
            }
        }

        private void Attack(GameTime gameTime, Player player)
        {
            if (player.position.Y == position.Y && !isHurt)
            {
                haveToShoot = true;
                timeSinceLastAnimation += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timeSinceLastAnimation >= 0.25 * animationDelay)
                {
                    if (IsAlive)
                        sounds[2].Play();
                    Shoot(player);
                    timeSinceLastAnimation = timeSinceLastShot = 0.0f;
                    haveToShoot = false;
                };
            }
            else haveToShoot = false;
        }

        public void MoveTowardsPlayer(Player player)
        {
            var playerPosition = player.position;
            Vector2 directionToPlayer = Vector2.Normalize(playerPosition - position);
            velocity = directionToPlayer * Speed;
            position += velocity;
            if (playerPosition == position)
                canMove = false;
            else if (playerPosition != position)
                canMove = true;
                haveToShoot = false;
            isMoving = Vector2.Distance(playerPosition, position) >= 60;
        }

        /// <summary>
        /// Обновление пуль
        /// </summary>
        /// <param name="gameTime">Игровое время</param>
        /// <param name="player">Игрок</param>
        private void UpdateBullets(GameTime gameTime, Player player)
        {
            foreach (var bullet in bullets)
            {
                bullet.Update(gameTime);
                if (Math.Abs(bullet.Position.X - position.X) > 1000 ||
                    Math.Abs(player.position.X - position.X) > 1000)
                    bullet.IsVisible = false;
            }
            HandleCollisions(player);
            bullets.RemoveAll(bullet => !bullet.IsVisible);
        }

        /// <summary>
        /// Метод добавляет в словарь пулю с текущей позицией врага и направлением в сторону игрока
        /// </summary>
        /// <param name="player"></param>
        public void Shoot(Player player)
        {
            shootDirection = Vector2.Normalize(new Vector2(player.position.X, player.position.Y + 25) - position);
            if (isShootType && IsAlive)
            {
                bullets.Add(new Bullet(new(position.X + 100, position.Y + 100), shootDirection));
            }
        }

        /// <summary>
        /// Обработка столкновения пули пришельца с игроком
        /// </summary>
        /// <param name="player">Игрок</param>
        private void HandleCollisions(Player player)
        {
            if (bullets.Any(bullet => player.collisionRectangle.Contains(bullet.Position)) && player.canTakeDamage)
            {
                bullets.RemoveAll(bullet => player.collisionRectangle.Contains(bullet.Position));
                player.health -= damage;
                player.canTakeDamage = false;
            }
        }

        /// <summary>
        /// Стремление повторить позицию игрока по OY
        /// </summary>
        private void FollowPlayer(Vector2 playerPosition)
        {
            Vector2 previousPosition = position;
            float deltaY = playerPosition.Y - position.Y;
            //Если враг находится выше цели, он будет двигаться вниз и наоборот
            if (Math.Abs(deltaY) > velocity.Y)
            {
                if (deltaY > 0)
                {
                    position.Y += 2 * velocity.Y;
                }
                else if (deltaY < 0)
                {
                    position.Y -= 2 * velocity.Y;
                }
            }
            //изменилась ли позиция врага
            isMoving = position != previousPosition;
        }

        private void IndicateRestrictions()
        {
            collisionRectangle = new Rectangle((int)position.X + 68, (int)position.Y + 100, 70, 128);
            var gameBounds = new Rectangle(-70, 438, 20130, 200);
            position.X = Math.Clamp(position.X, gameBounds.Left, gameBounds.Right);
            position.Y = Math.Clamp(position.Y, gameBounds.Top, gameBounds.Bottom);
        }

        /// <summary>
        /// Определение, в какую сторону будет направлен взгляд врага
        /// </summary>
        /// <param name="player">Игрок</param>
        /// <returns></returns>
        private Vector2 DetermineDirectionView(Player player) => player.position.X < position.X ? new Vector2(-1, 0) : new Vector2(1, 0);

        private void PauseToPlayHurtAnimation(GameTime gameTime)
        {
            if (isHurt)
            {
                hurt += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (hurt >= 0.35 * animationDelay)
            {
                isHurt = false;
                hurt = 0;
            }

        }

        /// <summary>
        /// Универсальный метод для рисования анимации врагов
        /// </summary>
        /// <param name="spriteBatch">спрайт</param>
        /// <param name="gameTime">игровое время</param>
        /// <param name="player">игрок</param>
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Player player)
        {
            if (IsAlive)
            {
                DrawCharge(spriteBatch);
                if (!isShootType) DrawCommonType(spriteBatch, gameTime, player);
                else DrawShootingType(spriteBatch, gameTime, player);
            }
        }

        /// <summary>
        /// инструкции для прорисовки анимации обычным врагам
        /// </summary>
        private void DrawCommonType(SpriteBatch spriteBatch, GameTime gameTime, Player player)
        {
            if (isHurt && direction == new Vector2(1, 0))
                enemyAnimation[7].DrawAnimation(spriteBatch, position, gameTime, 140);
            else if (isHurt && direction == new Vector2(-1, 0))
                enemyAnimation[8].DrawAnimation(spriteBatch, position, gameTime, 140);
            else if (velocity.X > 0 && isMoving && canMove)
                enemyAnimation[4].DrawAnimation(spriteBatch, position, gameTime, 115);
            else if (velocity.X < 0 && isMoving && canMove)
                enemyAnimation[5].DrawAnimation(spriteBatch, position, gameTime, 115);
            else if (Vector2.Distance(player.position, position) < 60)
                enemyAnimation[1].DrawAnimation(spriteBatch, position, gameTime, 130);
            else if (!isMoving)
                enemyAnimation[3].DrawAnimation(spriteBatch, position, gameTime, 135);
        }

        /// <summary>
        /// инструкции для прорисовки анимации врагам, умеющим стрелять
        /// </summary>
        private void DrawShootingType(SpriteBatch spriteBatch, GameTime gameTime, Player player)
        {
            if (haveToShoot && direction == new Vector2(-1, 0) && Math.Abs(player.position.X - position.X) < 1000)
                enemyAnimation[1].DrawAnimation(spriteBatch, position, gameTime, 120);
            else if (haveToShoot && direction == new Vector2(1, 0) && Math.Abs(player.position.X - position.X) < 1000)
                enemyAnimation[0].DrawAnimation(spriteBatch, position, gameTime, 120);
            else if (isHurt && direction == new Vector2(1, 0))
                enemyAnimation[7].DrawAnimation(spriteBatch, position, gameTime, 100);
            else if (isHurt && direction == new Vector2(-1, 0))
                enemyAnimation[8].DrawAnimation(spriteBatch, position, gameTime, 100);
            else if (direction == new Vector2(-1, 0) && isMoving)
                enemyAnimation[5].DrawAnimation(spriteBatch, position, gameTime, 100);
            else if (direction == new Vector2(1, 0) && isMoving)
                enemyAnimation[4].DrawAnimation(spriteBatch, position, gameTime, 100);
            else if (!isMoving && direction == new Vector2(-1, 0))
                enemyAnimation[3].DrawAnimation(spriteBatch, position, gameTime, 135);
            else if (!isMoving && direction == new Vector2(1, 0))
                enemyAnimation[2].DrawAnimation(spriteBatch, position, gameTime, 135);
            else if (!IsAlive)
                enemyAnimation[6].DrawAnimation(spriteBatch, position, gameTime, 135);
        }

        /// <summary>
        /// прорисовка заряда, которым враг стреляет 
        /// </summary>
        private void DrawCharge(SpriteBatch spriteBatch) =>
            bullets.ForEach(bullet => bullet.Draw(spriteBatch, bulletTexture));

        /// <summary>
        /// метод для передвижения "туда-сюда"
        /// </summary>
        private void SimpleMove()
        {
            if (movingDown)
            {
                position.Y += velocity.Y;
                traveledDistance += Math.Abs(velocity.Y);

                if (traveledDistance >= patrolDistance)
                {
                    movingDown = false;
                    traveledDistance = 0;
                }
            }
            else
            {
                position.Y -= velocity.Y;
                traveledDistance += Math.Abs(velocity.Y);

                if (traveledDistance >= patrolDistance)
                {
                    movingDown = true;
                    traveledDistance = 0;
                }
            }
        }
    }
}
