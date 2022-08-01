namespace GTA5OnlineTools.Features.Core;

public static class Screen
{
    #region Win32 API
    [DllImport("user32.dll")]
    private static extern IntPtr GetDC(IntPtr ptr);

    [DllImport("gdi32.dll")]
    private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    [DllImport("user32.dll")]
    private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
    #endregion

    #region DeviceCaps常量
    private const int HORZRES = 8;
    private const int VERTRES = 10;
    private const int LOGPIXELSX = 88;
    private const int LOGPIXELSY = 90;
    private const int DESKTOPVERTRES = 117;
    private const int DESKTOPHORZRES = 118;
    #endregion

    #region 属性
    /// <summary>
    /// 获取屏幕分辨率当前物理大小
    /// </summary>
    public static Size WorkingArea
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            Size size = new Size
            {
                Width = GetDeviceCaps(hdc, HORZRES),
                Height = GetDeviceCaps(hdc, VERTRES)
            };
            ReleaseDC(IntPtr.Zero, hdc);
            return size;
        }
    }

    /// <summary>
    /// 当前系统DPI_X 大小 一般为96
    /// </summary>
    public static int DpiX
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            int DpiX = GetDeviceCaps(hdc, LOGPIXELSX);
            ReleaseDC(IntPtr.Zero, hdc);
            return DpiX;
        }
    }

    /// <summary>
    /// 当前系统DPI_Y 大小 一般为96
    /// </summary>
    public static int DpiY
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            int DpiX = GetDeviceCaps(hdc, LOGPIXELSY);
            ReleaseDC(IntPtr.Zero, hdc);
            return DpiX;
        }
    }

    /// <summary>
    /// 获取真实设置的桌面分辨率大小
    /// </summary>
    public static Size DESKTOP
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            Size size = new Size
            {
                Width = GetDeviceCaps(hdc, DESKTOPHORZRES),
                Height = GetDeviceCaps(hdc, DESKTOPVERTRES)
            };
            ReleaseDC(IntPtr.Zero, hdc);
            return size;
        }
    }

    /// <summary>
    /// 获取宽度缩放百分比
    /// </summary>
    public static float ScaleX
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            float ScaleX = (float)GetDeviceCaps(hdc, DESKTOPHORZRES) / GetDeviceCaps(hdc, HORZRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return ScaleX;
        }
    }

    /// <summary>
    /// 获取高度缩放百分比
    /// </summary>
    public static float ScaleY
    {
        get
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            float ScaleY = (float)GetDeviceCaps(hdc, DESKTOPVERTRES) / GetDeviceCaps(hdc, VERTRES);
            ReleaseDC(IntPtr.Zero, hdc);
            return ScaleY;
        }
    }
    #endregion
}
