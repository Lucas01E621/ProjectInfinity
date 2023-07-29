using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ObjectData;
using ProjectInfinity.Core;
using ProjectInfinity.Content.Items.Blocks.CrystalDesert;

namespace ProjectInfinity.Content.Tiles.AstralOcean
{
    internal class AstralSand_Tile : ModTile
    {
        public override string Texture => AssetDirectory.AstralOcean_Tiles + Name;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileSand[Type] = true;
            Main.lightning = 15f;

            TileID.Sets.TouchDamageDestroyTile[Type] = true;
            TileID.Sets.ForAdvancedCollision.ForSandshark[Type] = true;
            TileID.Sets.Falling[Type] = true;
            

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(Color.DarkOliveGreen, name);
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile left = Main.tile[i - 1, j];
            Tile right = Main.tile[i + 1, j];
            Tile up = Main.tile[i, j - 1];
            Tile down = Main.tile[i, j + 1];
            if (left.HasTile && right.HasTile && up.HasTile && down.HasTile)
            {
                return;
            }
            r = 0.35f;
            g = 0.25f;
            b = 0.3f;
        }
        public override void RandomUpdate(int i, int j)
        {
            Spread(i,j);
        }
        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            //falling code
            return base.TileFrame(i, j, ref resetFrame, ref noBreak);
        }
        void Spread(int i, int j)
        {
            Tile left = Main.tile[i - 1, j];
            Tile right = Main.tile[i + 1, j];
            Tile up = Main.tile[i, j - 1];
            Tile down = Main.tile[i, j + 1];

            if(left.HasTile && left.TileType == TileID.Sand)
            {
                left.TileType = (ushort)this.Type;
            }
            if(up.HasTile && up.TileType == TileID.Sand)
            {
                up.TileType = (ushort)this.Type;
            }
            if(down.HasTile && down.TileType == TileID.Sand)
            {
                down.TileType = (ushort)this.Type;
            }
            if(right.HasTile && right.TileType == TileID.Sand)
            {
                right.TileType = (ushort)this.Type;
            }
        }
    }
}
