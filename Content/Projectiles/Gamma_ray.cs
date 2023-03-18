using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;


namespace ProjectInfinity.Content.Projectiles
{
    internal class Gamma_ray : ModProjectile
    {
        public override string Texture => AssetDirectory.Projectiles + Name;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gamma Ray");
        }
        public override void SetDefaults()
        {
            Projectile.friendly = false;
            Projectile.aiStyle = 0;
            Projectile.timeLeft = 480;
            Projectile.penetrate = 1;

        }


        public static Rectangle checkBox = new();
        public float maxDist { get; set; }
        public float Distance
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        private const float MOVE_DISTANCE = 60f;


        public int ParentIndex
        {
            get => (int)Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }


        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 c = Main.projectile[ParentIndex].Center;
            DrawLaser(Main.spriteBatch, TextureAssets.Projectile[ModContent.ProjectileType<Gamma_ray>()].Value, c,
                Projectile.velocity, 2, -1.57f, 4f, (int)MOVE_DISTANCE);
            return false;

        }



        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, int transDist = 50)
        {
            float r = unit.ToRotation() + rotation;

            // Draws the laser 'body'
            for (float i = transDist; i <= 1000; i += step)
            {
                Color c = Color.White;
                var origin = start + i * unit;
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                    new Rectangle(0, 26, 22, 26), Color.White, r,
                    new Vector2(22 * .5f, 26 * .5f), scale, 0, 0);
            }


            // Draws the laser 'tail'
            spriteBatch.Draw(texture, start + unit * (transDist - step) - Main.screenPosition,
                          new Rectangle(0, 0, 22, 26), Color.White, r, new Vector2(22 * .5f, 26 * .5f), scale, 0, 0);

            // Draws the laser 'head'
            spriteBatch.Draw(texture, start + (1000 + step) * unit - Main.screenPosition,
                new Rectangle(0, 52, 22, 26), Color.White, r, new Vector2(22 * .5f, 26 * .5f), scale, 0, 0);

        }


        public override void AI()
        {
            Projectile proj = Main.projectile[ParentIndex];
            checkBox = new Rectangle((int)proj.Center.X, (int)proj.Center.Y, 10, 10);
            Projectile.velocity = new Vector2(2,0);

            UpdatePosition();
            UpdateProj(proj);
            SetLaserPosition(Main.projectile[ParentIndex]);



        }


        private void SetLaserPosition(Projectile proj)
        {
            for (Distance = MOVE_DISTANCE; Distance <= maxDist; Distance += 5f)
            {
                var start = proj.Center + Projectile.velocity * Distance;
                if (!Collision.CanHit(proj.Center, 1, 1, start, 1, 1))
                {
                    Distance -= 5f;
                    break;
                }
            }
        }




        public void UpdatePosition()
        {
            Projectile.Center = Main.projectile[ParentIndex].Center;
        }

        public override bool ShouldUpdatePosition() => false;

        public void UpdateProj(Projectile proj)
        {
            Projectile.netUpdate = true;
        }


    }


}

