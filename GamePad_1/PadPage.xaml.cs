using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Networking.Proximity;
using Windows.Storage.Streams;
using GamePad_1.BE;
using Windows.Networking.Sockets;
using System.Threading.Tasks;

namespace GamePad_1
{
    public partial class PadPage : PhoneApplicationPage
    {

        public StreamSocket  socket { get; set; }
        public DataWriter dataWriter;

        public PadPage()
        {
            InitializeComponent();                    
            dataWriter = new DataWriter(BluethootHelper.elSocket.OutputStream);            
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
            Moving("L");
          
        }
        private void RigthButton_Click(object sender, RoutedEventArgs e)
        {
            Moving("R");

        }
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            Moving("D");

        }
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
           Moving("U");

        }

        public async void Moving(string direction)
        {
            try
            {                
                switch (direction)
                {
                    case "U":
                        dataWriter.WriteString(Movements.getInstancia().Up);
                        break;
                    case "D":
                        dataWriter.WriteString(Movements.getInstancia().Down);
                        break;
                    case "L":
                        dataWriter.WriteString(Movements.getInstancia().Left);
                        break;
                    case "R":
                        dataWriter.WriteString(Movements.getInstancia().Right);
                        break;
                    default:
                        break;
                }
                await dataWriter.StoreAsync();                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }        
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            dataWriter.Dispose();
            base.OnBackKeyPress(e);
        }
    }
}