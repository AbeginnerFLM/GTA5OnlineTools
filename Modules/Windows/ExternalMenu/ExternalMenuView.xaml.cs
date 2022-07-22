﻿using GTA5OnlineTools.Common.Data;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.Data;

using Microsoft.Toolkit.Mvvm.Input;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// ExternalMenuView.xaml 的交互逻辑
/// </summary>
public partial class ExternalMenuView : Window
{
    public List<MenuBar> MenuBars { get; set; } = new();
    public RelayCommand<MenuBar> NavigateCommand { get; private set; }

    public delegate void ClosingDispose();
    public static event ClosingDispose ClosingDisposeEvent;

    // 程序自身的窗口句柄
    private IntPtr EMHandle;
    private POINT EMPOINT;

    public delegate void IsShowWindow();
    public static IsShowWindow IsShowWindowDelegate;

    // 用户控件
    private EM00ReadMeView EM00ReadMeView { get; set; } = new();
    private EM01PlayerStateView EM01PlayerStateView { get; set; } = new();
    private EM02WorldFunctionView EM02WorldFunctionView { get; set; } = new();
    private EM03OnlineOptionView EM03OnlineOptionView { get; set; } = new();
    private EM04PlayerListView EM04PlayerListView { get; set; } = new();
    private EM05SpawnVehicleView EM05SpawnVehicleView { get; set; } = new();
    private EM06SpawnWeaponView EM06SpawnWeaponView { get; set; } = new();
    private EM07CustomTPView EM07CustomTPView { get; set; } = new();
    private EM08ExternalOverlayView EM08ExternalOverlayView { get; set; } = new();
    private EM09SessionChatView EM09SessionChatView { get; set; } = new();
    private EM10JobHelperView EM10JobHelperView { get; set; } = new();

    public ExternalMenuView()
    {
        InitializeComponent();

        Button_TitleMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
        Button_TitleClose.Click += (s, e) => { this.Close(); };
    }

