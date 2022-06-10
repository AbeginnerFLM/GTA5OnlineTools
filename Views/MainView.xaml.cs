﻿using Hardcodet.Wpf.TaskbarNotification;

using GTA5OnlineTools.Common.Utils;

namespace GTA5OnlineTools.Views;

/// <summary>
/// MainWindow.xaml 的交互逻辑
/// </summary>
public partial class MainView : Window
{
    // 任务栏图标
    private static TaskbarIcon TaskbarIcon_Main = null;

    public static Window MainWindow = null;

    public MainView()
    {
        InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        MainWindow = this;

        TaskbarIcon_Main = new TaskbarIcon();
        TaskbarIcon_Main.IconSource = new BitmapImage(new Uri("pack://application:,,,/Assets/Images/Favicon.ico", UriKind.RelativeOrAbsolute));
        TaskbarIcon_Main.MenuActivation = PopupActivationMode.RightClick;
        TaskbarIcon_Main.ToolTipText = "GTA5线上小助手";
        TaskbarIcon_Main.Visibility = Visibility.Visible;

        ContextMenu contextMenu = new ContextMenu();
        MenuItem MenuItem_Show = new MenuItem();
        MenuItem_Show.Header = "显示";
        MenuItem_Show.Click += TaskbarIcon_MenuItem_Show_Click;
        MenuItem MenuItem_Exit = new MenuItem();
        MenuItem_Exit.Header = "退出";
        MenuItem_Exit.Click += TaskbarIcon_MenuItem_Exit_Click;
        contextMenu.Items.Add(MenuItem_Show);
        contextMenu.Items.Add(MenuItem_Exit);
        TaskbarIcon_Main.ContextMenu = contextMenu;

        TaskbarIcon_Main.TrayMouseDoubleClick += TaskbarIcon_Main_TrayMouseDoubleClick;
    }

    private void Window_Closing(object sender, CancelEventArgs e)
    {
        ProcessUtil.CloseTheseProcess();
        TaskbarIcon_Main.IconSource = null;
        TaskbarIcon_Main.ContextMenu = null;
        TaskbarIcon_Main.Dispose();
        Application.Current.Shutdown();
    }

    private void TaskbarIcon_MenuItem_Show_Click(object sender, RoutedEventArgs e)
    {
        Topmost = true;
        Topmost = false;
        ShowInTaskbar = true;
        WindowState = WindowState.Normal;
    }

    private void TaskbarIcon_MenuItem_Exit_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void TaskbarIcon_Main_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
    {
        Topmost = true;
        Topmost = false;
        ShowInTaskbar = true;
        WindowState = WindowState.Normal;
    }

    public static void ShowNoticeInfo(string msg)
    {
        TaskbarIcon_Main?.ShowBalloonTip("提示", msg, BalloonIcon.Info);
    }
}
