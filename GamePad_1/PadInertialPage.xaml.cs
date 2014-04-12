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

namespace GamePad_1
{
    public partial class PadInertialPage : PhoneApplicationPage
    {
        private Accelerometer myAccelerometer;
        game game = new game();
        double VarX;
        double VarY;
        double VarZ;
        CompositeTransform ballTransform;
        Accelerometer accelerometer;
        DispatcherTimer DispatcherTimer1 = new DispatcherTimer();

        Stopwatch watch1 = new Stopwatch();
        // Konstruktor
        public PadInertialPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.Portrait;

            DispatcherTimer1.Tick += dispatcherTimer_Tick;
            DispatcherTimer1.Interval = new TimeSpan(0, 0, 0, 0, 10);
            accelerometer = new Accelerometer();
            accelerometer.Start();
            //accelerometer.TimeBetweenUpdates = New TimeSpan(0, 0, 0, 10, 0) 'waiting time
            accelerometer.CurrentValueChanged += accelerometer_CurrentValueChanged;

        }


        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            game.Updated(watch1.ElapsedMilliseconds, ActualWidth, ActualHeight);
            watch1.Restart();
            ballTransform.TranslateX = game.Ball.X;
            ballTransform.TranslateY = game.Ball.Y;

            game.Force = new System.Windows.Point(VarX * 25, -VarY * 25);
            
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);



            game.Game(ActualWidth, ActualHeight);
            Ball_IMG.Width = game.BallRadius * 2;
            Ball_IMG.Height = game.BallRadius * 2;
            Canvas.SetLeft(Ball_IMG, -game.BallRadius);
            Canvas.SetTop(Ball_IMG, -game.BallRadius);


            watch1.Restart();
            ballTransform = Ball_IMG.RenderTransform as CompositeTransform;
            ballTransform.TranslateX = game.Ball.X;
            ballTransform.TranslateY = game.Ball.X;
            DispatcherTimer1.Start();
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
            //give the accelerometer X value
            VarY = acceleration.Y;
            //give the accelerometer Y value
            //VarZ = acceleration.Z 'if you need the Z


        }

    }



}