﻿using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.Data;

public class Settings
{
    public static bool ShowWindow = true;

    public static float Forward = 1.5f;

    public static int ProduceTime = 5;

    public struct Player
    {
        public static bool GodMode = false;
        public static bool AntiAFK = false;
        public static bool NoRagdoll = false;
        public static bool WaterProof = false;
        public static bool Invisible = false;
        public static bool UndeadOffRadar = false;
        public static bool EveryoneIgnore = false;
        public static bool CopsIgnore = false;

        public static bool NoCollision = false;
    }

    public struct Vehicle
    {
        public static bool VehicleGodMode = false;
        public static bool VehicleSeatbelt = false;
    }

    public struct Special
    {
        public static bool FireAmmo = false;
        public static bool ExplosiveAmmo = false;
        public static bool SuperJump = false;
    }

    public struct Common
    {
        public static bool AutoClearWanted = false;
        public static bool AutoKillNPC = false;
        public static bool AutoKillHostilityNPC = false;
        public static bool AutoKillPolice = false;
    }

    public struct Overlay
    {
        public static bool VSync = false;
        public static int FPS = 300;

        public static bool ESP_2DBox = true;
        public static bool ESP_3DBox = false;
        public static bool ESP_2DLine = true;
        public static bool ESP_Bone = false;
        public static bool ESP_2DHealthBar = true;
        public static bool ESP_HealthText = false;
        public static bool ESP_NameText = false;
        public static bool ESP_Player = true;
        public static bool ESP_NPC = true;
        public static bool ESP_Crosshair = true;

        public static bool AimBot_Enabled = false;
        public static int AimBot_BoneIndex = 0;
        public static float AimBot_Fov = 8848.0f;
        public static WinVK AimBot_Key = WinVK.CONTROL;

        public static bool IsNoTOPMostHide = false;
    }
}
