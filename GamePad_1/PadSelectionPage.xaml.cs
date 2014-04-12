using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace GamePad_1
{
    public partial class PadSelectionPage : PhoneApplicationPage
    {
        public PadSelectionPage()
        {
            InitializeComponent();
        }        
        private void btnInertial_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PadInertialPage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PadPage.xaml", UriKind.Relative));
        }        
    }
}