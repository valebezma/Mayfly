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

namespace Mayfly.Fish.Windows
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TopNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {

            }
            else
            {
                switch (((NavigationViewItem)args.SelectedItem).Tag)
                {
                    case "Tag":
                        contentFrame.Navigate(typeof(Pages.CardGeneral));
                        break;

                    case "Weather":
                        contentFrame.Navigate(typeof(Pages.Environment));
                        break;

                    default:
                        //contentFrame.Navigate();
                        break;

                }
            }
        }

        private void TopNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {

        }
    }
}
