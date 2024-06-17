using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game
{
    /// <summary>
    /// Загрузка игрового контента
    /// </summary>
    public class ContentLoad
    {
        public static Texture2D[] BgLayersLoad(ContentManager Content)
        {
            return new Texture2D[]
            {
                Content.Load<Texture2D>("GameContent\\Textures\\Background\\sky"),
                Content.Load<Texture2D>("GameContent\\Textures\\Background\\panels"),
                Content.Load<Texture2D>("GameContent\\Textures\\Background\\brick_construction"),
                Content.Load<Texture2D>("GameContent\\Textures\\Background\\road")
            };
        }

        public static Texture2D[] PlayerSpritesLoad(ContentManager Content)
        {
            return new Texture2D[]
            {
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Idle"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Idle_reverse"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Walk"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Walk_reverse"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Run"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Run_reverse"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Shot"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Shot_reverse"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Recharge"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Recharge_reverse"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Hurt"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\MainHero\\Dead")
            };
        }

        public static Texture2D[] Enemy1SpritesLoad(ContentManager Content)
        {
            return new Texture2D[]
            {
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_1\\Attack"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_1\\Attack_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_1\\Idle"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_1\\Idle_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_1\\Walk"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_1\\Walk_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_1\\Dead"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_1\\Hurt"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_1\\Hurt_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\Alien2Charge")
            }; 
        }
        public static Texture2D[] Enemy2SpritesLoad(ContentManager Content)
        {
            return new Texture2D[]
            {
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_2\\Attack"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_2\\Attack_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_2\\Idle"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_2\\Idle_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_2\\Walk"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_2\\Walk_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_2\\Dead"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_2\\Hurt_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Enemy_2\\Hurt"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\Alien2Charge")
            };
        }
        public static Texture2D[] Alien1SpritesLoad(ContentManager Content)
        {
            return new Texture2D[]
            {
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_1\\Attack"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_1\\Attack_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_1\\Idle"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_1\\Idle_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_1\\Walk"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_1\\Walk_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_1\\Dead"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_1\\Hurt"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_1\\Hurt_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\Alien2Charge")
            };
        }

        public static Texture2D[] Alien2SpritesLoad(ContentManager Content)
        {
            return new Texture2D[]
            {
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_2\\Attack"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_2\\Attack_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_2\\Idle"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_2\\Idle_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_2\\Walk"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_2\\Walk_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_2\\Dead"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_2\\Hurt"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_2\\Hurt_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\Alien2Charge")
            };
        }

        public static Texture2D[] Alien3SpritesLoad(ContentManager Content)
        {
            return new Texture2D[]
            {
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_3\\Attack"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_3\\Attack_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_3\\Idle"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_3\\Idle_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_3\\Walk"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_3\\Walk_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_3\\Dead"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_3\\Hurt"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Alien_3\\Hurt_reversed"),
                Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\Alien3Charge")
            };
        }

        public static SoundEffect[] PlayerSound(ContentManager Content)
        {
            return new SoundEffect[]
            {
                Content.Load<SoundEffect>("GameContent\\Sounds\\Reload"),
                Content.Load<SoundEffect>("GameContent\\Sounds\\PlayerHurt"),
                Content.Load<SoundEffect>("GameContent\\Sounds\\PlayerShoot"),
            };
        }

        public static SoundEffect[] EnemySound(ContentManager Content)
        {
            return new SoundEffect[]
            {
                Content.Load<SoundEffect>("GameContent\\Sounds\\EnemyHurt"),
                Content.Load<SoundEffect>("GameContent\\Sounds\\AlienHurt"),
                Content.Load<SoundEffect>("GameContent\\Sounds\\EnemyShoot"),
            };
        }

        public static SoundEffect CollectSound(ContentManager Content) =>
            Content.Load<SoundEffect>("GameContent\\Sounds\\Collect");

        public static Texture2D BulletTextureLoad(ContentManager Content) =>
            Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\Bullet");

        public static Texture2D AidTextureLoad(ContentManager Content) =>
            Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\Aid");

        public static Texture2D ArmorTextureLoad(ContentManager Content) =>
            Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\Armor");

        public static Texture2D GunUpgradeTextureLoad(ContentManager Content) =>
            Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\GunUpgrade");

        public static Texture2D StoreUpgradeTextureLoad(ContentManager Content) =>
            Content.Load<Texture2D>("GameContent\\Textures\\GameSprites\\Stuff\\StoreUpgrade");

        public static Texture2D GameScreenLoad(ContentManager Content) =>
            Content.Load<Texture2D>("GameContent\\ScreenAndButton\\GameScreen");

        public static SoundEffect NoAmmoSound(ContentManager Content) =>
            Content.Load<SoundEffect>("GameContent\\Sounds\\NoAmmo");
    }
}