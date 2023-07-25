using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Dinkie.Icons.Avalonia;

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
    }
}