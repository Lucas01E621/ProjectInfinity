using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectInfinity.Core;
using ProjectInfinity.Content.NPCs.BaseTypes;
using ProjectInfinity.Content.Foregrounds;
using ProjectInfinity.Content.Bosses.CrystalDesert;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using Terraria.Enums;
using Terraria.GameContent;

namespace ProjectInfinity.Content.Bosses.CrystalDesert
{
    [AutoloadBossHead]
    public partial class CrystalHeartBody : ModBoss //this class is the main boss class which calls every partial classes hooks
    {
        public override int maxHP => 15000;
        public override int defense => 25;
        public override bool hasImmunityBeforeFight => true;
        public override bool hasDeathAnim => true;
        public override int DeathAnimDuration => 120;
        public override bool hasIntro => true;
        public override int IntroDuration => 120;
        public override string Texture => AssetDirectory.CrystalHeart + "CrystalHeartBody";
        public override string BossHeadTexture => AssetDirectory.CrystalHeart + "CrystalHeartBody_Head_Boss";
        public override int DeathAnimNPCType => ModContent.NPCType<CrystalHeartDeathAnim>();

        
        public static Vector2 MawPosition;
        public int deathAnimTimer = deathAnimDuration;

        public bool SpawnedMinions
        {
            get => NPC.localAI[0] == 1f;
            set => NPC.localAI[0] = value ? 1f : 0f;
        }
        public static int MinionType()
        {
            return ModContent.NPCType<CrystalVein>();
        }
        public virtual bool SafePreKill()
        {
            return !hasDeathAnim;
        }
        public override void SafeSetStaticDefaults()
        {
            //Main.npcFrameCount[Type] = 6;
            // Influences how the NPC looks in the Bestiary
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                CustomTexturePath = "ProjectInfinity/Assets/Bosses/CrystalHeart/CrystalHeartPreview",
                PortraitScale = 0.6f, // Portrait refers to the full picture when clicking on the icon in the bestiary
                PortraitPositionYOverride = 0f,
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

            // Automatically group with other bosses
            NPCID.Sets.BossBestiaryPriority.Add(Type);


            //immune
            NPCDebuffImmunityData debuffData = new NPCDebuffImmunityData
            {
                SpecificallyImmuneTo = new int[]
                {
                    BuffID.OnFire
                    ,
                    BuffID.Confused
                }
            };
            NPCID.Sets.DebuffImmunitySets.Add(Type, debuffData);

        }
        public override void SafeSetDefaults()
        {
            NPC.width = 560;
            NPC.height = 400;
            NPC.damage = 12;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = false;
            NPC.value = Item.buyPrice(gold: 5);
            NPC.SpawnWithHigherTime(30);
            NPC.npcSlots = 10f; 
            NPC.aiStyle = -1;

            // Custom boss bar
            //NPC.BossBar = ModContent.GetInstance<>();

            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/orthodoxia");
            }
        }
        public override void SafeAI()
        {
            attackAI();
            
            Vignette.offset = (NPC.Center - Main.LocalPlayer.Center) * 0.8f;
            Vignette.opacityMult = 0.3f;
            Vignette.visible = true;
        }
    }
}
