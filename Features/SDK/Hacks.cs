﻿using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Features.SDK;

public static class Hacks
{
    public static long GlobalAddress(int index)
    {
        return Memory.Read<long>(Globals.GlobalPTR + 0x8 * ((index >> 0x12) & 0x3F)) + 8 * (index & 0x3FFFF);
    }

    public static T ReadGA<T>(int index) where T : struct
    {
        return Memory.Read<T>(GlobalAddress(index));
    }

    public static void WriteGA<T>(int index, T vaule) where T : struct
    {
        Memory.Write<T>(GlobalAddress(index), vaule);
    }

    public static string ReadGAString(int index)
    {
        return Memory.ReadString(GlobalAddress(index), null, 20);
    }

    public static void WriteGAString(int index, string str)
    {
        Memory.WriteString(GlobalAddress(index), null, str);
    }

    /////////////////////////////////////////////////////

    public static int GetNetworkTime()
    {
        return ReadGA<int>(1574755 + 11);
    }

    public static int PlayerID()
    {
        return ReadGA<int>(Offsets.oPlayerGA);
    }

    public static int GetBusinessIndex(int ID)
    {
        // ID 0-5
        return 1853131 + 1 + (PlayerID() * 888) + 267 + 187 + 1 + (ID * 13);
    }

    /////////////////////////////////////////

    public static uint Joaat(string input)
    {
        uint num1 = 0U;
        input = input.ToLower();
        foreach (char c in input)
        {
            uint num2 = num1 + c;
            uint num3 = num2 + (num2 << 10);
            num1 = num3 ^ num3 >> 6;
        }
        uint num4 = num1 + (num1 << 3);
        uint num5 = num4 ^ num4 >> 11;

        return num5 + (num5 << 15);
    }

    /// <summary>
    /// 写入stat值，只支持int类型
    /// </summary>
    public static void WriteStat(string hash, int value)
    {
        if (hash.IndexOf("_") == 0)
        {
            int Stat_MP = ReadGA<int>(1574915);
            hash = $"MP{Stat_MP}{hash}";
        }

        uint Stat_ResotreHash = ReadGA<uint>(1655453 + 4);
        int Stat_ResotreValue = ReadGA<int>(1020252 + 5526);

        WriteGA<uint>(1655453 + 4, Joaat(hash));
        WriteGA<int>(1020252 + 5526, value);
        WriteGA<int>(1644218 + 1139, -1);
        Thread.Sleep(1000);
        WriteGA<uint>(1655453 + 4, Stat_ResotreHash);
        WriteGA<int>(1020252 + 5526, Stat_ResotreValue);
    }

    /// <summary>
    /// 掉落物品
    /// </summary>
    public static void CreateAmbientPickup(string pickup)
    {
        //uint modelHash = Joaat("prop_cash_pile_01");
        uint pickupHash = Joaat(pickup);

        float x = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionX);
        float y = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionY);
        float z = Memory.Read<float>(Globals.WorldPTR, Offsets.PlayerPositionZ);

        WriteGA<float>(2787534 + 3, x);
        WriteGA<float>(2787534 + 4, y);
        WriteGA<float>(2787534 + 5, z + 3.0f);
        WriteGA<int>(2787534 + 1, 9999);    // 9999

        WriteGA<int>(4534105 + 1 + (ReadGA<int>(2787534) * 85) + 66 + 2, 2);
        WriteGA<int>(2787534 + 6, 1);

        Thread.Sleep(150);

        var m_dwpPickUpInterface = Memory.Read<long>(Globals.ReplayInterfacePTR, new int[] { 0x20 });

        var dw_curPickUpNum = Memory.Read<long>(m_dwpPickUpInterface + 0x110, null);
        var m_dwpPedList = Memory.Read<long>(m_dwpPickUpInterface + 0x100, null);

        for (long i = 0; i < dw_curPickUpNum; i++)
        {
            long dwpPickup = Memory.Read<long>(m_dwpPedList + i * 0x10, null);
            uint dwPickupHash = Memory.Read<uint>(dwpPickup + 0x488, null);

            if (dwPickupHash == 4263048111)
            {
                Memory.Write<uint>(dwpPickup + 0x488, pickupHash);
                break;
            }
        }
    }
}
