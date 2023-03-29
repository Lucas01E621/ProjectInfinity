using ProjectInfinity.Content.Tiles.CrystalDesert;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.ObjectData;

namespace ProjectInfinity.Content.Tiles
{
    internal class CrystalAltar_TileEntity : ModTileEntity
    {
        public override bool IsTileValidForEntity(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            //The MyTile class is shown later
            return tile.TileType == ModContent.TileType<CrystalAltar_Tile>() && tile.HasTile;
        }
        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                //Sync the entire multitile's area.  Modify "width" and "height" to the size of your multitile in tiles
                int width = 11;
                int height = 14;
                NetMessage.SendTileSquare(Main.myPlayer, i, j, width, height);

                //Sync the placement of the tile entity with other clients
                //The "type" parameter refers to the tile type which placed the tile entity, so "Type" (the type of the tile entity) needs to be used here instead
                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
            }

            //ModTileEntity.Place() handles checking if the entity can be placed, then places it for you
            //Set "tileOrigin" to the same value you set TileObjectData.newTile.Origin to in the ModTile
            Point16 tileOrigin = new Point16(0, 0);
            int placedEntity = Place(i - tileOrigin.X, j - tileOrigin.Y);
            return placedEntity;
        }

        public override void Update()
        {
            /*int timer = 900;
            if(++timer >= 900)
            {
                SoundEngine.PlaySound(new SoundStyle("ProjectInfinity/Sounds/SFX/intemple"), Position.ToWorldCoordinates());
                timer = 0;
            }*/
            //loop sound and make it distance based
        }
        public Item storedItem = new Item();
        public bool HasStoredItem() => storedItem is not null && !storedItem.IsAir;
        public void StoreItem(int type)
        {
            storedItem = Main.LocalPlayer.HeldItem.Clone();
        }
        public void RemoveItem()
        {
            storedItem.TurnToAir();
        }
        public int CheckItem()
        {
            return storedItem.type;
        }
    }
}
