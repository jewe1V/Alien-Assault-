using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    public enum ItemType
    {
        Armor,
        Aid,
        AmmoUpgrade,
        WeaponUpgrade
    }

    public class Item
    {
        public Vector2 Position { get; private set; }
        public Texture2D Texture { get; private set; }
        public ItemType Type { get; private set; }
        public bool IsCollected { get; private set; } = false;

        public Rectangle collisionRectangle;
        
        public Player player;
        public Weapon weapon;
        public SoundEffect sound;

        public Item(Texture2D texture, SoundEffect sound, Vector2 position, ItemType type, Player player, Weapon weapon)
        {
            Texture = texture;
            Position = position;
            Type = type;

            this.player = player;
            this.weapon = weapon;
            this.sound = sound;
            collisionRectangle = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
        }

        /// <summary>
        /// применение эффекта
        /// </summary>
        /// <param name="player">Игрок</param>
        public void Update()
        {
            var playerCollisionRectangle = new Rectangle(player.collisionRectangle.X, player.collisionRectangle.Y + 116, 32, 32);
            if (collisionRectangle.Intersects(playerCollisionRectangle))
            {
                ApplyEffect(player);
            }
        }

        /// <summary>
        /// Метод для применения эффекта предмета на игрока
        /// </summary>
        /// <param name="player">Игрок</param>
        public void ApplyEffect(Player player)
        {
            if (IsCollected) return;

            switch (Type)
            {
                case ItemType.Armor:
                    sound.Play();
                    player.health = 200;
                    IsCollected = true;
                    break;
                case ItemType.Aid:
                    sound.Play();
                    player.health = Math.Max(player.health, player.maxHealth);
                    IsCollected = true;
                    break;
                case ItemType.AmmoUpgrade:
                    sound.Play();
                    weapon.maxAmmo += 1;
                    IsCollected = true;
                    break;
                case ItemType.WeaponUpgrade:
                    sound.Play();
                    weapon.Damage += 45;
                    IsCollected = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// изображение предмета на экране
        /// </summary>
        /// <param name="spriteBatch">спрайт</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!IsCollected)
            {
                spriteBatch.Draw(Texture, Position, Color.White);
            }
        }
    }
}
