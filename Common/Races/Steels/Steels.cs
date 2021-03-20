//make sure to add "modReferences = MrPlagueRaces" into your mod's Build.txt
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using MrPlagueRaces.Common.Races;
using SteelsRace.Sounds;

//this is a custom race file. It contains the code that makes up the race
namespace SteelsRace.Common.Races.Steels
{
	public class Steel : Race
	{
        //display name, used to override the race's displayed name. By default, a race will use its class name
        public override string RaceDisplayName => "Steel";
        //decides if the race has a custom hurt sound (prevents default hurt sound from playing)
        public override bool UsesCustomHurtSound => true;
        //decides if the race has a custom death sound (prevents default death sound from playing)
        public override bool UsesCustomDeathSound => true;
		//decides if the race has a custom female hurt sound (by default, the race will play the male/default hurt sound for both genders)
        public override bool HasFemaleHurtSound => true;

        //textures for the race's display background in the UI
		public override string RaceEnvironmentIcon => ($"SteelsRace/Common/UI/RaceDisplay/Environment/Environment_CustomTest");
		public override string RaceEnvironmentOverlay1Icon => ($"SteelsRace/Common/UI/RaceDisplay/Environment/EnvironmentOverlay_Meteor");
        public override string RaceEnvironmentOverlay2Icon => ($"SteelsRace/Common/UI/RaceDisplay/Environment/EnvironmentOverlay_Sandstorm");

		//information for the race's textures and lore in the UI
		public override string RaceSelectIcon => ($"SteelsRace/Common/UI/RaceDisplay/SteelSelect");
		public override string RaceDisplayMaleIcon => ($"SteelsRace/Common/UI/RaceDisplay/SteelDisplayMale");
        public override string RaceDisplayFemaleIcon => ($"SteelsRace/Common/UI/RaceDisplay/SteelDisplayFemale");
		public override string RaceLore1 => "Humanoid race" + "\nmade of living metal." + "\nVery sturdy, but" + "\nnot fond of fighting.";
        public override string RaceLore2 => "Their original appearance" + "\nis unknown, but it is known" + "\nthat their changes were made by" + "\nsome kind of virus they made.";
		//"\n" is normally used to move to the next line, but it conflicts with colored text so I split the ability and additional notes into several lines
		public override string RaceAbilityName => "Living Metal";
		public override string RaceAbilityDescription1 => "Increased health regeneration rate while standing still, but reduced Damage Resistance.";
		public override string RaceAbilityDescription2 => "";
		public override string RaceAbilityDescription3 => "";
		public override string RaceAbilityDescription4 => "";
		public override string RaceAbilityDescription5 => "";
        public override string RaceAbilityDescription6 => "";
		public override string RaceAdditionalNotesDescription1 => "-Unique hairstyles";
		public override string RaceAdditionalNotesDescription2 => "-Immune to life-draining debuffs";
		public override string RaceAdditionalNotesDescription3 => "-Can't drown, takes no knockback";
		public override string RaceAdditionalNotesDescription4 => "-Thorns ability, more max hp = more thorns damage multiplier";
		public override string RaceAdditionalNotesDescription5 => "-Regenerates faster while standing still. Immune to fireblocks";
        public override string RaceAdditionalNotesDescription6 => "-Damage and speed increase with decreasing health";

		//makes the race's display background in the UI appear darker, can be used to make it look like it is night
		public override bool DarkenEnvironment => true;

