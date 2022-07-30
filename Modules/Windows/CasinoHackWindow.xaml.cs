using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// CasinoHackWindow.xaml 的交互逻辑
/// </summary>
public partial class CasinoHackWindow : Window
{
    public CasinoHackModel CasinoHackModel { get; set; } = new();

    public CasinoHackWindow()
    {
        InitializeComponent();
    }

    private void Window_CasinoHack_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        Task.Run(() =>
        {
            Memory.Initialize(CoreUtil.TargetAppName);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.LocalScriptsMask);
            Globals.LocalScriptsPTR = Memory.Rip_37(Globals.TempPTR);
        });

        var thread0 = new Thread(MainThread);
        thread0.IsBackground = true;
        thread0.Start();

        for (int i = 0; i < 37; i++)
        {
            ComboBox_Roulette.Items.Add($"号码 {i}");
        }
        ComboBox_Roulette.Items.Add("号码 00");
    }

    private void Window_CasinoHack_Closing(object sender, CancelEventArgs e)
    {

    }

    private void MainThread()
    {
        while (true)
        {
            if (CasinoHackModel.BlackjackIsCheck)
            {
                long p = Locals.LocalAddress("blackjack");
                if (p != 0)
                {
                    p = Memory.Read<long>(p);
                    int i = Memory.Read<int>(p + (2026 + 2 + (1 + 1 * 1)) * 8);

                    var sb = new StringBuilder();
                    if ((i - 1) / 13 == 0)
                        sb.Append($"♣梅花{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 1)
                        sb.Append($"♦方块{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 2)
                        sb.Append($"♥红心{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 3)
                        sb.Append($"♠黑桃{(i - 1) % 13 + 1}");

                    CasinoHackModel.BlackjackContent = sb.ToString();
                }
            }

            if (CasinoHackModel.PokerIsCheck)
            {
                long p = Locals.LocalAddress("three_card_poker");
                if (p != 0)
                {
                    p = Memory.Read<long>(p);
                    int i = Memory.Read<int>(p + (1031 + 799 + 2 + (1 + 2 * 1)) * 8);

                    var sb = new StringBuilder();
                    if ((i - 1) / 13 == 0)
                        sb.Append($"♣梅花{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 1)
                        sb.Append($"♦方块{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 2)
                        sb.Append($"♥红心{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 3)
                        sb.Append($"♠黑桃{(i - 1) % 13 + 1}");

                    sb.Append(" ");
                    i = Memory.Read<int>(p + (1031 + 799 + 2 + (1 + 0 * 1)) * 8);
                    if ((i - 1) / 13 == 0)
                        sb.Append($"♣梅花{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 1)
                        sb.Append($"♦方块{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 2)
                        sb.Append($"♥红心{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 3)
                        sb.Append($"♠黑桃{(i - 1) % 13 + 1}");

                    sb.Append(" ");
                    i = Memory.Read<int>(p + (1031 + 799 + 2 + (1 + 1 * 1)) * 8);
                    if ((i - 1) / 13 == 0)
                        sb.Append($"♣梅花{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 1)
                        sb.Append($"♦方块{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 2)
                        sb.Append($"♥红心{(i - 1) % 13 + 1}");
                    if ((i - 1) / 13 == 3)
                        sb.Append($"♠黑桃{(i - 1) % 13 + 1}");

                    CasinoHackModel.PokerContent = sb.ToString();
                }
            }

            if (CasinoHackModel.RouletteIsCheck && CasinoHackModel.RouletteSelectedIndex != -1)
            {
                long p = Locals.LocalAddress("casinoroulette");
                if (p != 0)
                {
                    p = Memory.Read<long>(p);
                    for (int i = 0; i < 6; i++)
                    {
                        Memory.Write<int>(p + (117 + 1357 + 153 + (1 + i * 1)) * 8, CasinoHackModel.RouletteSelectedIndex);
                    }
                }
            }

            if (CasinoHackModel.SlotMachineIsCheck && CasinoHackModel.SlotMachineSelectedIndex != -1)
            {
                long p = Locals.LocalAddress("casino_slots");
                if (p != 0)
                {
                    p = Memory.Read<long>(p);
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 64; j++)
                        {
                            int index = 1341 + 1 + (1 + i * 65) + (1 + j * 1);
                            Memory.Write<int>(p + index * 8, CasinoHackModel.SlotMachineSelectedIndex);
                        }
                    }
                }
            }

            if (CasinoHackModel.LuckyWheelIsCheck && CasinoHackModel.LuckyWheelSelectedIndex != -1)
            {
                // https://www.unknowncheats.me/forum/grand-theft-auto-v/483416-gtavcsmm.html
                long p = Locals.LocalAddress("casino_lucky_wheel");
                if (p != 0)
                {
                    p = Memory.Read<long>(p);
                    int index = 273 + 14;
                    Memory.Write<int>(p + index * 8, CasinoHackModel.LuckyWheelSelectedIndex);
                }
            }

            Thread.Sleep(500);
        }
    }
}

public class CasinoHackModel : ObservableObject
{
    private bool _blackjackIsCheck;
    /// <summary>
    /// 黑杰克 是否 开启预测
    /// </summary>
    public bool BlackjackIsCheck
    {
        get => _blackjackIsCheck;
        set => SetProperty(ref _blackjackIsCheck, value);
    }

    private bool _pokerIsCheck;
    /// <summary>
    /// 三张扑克 是否 开启预测
    /// </summary>
    public bool PokerIsCheck
    {
        get => _pokerIsCheck;
        set => SetProperty(ref _pokerIsCheck, value);
    }

    private bool _rouletteIsCheck;
    /// <summary>
    /// 轮盘赌 是否 操控中奖
    /// </summary>
    public bool RouletteIsCheck
    {
        get => _rouletteIsCheck;
        set => SetProperty(ref _rouletteIsCheck, value);
    }

    private bool _slotMachineIsCheck;
    /// <summary>
    /// 老虎机 是否 操控中奖
    /// </summary>
    public bool SlotMachineIsCheck
    {
        get => _slotMachineIsCheck;
        set => SetProperty(ref _slotMachineIsCheck, value);
    }

    private bool _luckyWheelIsCheck;
    /// <summary>
    /// 幸运轮盘 是否 操控中奖
    /// </summary>
    public bool LuckyWheelIsCheck
    {
        get => _luckyWheelIsCheck;
        set => SetProperty(ref _luckyWheelIsCheck, value);
    }

    /////////////////////////////////////////////////////////

    private int _rouletteSelectedIndex;
    /// <summary>
    /// 轮盘赌 选中索引
    /// </summary>
    public int RouletteSelectedIndex
    {
        get => _rouletteSelectedIndex;
        set => SetProperty(ref _rouletteSelectedIndex, value);
    }

    private int _slotMachineSelectedIndex;
    /// <summary>
    /// 老虎机 选中索引
    /// </summary>
    public int SlotMachineSelectedIndex
    {
        get => _slotMachineSelectedIndex;
        set => SetProperty(ref _slotMachineSelectedIndex, value);
    }

    private int _luckyWheelSelectedIndex;
    /// <summary>
    /// 幸运轮盘 选中索引
    /// </summary>
    public int LuckyWheelSelectedIndex
    {
        get => _luckyWheelSelectedIndex;
        set => SetProperty(ref _luckyWheelSelectedIndex, value);
    }

    /////////////////////////////////////////////////////////

    private string _blackjackContent;
    /// <summary>
    /// 黑杰克 内容
    /// </summary>
    public string BlackjackContent
    {
        get => _blackjackContent;
        set => SetProperty(ref _blackjackContent, value);
    }

    private string _pokerContent;
    /// <summary>
    /// 三张扑克 内容
    /// </summary>
    public string PokerContent
    {
        get => _pokerContent;
        set => SetProperty(ref _pokerContent, value);
    }
}
