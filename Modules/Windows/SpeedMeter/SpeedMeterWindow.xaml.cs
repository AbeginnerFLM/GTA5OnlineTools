using GTA5OnlineTools.Common.Utils;
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
}
