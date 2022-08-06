using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows;

/// <summary>
/// ChatFilterWindow.xaml 的交互逻辑
/// </summary>
public partial class ChatFilterWindow : Window
{
    public ChatFilterWindow()
    {
        InitializeComponent();
    }

    private void Window_ChatFilter_Loaded(object sender, RoutedEventArgs e)
    {
        if (File.Exists(FileUtil.SensitiveWord_Path))
        {
            try
            {
                var file = new StreamReader(FileUtil.SensitiveWord_Path, Encoding.Default);
                string content = string.Empty;
                while (content != null)
                {
                    content = file.ReadLine();
                    if (!string.IsNullOrEmpty(content))
                        ListBox_SensitiveWord.Items.Add(content);
                }
                file.Close();
            }
            catch (Exception ex)
            {
                MsgBoxUtil.ExceptionMsgBox(ex);
            }
        }
        else
        {
            ListBox_SensitiveWord.Items.Add("微信");
            ListBox_SensitiveWord.Items.Add("群");
            ListBox_SensitiveWord.Items.Add("淘宝");
            ListBox_SensitiveWord.Items.Add("科技");
            ListBox_SensitiveWord.Items.Add("www");
        }
    }

    private void Window_ChatFilter_Closing(object sender, CancelEventArgs e)
    {
        Save();
    }

    private void Button_AddSensitiveWord_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var txt = TextBox_SensitiveWord.Text;
        if (!string.IsNullOrEmpty(txt))
        {
            foreach (string item in ListBox_SensitiveWord.Items)
            {
                if (item.Equals(txt))
                {
                    MsgBoxUtil.InformationMsgBox($"关键词 {txt} 已经添加过了，请勿重复添加");
                    return;
                }
            }

            ListBox_SensitiveWord.Items.Add(txt);
            ListBox_SensitiveWord.SelectedIndex = ListBox_SensitiveWord.Items.Count - 1;
            TextBox_SensitiveWord.Clear();
        }
    }

    private void Button_RemoveSensitiveWord_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int index = ListBox_SensitiveWord.SelectedIndex;
        if (index != -1)
        {
            ListBox_SensitiveWord.Items.RemoveAt(index);
            ListBox_SensitiveWord.SelectedIndex = ListBox_SensitiveWord.Items.Count - 1;
        }
    }

    private void Button_InjectGameProcess_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var InjectInfo = new InjectInfo();
        InjectInfo.DLLPath = FileUtil.Inject_Path + "BlcokSpamMsg.dll";

        if (string.IsNullOrEmpty(InjectInfo.DLLPath))
        {
            MsgBoxUtil.WarningMsgBox("发生异常，DLL路径为空");
            return;
        }

        var process = Process.GetProcessesByName("GTA5")[0];
        InjectInfo.PID = process.Id;
        InjectInfo.PName = process.ProcessName;
        InjectInfo.MWindowHandle = process.MainWindowHandle;

        foreach (ProcessModule module in Process.GetProcessById(InjectInfo.PID).Modules)
        {
            if (module.FileName == InjectInfo.DLLPath)
            {
                MsgBoxUtil.WarningMsgBox("该DLL已经被注入过了，请勿重复注入");
                return;
            }
        }

        try
        {
            BaseInjector.SetForegroundWindow(InjectInfo.MWindowHandle);
            BaseInjector.DLLInjector(InjectInfo.PID, InjectInfo.DLLPath);
        }
        catch (Exception ex)
        {
            MsgBoxUtil.ExceptionMsgBox(ex);
        }
    }

    private void Button_SaveSensitiveWord_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        Save();
    }

    private void Save()
    {
        try
        {
            using var fs = new FileStream(FileUtil.SensitiveWord_Path, FileMode.Create);
            using var sw = new StreamWriter(fs, Encoding.Default);
            for (int i = 0; i < ListBox_SensitiveWord.Items.Count; i++)
            {
                sw.WriteLine(ListBox_SensitiveWord.Items[i].ToString());
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }
        catch (Exception ex)
        {
            MsgBoxUtil.ExceptionMsgBox(ex);
        }
    }
}