    private void Window_ExternalMenuView_Loaded(object sender, RoutedEventArgs e)
    {
        this.DataContext = this;

        // 创建菜单
        CreateMenuBar();
        // 绑定菜单切换命令
        NavigateCommand = new(Navigate);
        // 设置主页
        ContentControl_Main.Content = EM00ReadMeView;

        // 获取自身窗口句柄
        EMHandle = new WindowInteropHelper(this).Handle;
        WinAPI.GetCursorPos(out EMPOINT);

        IsShowWindowDelegate = ShowWindow;

        Settings.ShowWindow = true;

        Topmost = false;

        // 初始化后台线程
        Task.Run(() =>
        {
            Memory.Initialize(CoreUtil.TargetAppName);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.WorldMask);
            Globals.WorldPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.BlipMask);
            Globals.BlipPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.GlobalMask);
            Globals.GlobalPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.PlayerchatterNameMask);
            Globals.PlayerChatterNamePTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.NetworkPlayerMgrMask);
            Globals.NetworkPlayerMgrPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.ReplayInterfaceMask);
            Globals.ReplayInterfacePTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.WeatherMask);
            Globals.WeatherPTR = Memory.Rip_6A(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.UnkModelMask);
            Globals.UnkModelPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.PickupDataMask);
            Globals.PickupDataPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.ViewPortMask);
            Globals.ViewPortPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.AimingPedMask);
            Globals.AimingPedPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.TempPTR = Memory.FindPattern(Offsets.Mask.CCameraMask);
            Globals.CCameraPTR = Memory.Rip_37(Globals.TempPTR);

            Globals.InfiniteAmmoADDR = Memory.FindPattern("41 2B D1 E8 ?? ?? ?? ??");
            if (Globals.InfiniteAmmoADDR == 0)
                Globals.InfiniteAmmoADDR = Memory.FindPattern("90 90 90 E8 ?? ?? ?? ??");

            Globals.NoReloadADDR = Memory.FindPattern("41 2B C9 3B C8 0F 4D C8");
            if (Globals.NoReloadADDR == 0)
                Globals.NoReloadADDR = Memory.FindPattern("90 90 90 3B C8");
        });

        var thread = new Thread(InitThread);
        thread.IsBackground = true;
        thread.Start();
    }

    private void Window_ExternalMenuView_Closing(object sender, CancelEventArgs e)
    {
        ClosingDisposeEvent();
    }

    private void CreateMenuBar()
    {
        MenuBars.Add(new MenuBar() { Icon = "\xe882", Title = "使用说明", ColorHex = "#333333", NameSpace = "EM00ReadMeView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe64b", Title = "玩家属性", ColorHex = "#333333", NameSpace = "EM01PlayerStateView" });
        MenuBars.Add(new MenuBar() { Icon = "\xed18", Title = "世界功能", ColorHex = "#333333", NameSpace = "EM02WorldFunctionView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe882", Title = "线上选项", ColorHex = "#333333", NameSpace = "EM03OnlineOptionView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe64b", Title = "玩家列表", ColorHex = "#333333", NameSpace = "EM04PlayerListView" });
        MenuBars.Add(new MenuBar() { Icon = "\xed18", Title = "线上载具", ColorHex = "#333333", NameSpace = "EM05SpawnVehicleView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe882", Title = "线上武器", ColorHex = "#333333", NameSpace = "EM06SpawnWeaponView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe64b", Title = "自定传送", ColorHex = "#333333", NameSpace = "EM07CustomTPView" });
        MenuBars.Add(new MenuBar() { Icon = "\xed18", Title = "外部ESP", ColorHex = "#333333", NameSpace = "EM08ExternalOverlayView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe882", Title = "战局聊天", ColorHex = "#333333", NameSpace = "EM09SessionChatView" });
        MenuBars.Add(new MenuBar() { Icon = "\xe64b", Title = "任务帮手", ColorHex = "#333333", NameSpace = "EM10JobHelperView" });
    }

    private void Navigate(MenuBar obj)
    {
        if (obj == null || string.IsNullOrEmpty(obj.NameSpace))
            return;

        switch (obj.NameSpace)
        {
            case "EM00ReadMeView":
                ContentControl_Main.Content = EM00ReadMeView;
                break;
            case "EM01PlayerStateView":
                ContentControl_Main.Content = EM01PlayerStateView;
                break;
            case "EM02WorldFunctionView":
                ContentControl_Main.Content = EM02WorldFunctionView;
                break;
            case "EM03OnlineOptionView":
                ContentControl_Main.Content = EM03OnlineOptionView;
                break;
            case "EM04PlayerListView":
                ContentControl_Main.Content = EM04PlayerListView;
                break;
            case "EM05SpawnVehicleView":
                ContentControl_Main.Content = EM05SpawnVehicleView;
                break;
            case "EM06SpawnWeaponView":
                ContentControl_Main.Content = EM06SpawnWeaponView;
                break;
            case "EM07CustomTPView":
                ContentControl_Main.Content = EM07CustomTPView;
                break;
            case "EM08ExternalOverlayView":
                ContentControl_Main.Content = EM08ExternalOverlayView;
                break;
            case "EM09SessionChatView":
                ContentControl_Main.Content = EM09SessionChatView;
                break;
            case "EM10JobHelperView":
                ContentControl_Main.Content = EM10JobHelperView;
                break;
        }
    }

    private void InitThread()
    {
        while (true)
        {
            if (!ProcessUtil.IsAppRun("GTA5"))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Close();
                });
                return;
            }

            Thread.Sleep(2000);
        }
    }

    private void ShowWindow()
    {
        Settings.ShowWindow = !Settings.ShowWindow;
        if (Settings.ShowWindow)
        {
            //Show();
            WindowState = WindowState.Normal;
            Focus();

            if (CheckBox_IsTopMost.IsChecked == false)
            {
                Topmost = true;
                Topmost = false;
            }

            WinAPI.SetCursorPos(EMPOINT.X, EMPOINT.Y);

            WinAPI.SetForegroundWindow(EMHandle);
        }
        else
        {
            //Hide();
            WindowState = WindowState.Minimized;

            WinAPI.GetCursorPos(out EMPOINT);

            Memory.SetForegroundWindow();
        }
    }

    private void CheckBox_IsTopMost_Click(object sender, RoutedEventArgs e)
    {
        if (CheckBox_IsTopMost.IsChecked == true)
            Topmost = true;
        else
            Topmost = false;
    }
}
