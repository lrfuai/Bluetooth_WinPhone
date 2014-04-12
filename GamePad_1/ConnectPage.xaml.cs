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
using Windows.Foundation;
using Windows.Networking.Sockets;
using System.IO;
using Windows.Storage.Streams;
using GamePad_1.BE;

namespace GamePad_1
{
    public partial class ConnectPage : PhoneApplicationPage
    {
        public ConnectPage()
        {
            InitializeComponent();
        }
        public StreamSocket  socket { get; set; }
        public StreamSocket getSocket()
        {

            return socket;

        }
        
        private async void btnDiscoverPeers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtNamePeer.Items.Clear();
                PeerFinder.AlternateIdentities["Bluetooth:Paired"] = "";
                var peerList = await PeerFinder.FindAllPeersAsync();
                if (peerList.Count > 0)
                {

                    for (int i = 0; i < peerList.Count; i++)
                    {

                        txtNamePeer.Items.Add(peerList[i].DisplayName);
                    }
                }
                else MessageBox.Show("No active peers");
            }
            catch (Exception)
            {
                MessageBox.Show("Check that is activated Bluethoot in your settings") ;
            }
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {
                var peerList = await PeerFinder.FindAllPeersAsync();
                if (peerList.Count > 0)
                {
                    for (int i = 0; i < peerList.Count; i++)
                    {
                        if (peerList[i].DisplayName == txtNamePeer.SelectedValue.ToString())
                        {
                            socket = new StreamSocket();
                            await socket.ConnectAsync(peerList[i].HostName, "1");

                            BluethootHelper.elSocket = socket;
                            MessageBox.Show("Connection successfull");
                            break;
                        }
                    }                    
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {                
                MessageBox.Show("An error has occurred...");
            }
            
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            PeerFinder.ConnectionRequested += PeerFinder_ConnectionRequested;
        }

        void PeerFinder_ConnectionRequested(object sender, ConnectionRequestedEventArgs args)
        {
            Connect(args.PeerInformation);
        }
        async void Connect(PeerInformation peerToConnect)
        {
            StreamSocket socket = await PeerFinder.ConnectAsync(peerToConnect);
        }
    }
}