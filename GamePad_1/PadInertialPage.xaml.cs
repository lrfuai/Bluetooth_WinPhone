using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Devices.Sensors;
using System.Diagnostics;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Windows.Storage.Streams;
using Windows.Networking.Sockets;
using GamePad_1.BE;
using System.Windows;

namespace GamePad_1
{
    public partial class PadInertialPage : PhoneApplicationPage
    {                
        public StreamSocket socket { get; set; }
        public DataWriter dataWriter;        
        game game = new game();
        double VarX;
        double VarY;        
        CompositeTransform ballTransform;
        Accelerometer accelerometer;
        DispatcherTimer DispatcherTimer1 = new DispatcherTimer();
        DispatcherTimer DispatcherTimer2 = new DispatcherTimer();
        Stopwatch watch1 = new Stopwatch();
        
        public PadInertialPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.Portrait;

            DispatcherTimer1.Tick += dispatcherTimer_Tick;
            DispatcherTimer1.Interval = new TimeSpan(0, 0, 0, 0, 10);

            DispatcherTimer2.Tick += dispatcherTimer2_Tick;
            DispatcherTimer2.Interval = new TimeSpan(0, 0, 0, 1, 0);

            accelerometer = new Accelerometer();
            accelerometer.Start();
            //accelerometer.TimeBetweenUpdates = New TimeSpan(0, 0, 0, 10, 0) 'waiting time
            accelerometer.CurrentValueChanged += accelerometer_CurrentValueChanged;
            Ball_IMG.ManipulationStarted += start;
            Ball_IMG.ManipulationDelta += delta;
            Ball_IMG.ManipulationCompleted += complet;
            
            
            dataWriter = new DataWriter(BluethootHelper.elSocket.OutputStream);   
        }

        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            game.Updated(watch1.ElapsedMilliseconds, ActualWidth, ActualHeight);
            watch1.Restart();
            ballTransform.TranslateX = game.Ball.X;
            ballTransform.TranslateY = game.Ball.Y;

            game.Force = new System.Windows.Point(VarX * 25d, -VarY * 25d);        
            
        }

        public void dispatcherTimer2_Tick(object sender, EventArgs e)
        {           

            SendNewCommand(game.Ball);
        }

        public void SendNewCommand(System.Windows.Point ball)
        {
            if (ball.X > ActualWidth / 2d)
            {
                Moving("R");
            }
            else if (ball.X < ActualWidth / 2d)
            {
                Moving("L");
            }

            if (ball.Y > ActualHeight / 2d)
            {
                Moving("U");
            }
            else if (ball.Y < ActualHeight / 2d)
            {
                Moving("D");
            }        
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            game.Game(ActualWidth, ActualHeight);
            Ball_IMG.Width = game.BallRadius * 2d;
            Ball_IMG.Height = game.BallRadius * 2d;
            Canvas.SetLeft(Ball_IMG, -game.BallRadius);
            Canvas.SetTop(Ball_IMG, -game.BallRadius);

            watch1.Restart();
            ballTransform = Ball_IMG.RenderTransform as CompositeTransform;
            ballTransform.TranslateX = game.Ball.X;
            ballTransform.TranslateY = game.Ball.X;
            DispatcherTimer1.Start();
            DispatcherTimer2.Start();
        }

        private void start(object sender, ManipulationStartedEventArgs e)
        {
            e.Handled = true;
            game.Customer = true;
        }

        private void delta(object sender, ManipulationDeltaEventArgs e)
        {
            e.Handled = true;
            game.Ball = new System.Windows.Point(game.Ball.X + e.DeltaManipulation.Translation.X, game.Ball.Y + e.DeltaManipulation.Translation.Y);

        }

        private void complet(object sender, ManipulationCompletedEventArgs e)
        {
            e.Handled = true;
            game.Vektor = e.FinalVelocities.LinearVelocity;
            game.Customer = false;

        }
        
        private void accelerometer_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            // Call UpdateUI on the UI thread and pass the AccelerometerReading.
            Dispatcher.BeginInvoke(() => UpdateUI(e.SensorReading));
        }
        
        private void UpdateUI(AccelerometerReading accelerometerReading)
        {
            Vector3 acceleration = accelerometerReading.Acceleration;

            VarX = acceleration.X;
            
            VarY = acceleration.Y;
            
            //VarZ = acceleration.Z  //si necesitaras el Z

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
            //dataWriter.Dispose();
            DispatcherTimer2.Stop();
            base.OnBackKeyPress(e);            
        }
    }
}