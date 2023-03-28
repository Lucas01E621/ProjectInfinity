using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMod;
using ProjectInfinity.Content.Items.Misc;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProjectInfinity.Content.Tiles.CrystalDesert
{
    [LegacyName("CrystalAltar")]
    internal class CrystalAltar_Tile : ModTile
    {
        public override string Texture => AssetDirectory.CrystalDesert_Tiles + Name;
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
            TileObjectData.newTile.Height = 14;
            TileObjectData.newTile.Width = 11;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0,0);

            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<CrystalAltar_TileEntity>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.newTile.UsesCustomCanPlace = true;

            TileObjectData.addTile(Type);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(Color.Pink, name);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Point16 origin = TileUtils.GetTileOrigin(i, j);
            ModContent.GetInstance<ItemPedestal_TileEntity>().Kill(origin.X, origin.Y);
        }
        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;

            bool holdingItem = !player.HeldItem.IsAir;

            Main.LocalPlayer.chest = -1;
            Main.mouseRightRelease = false;
            Item chosenItem = Main.mouseItem.IsAir ? player.HeldItem : Main.mouseItem;

            var origin = TileObjectData.GetTileData(this.Type, 0).Origin;
            int offsetX = Main.tile[i, j].TileFrameX / 18;
            int offsetY = Main.tile[i, j].TileFrameY / 18;
            CrystalAltar_TileEntity AltarEntity = (TileEntity.ByPosition[new Point16(i - offsetX + origin.X, j - offsetY + origin.Y)] as CrystalAltar_TileEntity);

            if (holdingItem && !AltarEntity.HasStoredItem() && chosenItem.type == ModContent.ItemType<CrystalCore>() || !Main.mouseItem.IsAir)
            {
                AltarEntity.StoreItem(chosenItem.type);
                chosenItem.stack--;
            } 
            else if (!holdingItem && AltarEntity.HasStoredItem())
            {
                int index = Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, AltarEntity.CheckItem());

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index);
                }
                SoundEngine.PlaySound(new SoundStyle("ProjectInfinity/Sounds/scream"), player.position);
                AltarEntity.RemoveItem();
            }
            return Main.mouseRight;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            var origin = TileObjectData.GetTileData(this.Type, 0).Origin;
            int offsetX = Main.tile[i, j].TileFrameX / 18;
            int offsetY = Main.tile[i, j].TileFrameY / 18;
            CrystalAltar_TileEntity AltarEntity = (TileEntity.ByPosition[new Point16(i - offsetX + origin.X, j - offsetY + origin.Y)] as CrystalAltar_TileEntity);
            if (AltarEntity.HasStoredItem())
            {
                Texture2D texture = TextureAssets.Item[AltarEntity.CheckItem()].Value;
                Vector2 drawPosition = AltarEntity.Position.ToWorldCoordinates() - Main.screenPosition + new Vector2(200, 170);
                Rectangle sourceRect = new(0, 0, texture.Width, texture.Height);
                Color lightColor = Lighting.GetColor(AltarEntity.Position.ToPoint());
                float rotation = 0f;
                Vector2 originn = sourceRect.Size() / 2f;
                float scale = 1f;
                SpriteEffects spriteEffects = SpriteEffects.None;
                float worthless = 0f;
                spriteBatch.Draw(texture, drawPosition, sourceRect, lightColor, rotation, originn, scale, spriteEffects, worthless);
            }
        }
    }
}
