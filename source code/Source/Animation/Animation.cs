using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    public class Animation
    {
        readonly Texture2D spriteSheet;
        readonly int frames;
        int sheetPos = 0;
        private static readonly int width = 256;
        float timeSinceLastFrame = 0;

        public Animation(Texture2D spriteSheet)
        {
            this.spriteSheet = spriteSheet;
            frames = spriteSheet.Width / width;
        }

        /// <summary>
        /// Прорисовка анимации спрайта
        /// </summary>
        /// <param name="spriteBatch">Текстура с партией "кадров" спрайтов</param>
        /// <param name="position">Позиция</param>
        /// <param name="gameTime">Игровое время</param>
        /// <param name="mileSecondsPerFrames">Время промежутков между кадрами в мс</param>
        public void DrawAnimation(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime, float mileSecondsPerFrames)
        {
            if (sheetPos < frames)
            {
                var rectangle = new Rectangle(256 * sheetPos, 0, 256, 256);
                spriteBatch.Draw(spriteSheet, position, rectangle, Color.White);

                timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timeSinceLastFrame > mileSecondsPerFrames)
                {
                    timeSinceLastFrame -= mileSecondsPerFrames;
                    sheetPos++;
                    if (sheetPos == frames) { sheetPos = 0; }
                }
            }
        }

        /// <summary>
        /// Принудительная установка текущего кадра анимации на начальный
        /// </summary>
        public void StopAnimation()
        {
            sheetPos = 0;
        }

        /// <summary>
        /// Принудительная установка текущего кадра анимации на конечный(когда герой погиб)
        /// </summary>
        public void EndAnimation()
        {
            sheetPos = 3;
        }
    }
}
