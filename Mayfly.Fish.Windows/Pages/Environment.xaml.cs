using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Mayfly.Fish.Windows.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Environment : Page
    {
        public Environment()
        {
            this.InitializeComponent();
        }

        private void Scroll_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width >= (
                Scroll.Padding.Left +
                ColumnWater.Margin.Left + ColumnWater.Width + ColumnWater.Margin.Right +
                36 + ColumnWeather.Width
                ))
            {
                Stack.Orientation = Orientation.Horizontal;
                ColumnWeather.Margin = new Thickness(36, 0, 0, 0);
            }
            else
            {
                Stack.Orientation = Orientation.Vertical;
                ColumnWeather.Margin = new Thickness(0, 36, 0, 0);
            }
        }
    }
}
