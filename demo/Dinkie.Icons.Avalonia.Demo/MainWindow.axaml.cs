using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Dinkie.Icons.Avalonia;

namespace Dinkie.Icons.Avalonia.Demo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public List<DinkieIconName> Names { get; set; } = Enum.GetValues<DinkieIconName>().ToList();
}