using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Dinkie.Icons;

public sealed partial class DinkieIcon : UserControl
{
    private static Lazy<IDictionary<DinkieIconName, string>> _factory = new(IconDataFactory.Create());

    static DinkieIcon()
    {
        IconNameProperty.Changed.AddClassHandler<DinkieIcon>((o, e) =>
        {
            o.UpdateData(e.GetNewValue<DinkieIconName>());
        });
    }
    public DinkieIcon()
    {
        InitializeComponent();
    }

    public static readonly StyledProperty<DinkieIconName> IconNameProperty = AvaloniaProperty.Register<DinkieIcon, DinkieIconName>(
        nameof(IconName));
    public DinkieIconName IconName
    {
        get => GetValue(IconNameProperty);
        set => SetValue(IconNameProperty, value);
    }

    private Geometry? _data;

    public static readonly DirectProperty<DinkieIcon, Geometry?> DataProperty = AvaloniaProperty.RegisterDirect<DinkieIcon, Geometry?>(
        nameof(Data), o => o.Data);
    
    public Geometry? Data
    {
        get => _data;
        private set => SetAndRaise(DataProperty, ref _data, value);
    }

    private double _iconSize;

    public static readonly DirectProperty<DinkieIcon, double> IconSizeProperty = AvaloniaProperty.RegisterDirect<DinkieIcon, double>(
        nameof(IconSize), o => o.IconSize);

    public double IconSize
    {
        get => _iconSize;
        private set => SetAndRaise(IconSizeProperty, ref _iconSize, value);
    }
    

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        UpdateData(IconName);
    }

    private void UpdateData(DinkieIconName name)
    {
        string? data = null;
        _factory.Value?.TryGetValue(IconName, out data);
        Data = data is null ? null : Geometry.Parse(data);
        string s = name.ToString();
        IconSize = s.EndsWith('0') ? 10 : 12;
    }
}