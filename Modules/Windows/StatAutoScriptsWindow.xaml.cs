using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;
using GTA5OnlineTools.Features.SDK;
using static GTA5OnlineTools.Features.Data.StatData;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// StatAutoScriptsWindow.xaml 的交互逻辑
/// </summary>
public partial class StatAutoScriptsWindow : Window
{
    public StatAutoScriptsWindow()
    {
        InitializeComponent();
    }

    private void Window_StatAutoScripts_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            Memory.Initialize(CoreUtil.TargetAppName);

            Globals.GlobalPTR = Memory.FindPattern(Offsets.Mask.GlobalMask);
            Globals.GlobalPTR = Memory.Rip_37(Globals.GlobalPTR);

            this.Dispatcher.Invoke(() =>
            {

            });
        });

        // STAT列表
        foreach (var item in StatDataClass)
        {
            ListBox_STATList.Items.Add(item.ClassName);
        }
        ListBox_STATList.SelectedIndex = 0;
    }

    private void Window_StatAutoScripts_Closing(object sender, CancelEventArgs e)
    {

    }

    private void Button_LoadSession_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Online.LoadSession(11);
    }

    private void AppendTextBox(string str)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            TextBox_Result.AppendText($"[{DateTime.Now:T}] {str}\r\n");
            TextBox_Result.ScrollToEnd();
        });
    }

    private void AutoScript(string statClassName)
    {
        TextBox_Result.Clear();

        Task.Run(() =>
        {
            try
            {
                int index = StatDataClass.FindIndex(t => t.ClassName == statClassName);
                if (index != -1)
                {
                    AppendTextBox($"正在执行 {StatDataClass[index].ClassName} 脚本代码");

                    for (int i = 0; i < StatDataClass[index].StatInfo.Count; i++)
                    {
                        AppendTextBox($"正在执行 第 {i + 1}/{StatDataClass[index].StatInfo.Count} 条代码");

                        Hacks.WriteStat(StatDataClass[index].StatInfo[i].Hash, StatDataClass[index].StatInfo[i].Value);
                        Task.Delay(500).Wait();
                    }

                    AppendTextBox($"{StatDataClass[index].ClassName} 脚本代码执行完毕");
                }
            }
            catch (Exception ex)
            {
                AppendTextBox($"错误：{ex.Message}");
            }
        });
    }

    private void Button_ExecuteAutoScript_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index = ListBox_STATList.SelectedIndex;
        if (index != -1)
        {
            AutoScript(ListBox_STATList.SelectedItem.ToString());
        }
    }
}
