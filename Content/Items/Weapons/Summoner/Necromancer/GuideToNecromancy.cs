using Microsoft.Xna.Framework;
using ProjectInfinity.Content.NPCs;
using ProjectInfinity.Content.Projectiles;
using ProjectInfinity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProjectInfinity.Content.Items.Weapons.Summoner.Necromancer
{
    internal class GuideToNecromancy : ModItem
    {
        public override string Texture => AssetDirectory.Magic + "PHwand";
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 20;
        }
        public override bool? UseItem(Player player)
        {
            // First get a list of all our nearby bones
            List<(Projectile, int)> bonesAndNearbyCount = new();
            foreach (var startBone in Main.projectile.SkipLast(1).Where(proj => proj.active && proj.type == ModContent.ProjectileType<BoneShard>()))
            {
                int numNearbyBones = 0;
                foreach (var targetBone in Main.projectile.SkipLast(1).Where(proj => proj.active && proj.type == ModContent.ProjectileType<BoneShard>()))
                {
                    if (startBone.whoAmI == targetBone.whoAmI)
                    {
                        continue;
                    }
                    if (startBone.WithinRange(targetBone.position, 80) && targetBone.WithinRange(player.Center, 500))
                    {
                        numNearbyBones++;
                    }
                }
                bonesAndNearbyCount.Add((startBone, numNearbyBones));
            }

            // Then find the bone with the most nearby bones
            if (bonesAndNearbyCount.Count == 0)
            {
                return true;
            }
            Projectile bestBone = bonesAndNearbyCount[0].Item1;
            int bestBoneNearbyCount = bonesAndNearbyCount[0].Item2;
            foreach (var boneAndNearbyCount in bonesAndNearbyCount.Skip(1))
            {
                if (boneAndNearbyCount.Item2 > bestBoneNearbyCount)
                {
                    bestBone = boneAndNearbyCount.Item1;
                    bestBoneNearbyCount = boneAndNearbyCount.Item2;
                }
            }

            // Then we collapse all our nearby bones into the npc
            List<Projectile> bones = Main.projectile.SkipLast(1).Where(proj => proj.active && proj.type == ModContent.ProjectileType<BoneShard>() && proj.WithinRange(bestBone.position, 80) && proj.WithinRange(player.Center, 500)).ToList();
            int npcHp = bones.Count * 15;
            int npcDmg = (bones.Count * 4) / 2; //add damage modifiers afterwards
            int npcDef = (bones.Count * 2) / 5;

            foreach (var bone in bones)
            {
                bone.Kill();
                Main.NewText("killed");
            }
            Main.NewText("spawned");

            int index = NPC.NewNPC(bestBone.GetSource_FromAI(), (int)bestBone.position.X, (int)bestBone.position.Y, ModContent.NPCType<UndeadSkeleton>(),0,npcHp);
            NPC npc = Main.npc[index];
            npc.lifeMax += npcHp;
            npc.life = npc.lifeMax;
            npc.damage += npcDmg;
            return true;
        }
    }
}
