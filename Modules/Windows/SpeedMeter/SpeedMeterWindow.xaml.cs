using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows.SpeedMeter;

/// <summary>
/// SpeedMeterWindow.xaml 的交互逻辑
/// </summary>
public partial class SpeedMeterWindow : Window
{
    private DrawWindow DrawWindow = null;

    public SpeedMeterWindow()
    {
        InitializeComponent();
    }

    private void Window_SpeedMeter_Loaded(object sender, RoutedEventArgs e)
    {
        Task.Run(() =>
        {
            Memory.Initialize(CoreUtil.TargetAppName);

            Globals.WorldPTR = Memory.FindPattern(Offsets.Mask.WorldMask);
            Globals.WorldPTR = Memory.Rip_37(Globals.WorldPTR);

            Globals.UnkPTR = Memory.FindPattern(Offsets.Mask.UnkMask);
            Globals.UnkPTR = Memory.Rip_37(Globals.UnkPTR);

            var windowData = Memory.GetGameWindowData();

            this.Dispatcher.Invoke(() =>
            {
                TextBlock_ScreenResolution.Text = $"屏幕分辨率 {SystemParameters.PrimaryScreenWidth} x {SystemParameters.PrimaryScreenHeight}";
                TextBlock_GameResolution.Text = $"游戏分辨率 {windowData.Width} x {windowData.Height}";
                TextBlock_ScreenScale.Text = $"缩放比例 {ScreenHelper.GetScalingRatio()}";
            });
        });
    }

    private void Window_SpeedMeter_Closing(object sender, CancelEventArgs e)
    {
        if (DrawWindow != null)
        {
            DrawWindow.Close();
            DrawWindow = null;
        }
    }

    private void Button_RunDraw_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Memory.SetForegroundWindow();

        if (DrawWindow == null)
        {
            DrawWindow = new DrawWindow();
            DrawWindow.Show();
        }
    }

    private void Button_StopDraw_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        if (DrawWindow != null)
        {
            DrawWindow.Close();
            DrawWindow = null;
        }
    }

    private void RadioButton_SpeedMeterPos_Center_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_SpeedMeterPos_Center.IsChecked == true)
        {
            DrawData.IsDrawCenter = true;
        }
        else
        {
            DrawData.IsDrawCenter = false;
        }
    }

    private void RadioButton_SpeedMeterUnit_MPH_Click(object sender, RoutedEventArgs e)
    {
        if (RadioButton_SpeedMeterUnit_MPH.IsChecked == true)
        {
            DrawData.IsShowMPH = true;
        }
        else
        {
            DrawData.IsShowMPH = false;
        }
    }
}
