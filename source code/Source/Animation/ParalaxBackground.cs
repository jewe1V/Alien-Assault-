using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    public class ParallaxBackground
    {
        public static void Draw(Player player, SpriteBatch spriteBatch, Texture2D[] bgLayers)
        {
            var spaceRestrict = MathHelper.Clamp(player.position.X, 600, 19200);
            var offset = 600 - spaceRestrict;
            List<float> coefficients = new() { 0.2f, 0.4f, 0.6f, 1.0f };

            for (int i = 0; i < bgLayers.Length; i++)
            {
                var transform = Matrix.CreateTranslation(offset * coefficients[i], 0, 0);
                spriteBatch.Begin(transformMatrix: transform);

                for (int j = 0; j < 4; j++)
                {
                    spriteBatch.Draw(bgLayers[i], new Vector2(j * 6400, 0), Color.White);
                }
                spriteBatch.End();
            }
        }
        public static Vector2 CalculateImageScreenPosition(Vector2 imagePosition, Vector2 playerPosition)
        {
            float cameraAdjustmentX = 600 - MathHelper.Clamp(playerPosition.X, 600, 19200);
            Vector2 correctedPosition = new(imagePosition.X - cameraAdjustmentX, imagePosition.Y);

            return correctedPosition;
        }
    }
}