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
    public sealed partial class CardGeneral : Page
    {
        public CardGeneral()
        {
            this.InitializeComponent();
        }

        private void Scroll_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width >= (
                Scroll.Padding.Left +
                ColumnGeneral.Margin.Left + ColumnGeneral.Width + ColumnGeneral.Margin.Right +
                36 + ColumnGear.Width
                ))
            {
                Stack.Orientation = Orientation.Horizontal;
                ColumnGear.Margin = new Thickness(36, 0, 0, 0);
                //Comments.Height = 150;
            }
            else
            {
                Stack.Orientation = Orientation.Vertical;
                ColumnGear.Margin = new Thickness(0, 36, 0, 0);
                //Comments.Height = 50;
            }
        }
    }
}
