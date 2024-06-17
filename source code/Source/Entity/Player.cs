using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game
{
    public class Player
    {
        public Vector2 position;
        private readonly float velocity = 2;
        private readonly Animation[] playerAnimation;
        private readonly PlayerStates currentState;
        public bool ignoreKeyboardInput;
        public Rectangle collisionRectangle;
        public int maxHealth = 100;
        public int health;
        public bool canTakeDamage = true;
        public bool isRight;
        private float mousePos;
        public SoundEffect[] sounds;
        private bool wasReloading = false;
        private bool wasShooting = false;
        private bool wasHurting = false;
        public bool isDead = false;
        public float animationTimer = 0;
        public double timeSinceLastDamage = 0;

        private KeyboardState keyboard;
        private MouseState mouseState;

        public Player(Texture2D[] sprites, SoundEffect[] sounds, PlayerStates currentState)
        {
            playerAnimation = new Animation[14];
            for (int i = 0; i < sprites.Length; i++)
            {
                playerAnimation[i] = new Animation(sprites[i]);
            }
            position = new Vector2(50, 50);
            this.currentState = currentState;

            health = maxHealth;
            this.sounds = sounds;
        }

        public void Update(GameTime gameTime)
        {
            Console.WriteLine(position);
            collisionRectangle = new Rectangle((int)position.X + 65, (int)position.Y + 90, 70, 130);
            UpdateInputStates();
            if (!ignoreKeyboardInput)
                currentState.SetIdle();
            UpdateMousePosition();
            ProcessMovement();
            CheckDamageState();
            ClampPlayerPosition();
            HealthUpdate(gameTime);
            PlaySounds();
            if (health <= 0)
                isDead = true;
        }

        private void HealthUpdate(GameTime gameTime)
        {

            if (!canTakeDamage)
            {
                timeSinceLastDamage += gameTime.ElapsedGameTime.TotalSeconds;
                if (timeSinceLastDamage > 3)
                {
                    canTakeDamage = true;
                    timeSinceLastDamage = 0;
                }
            }

            if (health <= 0)
            {
                health = 0;
            }
        }

        private void PlaySounds()
        {
            if (currentState.IsReloading && !wasReloading)
                sounds[0].Play();
            wasReloading = currentState.IsReloading;

            if (currentState.IsHurting && !wasHurting)
                sounds[1].Play();
            wasHurting = currentState.IsHurting;

            if (currentState.IsShooting && !wasShooting)
                sounds[2].Play();
            wasShooting = currentState.IsShooting;
        }

        private void UpdateInputStates()
        {
            keyboard = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        /// <summary>
        /// Смещение позиции мыши, когда игрок убегает за "пределы" статичного экрана
        /// </summary>
        private void UpdateMousePosition()
        {
            mousePos = position.X > 800 ? mouseState.X + position.X - 700 : mouseState.X;
            isRight = mousePos > position.X;
        }

        private void ProcessMovement()
        {
            bool isLeftShiftPressed = keyboard.IsKeyDown(Keys.LeftShift);
            if (!ignoreKeyboardInput && !currentState.IsShooting && !isDead)
            {
                if (keyboard.IsKeyDown(Keys.A))
                    MoveLeft(isLeftShiftPressed);
                if (keyboard.IsKeyDown(Keys.D))
                    MoveRight(isLeftShiftPressed);
                if (keyboard.IsKeyDown(Keys.W))
                    MoveUp(isLeftShiftPressed);
                if (keyboard.IsKeyDown(Keys.S))
                    MoveDown(isLeftShiftPressed);
            }
        }

        private void MoveLeft(bool isRunning)
        {
            currentState.SetLeft();
            position.X -= velocity;
            currentState.SetMoving();

            if (isRunning)
            {
                position.X -= 2 * velocity;
                currentState.SetRunning();
            }
        }

        private void MoveRight(bool isRunning)
        {
            position.X += velocity;
            currentState.SetRight();
            currentState.SetMoving();

            if (isRunning)
            {
                position.X += 2 * velocity;
                currentState.SetRunning();
            }
        }

        private void MoveUp(bool isRunning)
        {
            position.Y -= velocity;
            currentState.SetMoving();
            if (isRunning)
            {
                position.Y -= 2 * velocity;
                currentState.SetRunning();
            }
        }

        private void MoveDown(bool isRunning)
        {
            position.Y += velocity;
            currentState.SetMoving();

            if (isRunning)
            {
                position.Y += 2 * velocity;
                currentState.SetRunning();
            }
        }

        /// <summary>
        /// Пока игрок атакован, он некоторое время не может получать урон,
        /// устанавливается состояние получения урона
        /// </summary>
        private void CheckDamageState()
        {
            if (!canTakeDamage)
            {
                currentState.SetHurting();
            }
        }

        /// <summary>
        /// Ограничение передвижения игрока 
        /// </summary>
        private void ClampPlayerPosition()
        {
            var gameBounds = new Rectangle(-70, 438, 20130, 200);
            position.X = MathHelper.Clamp(position.X, gameBounds.X, gameBounds.X + gameBounds.Width);
            position.Y = MathHelper.Clamp(position.Y, gameBounds.Y, gameBounds.Y + gameBounds.Height);
        }

        //public bool Collide(Enemy enemy) => collisionRectangle.Intersects(enemy.collisionRectangle);

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            int animationOffset = isRight ? 0 : 1;

            if (!isDead)
            {
                if (currentState.IsReloading)
                    DrawAnimation(spriteBatch, gameTime, animationOffset + 8, 120);
                else if (currentState.IsMoving)
                    DrawAnimation(spriteBatch, gameTime, animationOffset + 2, 120);
                else if (currentState.IsRunning)
                    DrawAnimation(spriteBatch, gameTime, animationOffset + 4, 80);
                else if (currentState.IsShooting)
                    DrawAnimation(spriteBatch, gameTime, animationOffset + 6, 95);
                else if (currentState.IsIdle)
                    DrawAnimation(spriteBatch, gameTime, animationOffset, 200);
                else if (currentState.IsHurting)
                    DrawAnimation(spriteBatch, gameTime, 10, 100);
            }
            else if (isDead)
            {
                DrawAnimation(spriteBatch, gameTime, 11, 180);
                animationTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (animationTimer >= 0.7f)
                    playerAnimation[11].EndAnimation();
            }
            ignoreKeyboardInput = false;
        }

        /// <summary>
        /// Универсальный метод для запуска анимации
        /// </summary>
        /// <param name="spriteBatch">Спрайт</param>
        /// <param name="gameTime">Игровое время</param>
        /// <param name="animationIndex">Индекс анимации в массиве
        /// (Для правой стороны четный индекс, для левой нечетный)</param>
        /// <param name="animationSpeed">Скорость смены кадров</param>
        private void DrawAnimation(SpriteBatch spriteBatch, GameTime gameTime, int animationIndex, int animationSpeed)
        {
            playerAnimation[animationIndex].DrawAnimation(spriteBatch, position, gameTime, animationSpeed);
            if (!currentState.IsShooting) playerAnimation[7].StopAnimation();
            if (!currentState.IsShooting) playerAnimation[6].StopAnimation();
            if (!currentState.IsReloading) playerAnimation[8].StopAnimation();
            if (!currentState.IsReloading) playerAnimation[9].StopAnimation();
        }
    }
}