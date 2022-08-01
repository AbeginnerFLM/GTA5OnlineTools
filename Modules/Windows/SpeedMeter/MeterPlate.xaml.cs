﻿using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace GTA5OnlineTools.Modules.Windows.SpeedMeter;

/// <summary>
/// MeterPlate.xaml 的交互逻辑
/// </summary>
public partial class MeterPlate : UserControl
{
    public int Value
    {
        get { return (int)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register("Value", typeof(int), typeof(MeterPlate),
            new PropertyMetadata(default(int), new PropertyChangedCallback(OnValuePropertyChanged)));

    public int GearValue
    {
        get { return (int)GetValue(GearValueProperty); }
        set { SetValue(GearValueProperty, value); }
    }
    public static readonly DependencyProperty GearValueProperty =
        DependencyProperty.Register("GearValue", typeof(int), typeof(MeterPlate),
            new PropertyMetadata(default(int), new PropertyChangedCallback(OnValuePropertyChanged)));

    public double Minimum
    {
        get { return (double)GetValue(MinimumProperty); }
        set { SetValue(MinimumProperty, value); }
    }
    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register("Minimum", typeof(double), typeof(MeterPlate),
            new PropertyMetadata(double.NaN, new PropertyChangedCallback(OnPropertyChanged)));

    public double Maximum
    {
        get { return (double)GetValue(MaximumProperty); }
        set { SetValue(MaximumProperty, value); }
    }
    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register("Maximum", typeof(double), typeof(MeterPlate),
            new PropertyMetadata(double.NaN, new PropertyChangedCallback(OnPropertyChanged)));

    public Brush PlateBackground
    {
        get { return (Brush)GetValue(PlateBackgroundProperty); }
        set { SetValue(PlateBackgroundProperty, value); }
    }
    public static readonly DependencyProperty PlateBackgroundProperty =
        DependencyProperty.Register("PlateBackground", typeof(Brush), typeof(MeterPlate), null);

    public Brush PlateBorderBrush
    {
        get { return (Brush)GetValue(PlateBorderBrushProperty); }
        set { SetValue(PlateBorderBrushProperty, value); }
    }
    public static readonly DependencyProperty PlateBorderBrushProperty =
        DependencyProperty.Register("PlateBorderBrush", typeof(Brush), typeof(MeterPlate), null);

    public Thickness PlateBorderThickness
    {
        get { return (Thickness)GetValue(PlateBorderThicknessProperty); }
        set { SetValue(PlateBorderThicknessProperty, value); }
    }
    public static readonly DependencyProperty PlateBorderThicknessProperty =
        DependencyProperty.Register("PlateBorderThickness", typeof(Thickness), typeof(MeterPlate), null);

    public static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as MeterPlate).DrawScale();
    }

    public static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as MeterPlate).DrawAngle();
    }

    public MeterPlate()
    {
        InitializeComponent();

        Loaded += MeterPlate_Loaded;
    }

    private void MeterPlate_Loaded(object sender, RoutedEventArgs e)
    {
        this.DrawScale();
    }

    /// <summary>
    /// 绘制表盘刻度
    /// </summary>
    private void DrawScale()
    {
        this.canvasPlate.Children.Clear();

        for (double i = 0; i <= this.Maximum - this.Minimum; i++)
        {
            // 添加刻度线
            Line lineScale = new Line();

            if (i % 20 == 0)
            {
                // 注意Math.Cos和Math.Sin的参数是弧度，记得将角度转为弧度制
                lineScale.X1 = 200 - 170 * Math.Cos(i * (270 / (this.Maximum - this.Minimum)) * Math.PI / 180);
                lineScale.Y1 = 200 - 170 * Math.Sin(i * (270 / (this.Maximum - this.Minimum)) * Math.PI / 180);
                lineScale.Stroke = new SolidColorBrush(Colors.White);
                lineScale.StrokeThickness = 3;

                // 添加刻度值
                var txtScale = new TextBlock
                {
                    Text = (i + this.Minimum).ToString(),
                    Width = 34,
                    TextAlignment = TextAlignment.Center,
                    Foreground = new SolidColorBrush(Colors.White),
                    RenderTransform = new RotateTransform() { Angle = 45, CenterX = 17, CenterY = 8 },
                    FontSize = 18
                };

                Canvas.SetLeft(txtScale, 200 - 155 * Math.Cos(i * (270 / (this.Maximum - this.Minimum)) * Math.PI / 180) - 17);
                Canvas.SetTop(txtScale, 200 - 155 * Math.Sin(i * (270 / (this.Maximum - this.Minimum)) * Math.PI / 180) - 10);

                this.canvasPlate.Children.Add(txtScale);
            }
            else
            {
                lineScale.X1 = 200 - 180 * Math.Cos(i * (270 / (this.Maximum - this.Minimum)) * Math.PI / 180);
                lineScale.Y1 = 200 - 180 * Math.Sin(i * (270 / (this.Maximum - this.Minimum)) * Math.PI / 180);
                lineScale.Stroke = new SolidColorBrush(Colors.White);
                lineScale.StrokeThickness = 1;
            }

            lineScale.X2 = 200 - 190 * Math.Cos(i * (270 / (this.Maximum - this.Minimum)) * Math.PI / 180);
            lineScale.Y2 = 200 - 190 * Math.Sin(i * (270 / (this.Maximum - this.Minimum)) * Math.PI / 180);

            this.canvasPlate.Children.Add(lineScale);
        }
    }

    /// <summary>
    /// 绘制表盘指针
    /// </summary>
    private void DrawAngle()
    {
        double step = 270.0 / (this.Maximum - this.Minimum);
        DoubleAnimation da = new DoubleAnimation(this.Value * step - 45, new Duration(TimeSpan.FromMilliseconds(200)));
        this.rtPointer.BeginAnimation(RotateTransform.AngleProperty, da);
    }
}
