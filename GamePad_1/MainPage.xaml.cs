using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GamePad_1.Resources;
using GamePad_1.BE;
using System.IO.IsolatedStorage;

namespace GamePad_1
{
    public partial class MainPage : PhoneApplicationPage
    {
        

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ConfigButtons.xaml", UriKind.Relative));
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
        
                NavigationService.Navigate(new Uri("/ConnectPage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (BluethootHelper.elSocket == null)
                MessageBox.Show("must be connected to the device previously...");
            else
                NavigationService.Navigate(new Uri("/PadSelectionPage.xaml", UriKind.Relative));
        }

        
    }
}