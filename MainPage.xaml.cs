using DIAGmentia_UI_2.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DIAGmentia_UI_2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
          
            this.InitializeComponent();
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                if (Frame.CanGoBack) Frame.GoBack();
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        public IEnumerable<PageItem> Pages { get; set; }

        private void PageList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as PageItem;
            Frame.Navigate(item.Page);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
        }

        private async void Click_Cookie_Test(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var dialog = new ContentDialog()
            {
                Title = "Agreement",
                //RequestedTheme = ElementTheme.Dark,
                //FullSizeDesired = true,
                MaxWidth = this.ActualWidth // Required for Mobile!
            };

            // Setup Content
            var panel = new StackPanel();

            panel.Children.Add(new TextBlock
            {
                Text = "Aliquam laoreet magna sit amet mauris iaculis ornare. " +
                            "Morbi iaculis augue vel elementum volutpat.",
                TextWrapping = TextWrapping.Wrap,
            });

            var cb = new CheckBox
            {
                Content = "Agree"
            };

            cb.SetBinding(ToggleButton.IsCheckedProperty, new Binding
            {
                Source = dialog,
                Path = new PropertyPath("IsPrimaryButtonEnabled"),
                Mode = BindingMode.TwoWay,
            });

            panel.Children.Add(cb);
            dialog.Content = panel;

            // Add Buttons
            dialog.PrimaryButtonText = "OK";
            dialog.IsPrimaryButtonEnabled = false;
            dialog.PrimaryButtonClick += delegate {
                Frame.Navigate(typeof(CookieTheftInstructionPage));
            };

            dialog.SecondaryButtonText = "Cancel";
            dialog.SecondaryButtonClick += delegate {
                Frame.Navigate(typeof(MainPage));
            };

            // Show Dialog
            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.None)
            {
                btn.Content = "Result: NONE";
            }
        }

        private void Click_charts_btn(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(charts));
        }
    }
}