		//stat info for the UI's stat display. 34EB93 is the green text, FF4F64 is the red text
		public override string RaceHealthDisplayText => "[c/34EB93:+50%]";
		public override string RaceRegenerationDisplayText => "[c/34EB93:+HP%]";
		public override string RaceManaDisplayText => "[c/FF4F64:-25]";
		public override string RaceDefenseDisplayText => "[c/34EB93:+10]";
		public override string RaceDamageReductionDisplayText => "[c/34EB93:+20%]";
		public override string RaceThornsDisplayText => "[c/34EB93:HP%]";
		public override string RaceLavaResistanceDisplayText => "[c/34EB93:+2s]";
		public override string RaceRangedDamageDisplayText => "[c/FF4F64:-30%]";
        public override string RaceMagicDamageDisplayText => "[c/FF4F64:-30%]";
        public override string RaceSummonDamageDisplayText => "[c/FF4F64:-40%]";
        public override string RaceMovementSpeedDisplayText => "[c/34EB93:+10%]";
		public override string RaceJumpSpeedDisplayText => "[c/34EB93:+30%]";
		public override string RaceFallDamageResistanceDisplayText => "[c/34EB93:+20]";
		public override string RaceAllDamageDisplayText => "[c/FF4F64:-30%]";
		public override string RaceAggroDisplayText => "[c/FF4F64:+800]";
		public override string RaceRunSpeedDisplayText => "[c/34EB93:+10%]";
        public override string RaceRunAccelerationDisplayText => "[c/34EB93:+10%]";

		//race environment info (CURRENTLY COSMETIC)
		public override string RaceGoodBiomesDisplayText => "Corrupt Tundra, Underworld";
		public override string RaceBadBiomesDisplayText => "Underground Desert";

		//custom hurt sounds would normally be put in PreHurt, but they conflict with Godmode in other mods so I made a custom system to avoid the confliction
		public override bool PreHurt(Player player, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			return true;
		}

        public override bool PreKill(Player player, Mod mod, double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
			//death sound
			var SteelsRace = ModLoader.GetMod("SteelsRace");
			Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, SteelsRace.GetSoundSlot(SoundType.Custom, "Sounds/Steel_Killed"));
            return true;
        }

		public override void Load(Player player)
		{
			//if your custom race has increased health, it should also be added here (the player first joins in with vanilla health by default regardless of what their max health is). Does not need to be done with decreased health
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			if (modPlayer.RaceStats)
			{
				player.statLife += 50;
			}
		}

		//things that affect the player's stats should be put in ResetEffects
        public override void ResetEffects(Player player)
        {
            //RaceStats is a bool in MrPlagueRaces that decides whether the player's racial changes are enabled or not. Make sure to put gameplay-affecting racial changes in an 'if statement' that detects if RaceStats is true
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            if (modPlayer.RaceStats)
            {
                player.statLifeMax2 += (player.statLifeMax2 / 2);
                player.lifeRegen += 1 + (player.statLifeMax2 / 40);
                player.statManaMax2 -= 25;
                player.statDefense += 9;
                player.statDefense += player.statLifeMax2 / 100;
                player.endurance += 0.2f;
                player.lavaMax += 60;
                player.rangedDamage -= 0.3f;
                player.magicDamage -= 0.3f;
                player.minionDamage -= 0.4f;
                player.moveSpeed += 0.1f;
                player.jumpSpeedBoost += 0.3f;
                player.extraFall += 20;
                player.allDamage -= 0.3f;
                player.aggro += 800;
                player.maxRunSpeed += 0.1f;
                player.runAcceleration += 0.1f;
                player.noKnockback = true;
                player.gills = true;
                player.buffImmune[20] = true;
                player.buffImmune[70] = true;
                player.buffImmune[30] = true;
                player.buffImmune[68] = true;
                player.breath = 300;
                player.fireWalk = true;
                if (player.velocity.Y == 0f && player.velocity.X == 0f)
                {
                    player.lifeRegen += (player.statLifeMax2 / 60);
                    player.endurance -= 0.15f;
                }
                if (player.statLife <= (player.statLifeMax2 * 0.5))
                {
                    player.allDamage += 0.05f;
                    player.runAcceleration += 0.05f;
                }
                if (player.statLife <= (player.statLifeMax2 * 0.3))
                {
                    player.lifeRegen += (player.statLifeMax2 / 120);
                    player.longInvince = true;
                    player.allDamage += 0.1f;
                    player.maxRunSpeed += 0.05f;
                    if (player.statLifeMax2 < 750)
                    {
                        player.thorns += player.statLifeMax2 / 380f;
                    }
                    if (player.statLifeMax2 >= 750)
                    {
                        player.thorns += 2f;
                    }
                }
                if (player.statLifeMax2 >= 750)
                {
                    player.thorns += 2f;
                }
                if (player.statLifeMax2 < 750)
                {
                    player.thorns += player.statLifeMax2 / 380f;
                }
                if (player.lifeRegenCount <= 0)
                {
                    player.lifeRegenCount = 60;
                    player.lifeRegenTime = 60;
                    player.statLife += player.statLifeMax2 / 350;
                }
            }
        }

