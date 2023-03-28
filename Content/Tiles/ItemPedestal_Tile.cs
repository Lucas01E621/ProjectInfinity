using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMod;
using ProjectInfinity.Content.Items.Blocks;
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

namespace ProjectInfinity.Content.Tiles
{
    internal class ItemPedestal_Tile : ModTile
    {
        public override string Texture => AssetDirectory.CrystalDesert_Tiles + "CrystalAltar_Tile";
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(0, 0);

            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<ItemPedestal_TileEntity>().Hook_AfterPlacement, -1, 0, false);
            TileObjectData.newTile.UsesCustomCanPlace = true;

            TileObjectData.addTile(Type);

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(Color.Pink, name);
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            var org = TileObjectData.GetTileData(this.Type, 0).Origin;
            int offsetX = Main.tile[i, j].TileFrameX / 18;
            int offsetY = Main.tile[i, j].TileFrameY / 18;
            ItemPedestal_TileEntity PedestalEntity = (TileEntity.ByPosition[new Point16(i - offsetX + org.X, j - offsetY + org.Y)] as ItemPedestal_TileEntity);
            int index = Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, PedestalEntity.CheckItem());

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index);
            }

            Point16 origin = TileUtils.GetTileOrigin(i, j);
            ModContent.GetInstance<ItemPedestal_TileEntity>().Kill(origin.X, origin.Y);
        }
        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;

            

            Main.LocalPlayer.chest = -1;
            Main.mouseRightRelease = false;

            Item chosenItem = Main.mouseItem.IsAir ? player.HeldItem : Main.mouseItem;
            bool holdingItem = !chosenItem.IsAir;

            var origin = TileObjectData.GetTileData(this.Type, 0).Origin;
            int offsetX = Main.tile[i, j].TileFrameX / 18;
            int offsetY = Main.tile[i, j].TileFrameY / 18;
            ItemPedestal_TileEntity PedestalEntity = (TileEntity.ByPosition[new Point16(i - offsetX + origin.X, j - offsetY + origin.Y)] as ItemPedestal_TileEntity);

            if (holdingItem && !PedestalEntity.HasStoredItem())
            {
                PedestalEntity.StoreItem(chosenItem.type);
                chosenItem.stack--;
            }
            if (!holdingItem && PedestalEntity.HasStoredItem())
            {
                int index = Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 16, PedestalEntity.CheckItem());

                if (Main.netMode == NetmodeID.MultiplayerClient)
                {
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, index);
                }
                SoundEngine.PlaySound(new SoundStyle("ProjectInfinity/Sounds/scream"), player.position);
                PedestalEntity.RemoveItem();
            }
            return Main.mouseRight;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            var origin = TileObjectData.GetTileData(this.Type, 0).Origin;
            int offsetX = Main.tile[i, j].TileFrameX / 18;
            int offsetY = Main.tile[i, j].TileFrameY / 18;
            ItemPedestal_TileEntity PedestalEntity = (TileEntity.ByPosition[new Point16(i - offsetX + origin.X, j - offsetY + origin.Y)] as ItemPedestal_TileEntity);
            if (PedestalEntity.HasStoredItem())
            {
                Texture2D texture = TextureAssets.Item[PedestalEntity.CheckItem()].Value;
                Vector2 drawPosition = PedestalEntity.Position.ToWorldCoordinates() - Main.screenPosition + new Vector2(200,170);
                Rectangle sourceRect = new(0, 0, texture.Width, texture.Height);
                Color lightColor = Lighting.GetColor(PedestalEntity.Position.ToPoint());
                float rotation = 0f;
                Vector2 originn = sourceRect.Size() / 2f;
                float scale = 1f;
                SpriteEffects spriteEffects = SpriteEffects.None;
                float worthless = 0f;
                spriteBatch.Draw(texture,drawPosition, sourceRect, lightColor, rotation, originn, scale, spriteEffects, worthless);
            }
        }
    }
}
