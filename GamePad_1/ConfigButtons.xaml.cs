using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GamePad_1.BE;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace GamePad_1
{
    public partial class ConfigButtons : PhoneApplicationPage
    {
        public static DataSaver<Movements> mySettingsDataSaver = new DataSaver<Movements>();
        
        
        public ConfigButtons()
        {
            InitializeComponent();
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            mySettingsDataSaver.SaveMyData(getData(), "MySettingsDataFileName");
            Movements.setInstancia(mySettingsDataSaver.LoadMyData("MySettingsDataFileName"));
            MessageBox.Show("Successful");
        }

        public static Movements DefaultMovementConfiguration()
        {
            Movements settings = new Movements();
            settings.Left = "L";
            settings.Right = "R";
            settings.Up = "U";
            settings.Down = "D";
            return settings;
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {            
            var SettingsData = mySettingsDataSaver.LoadMyData("MySettingsDataFileName");
            if (SettingsData == null)
            { // Sets Default 
                SettingsData = DefaultMovementConfiguration();
            }
            
            txtDown.Text = SettingsData.Down;
            txtLeft.Text = SettingsData.Left;
            txtRight.Text = SettingsData.Right;
            txtUp.Text = SettingsData.Up;
            
        }

        public Movements getData()
        {
            Movements settings = new Movements();
            settings.Left = txtLeft.Text;
            settings.Right = txtRight.Text;
            settings.Up = txtUp.Text;
            settings.Down = txtDown.Text;
            return settings;        
        }


        public static Movements getSavedMovements()
        {
            var SettingsData = mySettingsDataSaver.LoadMyData("MySettingsDataFileName");
            if (SettingsData == null)
            { // Sets Default 
                SettingsData = DefaultMovementConfiguration();
            }
            return SettingsData;
        }
    }
}