		public override void ProcessTriggers(Player player, Mod mod)
		{
			//custom hotkey stuff goes here
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			if (modPlayer.RaceStats)
			{
			}
		}

		public override void PreUpdate(Player player, Mod mod)
		{
			//hurt sounds and any additional features of the race (abilities, etc) go here
			//custom hurt sounds would normally be put in PreHurt, but they conflict with Godmode in other mods so I made a custom system to avoid the confliction
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
			var _MrPlagueRaces = ModLoader.GetMod("MrPlagueRaces");
			var SteelsRace = ModLoader.GetMod("SteelsRace");
            if (player.HasBuff(_MrPlagueRaces.BuffType("DetectHurt")) && (player.statLife != player.statLifeMax2))
            {
                if (player.Male || !HasFemaleHurtSound)
                {
                    //when choosing a sound, make sure to put your mod's name before .GetSoundSlot instead of "mod". using mod will cause the program to search for the sound file in MrPlagueRace's sound folder
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, SteelsRace.GetSoundSlot(SoundType.Custom, "Sounds/" + this.Name + "_Hurt"));
                }
                else if (!player.Male && HasFemaleHurtSound)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, SteelsRace.GetSoundSlot(SoundType.Custom, "Sounds/" + this.Name + "_Hurt_Female"));
                }
                else
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Mushfolk_Hurt"));
                }
            }
            if (modPlayer.RaceStats)
            {
                player.buffImmune[20] = true;
                player.buffImmune[70] = true;
                player.buffImmune[30] = true;
                player.buffImmune[68] = true;
                player.breath = 300;
            }
        }

		public override void ModifyDrawInfo(Player player, Mod mod, ref PlayerDrawInfo drawInfo)
        {
			//custom race's default color values and clothing styles go here
            var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();
            Item familiarshirt = new Item();
            familiarshirt.SetDefaults(ItemID.FamiliarShirt);
            Item familiarpants = new Item();
            familiarpants.SetDefaults(ItemID.FamiliarPants);
            if (modPlayer.resetDefaultColors)
            {
                modPlayer.resetDefaultColors = false;
                player.hairColor = new Color(90, 87, 250);
                player.skinColor = new Color(178, 200, 232);
                player.eyeColor = new Color(94, 255, 50);
				player.shirtColor = new Color(87, 97, 116);
				player.underShirtColor = new Color(118, 133, 163);
				player.pantsColor = new Color(188, 158, 127);
				player.shoeColor = new Color(95, 81, 69);
				player.skinVariant = 3;
				if (player.armor[1].type < ItemID.IronPickaxe && player.armor[2].type < ItemID.IronPickaxe)
				{
					player.armor[1] = familiarshirt;
					player.armor[2] = familiarpants;
				}
			}
		}

		public override void ModifyDrawLayers(Player player, List<PlayerLayer> layers)
		{
			//applying the racial textures
			var modPlayer = player.GetModPlayer<MrPlagueRaces.MrPlagueRacesPlayer>();

			bool hideChestplate = modPlayer.hideChestplate;
			bool hideLeggings = modPlayer.hideLeggings;

			Main.playerTextures[0, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head");
			Main.playerTextures[0, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2");
			Main.playerTextures[0, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes");
			Main.playerTextures[0, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_1");
			}
			else
			{
				Main.playerTextures[0, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[0, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_1");
			}
			else
			{
				Main.playerTextures[0, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[0, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_1");
			}
			else
			{
				Main.playerTextures[0, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[0, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand");
			Main.playerTextures[0, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[0, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_1");
				Main.playerTextures[0, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_1");
			}
			else
			{
				Main.playerTextures[0, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[0, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[0, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_1_2");
			}
			else
			{
				Main.playerTextures[0, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[0, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_1_2");
			}
			else
			{
				Main.playerTextures[0, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[1, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head");
			Main.playerTextures[1, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2");
			Main.playerTextures[1, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes");
			Main.playerTextures[1, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_2");
			}
			else
			{
				Main.playerTextures[1, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[1, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_2");
			}
			else
			{
				Main.playerTextures[1, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[1, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_2");
			}
			else
			{
				Main.playerTextures[1, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[1, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand");
			Main.playerTextures[1, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[1, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_2");
				Main.playerTextures[1, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_2");
			}
			else
			{
				Main.playerTextures[1, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[1, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[1, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_2_2");
			}
			else
			{
				Main.playerTextures[1, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[1, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_2_2");
			}
			else
			{
				Main.playerTextures[1, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[2, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head");
			Main.playerTextures[2, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2");
			Main.playerTextures[2, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes");
			Main.playerTextures[2, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_3");
			}
			else
			{
				Main.playerTextures[2, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[2, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_3");
			}
			else
			{
				Main.playerTextures[2, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[2, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_3");
			}
			else
			{
				Main.playerTextures[2, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[2, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand");
			Main.playerTextures[2, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[2, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_3");
				Main.playerTextures[2, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_3");
			}
			else
			{
				Main.playerTextures[2, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[2, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[2, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_3_2");
			}
			else
			{
				Main.playerTextures[2, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[2, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_3_2");
			}
			else
			{
				Main.playerTextures[2, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[3, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head");
			Main.playerTextures[3, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2");
			Main.playerTextures[3, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes");
			Main.playerTextures[3, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_4");
			}
			else
			{
				Main.playerTextures[3, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[3, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_4");
			}
			else
			{
				Main.playerTextures[3, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[3, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_4");
			}
			else
			{
				Main.playerTextures[3, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[3, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand");
			Main.playerTextures[3, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[3, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_4");
				Main.playerTextures[3, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_4");
			}
			else
			{
				Main.playerTextures[3, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[3, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[3, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_4_2");
			}
			else
			{
				Main.playerTextures[3, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[3, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_4_2");
			}
			else
			{
				Main.playerTextures[3, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[8, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head");
			Main.playerTextures[8, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2");
			Main.playerTextures[8, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes");
			Main.playerTextures[8, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_9");
			}
			else
			{
				Main.playerTextures[8, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[8, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_9");
			}
			else
			{
				Main.playerTextures[8, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[8, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_9");
			}
			else
			{
				Main.playerTextures[8, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[8, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand");
			Main.playerTextures[8, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[8, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_9");
				Main.playerTextures[8, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_9");
			}
			else
			{
				Main.playerTextures[8, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[8, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[8, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_9_2");
			}
			else
			{
				Main.playerTextures[8, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[8, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_9_2");
			}
			else
			{
				Main.playerTextures[8, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[4, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head_Female");
			Main.playerTextures[4, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2_Female");
			Main.playerTextures[4, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_Female");
			Main.playerTextures[4, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_5");
			}
			else
			{
				Main.playerTextures[4, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[4, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_5");
			}
			else
			{
				Main.playerTextures[4, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[4, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_5");
			}
			else
			{
				Main.playerTextures[4, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[4, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand_Female");
			Main.playerTextures[4, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[4, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_5");
				Main.playerTextures[4, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_5");
			}
			else
			{
				Main.playerTextures[4, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[4, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[4, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_5_2");
			}
			else
			{
				Main.playerTextures[4, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[4, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_5_2");
			}
			else
			{
				Main.playerTextures[4, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[5, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head_Female");
			Main.playerTextures[5, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2_Female");
			Main.playerTextures[5, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_Female");
			Main.playerTextures[5, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_6");
			}
			else
			{
				Main.playerTextures[5, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[5, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_6");
			}
			else
			{
				Main.playerTextures[5, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[5, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_6");
			}
			else
			{
				Main.playerTextures[5, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[5, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand_Female");
			Main.playerTextures[5, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[5, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_6");
				Main.playerTextures[5, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_6");
			}
			else
			{
				Main.playerTextures[5, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[5, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[5, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_6_2");
			}
			else
			{
				Main.playerTextures[5, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[5, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_6_2");
			}
			else
			{
				Main.playerTextures[5, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[6, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head_Female");
			Main.playerTextures[6, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2_Female");
			Main.playerTextures[6, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_Female");
			Main.playerTextures[6, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_7");
			}
			else
			{
				Main.playerTextures[6, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[6, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_7");
			}
			else
			{
				Main.playerTextures[6, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[6, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_7");
			}
			else
			{
				Main.playerTextures[6, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[6, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand_Female");
			Main.playerTextures[6, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[6, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_7");
				Main.playerTextures[6, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_7");
			}
			else
			{
				Main.playerTextures[6, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[6, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[6, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_7_2");
			}
			else
			{
				Main.playerTextures[6, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[6, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_7_2");
			}
			else
			{
				Main.playerTextures[6, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[7, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head_Female");
			Main.playerTextures[7, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2_Female");
			Main.playerTextures[7, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_Female");
			Main.playerTextures[7, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_8");
			}
			else
			{
				Main.playerTextures[7, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[7, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_8");
			}
			else
			{
				Main.playerTextures[7, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[7, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_8");
			}
			else
			{
				Main.playerTextures[7, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[7, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand_Female");
			Main.playerTextures[7, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[7, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_8");
				Main.playerTextures[7, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_8");
			}
			else
			{
				Main.playerTextures[7, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[7, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[7, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_8_2");
			}
			else
			{
				Main.playerTextures[7, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[7, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_8_2");
			}
			else
			{
				Main.playerTextures[7, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[9, 0] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Head_Female");
			Main.playerTextures[9, 1] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_2_Female");
			Main.playerTextures[9, 2] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Eyes_Female");
			Main.playerTextures[9, 3] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Torso_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeves_10");
			}
			else
			{
				Main.playerTextures[9, 4] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[9, 5] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hands_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shirt_10");
			}
			else
			{
				Main.playerTextures[9, 6] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[9, 7] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Arm_Female");

			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_10");
			}
			else
			{
				Main.playerTextures[9, 8] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			Main.playerTextures[9, 9] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Hand_Female");
			Main.playerTextures[9, 10] = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Legs_Female");

			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[9, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_10");
				Main.playerTextures[9, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Shoes_10");
			}
			else
			{
				Main.playerTextures[9, 11] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
				Main.playerTextures[9, 12] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[1].type == ItemID.FamiliarShirt || player.armor[11].type == ItemID.FamiliarShirt) && !hideChestplate)
			{
				Main.playerTextures[9, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Sleeve_10_2");
			}
			else
			{
				Main.playerTextures[9, 13] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}
			if ((player.armor[2].type == ItemID.FamiliarPants || player.armor[12].type == ItemID.FamiliarPants) && !hideLeggings)
			{
				Main.playerTextures[9, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Pants_10_2");
			}
			else
			{
				Main.playerTextures[9, 14] = ModContent.GetTexture("MrPlagueRaces/Content/RaceTextures/Blank");
			}

			for (int i = 0; i < 133; i++)
			{
				Main.playerHairTexture[i] = ModContent.GetTexture($"SteelsRace/Content/RaceTextures/Steel/Hair/Steel_Hair_{i + 1}");
				Main.playerHairAltTexture[i] = ModContent.GetTexture($"SteelsRace/Content/RaceTextures/Steel/Hair/Steel_HairAlt_{i + 1}");
			}

			Main.ghostTexture = ModContent.GetTexture("SteelsRace/Content/RaceTextures/Steel/Steel_Ghost");
		}
	}
}
